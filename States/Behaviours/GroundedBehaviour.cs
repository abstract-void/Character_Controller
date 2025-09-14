using UnityEngine;

namespace ProjectDew.CharacterController
{
	public class GroundedBehaviour : ControllerBehaviour
	{
		private readonly Controller controller;

		private readonly ControllerData controllerData;

		public GroundedBehaviour(Controller controller, ControllerData controllerData) : base(controller, controllerData)
		{
			this.controller = controller;
			this.controllerData = controllerData;
		}

		protected override void CheckForGround()
		{
			float colliderHeight = controller.ColliderHandler.Size.y * controller.Transform.localScale.y;
			float rayLength = colliderHeight.Half() + colliderHeight.Quarter();
			
			controller.Raycaster.CastRay(controller.ColliderHandler.WorldCenter, Vector3.down, rayLength);
		}

		protected override void AdjustVerticalMovement()
		{
			controller.Movement -= controller.Transform.up * (controllerData.Gravity.y * Time.deltaTime);

			Vector3 verticalMovement = controller.Movement.GetDot(controller.Transform.up);

			if (Vector3.Dot(verticalMovement, controller.Transform.up.normalized) < Math.Zero)
			{
				verticalMovement = Vector3.zero;
			}

			controller.Movement = controller.Movement.RemoveDot(controller.Transform.up) + verticalMovement;
		}

        protected override void ApplyFriction() => controller.Movement = Vector3.MoveTowards(controller.Movement, Vector3.zero, controllerData.GroundFriction * Time.deltaTime);
	}
}
