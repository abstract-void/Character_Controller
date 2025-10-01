using UnityEngine;

namespace ProjectDew.CharacterController
{
	using static ControllerEditorConstants;

    [CreateAssetMenu(fileName = SlideDataName, menuName = SlideDataPath, order = SlideDataOrder)]
    public class SlideData : ScriptableObject, ISlideData
    {
		[SerializeField]
		private float slopeLimit = 30f;

		public float SlopeLimit => slopeLimit;
    }
}
