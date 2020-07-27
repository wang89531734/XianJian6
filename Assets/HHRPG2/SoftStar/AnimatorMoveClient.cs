using System;
using UnityEngine;

namespace SoftStar
{
	public class AnimatorMoveClient : MonoBehaviour
	{
		public delegate void AnimatorMoveApplyFunc(Animator animator);

		public AnimatorMoveClient.AnimatorMoveApplyFunc apply;

		private Animator animator;

		private void Start()
		{
			this.animator = base.GetComponent<Animator>();
		}

		private void OnAnimatorMove()
		{
			if (this.apply != null && this.animator != null)
			{
				this.apply(this.animator);
			}
		}
	}
}
