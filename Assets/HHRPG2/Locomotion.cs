using System;
using UnityEngine;

public class Locomotion : MonoBehaviour
{
	private Animator m_Animator;

	private int m_SpeedId;

	private int m_AgularSpeedId;

	private int m_DirectionId;

	public float gravityK = 1.5f;

	public PlayerAnimType playerAnimType = PlayerAnimType.Humanoid;

	private float m_SpeedDampTime = 0.1f;

	public float m_AnguarSpeedDampTime = 0.2f;

	public float m_DirectionResponseTime = 0.5f;

	private float VerticalSpeed;

	protected CharacterController charCtrller;

	public Vector3 ZhiKongSpeedVec;

	public float ZhiKongMag;

	public bool firstShang;

	public float RotSpeed = 6f;

	[NonSerialized]
	public float ORotSpeed = 2.5f;

	private float zhuodiTime = -1f;

	private bool lastinIdle;

	private bool lastinTurn;

	private bool lastinWalkRun;

	private string LocomotionName = "yidongState";

	private string WalkRunName = "YiDong";

	private string IdleName = "ZhanLi";

	private bool keyLate;

	private bool keyLate2;

	private Quaternion targetRotation;

	private void Awake()
	{
		this.m_Animator = base.GetComponentInChildren<Animator>();
		this.charCtrller = base.GetComponent<CharacterController>();
		this.m_SpeedId = Animator.StringToHash("Speed");
		this.m_AgularSpeedId = Animator.StringToHash("AngularSpeed");
		this.m_DirectionId = Animator.StringToHash("Direction");
	}

	public void Do(float speed, float direction2, Transform t, Vector3 moveDirection)
	{
		if (this.m_Animator == null)
		{
			this.m_Animator = base.GetComponentInChildren<Animator>();
		}
		AnimatorStateInfo currentAnimatorStateInfo = this.m_Animator.GetCurrentAnimatorStateInfo(0);
		AnimatorStateInfo nextAnimatorStateInfo = this.m_Animator.GetNextAnimatorStateInfo(0);
		bool flag = this.m_Animator.IsInTransition(0);
		bool flag2 = currentAnimatorStateInfo.IsName(this.LocomotionName + "." + this.IdleName);
		bool flag3 = currentAnimatorStateInfo.IsName(this.LocomotionName + ".TurnOnSpot") || currentAnimatorStateInfo.IsName(this.LocomotionName + ".PlantNTurnLeft") || currentAnimatorStateInfo.IsName(this.LocomotionName + ".PlantNTurnRight");
		bool @bool = this.m_Animator.GetBool("Move");
		float dampTime = (!flag2) ? this.m_SpeedDampTime : 0f;
		float dampTime2 = (!@bool && !flag) ? 0f : this.m_AnguarSpeedDampTime;
		float dampTime3 = (float)((!flag3 && !flag) ? 0 : 1000000);
        this.m_Animator.SetFloat(this.m_SpeedId, speed, dampTime, Time.deltaTime);
        //if (@bool)
        //{
        //	float value = direction2 / this.m_DirectionResponseTime;
        //	if (this.lastinTurn)
        //	{
        //		value = 0f;
        //	}
        //	if (speed > 0f)
        //	{
        //		this.m_Animator.rootRotation = Quaternion.Lerp(this.m_Animator.rootRotation, Quaternion.LookRotation(moveDirection), Time.deltaTime * this.RotSpeed);
        //		this.m_Animator.SetFloat(this.m_AgularSpeedId, value, dampTime2, Time.deltaTime);
        //	}
        //	else if (speed < 0f)
        //	{
        //		this.m_Animator.rootRotation = Quaternion.Lerp(this.m_Animator.rootRotation, Quaternion.LookRotation(-moveDirection), Time.deltaTime * this.RotSpeed);
        //		this.m_Animator.SetFloat(this.m_AgularSpeedId, 0f, dampTime2, Time.deltaTime);
        //	}
        //}
        //else
        //{
        //	this.m_Animator.SetFloat(this.m_AgularSpeedId, 0f);
        //}

        //if (direction2 != 0f && !currentAnimatorStateInfo.IsName(this.LocomotionName + ".TurnOnSpot"))
        //{
        //	this.m_Animator.SetFloat(this.m_DirectionId, direction2, dampTime3, Time.deltaTime);
        //	this.targetRotation = base.transform.rotation * Quaternion.AngleAxis(direction2, Vector3.up);
        //	this.keyLate = true;
        //	this.keyLate2 = false;
        //}

        //if (this.keyLate)
        //{
        //	if (currentAnimatorStateInfo.IsName(this.LocomotionName + ".TurnOnSpot"))
        //	{
        //		this.m_Animator.MatchTarget(Vector3.one, this.targetRotation, AvatarTarget.Root, new MatchTargetWeightMask(Vector3.zero, 1f), currentAnimatorStateInfo.normalizedTime);
        //	}
        //	if (!this.keyLate2 && (nextAnimatorStateInfo.IsName(this.LocomotionName + "." + this.IdleName) || nextAnimatorStateInfo.IsName(this.LocomotionName + "." + this.WalkRunName)))
        //	{
        //		this.keyLate2 = true;
        //	}
        //	else if (this.keyLate2 && (currentAnimatorStateInfo.IsName(this.LocomotionName + "." + this.IdleName) || @bool))
        //	{
        //		this.keyLate = false;
        //		this.keyLate2 = false;
        //		this.m_Animator.SetFloat(this.m_DirectionId, 0f);
        //	}
        //}
        this.lastinWalkRun = @bool;
		this.lastinIdle = flag2;
		this.lastinTurn = flag3;
	}
}
