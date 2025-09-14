using System;
using UnityEngine;

namespace ProjectDew.CharacterController
{
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

        public virtual Vector3 BoundsSize => Collider.bounds.size;

        public virtual Vector3 BoundsCenter => Collider.bounds.center;

        public virtual Vector3 WorldCenter => new Vector3(Collider.bounds.center.x, Collider.bounds.center.y - StepHeight.Half(), Collider.bounds.center.z);

        public virtual Vector3 LocalCenter => tr.InverseTransformPoint(WorldCenter);

        public virtual Vector3 Size => new Vector3(BoundsSize.x, BoundsSize.y + StepHeight * tr.localScale.y, BoundsSize.z);

        public virtual Vector3 BaseSize => colliderSize;

        public virtual Vector3 Offset => colliderOffset;

        public virtual float StepHeight => stepHeight;

        protected abstract void RecalculateColliderDimensions();

        protected virtual void OnRecalculateDimensions()
        {
            RecalculateDimensions?.Invoke();
        }

        private void Awake()
        {
			tr = transform;
            Collider = GetComponent<Collider>();
        }

        private void Start()
        {
            RecalculateColliderDimensions();
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