using SoftStar.Pal6;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Locomotion))]
//[RequireComponent(typeof(AnimCtrlScript))]
[RequireComponent(typeof(CharacterController))]
public class Agent : MonoBehaviour
{
    public delegate void void_fun();

    public PalNPC palNPC;

    public NavMeshAgent agent;

    public Animator animator;

    //public AnimCtrlScript animCtrl;

    public CharacterController charCtrller;

    [NonSerialized]
    public Perception perception;

    public bool bControl = true;

    public float SpeedK = 1f;

    private bool m_NeedEnableAgent = true;

    public ControlMode m_curCtrlMode = ControlMode.None;

    protected bool controlByAgent = true;

    public float RunSpeed = 10f;

    public float WalkSpeed = 2f;

    public float AniRunSpeed = 7f;

    public float RotSpeed = 15f;

    public float dampSpeed = 3.7f;

    public float CurSpeed = 1f;

    public float LongJumpSpeed = 0.45f;

    public float jumpPower = 1f;

    private float jumpPowerSecond = 1.3f;

    public float MaxJumpPower = 2f;

    public float ActionRadius = 1.5f;

    public Locomotion locomotion;

    public Transform destObj;

    protected Vector3 destOffsetDir;

    protected float destDistance;

    public Type destType;

    public float JudgeStopDistance = 7f;

    private float gravityK = 3f;

    private float JumpSpeed = 17f;

    public float VerticalSpeed;

    public float ZhuoDiTime = 100f;

    public float ZhuoDiDistance = 0.05f;

    protected bool IsUsedInSky;

    public bool IsInSky;

    public bool IsJump;

    public bool CanSlowByKeyUp;

    private bool bSlow;

    public float XiaLuoSpeed = 5f;

    public bool CanSecondJump;

    public float SecondJumpDegree = 1.2f;

    public float SmallMoveSpeed = 4f;

    //public AnimatorMoveClient animatorMoveClient;

    //private AnimEvent animEvent;

    public Agent.void_fun OnApplyAnimatorMove;

    public float AgentSpeedK = 1f;

    //public Landmark landmark;

    public float kkk = 1f;

    private Vector3 JumpTurnBodyVelocity = Vector3.zero;

    public float JumpTurnBodySpeed = 100f;

    [HideInInspector]
    public bool CanSmallMove;

    public float SlowSmoothTime = 0.1f;

    public float SlowDegree = 0.68f;

    private float SlowSpeed;

    private float orgColliderHeight;

    private float orgHeight;

    private Vector3 tempCtrlCenter = Vector3.zero;

    private bool bJumpStart;

    public float JumpMoveRatioWhenSlide = 0.3f;

    public float CrossZhuoDiTime = 0.08f;

    //	public bool NeedEnableAgent
    //	{
    //		get
    //		{
    //			return this.m_NeedEnableAgent;
    //		}
    //		set
    //		{
    //			this.m_NeedEnableAgent = value;
    //		}
    //	}

