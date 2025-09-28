using System;
using UnityEngine;

namespace ProjectDew.CharacterController
{
	public abstract class Controller : MonoBehaviour
	{
		public event Action<Collision> CollisionEnter;
		public event Action<Collision> CollisionStay;
		public event Action<Collision> CollisionExit;

		public event Action<Collider> TriggerEnter;
		public event Action<Collider> TriggerStay;
		public event Action<Collider> TriggerExit;

		public abstract Transform Transform { get; }

		public abstract Rigidbody Rigidbody { get; }

		public abstract ColliderHandler ColliderHandler { get; }

		public abstract Raycaster Raycaster { get; }

		public abstract Vector3 Direction { get; }

		public abstract Vector3 Movement { get; set; }

		protected virtual void OnCollisionEnter(Collision collision) => CollisionEnter?.Invoke(collision);

		protected virtual void OnCollisionStay(Collision collision) => CollisionStay?.Invoke(collision);

		protected virtual void OnCollisionExit(Collision collision) => CollisionExit?.Invoke(collision);

		protected virtual void OnTriggerEnter(Collider collider) => TriggerEnter?.Invoke(collider);

		protected virtual void OnTriggerStay(Collider collider) => TriggerStay?.Invoke(collider);

		protected virtual void OnTriggerExit(Collider collider) => TriggerExit?.Invoke(collider);
	}
}
