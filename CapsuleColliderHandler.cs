using UnityEngine;

namespace ProjectDew.CharacterController
{
	[RequireComponent(typeof(CapsuleCollider))]
	public class CapsuleColliderHandler : ColliderHandler
	{
		private const float BASE_COLLIDER_HEIGHT = 2f;
		private const float STEP_HEIGHT_ADJUSTMENT = 0.28f;

		private CapsuleCollider capsuleCollider;

		public override float StepHeight
		{
			get
			{
				float adjustedStepHeight = base.StepHeight - STEP_HEIGHT_ADJUSTMENT;
				return adjustedStepHeight.ClampMin(Math.Zero);
			}
		}

		public override void RecalculateColliderDimensions()
		{
			if (Collider == null)
			{
				Collider = GetComponent<CapsuleCollider>();
			}

			if (capsuleCollider == null && Collider != null)
			{
				capsuleCollider = (CapsuleCollider)Collider;
			}

			float colliderHeight = BaseSize.y - StepHeight;
			
			float adjustedColliderHeight = colliderHeight - BASE_COLLIDER_HEIGHT;
			float colliderCenterY = adjustedColliderHeight.Half() + StepHeight;

			capsuleCollider.height = colliderHeight;
			capsuleCollider.radius = Math.ClampMax(BaseSize.x.Half(), BoundsSize.y.Half());
			capsuleCollider.center = Offset + new Vector3(Math.Zero, colliderCenterY, Math.Zero);

			OnRecalculateDimensions();
		}
	}
}
