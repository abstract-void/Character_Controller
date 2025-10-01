using UnityEngine;

namespace ProjectDew.CharacterController
{
	using static ControllerEditorConstants;

    [CreateAssetMenu(fileName = MoveDataName, menuName = MoveDataPath, order = MoveDataOrder)]
    public class MoveData : ScriptableObject, IMoveData
    {
		[SerializeField]
		private float moveSpeed = 7f;

		[SerializeField, Range(Math.Zero, Math.One)]
		private float controlRate = 0.02f;
		
		[SerializeField]
		private float friction = 0.5f;

		[SerializeField]
		private Vector3 gravity = new(0f, 30f, 0f);

		public float MoveSpeed => moveSpeed;

		public float ControlRate => controlRate;

		public float Friction => friction;

		public Vector3 Gravity => gravity;
    }
}
