using System;
using UnityEngine;

public class TurnHead2 : MonoBehaviour
{
	private Animator animator;

	[HideInInspector]
	public float lookAtWeight;

	[HideInInspector]
	public Vector3 target = Vector3.zero;

	private void Start()
	{
		this.animator = base.GetComponent<Animator>();
		PlayersManager.OnTabPlayer -= new Action<int>(this.TabReset);
		PlayersManager.OnTabPlayer += new Action<int>(this.TabReset);
        //GameStateManager.AddEndStateFun(GameState.Normal, new GameStateManager.void_fun(this.Reset));
        lookAtWeight = 0f;
    }

	private void OnDestroy()
	{
		PlayersManager.OnTabPlayer -= new Action<int>(this.TabReset);
		//GameStateManager.RemoveEndStateFun(GameState.Normal, new GameStateManager.void_fun(this.Reset));
	}

	private void OnAnimatorIK(int layerIndex)
	{
		if (this.lookAtWeight <= 0f)
		{
			return;
		}
		this.animator.SetLookAtPosition(this.target);
		this.animator.SetLookAtWeight(this.lookAtWeight, 0.3f, 0.6f, 1f);
	}

	private void TabReset(int unusing)
	{
		this.Reset();
	}

	private void Reset()
	{
		this.lookAtWeight = 0f;
	}
}
