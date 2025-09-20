using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;

namespace ProjectDew.CharacterController
{
    public class DataHandler
    {
		private readonly SerializedObject serializedObject;
		private readonly SerializedProperty dataProperty;
		
		private PropertyField dataObject;

		private VisualElement dataContainer;

		private Button removeButton;

		public DataHandler(SerializedObject serializedObject, string dataPropertyPath)
		{
			this.serializedObject = serializedObject;
			dataProperty = serializedObject.FindProperty(dataPropertyPath);
		}

		public void UpdateInspectorLogic (VisualElement root, string dataObjectID, string dataContainerID, string removeButtonID)
		{
			dataObject = root.Q<PropertyField>(dataObjectID);
			dataContainer = root.Q<VisualElement>(dataContainerID);
			removeButton = root.Q<Button>(removeButtonID);

			dataObject.RegisterCallback<ChangeEvent<Object>>(SwitchDataVisibility);

			removeButton.clicked += RemoveDataObject;
		}

		private void SwitchDataVisibility(ChangeEvent<Object> evt)
		{
			dataContainer.style.display = (evt.newValue == null) ? DisplayStyle.Flex : DisplayStyle.None;
		}

		private void RemoveDataObject()
		{
			dataProperty.objectReferenceValue = null;
			serializedObject.ApplyModifiedProperties();
		}
    }
}
