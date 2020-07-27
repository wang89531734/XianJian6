using System;
using UnityEngine;

public class AnimEvent : MonoBehaviour
{
	public delegate void void_fun();

	public delegate void void_fun_float(float k);

	public delegate void void_fun_int(int idx);

	public AnimEvent.void_fun OnProcessFun;

	public AnimEvent.void_fun_float OnMatchStart;

	public AnimEvent.void_fun OnMatchEnd;

	public AnimEvent.void_fun_float OnJumpEvent;

	public AnimEvent.void_fun OnJumpStart;

	public AnimEvent.void_fun OnJumpOver;

	public AnimEvent.void_fun_int OnTakePlaceEvent;

	public AnimEvent.void_fun_int OnPlaceEvent;

	public AnimEvent.void_fun OnBattleStateToggleEvent;

	public AnimEvent.void_fun OnNormalStateEvent;

	public AnimEvent.void_fun_float OnStartFight;

	public AnimEvent.void_fun_int OnAudioEvent;

	public Action<int> OnFootStep;

	private void Update()
	{
		if (this.OnProcessFun == null)
		{
			return;
		}
		this.OnProcessFun();
	}

	private void OnDestroy()
	{
		this.OnProcessFun = null;
		this.OnMatchStart = null;
		this.OnMatchEnd = null;
		this.OnJumpEvent = null;
		this.OnJumpStart = null;
		this.OnJumpOver = null;
		this.OnTakePlaceEvent = null;
		this.OnPlaceEvent = null;
		this.OnBattleStateToggleEvent = null;
		this.OnNormalStateEvent = null;
		this.OnStartFight = null;
		this.OnAudioEvent = null;
		this.OnFootStep = null;
	}

	public void JumpEvent(float JumpSpeedK)
	{
		if (this.OnJumpEvent == null )
		{
			return;
		}
		this.OnJumpEvent(JumpSpeedK);
	}

	public void JumpStart()
	{
		if (this.OnJumpStart == null)
		{
			return;
		}
		this.OnJumpStart();
	}

	public void JumpOver()
	{
		if (this.OnJumpOver == null)
		{
			return;
		}
		this.OnJumpOver();
	}

	public void MatchStart(float k)
	{
		if (this.OnMatchStart == null)
		{
			return;
		}
		this.OnMatchStart(k);
	}

	public void MatchEnd()
	{
		if (this.OnMatchEnd == null)
		{
			return;
		}
		this.OnMatchEnd();
	}

	public void TakePlaceEvent(int idx)
	{
		if (this.OnTakePlaceEvent == null)
		{
			return;
		}
		this.OnTakePlaceEvent(idx);
	}

	public void BattleStateToggleEvent()
	{
		if (this.OnBattleStateToggleEvent == null)
		{
			return;
		}
		this.OnBattleStateToggleEvent();
	}

	public void FootStep(int index)
	{
		if (this.OnFootStep == null)
		{
			return;
		}
		this.OnFootStep(index);
	}

	public void AudioEvent(int index)
	{
		if (this.OnAudioEvent == null)
		{
			return;
		}
		this.OnAudioEvent(index);
	}

	public void StartFight(float duration)
	{
		if (this.OnStartFight == null)
		{
			return;
		}
		this.OnStartFight(duration);
	}
}
