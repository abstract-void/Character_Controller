using UnityEngine;

namespace ProjectDew.CharacterController
{
	public class FallBehaviour : ControllerBehaviour
	{
		private readonly Controller controller;

		public FallBehaviour(Controller controller, IMoveData moveData) : base(controller, moveData)
		{
			this.controller = controller;
		}

		public override void Execute()
		{
			Vector3 currentUpMomemtum = controller.Movement.GetDot(controller.Transform.up);
			
            controller.Movement = controller.Movement.RemoveDot(controller.Transform.up);
			controller.Movement -= controller.Transform.up * currentUpMomemtum.magnitude;
		}
	}
}