    public ControlMode curCtrlMode
    {
        get
        {
            return this.m_curCtrlMode;
        }
        set
        {
            this.m_curCtrlMode = value;
            if (this.m_curCtrlMode == ControlMode.ControlByAgent)
            {
                //if (this.agent != null)
                //{
                //    this.agent.enabled = true;
                //    this.agent.updatePosition = true;
                //    this.agent.updateRotation = true;
                //    this.animatorMoveClient = base.gameObject.GetComponent<AnimatorMoveClient>();
                //    if (this.animatorMoveClient == null)
                //    {
                //        this.animatorMoveClient = base.gameObject.AddComponent<AnimatorMoveClient>();
                //    }
                //    AnimatorMoveClient expr_81 = this.animatorMoveClient;
                //    expr_81.apply = (AnimatorMoveClient.AnimatorMoveApplyFunc)Delegate.Remove(expr_81.apply, new AnimatorMoveClient.AnimatorMoveApplyFunc(this.NavAgentMove));
                //    AnimatorMoveClient expr_A8 = this.animatorMoveClient;
                //    expr_A8.apply = (AnimatorMoveClient.AnimatorMoveApplyFunc)Delegate.Combine(expr_A8.apply, new AnimatorMoveClient.AnimatorMoveApplyFunc(this.NavAgentMove));
                //}
            }
            else
            {
                if (this.agent != null)
                {
                    this.agent.enabled = false;
                    this.agent.updatePosition = false;
                    this.agent.updateRotation = false;
                }

                if (this.animator == null)
                {
                    this.animator = base.GetComponent<Animator>();
                }

                if (this.animator != null)
                {
                    //if (this.m_curCtrlMode == ControlMode.None)
                    //{
                    //    this.animator.SetApplyRootMotion(false);
                    //}
                    //else if (this.m_curCtrlMode != ControlMode.ControlByCutscene)
                    //{
                    //    this.SetApplyRootMotion();
                    //}
                }

                //if (this.animatorMoveClient != null)
                //{
                //    AnimatorMoveClient expr_177 = this.animatorMoveClient;
                //    expr_177.apply = (AnimatorMoveClient.AnimatorMoveApplyFunc)Delegate.Remove(expr_177.apply, new AnimatorMoveClient.AnimatorMoveApplyFunc(this.NavAgentMove));
                //}
            }
        }
    }

    //	public float SecondJumpMinDistanceFromGroud
    //	{
    //		get
    //		{
    //			return 1.3f;
    //		}
    //	}

    //	public void ClearAnimMoveClient()
    //	{
    //		if (this.animatorMoveClient != null)
    //		{
    //			UnityEngine.Object.Destroy(this.animatorMoveClient);
    //			this.animatorMoveClient = null;
    //		}
    //	}

    //	private void SetApplyRootMotion()
    //	{
    //		bool bActive = true;
    //		if (this.palNPC != null)
    //		{
    //			int num = -100;
    //			string text = this.palNPC.name;
    //			int num2 = text.IndexOf('(');
    //			if (num2 > 0)
    //			{
    //				text = text.Substring(0, num2);
    //			}
    //			if (int.TryParse(text, out num))
    //			{
    //				if (num > 7 && base.gameObject != PlayersManager.Player.GetModelObj(false))
    //				{
    //					bActive = false;
    //				}
    //			}
    //			else
    //			{
    //				bActive = false;
    //			}
    //		}
    //		this.animator.SetApplyRootMotion(bActive);
    //	}

