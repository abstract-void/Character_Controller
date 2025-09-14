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

        public abstract void Enable();

        public abstract void Disable();

        public abstract void Dispose();

        public void AddCallback(IInputActionCollection2 input, PlayerAction action) => AddCallback(input, action.ID, action, DetectionMode.Complete);

        public void AddCallback(IInputActionCollection2 input, PlayerAction action, DetectionMode detectionMode) => AddCallback(input, action.ID, action, detectionMode);

        public void AddCallback(IInputActionCollection2 input, string actionName, PlayerAction action) => AddCallback(input, actionName, action, DetectionMode.Complete);

        public void AddCallback(IInputActionCollection2 input, string actionName, PlayerAction action, DetectionMode detectionMode)
        {
            input.FindAction(actionName).started += action.DetectInput;
            input.FindAction(actionName).canceled += action.DetectInput;
            
            if (detectionMode == DetectionMode.Complete)
            {
                input.FindAction(actionName).performed += action.DetectInput;
            }
        }

        public void RemoveCallback(IInputActionCollection2 input, PlayerAction action) => RemoveCallback(input, action.ID, action);

        public void RemoveCallback(IInputActionCollection2 input, string actionName, PlayerAction action)
        {
            input.FindAction(actionName).started -= action.DetectInput;
            input.FindAction(actionName).canceled -= action.DetectInput;
            input.FindAction(actionName).performed -= action.DetectInput;
        }
    }
}
