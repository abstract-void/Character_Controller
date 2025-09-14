namespace ProjectDew.CharacterController
{
    public interface IJumpData : IGravityData
    {
		float JumpSpeed { get; }

		float JumpDuration { get; }

		float CeilingAngleLimit { get; }

		float TimeBetweenJumps { get; }

		int MaxJumpsAllowed { get; }
    }
}
