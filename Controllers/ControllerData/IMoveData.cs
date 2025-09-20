using UnityEngine;

namespace ProjectDew.CharacterController
{
    public interface IMoveData
    {
		float MoveSpeed { get; }

		float ControlRate { get; }

		float Friction { get; }

		Vector3 Gravity { get; }
    }
}
