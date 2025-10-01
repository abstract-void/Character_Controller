using UnityEngine;

namespace ProjectDew.CharacterController
{
    public class AnimateMoveBehaviour : AnimateBehaviour
    {
		private readonly Controller controller;

		public AnimateMoveBehaviour(Controller controller, Animator animator, string animationName, AnimateData animateData) : base(animator, animationName, animateData)
		{
			this.controller = controller;
		}

		public override void ExecuteFixed () => Animator.SetFloat("Speed", controller.Rigidbody.linearVelocity.magnitude);
    }
}
