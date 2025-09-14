using UnityEngine;

namespace ProjectDew.CharacterController
{
	public abstract class Controller : MonoBehaviour
	{
		public abstract Transform Transform { get; }

		public abstract Rigidbody Rigidbody { get; }

		public abstract ColliderHandler ColliderHandler { get; }

		public abstract Raycaster Raycaster { get; }

		public abstract Vector3 Direction { get; }

		public abstract Vector3 Movement { get; set; }
	}
}
