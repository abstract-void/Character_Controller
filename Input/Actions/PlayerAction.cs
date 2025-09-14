using UnityEngine.InputSystem;

namespace ProjectDew.CharacterController
{
    public abstract class PlayerAction
    {
        public string ID { get; }

        public PlayerAction(string id)
        {
            ID = id;
        }

        public abstract void DetectInput(InputAction.CallbackContext context);
    }
}