    private void Start()
    {
        this.agent = base.GetComponent<NavMeshAgent>();
        this.animator = base.GetComponentInChildren<Animator>();
        if (this.animator != null)
        {
            this.animator.SetFloat("VerticalSpeed", 0f);
        }
        this.locomotion = base.GetComponent<Locomotion>();
        this.locomotion.RotSpeed = this.RotSpeed;
        //this.animCtrl = base.GetComponent<AnimCtrlScript>();
        //if (this.animCtrl == null)
        //{
        //    this.animCtrl = this.animator.gameObject.AddComponent<AnimCtrlScript>();
        //}

        this.charCtrller = base.GetComponent<CharacterController>();
        //if (this.animator != null)
        //{
        //    this.SetApplyRootMotion();
        //    this.animEvent = this.animator.GetComponent<AnimEvent>();
        //    if (this.animEvent == null)
        //    {
        //        this.animEvent = this.animator.gameObject.AddComponent<AnimEvent>();
        //    }
        //    AnimEvent expr_EF = this.animEvent;
        //    expr_EF.OnJumpStart = (AnimEvent.void_fun)Delegate.Remove(expr_EF.OnJumpStart, new AnimEvent.void_fun(this.OnJumpStart));
        //    AnimEvent expr_117 = this.animEvent;
        //    expr_117.OnJumpStart = (AnimEvent.void_fun)Delegate.Combine(expr_117.OnJumpStart, new AnimEvent.void_fun(this.OnJumpStart));
        //    AnimEvent expr_13F = this.animEvent;
        //    expr_13F.OnJumpOver = (AnimEvent.void_fun)Delegate.Remove(expr_13F.OnJumpOver, new AnimEvent.void_fun(this.OnJumpOver));
        //    AnimEvent expr_167 = this.animEvent;
        //    expr_167.OnJumpOver = (AnimEvent.void_fun)Delegate.Combine(expr_167.OnJumpOver, new AnimEvent.void_fun(this.OnJumpOver));
        //    AnimEvent expr_18F = this.animEvent;
        //    expr_18F.OnJumpEvent = (AnimEvent.void_fun_float)Delegate.Remove(expr_18F.OnJumpEvent, new AnimEvent.void_fun_float(this.OnJumpEvent));
        //    AnimEvent expr_1B7 = this.animEvent;
        //    expr_1B7.OnJumpEvent = (AnimEvent.void_fun_float)Delegate.Combine(expr_1B7.OnJumpEvent, new AnimEvent.void_fun_float(this.OnJumpEvent));
        //}
        //this.agent.enabled = false;
        //if (this.NeedEnableAgent && (!(PlayerCtrlManager.agentObj != null) || !(PlayerCtrlManager.agentObj == this)))
        //{
        //    this.agent.enabled = true;
        //}
        //if (((this.palNPC != null && this.palNPC.Data != null && this.palNPC.Data.CharacterID < 8) || this.palNPC == null) && this.palNPC != null && PlayersManager.Player != this.palNPC.gameObject)
        //{
        //    this.curCtrlMode = ControlMode.None;
        //}
        //if (this.palNPC != null && this.palNPC.MonsterGroups.Length < 1)
        //{
        //    this.agent.baseOffset = -0.1f;
        //    if (!NPCHeight.Instance.SetHeight(this.agent))
        //    {
        //        base.gameObject.AddComponent<AdjustNavAgentOffset>();
        //    }
        //}
        //GameObjectEvent x = base.GetComponent<GameObjectEvent>();
        //if (x == null)
        //{
        //    x = base.gameObject.AddComponent<GameObjectEvent>();
        //}
        //if (this.XiaLuoSpeed < 9f)
        //{
        //    this.XiaLuoSpeed = 9f;
        //}
    }

    protected void SetDestination0()
    {
        UnityEngine.Debug.Log("执行");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit = default(RaycastHit);
        if (Physics.Raycast(ray, out raycastHit))
        {
            this.agent.destination = raycastHit.point;
        }
    }

    //	public void ResetDestination()
    //	{
    //		if (this.destType == typeof(Landmark))
    //		{
    //			Landmark component = this.destObj.GetComponent<Landmark>();
    //			this.SetDestination(component);
    //		}
    //		else if (this.destType == typeof(GameObject))
    //		{
    //			this.SetDestination(this.destObj.gameObject);
    //		}
    //	}

    public void SetDestination(Vector3 position)
    {
        UnityEngine.Debug.Log("执行");
        this.agent.destination = position;
    }

    //	public void SetDestination(GameObject npcObj, Vector3 destPos)
    //	{
    //		this.destObj = npcObj.transform;
    //		PalNPC component = npcObj.GetComponent<PalNPC>();
    //		if (component != null && component.model)
    //		{
    //			this.destObj = component.model.transform;
    //		}
    //		this.destType = typeof(GameObject);
    //		this.SetDestination(this.destObj.transform.position + this.destObj.transform.rotation * destPos);
    //	}

    //	public void SetDestination(GameObject npcObj)
    //	{
    //		PalNPC component = base.GetComponent<PalNPC>();
    //		if (component == null)
    //		{
    //			UnityEngine.Debug.LogWarning("没有NPC类");
    //			return;
    //		}
    //		Vector3 aimPos = component.GetAimPos(npcObj);
    //		this.destObj = npcObj.transform;
    //		this.destType = typeof(GameObject);
    //		this.SetDestination(aimPos);
    //	}

