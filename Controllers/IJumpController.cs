namespace ProjectDew.CharacterController
{
    public interface IJumpController
    {
        bool BeginJumping { get; }
        
        bool IsJumping { get; }
        
        bool JumpWasInterrupted { get; }
    }
}
