namespace ProjectDew.CharacterController
{
    public sealed class PlayerInputReader : InputReader
    {
        private readonly PlayerInput input;

        public MoveAction Move { get; private set; }
        public LookAction Look { get; private set; }
        
        public ButtonAction Run { get; private set; }
        public ButtonAction Jump { get; private set; }
        public ButtonAction Crouch { get; private set; }
        public ButtonAction Interact { get; private set; }
        public ButtonAction LockCamera { get; private set; }

        public PlayerInputReader()
        {
            input = new PlayerInput();

            Move = new ("Move");
            Look = new ("Look");

            Run = new ("Run");
            Jump = new ("Jump");
            Crouch = new ("Crouch");
            Interact = new ("Interact");
            LockCamera = new ("LockCamera");

            AddCallback(input, Move);            
            AddCallback(input, Look);

            AddCallback(input, Run, DetectionMode.Pressed);
            AddCallback(input, Jump, DetectionMode.Pressed);
            AddCallback(input, Crouch, DetectionMode.Pressed);
            AddCallback(input, Interact, DetectionMode.Pressed);
            AddCallback(input, LockCamera, DetectionMode.Pressed);
        }

        public override void Enable() => input.Enable();

        public override void Disable() => input.Disable();

        public override void Dispose()
        {
            RemoveCallback(input, Move);            
            RemoveCallback(input, Look);
            
            RemoveCallback(input, Run);
            RemoveCallback(input, Jump);
            RemoveCallback(input, Crouch);
            RemoveCallback(input, Interact);
            RemoveCallback(input, LockCamera);

            input.Dispose();
        }
    }
}