    //	public void SetDestination(GameObject npcObj, Vector3 offsetDir, float distance)
    //	{
    //		float radius = PalNPC.GetRadius(npcObj);
    //		offsetDir.Normalize();
    //		offsetDir *= radius + this.agent.radius + distance;
    //		Vector3 destination = npcObj.transform.position + npcObj.transform.rotation * offsetDir;
    //		this.destObj = npcObj.transform;
    //		this.destType = typeof(GameObject);
    //		this.destOffsetDir = offsetDir;
    //		this.destDistance = distance;
    //		this.SetDestination(destination);
    //	}

    //	public void SetDestination(Landmark landmark)
    //	{
    //		this.destObj = landmark.transform;
    //		this.destType = typeof(Landmark);
    //		Vector3 vector = landmark.GetNewPos();
    //		if (!landmark.InSpace)
    //		{
    //			vector.y += 1.3f;
    //			RaycastHit raycastHit;
    //			if (Physics.Raycast(vector, Vector3.down, out raycastHit))
    //			{
    //				vector = raycastHit.point;
    //			}
    //			else if (Physics.Raycast(vector, Vector3.up, out raycastHit))
    //			{
    //				vector = raycastHit.point;
    //			}
    //		}
    //		this.SetDestination(vector);
    //	}

    //	public void SetupAgentLocomotion()
    //	{
    //		if (this.AgentDone())
    //		{
    //			this.locomotion.Do(0f, 0f, base.transform, this.agent.desiredVelocity);
    //		}
    //		else
    //		{
    //			this.SetupAgentLocomotionByUScript();
    //		}
    //	}

    //	public void ActiveDoMove(float Speed, GameObject destActor, Vector3 destPos)
    //	{
    //		this.agent.speed = Speed;
    //		this.agent.enabled = false;
    //		this.agent.enabled = true;
    //		this.curCtrlMode = ControlMode.ControlByAgent;
    //		if (destActor != null)
    //		{
    //			this.animator.SetBool("Move", true);
    //			this.landmark = destActor.GetComponent<Landmark>();
    //			if (this.landmark != null)
    //			{
    //				this.SetDestination(this.landmark);
    //			}
    //			else
    //			{
    //				this.SetDestination(destActor, destPos);
    //			}
    //		}
    //		else
    //		{
    //			this.animator.SetBool("Move", true);
    //			this.SetDestination(destPos);
    //		}
    //		AnimatorValueRestore component = base.GetComponent<AnimatorValueRestore>();
    //		if (component != null)
    //		{
    //			UnityEngine.Object.Destroy(component);
    //		}
    //	}

    public void DeActiveDoMove(bool resetCtrlMode = false)
    {
        UnityEngine.Debug.Log("执行");
        if (!base.gameObject.activeSelf)
        {
            return;
        }
        if (this.agent == null)
        {
            return;
        }
        this.agent.SetDestination(base.transform.position);
        if (resetCtrlMode)
        {
            this.curCtrlMode = ControlMode.None;
        }
    }

    public static void DeActiveDoMove(GameObject go, bool resetCtrlMode = false)
    {
        UnityEngine.Debug.Log("执行");
        if (go == null)
        {
            return;
        }
        go = go.GetModelObj(false);
        Agent component = go.GetComponent<Agent>();
        if (component != null)
        {
            component.DeActiveDoMove(resetCtrlMode);
        }
    }

    //	public void Pause()
    //	{
    //	}

    //	public void SetupAgentLocomotionByUScript()
    //	{
    //		float speed = this.agent.desiredVelocity.magnitude * this.AgentSpeedK;
    //		Vector3 vector = Quaternion.Inverse(base.transform.rotation) * this.agent.desiredVelocity;
    //		float direction = Mathf.Atan2(vector.x, vector.z) * 180f / 3.14159274f;
    //		this.locomotion.Do(speed, direction, base.transform, this.agent.desiredVelocity);
    //	}

