namespace ProjectDew.CharacterController
{
    public interface IMoveController
    {
        bool BeginLanding { get; }

        bool IsGrounded { get; }
    }
}
