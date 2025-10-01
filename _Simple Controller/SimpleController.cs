using UnityEngine;
using ProjectDew.FSM;

namespace ProjectDew.CharacterController
{
	[RequireComponent(typeof(Animator))]
	public class SimpleController : Controller, IMoveController, IGravityController, IRunController, IJumpController, ICrouchController, ISlideController
	{
		[SerializeField]
		private ControllerData controllerData;

		[SerializeField]
		private StateMachineData stateMachineData;

		[SerializeField]
		private MoveData groundedData;

		[SerializeField]
		private MoveData runData;

		[SerializeField]
		private MoveData crouchData;

		[SerializeField]
		private MoveData slideData;

		[SerializeField]
		private AnimateData animateData;
		
		[SerializeField]
		private Transform cameraTransform;

		[SerializeField]
		private float rotationSpeed;

		private Transform tr;

		private Rigidbody rb;

		private Animator animator;

		private ColliderHandler colliderHandler;

		private Raycaster raycaster;
		
		private SimpleStateMachine stateMachine;
		
		private PlayerInputReader input;

		private Quaternion targetRotation;

		public override Transform Transform => tr;

		public override Rigidbody Rigidbody => rb;

		public override ColliderHandler ColliderHandler => colliderHandler;

		public override Raycaster Raycaster => raycaster;

		public override Vector3 Direction
		{
			get
			{
				Vector3 direction;

				if (cameraTransform == null)
				{
					direction = new Vector3(input.Move.Direction.x, 0, input.Move.Direction.y);
				}
				else
				{
					Vector3 projectedDirectionX = Vector3.ProjectOnPlane(cameraTransform.right, Transform.up).normalized * input.Move.Direction.x;
					Vector3 projectedDirectionZ = Vector3.ProjectOnPlane(cameraTransform.forward, Transform.up).normalized * input.Move.Direction.y;

					direction = projectedDirectionX + projectedDirectionZ;
				}

				return direction.magnitude > Math.One ? direction.normalized : direction;
			}
		}

		public override Vector3 Movement { get; set; }

		public bool BeginLanding => raycaster.HasDetectedHit;

		public bool BeginRising => Vector3.Dot(Movement, Transform.up.normalized) > Math.Zero;

		public bool BeginFalling => Vector3.Dot(Movement, Transform.up.normalized) < Math.Zero;

		public bool BeginRunning => input.Run.IsHeld;

		public bool BeginJumping
		{
			get
			{
				if (input.Jump.IsActive && stateMachine.Jump.NumberOfJumps < controllerData.MaxJumpsAllowed)
				{
					return stateMachine.Jump.NumberOfJumps == 0 || input.Jump.TimeHeld > controllerData.TimeBetweenJumps;
				}

				return false;
			}
		}

		public bool BeginCrouching { get; private set; }

		public bool BeginSliding => Vector3.Angle(raycaster.HitNormal, Transform.up) > controllerData.SlopeLimit;

		public bool BeginInteracting { get; private set; }
		
		public bool JumpWasInterrupted => stateMachine.Jump.JumpWasInterrupted;

		public bool IsGrounded => stateMachine.IsCurrentState("Grounded");

		public bool IsRising => stateMachine.IsCurrentState("Rising");

		public bool IsFalling => stateMachine.IsCurrentState("Falling");

		public bool IsRunning => stateMachine.IsCurrentState("Running");

		public bool IsJumping => stateMachine.IsCurrentState("Jumping");

		public bool IsCrouching => stateMachine.IsCurrentState("Crouching");

		public bool IsSliding => stateMachine.IsCurrentState("Sliding");

		public bool IsInteracting => stateMachine.IsCurrentState("Interacting");

		private void Awake()
		{
			tr = transform;

			rb = GetComponent<Rigidbody>();

			animator = GetComponent<Animator>();

			colliderHandler = GetComponent<ColliderHandler>();
			
			raycaster = new(this, ColliderHandler.Size.y.Half());
			
			input = new();

			Rigidbody.freezeRotation = true;
			Rigidbody.useGravity = false;

			targetRotation = Rigidbody.rotation;
		}

		private void OnEnable()
		{
			input.Enable();
		}

		private void Start()
		{
			stateMachine = new(stateMachineData, this, animator, animateData);

			stateMachine.AddGroundedBehaviour(groundedData);
			stateMachine.AddRunBehaviour(runData);
			stateMachine.AddJumpBehaviour(controllerData, controllerData);
			stateMachine.AddCrouchBehaviour(crouchData);
			stateMachine.AddSlideBehaviour(slideData);
			stateMachine.AddRiseBehaviour(controllerData);
			stateMachine.AddFallBehaviour(controllerData);
			stateMachine.AddInteractBehaviour(controllerData);
			
			stateMachine.SetStateEventHandler += ResetJumpCount;

			input.Jump.Completed += IncreaseJumpCount;
			input.Crouch.Pressed += SwitchCrouchState;
			input.Interact.Pressed += CheckInteractState;
			
			ColliderHandler.RecalculateColliderDimensions();
		}
		
		private void Update()
		{
			CheckInput();
			stateMachine.Execute();
		}

		private void FixedUpdate()
		{
			Rotate();
			stateMachine.ExecuteFixed();
		}

		private void OnDisable()
		{
			input.Disable();
		}

		private void OnDestroy()
		{
			stateMachine.SetStateEventHandler -= ResetJumpCount;

			stateMachine.Dispose();

			input.Jump.Completed -= IncreaseJumpCount;
			input.Crouch.Pressed -= SwitchCrouchState;
			input.Interact.Pressed -= CheckInteractState;

			input.Dispose();
		}

		private void CheckInput()
		{
			switch (stateMachine.CurrentState.ID)
			{
				case "Jumping":
					input.Jump.HoldButton(controllerData.JumpDuration);
					break;

				case "Running":
					input.Run.HoldButton();
					break;
			}
		}

		private void Rotate()
		{
			if (Direction.magnitude > 0)
			{
				targetRotation = Quaternion.LookRotation (Direction);
			}

			Rigidbody.rotation = Quaternion.Slerp (Rigidbody.rotation, targetRotation, rotationSpeed * Time.deltaTime);
		}

		private void IncreaseJumpCount(float pressDuration)
		{
			if (IsJumping && input.Jump.IsActive)
			{
				stateMachine.Jump.IncreaseJumpCount(pressDuration);
			}
		}

		private void ResetJumpCount(State state)
		{
			if (state.ID == "Grounded")
			{
				stateMachine.Jump.ResetJumpCount();
			}
		}

		private void SwitchCrouchState() => BeginCrouching = !BeginCrouching;

		private void CheckInteractState() => BeginInteracting = stateMachine.Interact.Interact();
	}
}
