using UnityEngine;
using ProjectDew.FSM;

namespace ProjectDew.CharacterController
{
	[CreateAssetMenu (fileName = "SimpleStateMachineData", menuName = "Scriptable Objects/Simple State Machine Data")]
	public class SimpleStateMachineData : StateMachineData<SimpleController>
	{
		public override StateTransitionData<SimpleController>[] GetTransitionData ()
		{
			StateTransitionData<SimpleController>[] transitionData = new StateTransitionData<SimpleController>[]
			{
				new ("Grounded", "Sliding", (controller) => controller.BeginLanding && controller.BeginSliding),
				new ("Grounded", "Falling", (controller) => !controller.BeginLanding),
				new ("Grounded", "Jumping", (controller) => controller.BeginJumping),
				new ("Grounded", "Running", (controller) => controller.BeginRunning),
				new ("Grounded", "Crouching", (controller) => controller.BeginCrouching),
				new ("Grounded", "Interacting", (controller) => controller.BeginInteracting),

				new ("Interacting", "Grounded", (controller) => !controller.BeginInteracting && !controller.IsRunning && !controller.IsCrouching),
				new ("Interacting", "Running", (controller) => !controller.BeginInteracting && controller.IsRunning),
				new ("Interacting", "Crouching", (controller) => !controller.BeginInteracting && controller.IsCrouching),

				new ("Running", "Grounded", (controller) => !controller.BeginRunning),
				new ("Running", "Sliding", (controller) => controller.BeginLanding && controller.BeginSliding),
				new ("Running", "Falling", (controller) => !controller.BeginLanding),
				new ("Running", "Jumping", (controller) => controller.BeginJumping),
				new ("Running", "Interacting", (controller) => controller.BeginInteracting),

				new ("Jumping", "Rising", (controller) => !controller.BeginJumping),
				new ("Jumping", "Falling", (controller) => controller.JumpWasInterrupted),

				new ("Crouching", "Grounded", (controller) => !controller.BeginCrouching),
				new ("Crouching", "Sliding", (controller) => controller.BeginLanding && controller.BeginSliding),
				new ("Crouching", "Falling", (controller) => !controller.BeginLanding),
				new ("Crouching", "Interacting", (controller) => controller.BeginInteracting),

				new ("Sliding", "Falling", (controller) => !controller.BeginLanding),
				new ("Sliding", "Grounded", (controller) => controller.BeginLanding && !controller.BeginSliding),

				new ("Rising", "Grounded", (controller) => controller.BeginLanding && !controller.BeginSliding),
				new ("Rising", "Sliding", (controller) => controller.BeginLanding && controller.BeginSliding),
				new ("Rising", "Falling", (controller) => controller.BeginFalling || controller.JumpWasInterrupted),
				new ("Rising", "Jumping", (controller) => controller.BeginJumping),

				new ("Falling", "Grounded", (controller) => controller.BeginLanding && !controller.BeginSliding),
				new ("Falling", "Sliding", (controller) => controller.BeginLanding && controller.BeginSliding),
				new ("Falling", "Jumping", (controller) => controller.BeginJumping)
			};

			return transitionData;
		}
	}
}
