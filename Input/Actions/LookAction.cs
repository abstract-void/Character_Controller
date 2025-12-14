using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectDew.CharacterController
{
    public class LookAction : PlayerAction
    {
        public event Action<Vector2, bool> Look;

        public Vector2 Direction { get; private set; }

        public LookAction(string name) : base(name) { }

        public override void DetectInput(InputAction.CallbackContext context)
        {
            bool isDeviceMouse = context.control.device.name == "Mouse";

            Direction = context.ReadValue<Vector2>();

            Look?.Invoke(Direction, isDeviceMouse);
        }
    }
}
