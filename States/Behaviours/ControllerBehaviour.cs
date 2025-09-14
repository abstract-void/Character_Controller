using UnityEngine;
using ProjectDew.FSM;

namespace ProjectDew.CharacterController
{
	public abstract class ControllerBehaviour : StateBehaviour
	{
		private readonly Controller controller;

		private readonly ControllerData controllerData;

		public ControllerBehaviour(Controller controller, ControllerData controllerData)
		{
			this.controller = controller;
			this.controllerData = controllerData;
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
			
			horizontalMovement += controller.Direction * (controllerData.MoveSpeed * Time.deltaTime * controllerData.ControlRate * Math.Hundred);
			horizontalMovement = Vector3.ClampMagnitude(horizontalMovement, controllerData.MoveSpeed);
			
			controller.Movement = horizontalMovement + controller.Movement.GetDot(controller.Transform.up);
		}

		protected virtual void ApplyFriction() => controller.Movement = Vector3.MoveTowards(controller.Movement, Vector3.zero, controllerData.AirFriction * Time.deltaTime);

		protected virtual void AdjustVerticalMovement() => controller.Movement -= controller.Transform.up * (controllerData.Gravity.y * Time.deltaTime);

		protected virtual void ApplyMovement()
		{
			if (!controller.Raycaster.HasDetectedHit)
			{
				controller.Rigidbody.linearVelocity = controller.Movement;
				return;
			}
			
			Vector3 velocityAdjustment = controller.Transform.up * (controller.Raycaster.DistanceToHit / Time.fixedDeltaTime); // Keep ground contact

			controller.Rigidbody.linearVelocity = controller.Movement + controllerData.MoveSpeed * controller.Direction + velocityAdjustment;
		}
	}
}
