using ProjectDew.FSM;

namespace ProjectDew.CharacterController
{
	public class SimpleStateMachine : StateMachine
	{
		private readonly SimpleController controller;

		public GroundedBehaviour Grounded { get; private set; }

		public JumpBehaviour Jump { get; private set; }

		public SlideBehaviour Slide { get; private set; }

		public RiseBehaviour Rise { get; private set; }

		public FallBehaviour Fall { get; private set; }

		public SimpleStateMachine(State[] states, SimpleController controller) : base(states)
		{
			this.controller = controller;
			InitializeStates();
		}

		public void AddGroundedBehaviour(IMoveData moveData)
		{
			Grounded = new GroundedBehaviour(controller, moveData);
			AddBehaviour("Grounded", Grounded);
		}

		public void AddJumpBehaviour(IMoveData moveData, IJumpData jumpData)
		{
			Jump = new JumpBehaviour(controller, moveData, jumpData);
			AddBehaviour("Jumping", Jump);
		}

		public void AddSlideBehaviour(IMoveData moveData)
		{
			Slide = new SlideBehaviour(controller, moveData);
			AddBehaviour("Sliding", Slide);
		}

		public void AddRiseBehaviour(IMoveData moveData)
		{
			Rise = new RiseBehaviour(controller, moveData);

			AddBehaviour("Rising", Rise);
			AddBehaviour("Sliding", Rise);
		}

		public void AddFallBehaviour(IMoveData moveData)
		{
			Fall = new FallBehaviour(controller, moveData);
			AddBehaviour("Falling", Fall);
		}

		private void InitializeStates()
		{
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
