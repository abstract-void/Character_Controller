using UnityEngine;

namespace ProjectDew.CharacterController
{
	public class RiseBehaviour : MoveBehaviour
	{
		private readonly Controller controller;

		private readonly IMoveData moveData;

		public RiseBehaviour(Controller controller, IMoveData moveData) : base(controller, moveData)
		{
			this.controller = controller;
			this.moveData = moveData;
		}

		public override void Execute()
		{
			Vector3 velocity = moveData.MoveSpeed * controller.Direction;

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
