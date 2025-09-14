using UnityEngine;

namespace ProjectDew.CharacterController
{
	using static ControllerEditorConstants;

    [CreateAssetMenu(fileName = SurfaceDataName, menuName = SurfaceDataPath, order = SurfaceDataOrder)]
    public class SurfaceData : ScriptableObject
    {
        [SerializeField]
        private int layer;

        [SerializeField]
        private string tag;

        [SerializeField]
        private float friction;

        public int Layer => layer;

        public string Tag => tag;

        public float Friction => friction;
    }
}
