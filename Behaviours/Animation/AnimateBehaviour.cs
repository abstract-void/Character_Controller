using UnityEngine;
using ProjectDew.FSM;

namespace ProjectDew.CharacterController
{
    public class AnimateBehaviour : StateBehaviour
    {
		protected virtual Animator Animator { get; private set; }

		protected virtual AnimateData AnimateData { get; private set; }

		protected virtual int AnimationHash { get; private set; }

		public AnimateBehaviour(Animator animator, string animationName, AnimateData animateData)
		{
			Animator = animator;
			AnimateData = animateData;
			AnimationHash = Animator.StringToHash(animationName);
		}

		public override void Set ()
		{
			if (Animator.runtimeAnimatorController == null)
			{
				return;
			}

			AnimatorStateInfo animatorInfo = Animator.GetCurrentAnimatorStateInfo(0);

			if (AnimationHash != animatorInfo.shortNameHash && AnimationHash != animatorInfo.fullPathHash)
			{
				Animator.CrossFade(AnimationHash, AnimateData.TransitionDuration, AnimateData.Layer, AnimateData.TransitionOffset, AnimateData.TransitionTime);
			}
		}
    }
}
