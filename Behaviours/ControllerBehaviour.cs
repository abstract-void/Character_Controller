using UnityEngine;
using ProjectDew.FSM;

namespace ProjectDew.CharacterController
{
	public abstract class ControllerBehaviour : StateBehaviour
	{
		private readonly Controller controller;

		private readonly IMoveData moveData;

		public ControllerBehaviour(Controller controller, IMoveData moveData)
		{
			this.controller = controller;
			this.moveData = moveData;
		}

		public override void Execute() { }

		public override void ExecuteFixed()
		{
			CheckForGround();

			AdjustHorizontalMovement();

			ApplyFriction();

			AdjustVerticalMovement();
			
			ApplyMovement();
		}

		protected virtual void CheckForGround()
		{
			float rayLength = controller.ColliderHandler.Size.y.Half();
			controller.Raycaster.CastRay(controller.ColliderHandler.WorldCenter, Vector3.down, rayLength);
		}

		protected virtual void AdjustHorizontalMovement()
		{
			Vector3 horizontalMovement = controller.Movement.RemoveDot(controller.Transform.up);
			
			horizontalMovement += controller.Direction * (moveData.MoveSpeed * Time.deltaTime * moveData.ControlRate * Math.Hundred);
			horizontalMovement = Vector3.ClampMagnitude(horizontalMovement, moveData.MoveSpeed);
			
			controller.Movement = horizontalMovement + controller.Movement.GetDot(controller.Transform.up);
		}

		protected virtual void ApplyFriction() => controller.Movement = Vector3.MoveTowards(controller.Movement, Vector3.zero, moveData.Friction * Time.deltaTime);

		protected virtual void AdjustVerticalMovement() => controller.Movement -= controller.Transform.up * (moveData.Gravity.y * Time.deltaTime);

		protected virtual void ApplyMovement()
		{
			if (!controller.Raycaster.HasDetectedHit)
			{
				controller.Rigidbody.linearVelocity = controller.Movement;
				return;
			}
			
			Vector3 velocityAdjustment = controller.Transform.up * (controller.Raycaster.DistanceToHit / Time.fixedDeltaTime); // Keep ground contact

			controller.Rigidbody.linearVelocity = controller.Movement + moveData.MoveSpeed * controller.Direction + velocityAdjustment;
		}
	}
}
