using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectDew.CharacterController
{
    public class MoveAction : PlayerAction
    {
        public event Action<Vector2> Move;

        public Vector2 Direction { get; private set; }

        public MoveAction(string name) : base(name) { }

        public override void DetectInput(InputAction.CallbackContext context)
        {
            Direction = context.ReadValue<Vector2>();
            Move?.Invoke(Direction);
        }
    }
}
