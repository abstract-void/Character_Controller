using System;
using UnityEngine;
using ProjectDew.FSM;

namespace ProjectDew.CharacterController
{
	public class SimpleStateMachine : StateMachine, IDisposable
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

		public SimpleStateMachine(StateMachineData stateMachineData, SimpleController controller, Animator animator, AnimateData animateData) : base(stateMachineData)
		{
			this.controller = controller;
			this.animator = animator;
			this.animateData = animateData;

			InitializeStates();
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
			AddBehaviours("Crouching", Crouch, new AnimateBehaviour(animator, "Crouch", animateData));
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

		private void InitializeStates()
		{
			AddTransition("Grounded", "Sliding", () => controller.BeginLanding && controller.BeginSliding);
			AddTransition("Grounded", "Falling", () => !controller.BeginLanding);
			AddTransition("Grounded", "Jumping", () => controller.BeginJumping);
			AddTransition("Grounded", "Running", () => controller.BeginRunning);
			AddTransition("Grounded", "Crouching", () => controller.BeginCrouching);
			AddTransition("Grounded", "Interacting", () => controller.BeginInteracting);

			AddTransition("Interacting", "Grounded", () => !controller.BeginInteracting && !controller.IsRunning && !controller.IsCrouching);
			AddTransition("Interacting", "Running", () => !controller.BeginInteracting && controller.IsRunning);
			AddTransition("Interacting", "Crouching", () => !controller.BeginInteracting && controller.IsCrouching);

			AddTransition("Running", "Grounded", () => !controller.BeginRunning);
			AddTransition("Running", "Sliding", () => controller.BeginLanding && controller.BeginSliding);
			AddTransition("Running", "Falling", () => !controller.BeginLanding);
			AddTransition("Running", "Jumping", () => controller.BeginJumping);
			AddTransition("Running", "Interacting", () => controller.BeginInteracting);

			AddTransition("Jumping", "Rising", () => !controller.BeginJumping);
			AddTransition("Jumping", "Falling", () => controller.JumpWasInterrupted);
			
			AddTransition("Crouching", "Grounded", () => !controller.BeginCrouching);
			AddTransition("Crouching", "Sliding", () => controller.BeginLanding && controller.BeginSliding);
			AddTransition("Crouching", "Falling", () => !controller.BeginLanding);
			AddTransition("Crouching", "Interacting", () => controller.BeginInteracting);

			AddTransition("Sliding", "Falling", () => !controller.BeginLanding);
			AddTransition("Sliding", "Grounded", () => controller.BeginLanding && !controller.BeginSliding);

			AddTransition("Rising", "Grounded", () => controller.BeginLanding && !controller.BeginSliding);
			AddTransition("Rising", "Sliding", () => controller.BeginLanding && controller.BeginSliding);
			AddTransition("Rising", "Falling", () => controller.BeginFalling || controller.JumpWasInterrupted);
			AddTransition("Rising", "Jumping", () => controller.BeginJumping);

			AddTransition("Falling", "Grounded", () => controller.BeginLanding && !controller.BeginSliding);
			AddTransition("Falling", "Sliding", () => controller.BeginLanding && controller.BeginSliding);
			AddTransition("Falling", "Jumping", () => controller.BeginJumping);
		}
	}
}
