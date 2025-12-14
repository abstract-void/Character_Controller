using System;
using UnityEngine;
using ProjectDew.FSM;

namespace ProjectDew.CharacterController
{
	public class SimpleStateMachine : StateMachine<SimpleController>, IDisposable
	{
		private readonly SimpleController controller;

		private readonly Animator animator;

		private readonly AnimateData animateData;

		public GroundedBehaviour Grounded { get; private set; }

		public RunBehaviour Run { get; private set; }

		public JumpBehaviour Jump { get; private set; }

		public CrouchBehaviour Crouch { get; private set; }

		public SlideBehaviour Slide { get; private set; }

		public RiseBehaviour Rise { get; private set; }

		public FallBehaviour Fall { get; private set; }

		public InteractBehaviour Interact { get; private set; }

		public SimpleStateMachine(SimpleStateMachineData stateMachineData, SimpleController controller, Animator animator, AnimateData animateData) : base(stateMachineData, controller)
		{
			this.controller = controller;
			this.animator = animator;
			this.animateData = animateData;

			//InitializeStates();
		}

		public void Dispose()
		{
			Jump.Dispose();
			Interact.Dispose();
		}

		public void AddGroundedBehaviour(IMoveData moveData)
		{
			Grounded = new GroundedBehaviour(controller, moveData);
			AddBehaviours("Grounded", Grounded, new AnimateMoveBehaviour(controller, animator, "Move", animateData));
		}

		public void AddRunBehaviour(IMoveData moveData)
		{
			Run = new RunBehaviour(controller, moveData);
			AddBehaviours("Running", Run, new AnimateMoveBehaviour(controller, animator, "Move", animateData));
		}

		public void AddJumpBehaviour(IMoveData moveData, IJumpData jumpData)
		{
			Jump = new JumpBehaviour(controller, moveData, jumpData);
			AddBehaviours("Jumping", Jump, new AnimateBehaviour(animator, "Jump", animateData));
		}

		public void AddCrouchBehaviour(IMoveData moveData)
		{
			Crouch = new CrouchBehaviour(controller, moveData);
			AddBehaviours("Crouching", Crouch, new AnimateMoveBehaviour(controller, animator, "Crouch", animateData));
		}

		public void AddSlideBehaviour(IMoveData moveData)
		{
			Slide = new SlideBehaviour(controller, moveData);
			AddBehaviours("Sliding", Slide, new AnimateBehaviour(animator, "Slide", animateData));
		}

		public void AddRiseBehaviour(IMoveData moveData)
		{
			Rise = new RiseBehaviour(controller, moveData);

			AddBehaviours("Rising", Rise, new AnimateBehaviour(animator, "Hover", animateData));
			AddBehaviour("Sliding", Rise);
		}

		public void AddFallBehaviour(IMoveData moveData)
		{
			Fall = new FallBehaviour(controller, moveData);
			AddBehaviours("Falling", Fall, new AnimateBehaviour(animator, "Hover", animateData));
		}

		public void AddInteractBehaviour(IInteractData interactData)
		{
			Interact = new InteractBehaviour(controller, interactData);
			AddBehaviours("Interacting", Interact, new AnimateBehaviour(animator, "Interact", animateData));
		}

		//private void InitializeStates()
		//{
			//AddTransition("Grounded", "Sliding", (controller) => controller.BeginLanding && controller.BeginSliding);
			//AddTransition("Grounded", "Falling", (controller) => !controller.BeginLanding);
			//AddTransition("Grounded", "Jumping", (controller) => controller.BeginJumping);
			//AddTransition("Grounded", "Running", (controller) => controller.BeginRunning);
			//AddTransition("Grounded", "Crouching", (controller) => controller.BeginCrouching);
			//AddTransition("Grounded", "Interacting", (controller) => controller.BeginInteracting);

			//AddTransition("Interacting", "Grounded", (controller) => !controller.BeginInteracting && !controller.IsRunning && !controller.IsCrouching);
			//AddTransition("Interacting", "Running", (controller) => !controller.BeginInteracting && controller.IsRunning);
			//AddTransition("Interacting", "Crouching", (controller) => !controller.BeginInteracting && controller.IsCrouching);

			//AddTransition("Running", "Grounded", (controller) => !controller.BeginRunning);
			//AddTransition("Running", "Sliding", (controller) => controller.BeginLanding && controller.BeginSliding);
			//AddTransition("Running", "Falling", (controller) => !controller.BeginLanding);
			//AddTransition("Running", "Jumping", (controller) => controller.BeginJumping);
			//AddTransition("Running", "Interacting", (controller) => controller.BeginInteracting);

			//AddTransition("Jumping", "Rising", (controller) => !controller.BeginJumping);
			//AddTransition("Jumping", "Falling", (controller) => controller.JumpWasInterrupted);
			
			//AddTransition("Crouching", "Grounded", (controller) => !controller.BeginCrouching);
			//AddTransition("Crouching", "Sliding", (controller) => controller.BeginLanding && controller.BeginSliding);
			//AddTransition("Crouching", "Falling", (controller) => !controller.BeginLanding);
			//AddTransition("Crouching", "Interacting", (controller) => controller.BeginInteracting);

			//AddTransition("Sliding", "Falling", (controller) => !controller.BeginLanding);
			//AddTransition("Sliding", "Grounded", (controller) => controller.BeginLanding && !controller.BeginSliding);

			//AddTransition("Rising", "Grounded", (controller) => controller.BeginLanding && !controller.BeginSliding);
			//AddTransition("Rising", "Sliding", (controller) => controller.BeginLanding && controller.BeginSliding);
			//AddTransition("Rising", "Falling", (controller) => controller.BeginFalling || controller.JumpWasInterrupted);
			//AddTransition("Rising", "Jumping", (controller) => controller.BeginJumping);

			//AddTransition("Falling", "Grounded", (controller) => controller.BeginLanding && !controller.BeginSliding);
			//AddTransition("Falling", "Sliding", (controller) => controller.BeginLanding && controller.BeginSliding);
			//AddTransition("Falling", "Jumping", (controller) => controller.BeginJumping);
		//}
	}
}
