namespace ProjectDew.CharacterController
{
    public interface IGravityController
    {
        bool BeginRising { get; }

        bool BeginFalling { get; }

        bool IsRising { get; }

        bool IsFalling { get; }
    }
}