    //	public void SetupAgentStopByUScript()
    //	{
    //		this.locomotion.Do(0f, 0f, base.transform, this.agent.desiredVelocity);
    //	}

    //	public float GetSpeed()
    //	{
    //		return this.animator.GetFloat("Speed");
    //	}

    //	public bool AgentDone()
    //	{
    //		return !this.agent.pathPending && this.AgentStopping();
    //	}

    //	protected bool AgentStopping()
    //	{
    //		return !base.gameObject.activeSelf || this.agent.remainingDistance <= this.agent.stoppingDistance;
    //	}

    //	public bool AgentInJudgeStopPos()
    //	{
    //		return !base.gameObject.activeSelf || this.agent.remainingDistance < this.JudgeStopDistance;
    //	}

    //	public float GetRadius()
    //	{
    //		float result = 1f;
    //		if (this.agent != null)
    //		{
    //			result = this.agent.radius;
    //		}
    //		else if (this.charCtrller != null)
    //		{
    //			result = this.charCtrller.radius;
    //		}
    //		else if (base.GetComponent<Collider>() != null)
    //		{
    //			SphereCollider component = base.GetComponent<SphereCollider>();
    //			if (component != null)
    //			{
    //				result = component.radius;
    //			}
    //			else
    //			{
    //				CapsuleCollider component2 = base.GetComponent<CapsuleCollider>();
    //				if (component2 != null)
    //				{
    //					result = component2.radius;
    //				}
    //				else
    //				{
    //					result = base.GetComponent<Collider>().bounds.extents.magnitude;
    //				}
    //			}
    //		}
    //		return result;
    //	}

    //	private void NavAgentMove()
    //	{
    //		if (!float.IsNaN(this.animator.deltaPosition.x) && Time.deltaTime != 0f)
    //		{
    //			this.agent.velocity = this.animator.deltaPosition / Time.deltaTime;
    //		}
    //		else
    //		{
    //			this.agent.velocity = Vector3.zero;
    //		}
    //		if (this.agent.desiredVelocity.magnitude > 0f)
    //		{
    //			float num = (-Vector3.Dot(base.transform.forward, this.agent.desiredVelocity) + 2f) * 2f;
    //			base.transform.forward = Vector3.Lerp(base.transform.forward, this.agent.desiredVelocity, Time.deltaTime * 7.1f * num);
    //		}
    //	}

    public void AgentUpdate()
    {
        if (this.CanFall())
        {
            this.Fall();
        }

        //if (this.IsInSky && this.locomotion.ZhiKongSpeedVec != Vector3.zero)
        //{
        //    float num = Vector3.Angle(base.transform.forward, this.locomotion.ZhiKongSpeedVec);
        //    if (num > 1f)
        //    {
        //        base.transform.forward = Vector3.Lerp(base.transform.forward, this.locomotion.ZhiKongSpeedVec, this.JumpTurnBodySpeed * Time.deltaTime);
        //    }
        //}
    }

    //	[DebuggerHidden]
    //	private IEnumerator TurnBodyOnJump()
    //	{
    //		Agent.<TurnBodyOnJump>c__Iterator38 <TurnBodyOnJump>c__Iterator = new Agent.<TurnBodyOnJump>c__Iterator38();
    //		<TurnBodyOnJump>c__Iterator.<>f__this = this;
    //		return <TurnBodyOnJump>c__Iterator;
    //	}

    //	[DebuggerHidden]
    //	private IEnumerator slowDownVel()
    //	{
    //		Agent.<slowDownVel>c__Iterator39 <slowDownVel>c__Iterator = new Agent.<slowDownVel>c__Iterator39();
    //		<slowDownVel>c__Iterator.<>f__this = this;
    //		return <slowDownVel>c__Iterator;
    //	}

