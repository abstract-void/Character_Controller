using UnityEngine.InputSystem;

namespace ProjectDew.CharacterController
{
    public abstract class PlayerAction
    {
        public string Name { get; }

        public PlayerAction(string name)
        {
            Name = name;
        }

        public abstract void DetectInput(InputAction.CallbackContext context);
    }
}
