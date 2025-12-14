namespace ProjectDew.CharacterController
{
    public sealed class SimpleInputReader : InputReader
    {
		private readonly SimpleInputData inputData;

        public MoveAction Move { get; private set; }

        public LookAction Look { get; private set; }
        
        public ButtonAction Run { get; private set; }

        public ButtonAction Jump { get; private set; }

        public ButtonAction Crouch { get; private set; }

        public ButtonAction Interact { get; private set; }

        public ButtonAction LockCamera { get; private set; }

        public SimpleInputReader(SimpleInputData inputData) : base(inputData.InputActions)
        {
			this.inputData = inputData;

            Move = new (inputData.Move.name);
            Look = new (inputData.Look.name);

            Run = new (inputData.Run.name);
            Jump = new (inputData.Jump.name);
            Crouch = new (inputData.Crouch.name);
            Interact = new (inputData.Interact.name);
            LockCamera = new (inputData.LockCamera.name);

            AddCallback(Move);            
            AddCallback(Look);

            AddCallback(Run, DetectionMode.Pressed);
            AddCallback(Jump, DetectionMode.Pressed);
            AddCallback(Crouch, DetectionMode.Pressed);
            AddCallback(Interact, DetectionMode.Pressed);
            AddCallback(LockCamera, DetectionMode.Pressed);
        }

        public override void Enable() => inputData.InputActions.Enable();

        public override void Disable() => inputData.InputActions.Disable();

        public override void Dispose()
        {
            RemoveCallback(Move);            
            RemoveCallback(Look);
            
            RemoveCallback(Run);
            RemoveCallback(Jump);
            RemoveCallback(Crouch);
            RemoveCallback(Interact);
            RemoveCallback(LockCamera);
        }
    }
}