    //	public void SlowDownVel()
    //	{
    //		this.CanSlowByKeyUp = false;
    //		this.bSlow = true;
    //		base.StartCoroutine(this.slowDownVel());
    //	}

    public bool CanFall()
    {
        if (!this.IsInSky && this.charCtrller != null && !this.charCtrller.isGrounded && this.charCtrller.velocity.y < -this.XiaLuoSpeed)
        {
            AnimatorStateInfo currentAnimatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
            if (!currentAnimatorStateInfo.IsName("yidongState.TiaoQi") && !currentAnimatorStateInfo.IsName("yidongState.ZhiKong") && !currentAnimatorStateInfo.IsName("yidongState.ZhuoDi"))
            {
                return true;
            }
        }
        return false;
    }

    public void Fall()
    {
        this.locomotion.firstShang = false;
        this.IsInSky = true;
        //UIManager.Instance.DoNotOpenMainMenu = true;
        //this.animatorMoveClient = base.gameObject.GetComponent<AnimatorMoveClient>();
        //if (this.animatorMoveClient == null)
        //{
        //    this.animatorMoveClient = base.gameObject.AddComponent<AnimatorMoveClient>();
        //}
        //AnimatorMoveClient expr_57 = this.animatorMoveClient;
        //expr_57.apply = (AnimatorMoveClient.AnimatorMoveApplyFunc)Delegate.Remove(expr_57.apply, new AnimatorMoveClient.AnimatorMoveApplyFunc(this.JumpProcessMove));
        //AnimatorMoveClient expr_7E = this.animatorMoveClient;
        //expr_7E.apply = (AnimatorMoveClient.AnimatorMoveApplyFunc)Delegate.Combine(expr_7E.apply, new AnimatorMoveClient.AnimatorMoveApplyFunc(this.JumpProcessMove));
        this.VerticalSpeed = -2f;
        this.animator.SetFloat("VerticalSpeed", this.VerticalSpeed);
       // this.animCtrl.ActiveAnimCrossFade("ZhiKong", false, 0.1f, true);
        this.locomotion.ZhiKongSpeedVec = Vector3.zero;
        this.CanSecondJump = true;
        this.CanSmallMove = false;
        this.CanSlowByKeyUp = false;
        this.bSlow = false;
    }

    //	public virtual void OnJumpEvent(float JumpSpeedK)
    //	{
    //		AnimatorStateInfo currentAnimatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
    //		if (currentAnimatorStateInfo.IsName("yidongState.TiaoQi"))
    //		{
    //			if (this.IsInSky)
    //			{
    //				return;
    //			}
    //		}
    //		else if (currentAnimatorStateInfo.IsName("yidongState.TiaoQi") && !this.CanSecondJump)
    //		{
    //			return;
    //		}
    //		if (JumpSpeedK < 0.001f)
    //		{
    //			JumpSpeedK = 1f;
    //		}
    //		this.locomotion.firstShang = false;
    //		float num = (!this.CanSecondJump) ? this.jumpPowerSecond : this.jumpPower;
    //		this.VerticalSpeed = JumpSpeedK * this.JumpSpeed * num;
    //		this.animatorMoveClient = base.gameObject.GetComponent<AnimatorMoveClient>();
    //		if (this.animatorMoveClient == null)
    //		{
    //			this.animatorMoveClient = base.gameObject.AddComponent<AnimatorMoveClient>();
    //		}
    //		AnimatorMoveClient expr_D0 = this.animatorMoveClient;
    //		expr_D0.apply = (AnimatorMoveClient.AnimatorMoveApplyFunc)Delegate.Remove(expr_D0.apply, new AnimatorMoveClient.AnimatorMoveApplyFunc(this.JumpProcessMove));
    //		AnimatorMoveClient expr_F7 = this.animatorMoveClient;
    //		expr_F7.apply = (AnimatorMoveClient.AnimatorMoveApplyFunc)Delegate.Combine(expr_F7.apply, new AnimatorMoveClient.AnimatorMoveApplyFunc(this.JumpProcessMove));
    //		this.animator.SetFloat("VerticalSpeed", this.VerticalSpeed);
    //		Vector3 curMoveDir = PlayerCtrlManager.GetCurMoveDir();
    //		this.locomotion.ZhiKongSpeedVec = curMoveDir.normalized;
    //		this.locomotion.ZhiKongSpeedVec.y = 0f;
    //		this.locomotion.ZhiKongSpeedVec *= this.locomotion.ZhiKongMag;
    //		if (this.locomotion.ZhiKongSpeedVec == Vector3.zero)
    //		{
    //			this.CanSmallMove = true;
    //		}
    //		else
    //		{
    //			this.CanSmallMove = false;
    //			this.CanSlowByKeyUp = true;
    //		}
    //		if (!this.CanSecondJump)
    //		{
    //			this.locomotion.ZhiKongSpeedVec *= this.SecondJumpDegree;
    //		}
    //		this.bSlow = false;
    //	}

