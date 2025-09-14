using UnityEngine;

namespace ProjectDew.CharacterController
{
    public interface IGravityData : IMoveData
    {
		Vector3 Gravity { get; }

		float AirFriction { get; }
    }
}
