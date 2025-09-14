using UnityEngine;

namespace ProjectDew.CharacterController
{
	public class FallBehaviour : ControllerBehaviour
	{
		private readonly Controller controller;

		private readonly ControllerData controllerData;

		public FallBehaviour(Controller controller, ControllerData controllerData) : base(controller, controllerData)
		{
			this.controller = controller;
			this.controllerData = controllerData;
		}

		public override void Execute()
		{
			Vector3 currentUpMomemtum = controller.Movement.GetDot(controller.Transform.up);
			
            controller.Movement = controller.Movement.RemoveDot(controller.Transform.up);
			controller.Movement -= controller.Transform.up * currentUpMomemtum.magnitude;
		}
	}
}
