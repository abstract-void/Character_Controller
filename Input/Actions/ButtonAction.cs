using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectDew.CharacterController
{
    public class ButtonAction : PlayerAction
    {
        public event Action Pressed;
        public event Action<float> Held;
        public event Action<float> Completed;
        public event Action<float> Released;

		public virtual float TimeHeld { get; protected set; }

        public virtual bool IsPressed { get; protected set; }

        public virtual bool IsActive { get; protected set; }

        public virtual bool IsHeld { get; protected set; }

        public ButtonAction(string id) : base(id) { }

        public override void DetectInput(InputAction.CallbackContext context)
        {
			if (context.phase == InputActionPhase.Started)
			{
				Press();
			}
			else if (context.phase == InputActionPhase.Canceled)
			{
				Release();
			}
        }

        public virtual void HoldButton(float timeLimit)
		{
			if (!IsHeld || !IsActive)
			{
				return;
			}

			TimeHeld += Time.deltaTime;
			TimeHeld = TimeHeld.ClampMax(timeLimit);

			Held?.Invoke(TimeHeld);

			if (TimeHeld >= timeLimit)
			{
				if (IsActive)
				{
					Completed?.Invoke(TimeHeld);
				}

				IsActive = false;
			}
		}

		private void Press()
		{
			TimeHeld = 0;
			
			if (!IsHeld)
			{
				IsPressed = true;
				IsActive = true;

				Pressed?.Invoke();
			}

			IsPressed = false;
			IsHeld = true;
		}

		private void Release()
		{
			if (IsHeld)
			{
				if (IsActive)
				{
					Completed?.Invoke(TimeHeld);
				}

				IsActive = false;

				Released?.Invoke(TimeHeld);
			}

			IsHeld = false;
		}
    }
}
