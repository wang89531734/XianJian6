using SoftStar.Pal6;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerCtrlManager
{
	private enum PlayerControlModel
	{
		None,
		Keyboard,
		Mouse1,
		Mouse2,
		Auto
	}

    public delegate void void_fun_Agent(Agent agentObj);

    private static bool m_bControl = true;

	public static bool bCanTab = true;

	public static bool bCtrlOther = false;

	public static bool CanChangeState = false;

	protected static Transform m_MainCam;

	public static bool UseSunddenlyTurn = true;

	private static int MoveID = 0;

	private static Quaternion LastRot;

	public static Vector3 LastForward;

	private static Vector3 LastStrickDirection;

	private static Vector3 LastPosition;

	private static Vector3 moveDirection;

	private static float speed = 0f;

	private static Vector3 TempForward;

    private static KeyDirection JumpKeyDir = KeyDirection.NONE;

    public static int maskValue = 0;

	public static bool CanJumpSecond = true;

    private static Agent m_agentObj;

    public static Transform PlayerModelTF = null;

    private static Agent lastAgentObj = null;

    public static PlayerCtrlManager.void_fun_Agent ProcessSpaceKey = null;

    public static Vector3 JumpPosition = Vector3.zero;

	private static float lookAtWeight = 0f;

	private static bool RRoundUp = false;

	public static bool bAutoWalk = false;

	private static float XiuXianDelay = 15f;

	private static float MouseLeftDownTime = 0f;

	private static float MouseRightDownTime = 0f;

	private static bool MouseLeftDown = false;

	private static bool MouseLeftMove = false;

	private static Vector3 MouseLeftDownPos = Vector3.zero;

	private static Vector3 PlayerScenePos = Vector3.zero;

	private static Vector3 tempDirection = Vector3.zero;

	private static float angle = 0f;

	private static Vector3 cross = Vector3.up;

	private static Quaternion referentialShift = Quaternion.identity;

	private static float ResetHeight = 0f;

	private static bool ResetSpecil = false;

	private static Vector3 ResetSepcilPos = Vector3.zero;

	private static PlayerCtrlManager.PlayerControlModel curControlModel = PlayerCtrlManager.PlayerControlModel.None;

	public static bool bControl
	{
		get
		{
			return PlayerCtrlManager.m_bControl;
		}
		set
		{
			if (PlayerCtrlManager.m_bControl != value)
			{
				PlayerCtrlManager.m_bControl = value;
                if (PlayerCtrlManager.m_bControl)
                {
                    if (PlayerCtrlManager.agentObj != null)
                    {
                        PlayerCtrlManager.agentObj.curCtrlMode = ControlMode.ControlByPlayer;
                        PlayerCtrlManager.agentObj.IsInSky = false;
                    }
                }
                else if (PlayerCtrlManager.agentObj != null && PlayerCtrlManager.agentObj.animator != null)
                {
                    PlayerCtrlManager.agentObj.animator.SetBool("YuanDiZou", false);
                }
            }
		}
	}

	protected static Transform MainCam
	{
		get
		{
			if (PlayerCtrlManager.m_MainCam == null)
			{
				PlayerCtrlManager.m_MainCam = Camera.main.transform;
			}
			return PlayerCtrlManager.m_MainCam;
		}
	}

    private static CharacterController charCtrler
    {
        get
        {
            return PlayerCtrlManager.agentObj.GetComponent<CharacterController>();
        }
    }

    public static Agent agentObj
    {
        get
        {
            if (PlayerCtrlManager.m_agentObj == null && PlayersManager.Player != null)
            {
                GameObject modelObj = PlayersManager.Player.GetModelObj(false);
                PlayerCtrlManager.m_agentObj = modelObj.GetComponent<Agent>();

                if (PlayerCtrlManager.m_agentObj)
                {
                    PlayerCtrlManager.PlayerModelTF = PlayerCtrlManager.m_agentObj.transform;
                    PlayerCtrlManager.SetAgentProperty(PlayerCtrlManager.m_agentObj);
                }
            }
            return PlayerCtrlManager.m_agentObj;
        }
    }

    private static PlayerCtrlManager.PlayerControlModel CurControlModel
	{
		get
		{
			return PlayerCtrlManager.curControlModel;
		}
		set
		{
			if (PlayerCtrlManager.curControlModel == PlayerCtrlManager.PlayerControlModel.Mouse1 && value != PlayerCtrlManager.PlayerControlModel.Mouse1)
			{
				//SmoothFollow2 component = Camera.main.GetComponent<SmoothFollow2>();
				//if (component != null)
				//{
				//	component.bControl = true;
				//}
				//CursorScriptTemp.CursorShow = new CursorScriptTemp.FunHander(CursorScriptTemp.Instance.ProcessCursorShow);
				//CursorScriptTemp.Instance.CursorTexToNormal();
			}
			PlayerCtrlManager.curControlModel = value;
		}
	}

    private static void SetAgentProperty(Agent agent)
    {
        agent.curCtrlMode = ControlMode.ControlByPlayer;

        if (agent.charCtrller == null)
        {
            agent.charCtrller = agent.GetComponent<CharacterController>();
        }

        if (agent.charCtrller != null && !agent.charCtrller.enabled)
        {
            agent.charCtrller.enabled = true;
            agent.charCtrller.detectCollisions = true;
        }
        //agent.gameObject.layer = 8;
        Animator animator = agent.animator;
        if (animator != null)
        {      
            animator.Play("ZhanLi");
            animator.SetFloat("Speed", 0f);
            animator.SetBool("Move", false);
            animator.speed = 1f;
        }
        agent.IsJump = false;
        SmoothFollow2 orAddComponent = PalMain.MainCamera.GetOrAddComponent<SmoothFollow2>();
        orAddComponent.Init(agent.gameObject);
        orAddComponent.InNormal();
        //if (agent.name == "YueJinChao" && animator != null)
        //{
        //    float layerWeight = animator.GetLayerWeight(1);
        //    if (layerWeight < 0.5f && agent.palNPC != null)
        //    {
        //        List<GameObject> weapons = agent.palNPC.Weapons;
        //        foreach (GameObject current in weapons)
        //        {
        //            if (!(current == null))
        //            {
        //                UtilFun.YueJinChaoShenSuo(current.transform, Vector3.zero);
        //            }
        //        }
        //    }
        //}
    }

    public static void Reset()
	{
        PlayerCtrlManager.m_agentObj = null;
    }

	public static void SetCtrlModel(GameObject go)
	{
		if (go == null)
		{
			return;
		}
        //go = go.GetModelObj(false);
        //Agent component = go.GetComponent<Agent>();
        //if (component == null)
        //{
        //	return;
        //}
        PlayerCtrlManager.lastAgentObj = PlayerCtrlManager.m_agentObj;
        //PlayerCtrlManager.m_agentObj = component;
        //PlayerCtrlManager.m_agentObj.curCtrlMode = ControlMode.ControlByPlayer;
        //PlayerCtrlManager.m_agentObj.animator.SetApplyRootMotion(true);
        //SmoothFollow2 smoothFollow = Camera.main.GetComponent<SmoothFollow2>();
        //if (smoothFollow == null)
        //{
        //	smoothFollow = Camera.main.gameObject.AddComponent<SmoothFollow2>();
        //}
        //smoothFollow.Init(component.gameObject);
        //PlayersManager.curCtrlModel = go;
        //UIManager.Instance.DoNotOpenMainMenu = true;
    }

	public static void RestoreModel()
	{
		//if (PlayerCtrlManager.lastAgentObj == null)
		//{
		//	UnityEngine.Debug.LogError("lastAgentObj == null");
		//	return;
		//}
		//if (PlayerCtrlManager.m_agentObj != null)
		//{
		//	PlayerCtrlManager.m_agentObj.curCtrlMode = ControlMode.ControlByAgent;
		//}
		//else
		//{
		//	UnityEngine.Debug.LogError("RestoreModel恢复主角控制时m_agentObj==null");
		//}
		//if (PlayerCtrlManager.lastAgentObj != null)
		//{
		//	PlayerCtrlManager.Reset();
		//}
		//else
		//{
		//	UnityEngine.Debug.LogError("RestoreModel恢复主角控制时lastAgentObj==null");
		//}
		//UIManager.Instance.DoNotOpenMainMenu = false;
	}

	public static void Initialize()
	{
        PalMain.GameMain.updateHandles += new PalMain.void_func_float_float(PlayerCtrlManager.Update);
        PlayerCtrlManager.MoveID = Animator.StringToHash("Move");
        //SaveManager.OnLoadOver -= new SaveManager.void_fun(PlayerCtrlManager.LoadOver);
        //SaveManager.OnLoadOver += new SaveManager.void_fun(PlayerCtrlManager.LoadOver);
        PlayerCtrlManager.maskValue = 393220;
		PlayerCtrlManager.maskValue = ~PlayerCtrlManager.maskValue;
	}

	public static void OnInit()
    {
        PlayerCtrlManager.SetJumpEvent(true);
		PlayerCtrlManager.bControl = true;
        if (PlayerCtrlManager.agentObj != null)
        {
            PlayerCtrlManager.SetAgentProperty(PlayerCtrlManager.agentObj);
        }
        SmoothFollow2[] componentsInChildren = PalMain.MainCamera.GetComponentsInChildren<SmoothFollow2>(true);
        for (int i = 0; i < componentsInChildren.Length; i++)
        {
            componentsInChildren[i].enabled = true;
        }
        PlayerCtrlManager.bCanTab = true;
		PlayerCtrlManager.bCtrlOther = false;
	}

	public static void OnExit()
	{
		//if (GameStateManager.NextGameState != GameState.SmallGame)
		//{
		//	PlayerCtrlManager.bControl = false;
		//	if (PlayerCtrlManager.agentObj != null && PlayerCtrlManager.agentObj.animator != null)
		//	{
		//		PlayerCtrlManager.agentObj.animator.SetBool("Move", false);
		//		if (PlayerCtrlManager.agentObj.animCtrl == null)
		//		{
		//			UnityEngine.Debug.LogError("Error : " + PlayerCtrlManager.agentObj.name + " animCtrl==null 现在补上");
		//			PlayerCtrlManager.agentObj.animCtrl = PlayerCtrlManager.agentObj.gameObject.GetOrAddComponent<AnimCtrlScript>();
		//		}
		//		PlayerCtrlManager.agentObj.animCtrl.ActiveAnim("ZhanLi", false, false, false);
		//		PlayerCtrlManager.agentObj.animCtrl.ActiveZhanDou(false, 1, true, true, false);
		//		AnimatorValueRestore.ActiveSet(PlayerCtrlManager.agentObj.animator, "Speed", 0f, 0.5f, false);
		//	}
		//}
		//if (GameStateManager.NextGameState == GameState.MainGUI && PlayerCtrlManager.agentObj != null && PlayerCtrlManager.agentObj.charCtrller != null)
		//{
		//	PlayerCtrlManager.agentObj.charCtrller.enabled = false;
		//}
		//SmoothFollow2 component = Camera.main.GetComponent<SmoothFollow2>();
		//if (component != null)
		//{
		//	component.enabled = false;
		//}
	}

	public static void SetJumpEvent(bool bActive)
	{
		if (bActive)
		{
            PlayerCtrlManager.ProcessSpaceKey = (PlayerCtrlManager.void_fun_Agent)Delegate.Remove(PlayerCtrlManager.ProcessSpaceKey, new PlayerCtrlManager.void_fun_Agent(PlayerCtrlManager.JumpEvent));
            PlayerCtrlManager.ProcessSpaceKey = (PlayerCtrlManager.void_fun_Agent)Delegate.Combine(PlayerCtrlManager.ProcessSpaceKey, new PlayerCtrlManager.void_fun_Agent(PlayerCtrlManager.JumpEvent));
        }
		else
		{
            PlayerCtrlManager.ProcessSpaceKey = (PlayerCtrlManager.void_fun_Agent)Delegate.Remove(PlayerCtrlManager.ProcessSpaceKey, new PlayerCtrlManager.void_fun_Agent(PlayerCtrlManager.JumpEvent));
        }
	}

    public static void JumpEvent(Agent agentObj)
    {
        UnityEngine.Debug.Log("跳起");
        if (PlayerCtrlManager.bCtrlOther)
        {
            return;
        }
        Animator animator = agentObj.animator;
        Locomotion locomotion = agentObj.locomotion;
        //AnimatorStateInfo currentAnimatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        //AnimatorStateInfo nextAnimatorStateInfo = animator.GetNextAnimatorStateInfo(0);
        //if (currentAnimatorStateInfo.IsName("yidongState.ZhanLi") || currentAnimatorStateInfo.IsName("yidongState.YiDong") || currentAnimatorStateInfo.IsName("yidongState.ZhuoDi") || currentAnimatorStateInfo.IsName("yidongState.ZhuoDiPao") || currentAnimatorStateInfo.IsName("yidongState.XiuXian") || currentAnimatorStateInfo.IsName("yidongState.YuanDiZou"))
        //{
        //    UIManager.Instance.DoNotOpenMainMenu = true;
        //    agentObj.IsJump = true;
        //    agentObj.animCtrl.ActiveAnimCrossFade("TiaoQi", false, 0.03f, true);
        //    animator.SetFloat("VerticalSpeed", 10f);
        //    locomotion.firstShang = true;
        //    agentObj.CanSecondJump = true;
        //    bool flag = InputManager.GetDir() != Vector3.zero || PlayerCtrlManager.CurControlModel == PlayerCtrlManager.PlayerControlModel.Auto || PlayerCtrlManager.CurControlModel == PlayerCtrlManager.PlayerControlModel.Mouse1 || PlayerCtrlManager.CurControlModel == PlayerCtrlManager.PlayerControlModel.Mouse2;
        //    locomotion.ZhiKongMag = ((!flag) ? 0f : agentObj.AniRunSpeed);
        //    PlayerCtrlManager.JumpPosition = agentObj.transform.position;
        //}
    }

    private static void Update(float curTime, float deltaTime)
    {
        if (!PlayerCtrlManager.bControl || PlayerCtrlManager.agentObj == null || !PlayerCtrlManager.agentObj.animator)
        {
            return;
        }
        Animator animator = PlayerCtrlManager.agentObj.animator;
        Locomotion locomotion = PlayerCtrlManager.agentObj.locomotion;

        if (InputManager.GetKeyDown(KEY_ACTION.MOUSE_RIGHT, false) || InputManager.GetKeyUp(KEY_ACTION.MOUSE_RIGHT, false) || InputManager.GetKeyDown(KEY_ACTION.CAMERA_LEFT, false) || InputManager.GetKeyDown(KEY_ACTION.CAMERA_RIGHT, false) || InputManager.GetKeyUp(KEY_ACTION.CAMERA_LEFT, false) || InputManager.GetKeyUp(KEY_ACTION.CAMERA_RIGHT, false))
        {
            PlayerCtrlManager.LastForward = PlayerCtrlManager.MainCam.forward;
        }

        if (Input.anyKeyDown)
        {
            //if (PlayerCtrlManager.CanChangeState && InputManager.GetKeyDown(KEY_ACTION.CHAGNESTATE, false))
            //{
            //    AnimCtrlScript component = animator.GetComponent<AnimCtrlScript>();
            //    if (component != null)
            //    {
            //        component.ActiveBattle(animator.GetLayerWeight(1) <= 0.5f);
            //    }
            //}

            //if (PlayerCtrlManager.bCanTab && InputManager.GetKeyDown(KEY_ACTION.TAB, false) && GameStateManager.CurGameState != GameState.Battle && (PlayerCtrlManager.charCtrler.isGrounded || Physics.Raycast(PlayerCtrlManager.agentObj.transform.position, Vector3.down, 0.07f)) && !PlayerCtrlManager.agentObj.IsJump)
            //{
            //    PlayersManager.TabPlayer();
            //}

            //if (InputManager.GetKeyDown(KEY_ACTION.ACTION, false) && !PlayerCtrlManager.agentObj.IsJump)
            //{
            //    PalNPC palNPC = PlayerCtrlManager.agentObj.palNPC;
            //    if (palNPC != null && palNPC.perception != null)
            //    {
            //        if (palNPC.interActs.Count < 1)
            //        {
            //            Transform transform = null;
            //            float num = 10000f;
            //            foreach (Transform current in palNPC.perception.hostsCanBeSeen)
            //            {
            //                Vector3 vector = current.gameObject.GetModelObj(false).transform.position - PlayerCtrlManager.agentObj.transform.position;
            //                if (vector.magnitude < num)
            //                {
            //                    num = vector.magnitude;
            //                    transform = current;
            //                }
            //            }
            //            if (transform != null)
            //            {
            //                Interact component2 = transform.GetComponent<Interact>();
            //                if (component2 != null)
            //                {
            //                    float num2 = PlayerCtrlManager.agentObj.ActionRadius;
            //                    MouseEnterCursor componentInChildren = component2.GetComponentInChildren<MouseEnterCursor>();
            //                    if (componentInChildren != null && CursorScriptTemp.Instance.tempTypeDic.ContainsKey(componentInChildren.curState))
            //                    {
            //                        num2 = CursorScriptTemp.Instance.tempTypeDic[componentInChildren.curState].dis;
            //                    }
            //                    if (num < num2)
            //                    {
            //                        component2.InterAct();
            //                    }
            //                }
            //            }
            //        }
            //        else
            //        {
            //            Interact.LastInteractNPC = palNPC.gameObject;
            //            palNPC.interActs[0].InterAct();
            //        }
            //    }
            //}
        }

        if (InputManager.GetKeyDown(KEY_ACTION.JUMP, false) && PlayerCtrlManager.ProcessSpaceKey != null)
        {
            //SlideDown instance = SlideDown.Instance;
            //if (instance != null)
            //{
            //    if (instance.CanJump())
            //    {
            //        SlideDown.Instance.enabled = false;
            //        PlayerCtrlManager.ProcessSpaceKey(PlayerCtrlManager.agentObj);
            //    }
            //}
            //else
            //{
            //    PlayerCtrlManager.ProcessSpaceKey(PlayerCtrlManager.agentObj);
            //}
        }

        //if (PlayerCtrlManager.agentObj.IsInSky && PlayerCtrlManager.agentObj.CanSmallMove && InputManager.curKeyDir != KeyDirection.NONE)
        //{
        //    PlayerCtrlManager.agentObj.CanSmallMove = false;
        //    Vector3 curMoveDir = PlayerCtrlManager.GetCurMoveDir();
        //    Transform transform2 = PlayerCtrlManager.agentObj.transform;
        //    locomotion.ZhiKongSpeedVec = curMoveDir.normalized;
        //    locomotion.ZhiKongSpeedVec.y = 0f;
        //    locomotion.ZhiKongSpeedVec *= PlayerCtrlManager.agentObj.SmallMoveSpeed;
        //    PlayerCtrlManager.agentObj.CanSlowByKeyUp = true;
        //}

        //if (PlayerCtrlManager.agentObj.IsInSky && PlayerCtrlManager.agentObj.CanSlowByKeyUp && InputManager.curKeyDir == KeyDirection.NONE)
        //{
        //    PlayerCtrlManager.agentObj.SlowDownVel();
        //}

        if (!InputManager.GetKey(KEY_ACTION.WALK, false))
        {
            if (PlayerCtrlManager.agentObj.CurSpeed < PlayerCtrlManager.agentObj.RunSpeed - 0.01f)
            {
                PlayerCtrlManager.agentObj.CurSpeed = Mathf.Lerp(PlayerCtrlManager.agentObj.CurSpeed, PlayerCtrlManager.agentObj.RunSpeed, PlayerCtrlManager.agentObj.dampSpeed * Time.deltaTime);
            }
            else if (PlayerCtrlManager.agentObj.CurSpeed > PlayerCtrlManager.agentObj.RunSpeed)
            {
                PlayerCtrlManager.agentObj.CurSpeed = PlayerCtrlManager.agentObj.RunSpeed;
            }
        }
        else if (PlayerCtrlManager.agentObj.CurSpeed > PlayerCtrlManager.agentObj.WalkSpeed + 0.01f)
        {
            PlayerCtrlManager.agentObj.CurSpeed = Mathf.Lerp(PlayerCtrlManager.agentObj.CurSpeed, PlayerCtrlManager.agentObj.WalkSpeed, PlayerCtrlManager.agentObj.dampSpeed * 0.7f * Time.deltaTime);
        }
        else if (PlayerCtrlManager.agentObj.CurSpeed < PlayerCtrlManager.agentObj.WalkSpeed)
        {
            PlayerCtrlManager.agentObj.CurSpeed = PlayerCtrlManager.agentObj.WalkSpeed;
        }

        if (InputManager.GetKeyDown(KEY_ACTION.MOUSE_LEFT, false))
        {
            if (PlayerCtrlManager.CurControlModel == PlayerCtrlManager.PlayerControlModel.Mouse1)
            {
                //MessageProcess.Instance.AddMess(Message.Style.EndAction, new Action(PlayerCtrlManager.OnMouseMove));
            }
            else
            {
                //MessageProcess.Instance.AddMess(Message.Style.Action, new Action(PlayerCtrlManager.OnMouseMove));
            }
        }

        if (PlayerCtrlManager.MouseLeftDown && InputManager.GetKey(KEY_ACTION.MOUSE_LEFT, false) && PlayerCtrlManager.CurControlModel != PlayerCtrlManager.PlayerControlModel.Mouse1 && PlayerCtrlManager.CurControlModel != PlayerCtrlManager.PlayerControlModel.Mouse2)
        {
            if (Vector3.SqrMagnitude(Input.mousePosition - PlayerCtrlManager.MouseLeftDownPos) > 4f)
            {
                PlayerCtrlManager.MouseLeftMove = true;
            }

            //if (!PlayerCtrlManager.MouseLeftMove && Time.time - PlayerCtrlManager.MouseLeftDownTime > CursorScriptTemp.Instance.followCursorTime)
            //{
            //    SmoothFollow2 component3 = Camera.main.GetComponent<SmoothFollow2>();
            //    if (component3 != null)
            //    {
            //        component3.bControl = false;
            //    }
            //    PlayerCtrlManager.CurControlModel = PlayerCtrlManager.PlayerControlModel.Mouse1;
            //    CursorScriptTemp.CursorShow = null;
            //    CursorScriptTemp.Instance.CursorTexToState(CursorTextureState.FollowCursor, -1f);
            //    PalMain.Instance.StartCoroutine(PlayerCtrlManager.DelayShow());
            //}
        }

        //if (InputManager.GetKeyUp(KEY_ACTION.MOUSE_LEFT, false))
        //{
        //    UnityEngine.Debug.Log("MouseLeftDown = false");
        //    PlayerCtrlManager.MouseLeftDown = false;
        //    if (PlayerCtrlManager.curControlModel == PlayerCtrlManager.PlayerControlModel.Mouse1 || PlayerCtrlManager.curControlModel == PlayerCtrlManager.PlayerControlModel.Mouse2)
        //    {
        //        MessageProcess.Instance.AddMess(Message.Style.EndAction, new Action(PlayerCtrlManager.OnMouseUp));
        //    }
        //}

        //if (InputManager.GetKeyDown(KEY_ACTION.AUTO_WALK, false))
        //{
        //    if (PlayerCtrlManager.CurControlModel != PlayerCtrlManager.PlayerControlModel.Auto)
        //    {
        //        PlayerCtrlManager.CurControlModel = PlayerCtrlManager.PlayerControlModel.Auto;
        //    }
        //    else
        //    {
        //        PlayerCtrlManager.CurControlModel = PlayerCtrlManager.PlayerControlModel.None;
        //    }
        //}

        if (InputManager.GetKey(KEY_ACTION.UP, false) || InputManager.GetKey(KEY_ACTION.DOWN, false) || InputManager.GetKey(KEY_ACTION.LEFT, false) || InputManager.GetKey(KEY_ACTION.RIGHT, false) || InputManager.GetKey(KEY_ACTION.UPARROW, false) || InputManager.GetKey(KEY_ACTION.DOWNARROW, false) || InputManager.GetKey(KEY_ACTION.LEFTARROW, false) || InputManager.GetKey(KEY_ACTION.RIGHTARROW, false))
        {
            PlayerCtrlManager.CurControlModel = PlayerCtrlManager.PlayerControlModel.Keyboard;
        }
        else if (PlayerCtrlManager.MouseLeftDown && InputManager.GetKey(KEY_ACTION.MOUSE_LEFT, false) && InputManager.GetKey(KEY_ACTION.MOUSE_RIGHT, false))
        {
            PlayerCtrlManager.CurControlModel = PlayerCtrlManager.PlayerControlModel.Mouse2;
        }
        else if (PlayerCtrlManager.CurControlModel != PlayerCtrlManager.PlayerControlModel.Auto && PlayerCtrlManager.CurControlModel != PlayerCtrlManager.PlayerControlModel.Mouse1)
        {
            PlayerCtrlManager.CurControlModel = PlayerCtrlManager.PlayerControlModel.None;
        }

        TurnHead2 component4 = PlayersManager.Player.GetModelObj(false).GetComponent<TurnHead2>();
        if (PlayerCtrlManager.CurControlModel == PlayerCtrlManager.PlayerControlModel.Keyboard || PlayerCtrlManager.CurControlModel == PlayerCtrlManager.PlayerControlModel.Mouse2 || PlayerCtrlManager.CurControlModel == PlayerCtrlManager.PlayerControlModel.Mouse1 || PlayerCtrlManager.CurControlModel == PlayerCtrlManager.PlayerControlModel.Auto)
        {
            Vector3 forward = animator.transform.forward;
            forward.y = 0f;
            Vector3 dir = InputManager.GetDir();
            if (PlayerCtrlManager.CurControlModel == PlayerCtrlManager.PlayerControlModel.Auto)
            {
                dir.z = 1f;
            }
            else if (PlayerCtrlManager.CurControlModel == PlayerCtrlManager.PlayerControlModel.Mouse2)
            {
                dir.z = 1f;
            }
            else if (PlayerCtrlManager.CurControlModel == PlayerCtrlManager.PlayerControlModel.Mouse1)
            {
                PlayerCtrlManager.PlayerScenePos = Camera.main.WorldToScreenPoint(PlayerCtrlManager.agentObj.transform.position);
                PlayerCtrlManager.PlayerScenePos.z = 0f;
                PlayerCtrlManager.tempDirection = Input.mousePosition - PlayerCtrlManager.PlayerScenePos;
                PlayerCtrlManager.tempDirection.Normalize();
                if (PlayerCtrlManager.PlayerScenePos.y < 0f && Input.mousePosition.y < 15f)
                {
                    PlayerCtrlManager.tempDirection.y = -1f;
                }
                dir.x = PlayerCtrlManager.tempDirection.x;
                dir.z = PlayerCtrlManager.tempDirection.y;
            }

            if (dir != Vector3.zero)
            {
                if (!animator.GetBool(PlayerCtrlManager.MoveID))
                {
                    animator.SetBool(PlayerCtrlManager.MoveID, true);
                }
                dir.Normalize();
                Vector3 vector2;
                if (InputManager.GetKey(KEY_ACTION.MOUSE_RIGHT, false) || InputManager.GetKey(KEY_ACTION.CAMERA_LEFT, false) || InputManager.GetKey(KEY_ACTION.CAMERA_RIGHT, false))
                {
                    vector2 = PlayerCtrlManager.MainCam.forward;
                    animator.SetBool("YuanDiZou", false);
                    PlayerCtrlManager.RRoundUp = true;
                }
                else
                {
                    vector2 = PlayerCtrlManager.LastForward;
                }
                vector2.y = 0f;
                PlayerCtrlManager.angle = Vector3.Angle(Vector3.forward, vector2);
                PlayerCtrlManager.cross = Vector3.Cross(Vector3.forward, vector2);
                if (PlayerCtrlManager.cross.y < -0.001f)
                {
                    PlayerCtrlManager.angle = 360f - PlayerCtrlManager.angle;
                }
                PlayerCtrlManager.referentialShift = Quaternion.AngleAxis(PlayerCtrlManager.angle, Vector3.up);
                PlayerCtrlManager.moveDirection = PlayerCtrlManager.referentialShift * dir;
                PlayerCtrlManager.moveDirection.y = 0f;
                if (!PlayerCtrlManager.agentObj.IsInSky)
                {
                    float num3 = Vector3.Angle(forward, PlayerCtrlManager.moveDirection);
                    PlayerCtrlManager.speed = Mathf.Lerp(PlayerCtrlManager.speed, PlayerCtrlManager.agentObj.CurSpeed, Time.deltaTime * PlayerCtrlManager.agentObj.dampSpeed);
                    float num4 = PlayerCtrlManager.speed;

                    if (PlayerCtrlManager.UseSunddenlyTurn && (!InputManager.GetKey(KEY_ACTION.MOUSE_RIGHT, false) || PlayerCtrlManager.agentObj.CurSpeed > PlayerCtrlManager.agentObj.WalkSpeed) && num3 > 135f && !PlayerCtrlManager.agentObj.IsInSky)
                    {
                        PlayerCtrlManager.agentObj.transform.forward = PlayerCtrlManager.moveDirection;
                        num3 = 0f;
                    }

                    if (PlayerCtrlManager.agentObj.CurSpeed <= PlayerCtrlManager.agentObj.WalkSpeed && InputManager.GetKey(KEY_ACTION.MOUSE_RIGHT, false) && num3 > 100f && num4 > 0f)
                    {
                        num4 *= -1f;
                    }

                    if (Vector3.Cross(forward, PlayerCtrlManager.moveDirection).y < 0f)
                    {
                        num3 *= -1f;
                    }
                    locomotion.Do(num4, num3, PlayerCtrlManager.agentObj.transform, PlayerCtrlManager.moveDirection);
                }
            }
            else
            {
                PlayerCtrlManager.CurControlModel = PlayerCtrlManager.PlayerControlModel.None;
            }
        }

        if (PlayerCtrlManager.CurControlModel == PlayerCtrlManager.PlayerControlModel.None)
        {
            //float @float = animator.GetFloat("Speed");
            //if (Mathf.Abs(@float) > 0.01f)
            //{
            //    PlayerCtrlManager.speed = 0f;
            //    locomotion.Do(0f, 0f, PlayerCtrlManager.agentObj.transform, PlayerCtrlManager.moveDirection);
            //}
            //else
            //{
            //    animator.SetFloat("Speed", 0f);
            //    bool @bool = animator.GetBool(PlayerCtrlManager.MoveID);
            //    if (@bool)
            //    {
            //        animator.SetBool(PlayerCtrlManager.MoveID, false);
            //    }
            //}
            //if ((InputManager.GetKey(KEY_ACTION.MOUSE_RIGHT, false) || InputManager.GetKey(KEY_ACTION.CAMERA_LEFT, false) || InputManager.GetKey(KEY_ACTION.CAMERA_RIGHT, false)) && !PlayerCtrlManager.agentObj.IsJump)
            //{
            //    if (PlayerCtrlManager.RRoundUp)
            //    {
            //        PlayerCtrlManager.RRoundUp = false;
            //    }
            //    PlayerCtrlManager.TempForward = PlayerCtrlManager.MainCam.forward;
            //    PlayerCtrlManager.TempForward.y = 0f;
            //    Vector3 forward2 = PlayerCtrlManager.agentObj.transform.forward;
            //    forward2.y = 0f;
            //    float num5 = Vector3.Angle(PlayerCtrlManager.TempForward, forward2);
            //    if (num5 > 0.4f)
            //    {
            //        animator.SetBool("YuanDiZou", true);
            //        num5 = ((num5 <= 100f) ? num5 : 100f);
            //        PlayerCtrlManager.lookAtWeight = num5 / 100f;
            //        if (Vector3.Cross(forward2, PlayerCtrlManager.TempForward).y < 0f)
            //        {
            //            num5 = -num5;
            //        }
            //        Transform transform3 = PlayerCtrlManager.agentObj.transform;
            //        Transform transform4 = GameObjectPath.GetEyeObjs(transform3)[0];
            //        float num6 = transform4.position.y - transform3.position.y;
            //        Quaternion rotation = Quaternion.AngleAxis(num5, transform3.up);
            //        Vector3 target = rotation * transform3.forward * 10f + transform3.position;
            //        target.y = transform3.position.y + num6;
            //        component4.target = target;
            //    }
            //    else if (num5 < 0.01f)
            //    {
            //        animator.SetBool("YuanDiZou", false);
            //        PlayerCtrlManager.RRoundUp = true;
            //    }
            //    PlayerCtrlManager.agentObj.transform.forward = Vector3.RotateTowards(PlayerCtrlManager.agentObj.transform.forward, PlayerCtrlManager.TempForward, deltaTime * locomotion.ORotSpeed, deltaTime * locomotion.ORotSpeed);
            //}
            //else if (!PlayerCtrlManager.agentObj.IsInSky)
            //{
            //    if (PlayerCtrlManager.XiuXianDelay <= 0f)
            //    {
            //        float layerWeight = animator.GetLayerWeight(1);
            //        if (layerWeight <= 0f)
            //        {
            //            animator.CrossFade("yidongState.XiuXian", 0.05f);
            //        }
            //        PlayerCtrlManager.XiuXianDelay = UnityEngine.Random.Range(10f, 25f);
            //    }
            //    else
            //    {
            //        PlayerCtrlManager.XiuXianDelay -= Time.deltaTime;
            //    }
            //}
            //if (InputManager.GetKeyUp(KEY_ACTION.MOUSE_RIGHT, false) || InputManager.GetKeyUp(KEY_ACTION.CAMERA_LEFT, false) || InputManager.GetKeyUp(KEY_ACTION.CAMERA_RIGHT, false))
            //{
            //    animator.SetBool("YuanDiZou", false);
            //    PlayerCtrlManager.RRoundUp = true;
            //}
            //if (PlayerCtrlManager.RRoundUp)
            //{
            //    PlayerCtrlManager.lookAtWeight -= deltaTime;
            //    if (PlayerCtrlManager.lookAtWeight <= 0f)
            //    {
            //        PlayerCtrlManager.RRoundUp = false;
            //        PlayerCtrlManager.lookAtWeight = 0f;
            //        component4.lookAtWeight = 0f;
            //    }
            //}
        }
        else
        {
            //PlayerCtrlManager.lookAtWeight = 0f;
            //component4.lookAtWeight = 0f;
            //if (PlayerCtrlManager.XiuXianDelay <= 0f)
            //{
            //    PlayerCtrlManager.XiuXianDelay = UnityEngine.Random.Range(10f, 25f);
            //}
        }

        if (PlayerCtrlManager.lookAtWeight > 0f)
        {
            //component4.lookAtWeight = PlayerCtrlManager.lookAtWeight;
        }

        //PlayerCtrlManager.agentObj.AgentUpdate();
        if (PlayerCtrlManager.agentObj.IsInSky)
        {
            //PlayerCtrlManager.CheckReset();
        }
    }

    public static void OnLevelLoaded(int level)
	{
		if (level != 11)
		{
			if (level != 19)
			{
				if (level != 27)
				{
					PlayerCtrlManager.ResetHeight = -10000f;
					PlayerCtrlManager.ResetSpecil = false;
				}
				else
				{
					PlayerCtrlManager.ResetHeight = 59f;
					PlayerCtrlManager.ResetSpecil = false;
				}
			}
			else
			{
				PlayerCtrlManager.ResetHeight = 270f;
				PlayerCtrlManager.ResetSpecil = false;
			}
		}
		else
		{
			PlayerCtrlManager.ResetHeight = 90f;
			PlayerCtrlManager.ResetSpecil = true;
			PlayerCtrlManager.ResetSepcilPos = new Vector3(599.7473f, 104.578f, 198.2703f);
		}
	}

	//private static void CheckReset()
	//{
	//	if (PlayerCtrlManager.agentObj.transform.position.y < PlayerCtrlManager.ResetHeight)
	//	{
	//		if (!PlayerCtrlManager.ResetSpecil)
	//		{
	//			GameObject gameObject = GameObject.Find("SceneEnter");
	//			if (gameObject != null)
	//			{
	//				SceneFall.Reset(gameObject.transform.position);
	//			}
	//		}
	//		else
	//		{
	//			SceneFall.Reset(PlayerCtrlManager.ResetSepcilPos);
	//		}
	//	}
	//}

	public static Vector3 GetCurMoveDir()
	{
		return PlayerCtrlManager.moveDirection;
	}

	public static void LoadOver()
	{
		PlayerCtrlManager.LastForward = PlayerCtrlManager.MainCam.forward;
	}

	private static void OnMouseMove()
	{
		PlayerCtrlManager.MouseLeftDownTime = Time.time;
		PlayerCtrlManager.MouseLeftDown = true;
		PlayerCtrlManager.MouseLeftMove = false;
		PlayerCtrlManager.MouseLeftDownPos = Input.mousePosition;
	}

	private static void OnMouseUp()
	{
		//if (Time.time - PlayerCtrlManager.MouseLeftDownTime < CursorScriptTemp.Instance.followCursorTime)
		//{
		//	PlayerCtrlManager.CurControlModel = PlayerCtrlManager.PlayerControlModel.None;
		//}
	}

	//[DebuggerHidden]
	//private static IEnumerator DelayShow()
	//{
	//	return new PlayerCtrlManager.<DelayShow>c__Iterator55();
	//}
}
