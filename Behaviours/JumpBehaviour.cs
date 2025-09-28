using System;
using UnityEngine;

namespace ProjectDew.CharacterController
{
	public class JumpBehaviour : ControllerBehaviour, IDisposable
	{
		private readonly Controller controller;

		private readonly IJumpData jumpData;

		public int NumberOfJumps { get; private set; }

		public bool JumpWasInterrupted { get; private set; }

		public JumpBehaviour(Controller controller, IMoveData moveData, IJumpData jumpData) : base(controller, moveData)
		{
			this.controller = controller;
			this.jumpData = jumpData;

			controller.CollisionEnter += CheckForCeilingContact;
			controller.CollisionStay += CheckForCeilingContact;
		}

		public void Dispose()
		{
			controller.CollisionEnter -= CheckForCeilingContact;
			controller.CollisionStay -= CheckForCeilingContact;
		}

		public override void ExecuteFixed()
		{
			base.ExecuteFixed();
			JumpWasInterrupted = false;
		}

		protected override void AdjustVerticalMovement()
		{
			controller.Movement = controller.Movement.RemoveDot(controller.Transform.up);
			controller.Movement += controller.Transform.up * jumpData.JumpSpeed;
		}

		public void CheckForCeilingContact(Collision collision)
		{
			if (collision.contacts.Length == 0)
			{
				return;
			}
			
			float angle = Vector3.Angle(-controller.Transform.up, collision.contacts[0].normal);

			if (angle < jumpData.CeilingAngleLimit)
			{
				JumpWasInterrupted = true;
			}
		}

		public void IncreaseJumpCount(float pressDuration)
		{
			if (NumberOfJumps < jumpData.MaxJumpsAllowed)
			{
				if (NumberOfJumps == 0 || pressDuration > jumpData.TimeBetweenJumps)
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
