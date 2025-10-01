using UnityEngine;

namespace ProjectDew.CharacterController
{
	using static ControllerEditorConstants;

    [CreateAssetMenu(fileName = AnimateDataName, menuName = AnimateDataPath, order = AnimateDataOrder)]
    public class AnimateData : ScriptableObject
    {
		[SerializeField]
		private int animationLayer = 0;

		[SerializeField, Range(0, 1)]
		private float transitionOffset = 0.0f;

		[SerializeField, Range(0, 1)]
		private float transitionTime = 0.9f;

		[SerializeField, Range(0, 1)]
		private float transitionDuration = 0.1f;

		public int Layer => animationLayer;

		public float TransitionOffset => transitionOffset;

		public float TransitionTime => transitionTime;

		public float TransitionDuration => transitionDuration;
    }
}
