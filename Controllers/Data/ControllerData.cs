using UnityEngine;

namespace ProjectDew.CharacterController
{
	using static ControllerEditorConstants;

	[CreateAssetMenu(fileName = ControllerDataName, menuName = ControllerDataPath, order = ControllerDataOrder)]
	public class ControllerData : ScriptableObject, IMoveData, IJumpData, ISlideData
	{
		[SerializeField]
		private MoveData moveData;
		
		[SerializeField]
		private JumpData jumpData;
		
		[SerializeField]
		private SlideData slideData;

		[SerializeField]
		private float moveSpeed = 7f;

		[SerializeField, Range(Math.Zero, Math.One)]
		private float controlRate = 0.02f;
		
		[SerializeField]
		private float friction = 0.5f;
		
		[SerializeField]
		private Vector3 gravity = new(0f, 30f, 0f);
		
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
		
		[SerializeField]
		private float slopeLimit = 30f;

		#region Properties

		public float MoveSpeed => (moveData != null) ? moveData.MoveSpeed : moveSpeed;

		public float ControlRate => (moveData != null) ? moveData.ControlRate : controlRate;

		public float Friction => (moveData != null) ? moveData.Friction : friction;

		public Vector3 Gravity => (moveData != null) ? moveData.Gravity : gravity;

		public float JumpSpeed => (jumpData != null) ? jumpData.JumpSpeed : jumpSpeed;

		public float JumpDuration => (jumpData != null) ? jumpData.JumpDuration : jumpDuration;

		public float CeilingAngleLimit => (jumpData != null) ? jumpData.CeilingAngleLimit : ceilingAngleLimit;

		public float TimeBetweenJumps => (jumpData != null) ? jumpData.TimeBetweenJumps : timeBetweenJumps;

		public int MaxJumpsAllowed => (jumpData != null) ? jumpData.MaxJumpsAllowed : maxJumpsAllowed;

		public float SlopeLimit => (slideData != null) ? slideData.SlopeLimit : slopeLimit;
		
		#endregion
	}
}