    //	public virtual void OnJumpStart()
    //	{
    //		AnimEvent expr_06 = this.animEvent;
    //		expr_06.OnProcessFun = (AnimEvent.void_fun)Delegate.Remove(expr_06.OnProcessFun, new AnimEvent.void_fun(this.JumpProcess));
    //		AnimEvent expr_2E = this.animEvent;
    //		expr_2E.OnProcessFun = (AnimEvent.void_fun)Delegate.Combine(expr_2E.OnProcessFun, new AnimEvent.void_fun(this.JumpProcess));
    //		this.orgColliderHeight = this.charCtrller.height;
    //		this.orgHeight = this.charCtrller.center.y;
    //		this.CanSecondJump = true;
    //		this.bJumpStart = true;
    //	}

    //	public virtual void JumpProcess()
    //	{
    //		float @float = this.animator.GetFloat("Height");
    //		float float2 = this.animator.GetFloat("ColliderHeight");
    //		this.charCtrller.height = this.orgColliderHeight * float2;
    //		this.tempCtrlCenter.y = this.orgHeight + @float;
    //		this.charCtrller.center = this.tempCtrlCenter;
    //	}

    //	public virtual void OnJumpOver()
    //	{
    //		AnimEvent expr_06 = this.animEvent;
    //		expr_06.OnProcessFun = (AnimEvent.void_fun)Delegate.Remove(expr_06.OnProcessFun, new AnimEvent.void_fun(this.JumpProcess));
    //		this.charCtrller.height = this.orgColliderHeight;
    //		this.tempCtrlCenter.y = this.orgHeight;
    //		this.charCtrller.center = this.tempCtrlCenter;
    //		this.CanSecondJump = false;
    //		this.bJumpStart = false;
    //	}

