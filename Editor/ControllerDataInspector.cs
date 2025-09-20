using UnityEditor;
using UnityEngine.UIElements;

namespace ProjectDew.CharacterController
{
	[CustomEditor(typeof(ControllerData))]
    public class ControllerDataInspector : BaseInspector
    {
		private DataHandler moveDataHandler;
		private DataHandler jumpDataHandler;
		private DataHandler slideDataHandler;

		private void OnEnable()
		{
			moveDataHandler = new(serializedObject, "moveData");
			jumpDataHandler = new(serializedObject, "jumpData");
			slideDataHandler = new(serializedObject, "slideData");
		}

		public override VisualElement CreateInspectorGUI()
		{
			VisualElement root = base.CreateInspectorGUI();

			moveDataHandler.UpdateInspectorLogic(root, "move_asset", "move_data_container", "remove_move_asset_button");
			jumpDataHandler.UpdateInspectorLogic(root, "jump_asset", "jump_data_container", "remove_jump_asset_button");
			slideDataHandler.UpdateInspectorLogic(root, "slide_asset", "slide_data_container", "remove_slide_asset_button");

			return root;
		}
    }
}
