namespace ProjectDew.CharacterController
{
    public interface IMoveData
    {
		float MoveSpeed { get; }

		float ControlRate { get; }

		float GroundFriction { get; }
    }
}
