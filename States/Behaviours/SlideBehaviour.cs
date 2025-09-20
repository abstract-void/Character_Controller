using UnityEngine;

namespace ProjectDew.CharacterController
{
    public class SlideBehaviour : ControllerBehaviour
    {
        private readonly Controller controller;

        private readonly IMoveData moveData;

        public SlideBehaviour(Controller controller, IMoveData moveData) : base(controller, moveData)
        {
            this.controller = controller;
			this.moveData = moveData;
        }

		protected override void CheckForGround()
		{
			float colliderHeight = controller.ColliderHandler.Size.y * controller.Transform.localScale.y;
			float rayLength = colliderHeight.Half() + colliderHeight.Quarter();
			
			controller.Raycaster.CastRay(controller.ColliderHandler.WorldCenter, Vector3.down, rayLength);
		}

        protected override void AdjustHorizontalMovement()
        {
			Vector3 slideDirection = Vector3.ProjectOnPlane(controller.Raycaster.HitNormal, controller.Transform.up).normalized;

			Vector3 controllerDirection = Vector3.zero;

			if (controller.Raycaster.HitCollider != null)
			{
				controllerDirection = controller.Raycaster.HitCollider.transform.TransformDirection(controller.Direction).normalized;
			}

			controller.Movement = slideDirection * moveData.Gravity.y.Half() + controllerDirection.GetDot(Vector3.right) * moveData.MoveSpeed;
        }

        protected override void AdjustVerticalMovement()
        {
			controller.Movement -= controller.Transform.up * (moveData.Gravity.y * Time.deltaTime);

            controller.Movement = Vector3.ProjectOnPlane(controller.Movement, controller.Raycaster.HitNormal);

            if (Vector3.Dot(controller.Movement, controller.Transform.up.normalized) > Math.Zero)
            {
                controller.Movement = controller.Movement.RemoveDot(controller.Transform.up);
            }
        
            Vector3 slideDirection = Vector3.ProjectOnPlane(-controller.Transform.up, controller.Raycaster.HitNormal).normalized;

            controller.Movement += slideDirection * (moveData.Gravity.y * Time.deltaTime);
        }
    }
}
