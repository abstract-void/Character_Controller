using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectDew.CharacterController
{
    [CreateAssetMenu(fileName = "SimpleInputData", menuName = "Scriptable Objects/Simple Input Data")]
    public class SimpleInputData : ScriptableObject
    {
		[SerializeField]
		private InputActionAsset inputActions;
		
		[SerializeField]
		private InputActionReference moveAction;

		[SerializeField]
		private InputActionReference lookAction;

		[SerializeField]
		private InputActionReference runAction;

		[SerializeField]
		private InputActionReference jumpAction;

		[SerializeField]
		private InputActionReference crouchAction;

		[SerializeField]
		private InputActionReference interactAction;

		[SerializeField]
		private InputActionReference lockCameraAction;

		public InputActionAsset InputActions => inputActions;

		public InputActionReference Move => moveAction;

		public InputActionReference Look => lookAction;

		public InputActionReference Run => runAction;

		public InputActionReference Jump => jumpAction;

		public InputActionReference Crouch => crouchAction;

		public InputActionReference Interact => interactAction;

		public InputActionReference LockCamera => lockCameraAction;
    }
}
