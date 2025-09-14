using ProjectDew.FSM;

namespace ProjectDew.CharacterController
{
	public class SimpleStateMachine : StateMachine
	{
		private readonly SimpleController controller;

		private readonly ControllerData controllerData;

		public GroundedBehaviour Grounded { get; private set; }

		public JumpBehaviour Jump { get; private set; }

		public SlideBehaviour Slide { get; private set; }

		public RiseBehaviour Rise { get; private set; }

		public FallBehaviour Fall { get; private set; }

		public SimpleStateMachine(State[] states, SimpleController controller, ControllerData controllerData) : base(states)
		{
			this.controller = controller;
			this.controllerData = controllerData;

			InitializeStates();
		}

		private void InitializeStates()
		{
			Grounded = new GroundedBehaviour(controller, controllerData);
			Jump = new JumpBehaviour(controller, controllerData);
			Slide = new SlideBehaviour(controller, controllerData);
			Rise = new RiseBehaviour(controller, controllerData);
			Fall = new FallBehaviour(controller, controllerData);

			AddBehaviours("Grounded", Grounded);
			AddBehaviours("Jumping", Jump);
			AddBehaviours("Sliding", Slide, Rise);
			AddBehaviours("Rising", Rise);
			AddBehaviours("Falling", Fall);

			AddTransition("Grounded", "Sliding", () => controller.BeginLanding && controller.BeginSliding);
			AddTransition("Grounded", "Falling", () => !controller.BeginLanding);
			AddTransition("Grounded", "Jumping", () => controller.BeginJumping);

			AddTransition("Jumping", "Rising", () => !controller.BeginJumping);
			AddTransition("Jumping", "Falling", () => controller.JumpWasInterrupted);

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