    //	private void JumpProcessMove()
    //	{
    //		AnimatorStateInfo currentAnimatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
    //		AnimatorStateInfo nextAnimatorStateInfo = this.animator.GetNextAnimatorStateInfo(0);
    //		float num = -Physics.gravity.y * this.gravityK;
    //		if (!this.charCtrller.isGrounded)
    //		{
    //			this.animator.SetFloat("VerticalSpeed", this.VerticalSpeed);
    //		}
    //		if (!this.IsInSky && currentAnimatorStateInfo.IsName("yidongState.ZhiKong"))
    //		{
    //			this.IsInSky = true;
    //		}
    //		if (((this.IsInSky && !this.charCtrller.isGrounded && !this.animator.GetBool("ZhuoDi")) || (this.IsInSky && this.charCtrller.isGrounded)) && !nextAnimatorStateInfo.IsName("yidongState.ZhuoDi") && !currentAnimatorStateInfo.IsName("yidongState.ZhuoDi"))
    //		{
    //			this.IsUsedInSky = true;
    //			RaycastHit raycastHit;
    //			if (this.charCtrller.velocity.y < 0f && Physics.Raycast(base.transform.position, Vector3.down, out raycastHit, float.PositiveInfinity, Cutscene.MaskValue) && raycastHit.distance < this.ZhuoDiDistance)
    //			{
    //				this.JumpFinish();
    //			}
    //		}
    //		if (this.IsUsedInSky && this.charCtrller.isGrounded && !currentAnimatorStateInfo.IsName("yidongState.TiaoQi4") && !nextAnimatorStateInfo.IsName("yidongState.TiaoQi4"))
    //		{
    //			this.JumpFinish();
    //		}
    //		Vector3 motion = base.transform.forward;
    //		SlideDown instance = SlideDown.Instance;
    //		if (instance != null && instance.enabled)
    //		{
    //			Vector3 normalized = this.locomotion.ZhiKongSpeedVec.normalized;
    //			float magnitude = this.locomotion.ZhiKongSpeedVec.magnitude;
    //			this.locomotion.ZhiKongSpeedVec = normalized * magnitude * this.JumpMoveRatioWhenSlide;
    //		}
    //		motion = this.locomotion.ZhiKongSpeedVec * Time.deltaTime;
    //		base.transform.rotation = this.animator.rootRotation;
    //		this.VerticalSpeed += -num * Time.deltaTime;
    //		motion.y = this.VerticalSpeed * Time.deltaTime;
    //		if (this.charCtrller.Move(motion) == CollisionFlags.Below)
    //		{
    //			this.VerticalSpeed = 0f;
    //		}
    //	}

    //	private void JumpFinish()
    //	{
    //		this.IsUsedInSky = false;
    //		this.IsInSky = false;
    //		this.CanSlowByKeyUp = false;
    //		bool @bool = this.animator.GetBool("Move");
    //		if (@bool)
    //		{
    //			this.animator.CrossFade("ZhuoDi", this.CrossZhuoDiTime);
    //		}
    //		else
    //		{
    //			this.animator.CrossFade("ZhuoDi", 0.025f);
    //		}
    //		this.ZhuoDiTime = 100f;
    //		this.locomotion.ZhiKongSpeedVec = Vector3.zero;
    //		if (this.bJumpStart)
    //		{
    //			this.OnJumpOver();
    //		}
    //		UnityEngine.Object.DestroyImmediate(this.animatorMoveClient);
    //		this.IsJump = false;
    //		SlideDown orAddComponent = base.gameObject.GetOrAddComponent<SlideDown>();
    //		orAddComponent.enabled = true;
    //		SneakAttack.canQiXi = true;
    //		LateSetDoNotOpenMainMenu.Instance.Set(false, 0.4f);
    //		this.palNPC.footMark.OnJumpFinish();
    //	}

    //	private void OnDestroy()
    //	{
    //		this.animator = null;
    //	}

    //	private void OnEnable()
    //	{
    //		if (this.palNPC != null && this.palNPC.gameObject.IsMonster())
    //		{
    //			Interact component = this.palNPC.GetComponent<Interact>();
    //			if (component != null)
    //			{
    //				component.SendMessageToBehaviour("stroll", "Start");
    //			}
    //			this.palNPC.gameObject.SetEnableStroll(true);
    //		}
    //	}

    //	private void OnDisable()
    //	{
    //		if (this.palNPC != null && this.palNPC.gameObject.IsMonster())
    //		{
    //			this.DeActiveDoMove(false);
    //			Interact component = this.palNPC.GetComponent<Interact>();
    //			if (component != null)
    //			{
    //				component.SendMessageToBehaviour("stroll", "Pause");
    //				component.SendMessageToBehaviour("Pursue", "Pause");
    //			}
    //			this.palNPC.gameObject.SetEnableStroll(false);
    //		}
    //	}
}
