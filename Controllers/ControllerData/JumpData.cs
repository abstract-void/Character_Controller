using UnityEngine;

namespace ProjectDew.CharacterController
{
	using static ControllerEditorConstants;

    [CreateAssetMenu(fileName = JumpDataName, menuName = JumpDataPath, order = JumpDataOrder)]
    public class JumpData : ScriptableObject, IJumpData
    {
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

		public float JumpSpeed => jumpSpeed;

		public float JumpDuration => jumpDuration;

		public float CeilingAngleLimit => ceilingAngleLimit;

		public float TimeBetweenJumps => timeBetweenJumps;

		public int MaxJumpsAllowed => maxJumpsAllowed;
    }
}
