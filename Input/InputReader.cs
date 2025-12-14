using System;
using UnityEngine.InputSystem;

namespace ProjectDew.CharacterController
{
    public abstract class InputReader : IDisposable
    {
        public enum DetectionMode
        {
            Complete,
            Pressed
        }

		private readonly IInputActionCollection2 inputActions;

		public InputReader(IInputActionCollection2 inputActions)
		{
			this.inputActions = inputActions;
		}

        public abstract void Enable();

        public abstract void Disable();

        public abstract void Dispose();

        public void AddCallback(PlayerAction action) => AddCallback(action.Name, action, DetectionMode.Complete);

        public void AddCallback(PlayerAction action, DetectionMode detectionMode) => AddCallback(action.Name, action, detectionMode);

        public void AddCallback(string actionName, PlayerAction action) => AddCallback(actionName, action, DetectionMode.Complete);

        public void AddCallback(string actionName, PlayerAction action, DetectionMode detectionMode)
        {
            inputActions.FindAction(actionName).started += action.DetectInput;
            inputActions.FindAction(actionName).canceled += action.DetectInput;
            
            if (detectionMode == DetectionMode.Complete)
            {
                inputActions.FindAction(actionName).performed += action.DetectInput;
            }
        }

        public void RemoveCallback(PlayerAction action) => RemoveCallback(action.Name, action);

        public void RemoveCallback(string actionName, PlayerAction action)
        {
            inputActions.FindAction(actionName).started -= action.DetectInput;
            inputActions.FindAction(actionName).canceled -= action.DetectInput;
            inputActions.FindAction(actionName).performed -= action.DetectInput;
        }
    }
}
