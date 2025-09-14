using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectDew.CharacterController
{
    public class MoveAction : PlayerAction
    {
        public event Action<Vector2> Move;

        public Vector2 Direction { get; private set; }

        public MoveAction(string id) : base(id) { }

        public override void DetectInput(InputAction.CallbackContext context)
        {
            Direction = context.ReadValue<Vector2>();
            Move?.Invoke(Direction);
        }
    }
}
