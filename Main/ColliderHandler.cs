using System;
using UnityEngine;

namespace ProjectDew.CharacterController
{
	[RequireComponent(typeof(Collider))]
    public abstract class ColliderHandler : MonoBehaviour
    {
        private const float MIN_COLLIDER_RANGE = 0.01f;
        private const float MAX_COLLIDER_RANGE = 20f;

        public event Action RecalculateDimensions;

        [SerializeField]
        private Vector3 colliderOffset;

        [SerializeField]
        private Vector3 colliderSize = Vector3.one;

        [SerializeField, Range(MIN_COLLIDER_RANGE, MAX_COLLIDER_RANGE)]
        private float stepHeight = 0.1f;

		private Transform tr;

        public virtual Collider Collider { get; protected set; }

		public virtual Bounds Bounds => Collider.bounds;

        public virtual Vector3 Size => new Vector3(Bounds.size.x, Bounds.size.y + StepHeight * tr.localScale.y, Bounds.size.z);

        public virtual Vector3 WorldCenter => new Vector3(Bounds.center.x, Bounds.center.y - (StepHeight * tr.localScale.y).Half(), Bounds.center.z);

        public virtual Vector3 LocalCenter => tr.InverseTransformPoint(WorldCenter);

        protected virtual Vector3 BaseSize => colliderSize;

        protected virtual Vector3 Offset => colliderOffset;

        protected virtual float StepHeight => stepHeight;

        public abstract void RecalculateColliderDimensions();

        protected virtual void OnRecalculateDimensions()
        {
            RecalculateDimensions?.Invoke();
        }

        private void Awake()
        {
			tr = transform;
            Collider = GetComponent<Collider>();
        }

        private void OnValidate()
        {
			if (tr == null)
			{
				tr = transform;
			}

			if (Collider == null)
			{
				Collider = GetComponent<Collider>();
			}

            RecalculateColliderDimensions();
		}
    }
}