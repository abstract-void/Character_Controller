using UnityEngine;
using ProjectDew.FSM;

namespace ProjectDew.CharacterController
{
	public class SimpleController : Controller, IMoveController, IGravityController, IJumpController, ISlideController
	{
		[SerializeField]
		private State[] states;

		[SerializeField]
		private ControllerData controllerData;
		
		[SerializeField]
		private Transform cameraTransform;

		private Transform tr;

		private Rigidbody rb;
		
		private SimpleStateMachine stateMachine;
		
		private PlayerInputReader input;

		private ColliderHandler colliderHandler;

		private Raycaster raycaster;

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
					direction = Transform.right * input.Move.Direction.x + Transform.forward * input.Move.Direction.y;
				}
				else
				{
					Vector3 projectedDirectionX = Vector3.ProjectOnPlane(cameraTransform.right, Transform.up).normalized * input.Move.Direction.x;
					Vector3 projectedDirectionY = Vector3.ProjectOnPlane(cameraTransform.forward, Transform.up).normalized * input.Move.Direction.y;

					direction = projectedDirectionX + projectedDirectionY;
				}

				return direction.magnitude > Math.One ? direction.normalized : direction;
			}
		}

		public override Vector3 Movement { get; set; }

		public bool BeginLanding => raycaster.HasDetectedHit;

		public bool BeginSliding => Vector3.Angle(raycaster.HitNormal, Transform.up) > controllerData.SlopeLimit;

		public bool BeginRising => Vector3.Dot(Movement, Transform.up.normalized) > Math.Zero;

		public bool BeginFalling => Vector3.Dot(Movement, Transform.up.normalized) < Math.Zero;

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
		
		public bool JumpWasInterrupted => stateMachine.Jump.JumpWasInterrupted;

		public bool IsGrounded => stateMachine.IsCurrentState("Grounded");

		public bool IsRising => stateMachine.IsCurrentState("Rising");

		public bool IsFalling => stateMachine.IsCurrentState("Falling");

		public bool IsJumping => stateMachine.IsCurrentState("Jumping");

		public bool IsSliding => stateMachine.IsCurrentState("Sliding");

		private void Awake()
		{
			tr = transform;

			rb = GetComponent<Rigidbody>();

			colliderHandler = GetComponent<ColliderHandler>();
			
			input = new();

			Rigidbody.freezeRotation = true;
			Rigidbody.useGravity = false;
		}

		private void OnEnable()
		{
			input.Enable();
		}

		private void Start()
		{
			stateMachine = new(states, this, controllerData);
			
			raycaster = new(this, ColliderHandler.Size.y.Half());

			stateMachine.SetStateEventHandler += ResetJumpCount;
			input.Jump.Completed += IncreaseJumpCount;
		}
		
		private void Update()
		{
			CheckInput();
			stateMachine.Execute();
		}

		private void FixedUpdate()
		{
			stateMachine.ExecuteFixed();
		}

		private void OnDisable()
		{
			input.Disable();
		}

		private void OnDestroy()
		{
			input.Jump.Completed -= IncreaseJumpCount;
			stateMachine.SetStateEventHandler -= ResetJumpCount;

			input.Dispose();
		}
	
		private void OnCollisionEnter(Collision collision) => stateMachine.Jump.CheckForCeilingContact(collision);

		private void OnCollisionStay(Collision collision) => stateMachine.Jump.CheckForCeilingContact(collision);

		private void CheckInput()
		{
			input.Jump.HoldButton(controllerData.JumpDuration);
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
	}
}
