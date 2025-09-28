using UnityEngine;

namespace ProjectDew.CharacterController
{
    public interface IInteractData
    {
		LayerMask InteractableLayer { get; }
    }
}
