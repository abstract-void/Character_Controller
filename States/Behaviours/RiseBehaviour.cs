using UnityEngine;

namespace ProjectDew.CharacterController
{
	public class RiseBehaviour : ControllerBehaviour
	{
		private readonly Controller controller;

		private readonly ControllerData controllerData;

		public RiseBehaviour(Controller controller, ControllerData controllerData) : base(controller, controllerData)
		{
			this.controller = controller;
			this.controllerData = controllerData;
		}

		public override void Execute()
		{
			Vector3 velocity = controllerData.MoveSpeed * controller.Direction;//Vector3 velocity = controller.Velocity;

			if (velocity.sqrMagnitude >= Math.Zero && controller.Movement.sqrMagnitude > Math.Zero)
			{
				Vector3 projectedMovement = Vector3.Project(controller.Movement, velocity.normalized);
				
				if (Vector3.Dot(projectedMovement.normalized, velocity.normalized) > Math.Zero)
				{
					if (projectedMovement.sqrMagnitude >= velocity.sqrMagnitude)
					{
						velocity = Vector3.zero;
					}
					else
					{
						velocity -= projectedMovement;
					}
				}
			}

			controller.Movement += velocity;
		}
	}
}
