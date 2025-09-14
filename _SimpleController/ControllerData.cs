using UnityEngine;

namespace ProjectDew.CharacterController
{
	using static ControllerEditorConstants;

	[CreateAssetMenu(fileName = ControllerDataName, menuName = ControllerDataPath, order = ControllerDataOrder)]
	public class ControllerData : ScriptableObject, IMoveData, IGravityData, IJumpData, ISlideData
	{
		[Header("Move")]

		[SerializeField]
		private float moveSpeed = 7f;

		[SerializeField, Range(Math.Zero, Math.One)]
		private float controlRate = 0.02f;
		
		[SerializeField]
		private float groundFriction = 100f;

		[Header("Gravity")]
		
		[SerializeField]
		private Vector3 gravity = new(0f, 30f, 0f); // slideGravity = 5f;
		
		[SerializeField]
		private float airFriction = 0.5f;
		
		[Header("Jump")]
		
		[SerializeField]
		private float jumpSpeed = 10f;
		
		[SerializeField]
		private float jumpDuration = 0.2f;

		[SerializeField]
		private float ceilingAngleLimit = 10f;
		
		[SerializeField]
		private float timeBetweenJumps = 0.05f;

		[SerializeField, Range(0, 10)]
		private int maxJumpsAllowed = 1;

		[Header("Slide")]
		
		[SerializeField]
		private float slopeLimit = 30f;

		#region Properties

		public float MoveSpeed => moveSpeed;

		public float ControlRate => controlRate;

		public float SlopeLimit => slopeLimit;

		public float GroundFriction => groundFriction;

		public Vector3 Gravity => gravity;

		public float AirFriction => airFriction;

		public float JumpSpeed => jumpSpeed;

		public float JumpDuration => jumpDuration;

		public float CeilingAngleLimit => ceilingAngleLimit;

		public float TimeBetweenJumps => timeBetweenJumps;

		public int MaxJumpsAllowed => maxJumpsAllowed;
		
		#endregion
	}
}
