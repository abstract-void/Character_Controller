using UnityEngine;

namespace ProjectDew.CharacterController
{
	using static ControllerEditorConstants;

    [CreateAssetMenu(fileName = "InteractData", menuName = "Scriptable Objects/InteractData")]
    public class InteractData : ScriptableObject, IInteractData
    {
		[SerializeField]
		private LayerMask interactableLayer;

		public LayerMask InteractableLayer => interactableLayer;
    }
}
