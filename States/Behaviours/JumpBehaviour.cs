using UnityEngine;

namespace ProjectDew.CharacterController
{
	public class JumpBehaviour : ControllerBehaviour
	{
		private readonly Controller controller;

		private readonly ControllerData controllerData;

		public int NumberOfJumps { get; private set; }

		public bool JumpWasInterrupted { get; private set; }

		public JumpBehaviour(Controller controller, ControllerData controllerData) : base(controller, controllerData)
		{
			this.controller = controller;
			this.controllerData = controllerData;
		}

		public override void ExecuteFixed()
		{
			base.ExecuteFixed();
			JumpWasInterrupted = false;
		}

		protected override void AdjustVerticalMovement()
		{
			controller.Movement = controller.Movement.RemoveDot(controller.Transform.up);
			controller.Movement += controller.Transform.up * controllerData.JumpSpeed;
		}

		public void CheckForCeilingContact(Collision collision)
		{
			if (collision.contacts.Length == 0)
			{
				return;
			}
			
			float angle = Vector3.Angle(-controller.Transform.up, collision.contacts[0].normal);

			if (angle < controllerData.CeilingAngleLimit)
			{
				JumpWasInterrupted = true;
			}
		}

		public void IncreaseJumpCount(float pressDuration)
		{
			if (NumberOfJumps < controllerData.MaxJumpsAllowed)
			{
				if (NumberOfJumps == 0 || pressDuration > controllerData.TimeBetweenJumps)
				{
					NumberOfJumps++;
				}
			}
		}

		public void ResetJumpCount()
		{
			NumberOfJumps = 0;
		}
	}
}
