using Funfia.File;
using SoftStar.Pal6;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayersManager
{
    private class AfterSetPlayer : IDisposable
    {
        public void Dispose()
        {
            if (PlayersManager.PlayerIndex < 0 || PlayersManager.PlayerIndex >= PlayersManager.ActivePlayers.Count)
            {
                return;
            }
            GameObject gameObject = PlayersManager.ActivePlayers[PlayersManager.PlayerIndex];
            PalNPC obj = null;
            if (gameObject != null)
            {
                obj = gameObject.GetComponent<PalNPC>();
            }
            if (PlayersManager.OnAfterSetPlayer != null)
            {
                PlayersManager.OnAfterSetPlayer(obj);
            }
        }
    }

    public static GameObject curCtrlModel = null;

    public static List<GameObject> ActivePlayers = new List<GameObject>();

    public static List<GameObject> AllPlayers = new List<GameObject>();

    private static List<PerceptionRange> AllPlayersPerceptionRange = new List<PerceptionRange>();

    private static int PlayerIndex = 0;

    private static string PlayerTemplatePath = "/Resources/Template/Character/";

    public static Action<int> OnAddPlayer = null;

    private static Transform tempDestTF = null;

    private static int TempPlayersCount = 0;

    private static int TempPlayerIndex = 0;

    public static event Action<int> OnTabPlayer;

    public static event Action<PalNPC> OnAfterSetPlayer;

    public static event Action<int> OnRemovePlayer;

    public static GameObject Player
    {
        get
        {
            if (PlayersManager.curCtrlModel != null)
            {
                return PlayersManager.curCtrlModel;
            }
            if (PlayersManager.ActivePlayers.Count < 1)
            {
                GameObject gameObject = GameObject.FindWithTag("Player");
                if (gameObject != null)
                {
                    PlayersManager.ActivePlayers.Add(gameObject);
                }
            }
            if (PlayersManager.PlayerIndex < 0 || PlayersManager.PlayerIndex >= PlayersManager.ActivePlayers.Count)
            {
                return null;
            }
            return PlayersManager.ActivePlayers[PlayersManager.PlayerIndex];
        }
    }

    static PlayersManager()
    {
        PlayersManager.OnTabPlayer = null;
        PlayersManager.OnRemovePlayer = null;
    }

    public static void Initialize()
    {
        PlayersManager.AllPlayers.Clear();
        PlayersManager.AllPlayersPerceptionRange.Clear();
        GameObject gameObject2 = PlayersManager.FindMainChar(0, true);
        if (gameObject2 != null)
        {
            PalNPC component2 = gameObject2.GetComponent<PalNPC>();
            if (component2 == null)
            {
                Debug.Log("PlayersManager.Initialize: NPC 0 is null");
            }
            else
            {
                PalNPC expr_BF = component2;
                expr_BF.OnLoadModelEnd = (PalNPC.void_fun_TF)Delegate.Combine(expr_BF.OnLoadModelEnd, new PalNPC.void_fun_TF(PlayersManager.OnLoadModelEnd));
            }
        }

        GameObject gameObject3 = PlayersManager.FindMainChar(5, true);
        if (gameObject3 != null)
        {
            PalNPC component3 = gameObject3.GetComponent<PalNPC>();
            if (component3 == null)
            {
                Debug.Log("PlayersManager.Initialize: NPC 5 is null");
            }
            else
            {
                PalNPC expr_11D = component3;
                expr_11D.OnLoadModelEnd = (PalNPC.void_fun_TF)Delegate.Combine(expr_11D.OnLoadModelEnd, new PalNPC.void_fun_TF(PlayersManager.OnLoadModelEnd));
            }
        }

        GameObject gameObject4 = PlayersManager.FindMainChar(3, true);
        if (gameObject4 != null)
        {
            //ModelChangeScript component4 = gameObject4.GetComponent<ModelChangeScript>();
            //if (component4 != null)
            //{
            //    ModelChangeScript expr_16C = component4;
            //    expr_16C.OnSetModeEnd = (Action<PalNPC>)Delegate.Combine(expr_16C.OnSetModeEnd, new Action<PalNPC>(PlayersManager.OnSetModeEnd));
            //}
        }

        GameObject gameObject5 = PlayersManager.FindMainChar(4, true);
        if (gameObject5 != null)
        {
            PalNPC component5 = gameObject5.GetComponent<PalNPC>();
            if (component5 == null)
            {
                Debug.Log("PlayersManager.Initialize: NPC 4 is null");
            }
            else if (component5.model != null)
            {
               // Agent component6 = component5.model.GetComponent<Agent>();
               // component6.CrossZhuoDiTime = 0.12f;
            }
            else
            {
                PalNPC expr_1FB = component5;
               // expr_1FB.OnLoadModelEnd = (PalNPC.void_fun_TF)Delegate.Combine(expr_1FB.OnLoadModelEnd, new PalNPC.void_fun_TF(PlayersManager.OnLoadModelEnd));
            }
        }
        //ScenesManager.Instance.OnChangeMap -= new Action<int>(PlayersManager.OnChangeMap);
        //ScenesManager.Instance.OnChangeMap += new Action<int>(PlayersManager.OnChangeMap);
        //  FlagManager.OnSetFlag = (Action<int, int>)Delegate.Remove(FlagManager.OnSetFlag, new Action<int, int>(PlayersManager.OnSetFlag));
        // FlagManager.OnSetFlag = (Action<int, int>)Delegate.Combine(FlagManager.OnSetFlag, new Action<int, int>(PlayersManager.OnSetFlag));
    }

    //	public static void BeforeLoadData()
    //	{
    //		for (int i = 0; i < PlayersManager.AllPlayers.Count; i++)
    //		{
    //			GameObject gameObject = PlayersManager.AllPlayers[i];
    //			if (!(gameObject == null))
    //			{
    //				PalGameObjectBase component = gameObject.GetComponent<PalGameObjectBase>();
    //				if (!(component == null))
    //				{
    //					component.CurObjType = ObjType.none;
    //				}
    //			}
    //		}
    //	}

    private static void OnChangeMap(int mapIndex)
    {
        for (int i = 0; i < PlayersManager.AllPlayers.Count; i++)
        {
            GameObject gameObject = PlayersManager.AllPlayers[i];
            if (!(gameObject == null))
            {
                PalNPC component = gameObject.GetComponent<PalNPC>();
                if (!(component == null))
                {
                    //if (component.perception == null)
                    //{
                    //    Debug.LogError("Error : PlayersManager.OnChangeMap " + component.name + " perception == null");
                    //}
                    //else
                    //{
                    //    component.perception.Clear();
                    //}
                }
            }
        }
    }

    //	public static void ChangeHairShader(bool bUseAlpha)
    //	{
    //		UtilFun.ChangeHairShader(bUseAlpha, PlayersManager.AllPlayers.ToArray());
    //	}

    //	private static void OnSetModeEnd(PalNPC npc)
    //	{
    //		PlayersManager.AddNeedComponent(npc);
    //	}

    private static void OnLoadModelEnd(PalNPC npc)
    {
        //switch (npc.Data.CharacterID)
        //{
        //    case 0:
        //        FlagManager.SetFlag(6, 1, true);
        //     // SetActiveChildByFlag.Init(npc.gameObject, 6, "yanzhao");
        //        break;
        //    case 4:
        //        {
        //          //  Agent component = npc.model.GetComponent<Agent>();
        //           // component.CrossZhuoDiTime = 0.12f;
        //            break;
        //        }
        //    case 5:
        //        FlagManager.SetFlag(7, 0, true);
        //       // SetActiveChildByFlag.Init(npc.gameObject, 7, "YanZhao");
        //        break;
        //}
    }

    //	public static void Restart()
    //	{
    //		for (int i = 0; i < PlayersManager.AllPlayers.Count; i++)
    //		{
    //			if (PlayersManager.AllPlayers[i] != null && PlayersManager.AllPlayers[i].GetModelObj(false) != PlayersManager.AllPlayers[i])
    //			{
    //				AnimCtrlScript component = PlayersManager.AllPlayers[i].GetModelObj(false).GetComponent<AnimCtrlScript>();
    //				component.ActiveAnimCrossFade("ZhanLi", false, 0f, true);
    //				LateSetActive.Init(PlayersManager.AllPlayers[i].GetModelObj(false), false, 0.01f);
    //			}
    //			try
    //			{
    //				if (i < PlayersManager.AllPlayers.Count)
    //				{
    //					PalNPC component2 = PlayersManager.AllPlayers[i].GetComponent<PalNPC>();
    //					if (component2 != null && component2.Data != null)
    //					{
    //						PlayersManager.AllPlayers[i].GetComponent<PalNPC>().Data.Reset();
    //					}
    //				}
    //			}
    //			catch
    //			{
    //			}
    //		}
    //		PlayersManager.ActivePlayers.Clear();
    //		GameObject gameObject = PlayersManager.FindMainChar(3, true);
    //		if (gameObject != null)
    //		{
    //			ModelChangeScript component3 = gameObject.GetComponent<ModelChangeScript>();
    //			if (component3 != null)
    //			{
    //				component3.Reset();
    //			}
    //		}
    //	}

    //	public static void RestoreLayer(bool NeedRestorePos)
    //	{
    //		for (int i = 0; i < PlayersManager.AllPlayers.Count; i++)
    //		{
    //			GameObject gameObject = PlayersManager.AllPlayers[i];
    //			if (!(gameObject == null))
    //			{
    //				if (!gameObject.activeSelf)
    //				{
    //					gameObject.SetActive(true);
    //				}
    //				PalNPC component = gameObject.GetComponent<PalNPC>();
    //				if (component == null)
    //				{
    //					Debug.LogError("Error : " + gameObject.name + " 没有PalNPC");
    //				}
    //				else
    //				{
    //					Transform transform = component.transform;
    //					if (component.model == null)
    //					{
    //						Debug.LogError("Error : " + component.name + " 没有model");
    //					}
    //					else
    //					{
    //						Transform transform2 = component.model.transform;
    //						if (transform2.parent != transform)
    //						{
    //							transform2.parent = transform;
    //						}
    //						Footmark component2 = component.model.GetComponent<Footmark>();
    //						if (component2 != null)
    //						{
    //							component2.CurMode = Footmark.Mode.Ground;
    //						}
    //						if (NeedRestorePos)
    //						{
    //							transform2.localPosition = Vector3.zero;
    //							component.model.SetVisible(true);
    //							if (component.gameObject != PlayersManager.Player && component.model.activeSelf)
    //							{
    //								AnimCtrlScript component3 = component.model.GetComponent<AnimCtrlScript>();
    //								component3.ActiveAnimCrossFade("ZhanLi", false, 0f, true);
    //								LateSetActive.Init(component.model, false, 0.01f);
    //							}
    //						}
    //					}
    //				}
    //			}
    //		}
    //	}

    //	public static bool ShouldLoad(GameObject go)
    //	{
    //		if (go == null)
    //		{
    //			return false;
    //		}
    //		if (!PlayersManager.ActivePlayers.Contains(go))
    //		{
    //			PalNPC component = go.GetComponent<PalNPC>();
    //			return component != null && !PlayersManager.ExsitsInPlayers(component.Data.CharacterID);
    //		}
    //		return true;
    //	}

    //	public static int GetAverageLevel()
    //	{
    //		int num = 0;
    //		int count = PlayersManager.ActivePlayers.Count;
    //		for (int i = 0; i < count; i++)
    //		{
    //			GameObject gameObject = PlayersManager.ActivePlayers[i];
    //			PalNPC component = gameObject.GetComponent<PalNPC>();
    //			num += component.Data.Level;
    //		}
    //		return num / count;
    //	}

    //	public static int GetMaxLevel()
    //	{
    //		int num = 0;
    //		int count = PlayersManager.ActivePlayers.Count;
    //		for (int i = 0; i < count; i++)
    //		{
    //			GameObject gameObject = PlayersManager.ActivePlayers[i];
    //			PalNPC component = gameObject.GetComponent<PalNPC>();
    //			int level = component.Data.Level;
    //			if (num < level)
    //			{
    //				num = level;
    //			}
    //		}
    //		return num;
    //	}

    //	public static bool IsMainChar(GameObject go)
    //	{
    //		return PlayersManager.AllPlayers.Contains(go);
    //	}

    //	public static List<AnimatorOverrideController> GetMainOverrideCtrlList()
    //	{
    //		List<AnimatorOverrideController> list = new List<AnimatorOverrideController>();
    //		for (int i = 0; i < PlayersManager.AllPlayers.Count; i++)
    //		{
    //			GameObject gameObject = PlayersManager.AllPlayers[i];
    //			PalNPC component = gameObject.GetComponent<PalNPC>();
    //			Animator component2 = component.model.GetComponent<Animator>();
    //			AnimatorOverrideController item = component2.runtimeAnimatorController as AnimatorOverrideController;
    //			list.Add(item);
    //		}
    //		return list;
    //	}

    //	public static bool ExsitsInPlayers(int ID)
    //	{
    //		for (int i = 0; i < PlayersManager.ActivePlayers.Count; i++)
    //		{
    //			GameObject gameObject = PlayersManager.ActivePlayers[i];
    //			if (!(gameObject == null))
    //			{
    //				PalNPC component = gameObject.GetComponent<PalNPC>();
    //				if (!(component == null))
    //				{
    //					if (component.Data.CharacterID == ID)
    //					{
    //						return true;
    //					}
    //				}
    //			}
    //		}
    //		return false;
    //	}

    //	private static int ExcludeJiGuanXiong(int newPlayerIndex)
    //	{
    //		GameObject gameObject = PlayersManager.ActivePlayers[newPlayerIndex];
    //		if (gameObject != null)
    //		{
    //			PalNPC component = gameObject.GetComponent<PalNPC>();
    //			if (component.Data.CharacterID == 6)
    //			{
    //				newPlayerIndex++;
    //				if (newPlayerIndex >= PlayersManager.ActivePlayers.Count)
    //				{
    //					newPlayerIndex = 0;
    //				}
    //			}
    //		}
    //		return newPlayerIndex;
    //	}

    //	public static void TabPlayer()
    //	{
    //		int count = PlayersManager.ActivePlayers.Count;
    //		int num = PlayersManager.PlayerIndex + 1;
    //		if (num >= count)
    //		{
    //			num = 0;
    //		}
    //		num = PlayersManager.ExcludeJiGuanXiong(num);
    //		PlayersManager.SetPlayer(num, true);
    //		MiniMap.Instance.MapSkillTime_Cur.fillAmount = 1f;
    //		if (PlayersManager.OnTabPlayer != null)
    //		{
    //			PlayersManager.OnTabPlayer(num);
    //		}
    //	}

    //	private static void AddNeedComponent(PalNPC npc)
    //	{
    //		if (npc == null)
    //		{
    //			Debug.LogError("Error : AddNeedComponent npc==null");
    //			return;
    //		}
    //		if (npc.model == null)
    //		{
    //			return;
    //		}
    //		GameObject model = npc.model;
    //		if (model.GetComponent<BattleTrigger>() == null)
    //		{
    //			model.AddComponent<BattleTrigger>();
    //		}
    //		if (model.GetComponent<TakePlace>() == null)
    //		{
    //			model.AddComponent<TakePlace>();
    //		}
    //		model.SetHeadLight(true);
    //	}

    public static void SetPlayer(int newPlayerIndex, bool SetPos = true)
    {
        using (new PlayersManager.AfterSetPlayer())
        {
            if (newPlayerIndex == PlayersManager.PlayerIndex)
            {
                if (PlayersManager.Player == null)
                {
                    Debug.LogError("Error : PlayerIndex==" + PlayersManager.PlayerIndex.ToString() + " Player == null 596行");
                }
                else
                {
                    PalNPC component = PlayersManager.Player.GetComponent<PalNPC>();
                    if (component == null)
                    {
                        Debug.LogError("Error : " + PlayersManager.Player.name + " npc == null 604行");
                    }
                    else
                    {                  
                        //GameObject model = component.model;
                        //if (model != null)
                        //{
                        //    model.layer = 8;
                        //}
                        //PlayersManager.Player.tag = "Player";
                        //PlayersManager.Player.layer = SmoothFollow2.IgnoreLayer;
                        //UtilFun.SetActive(PlayersManager.Player, true);
                        //if (model != null)
                        //{
                        //    PalNPC expr_FC = component;
                        //    expr_FC.OnLoadModelEnd = (PalNPC.void_fun_TF)Delegate.Remove(expr_FC.OnLoadModelEnd, new PalNPC.void_fun_TF(PlayersManager.AddNeedComponent));
                        //    LateSetActive.DeleteKey(model.name);
                        //    if (!model.activeSelf)
                        //    {
                        //        UtilFun.SetActive(model, true);
                        //    }
                        //    PlayersManager.AddNeedComponent(component);
                        //    model.SetHeadLight(true);
                        //    SkillSEPreviewAnimMove component2 = model.GetComponent<SkillSEPreviewAnimMove>();
                        //    if (component2 != null)
                        //    {
                        //        UnityEngine.Object.Destroy(component2);
                        //    }
                        //}
                        //else
                        //{
                        //    PalNPC expr_169 = component;
                        //    expr_169.OnLoadModelEnd = (PalNPC.void_fun_TF)Delegate.Combine(expr_169.OnLoadModelEnd, new PalNPC.void_fun_TF(PlayersManager.AddNeedComponent));
                        //}
                        //Agent component3 = model.GetComponent<Agent>();
                        //if (component3 != null && component3.charCtrller != null && !component3.charCtrller.enabled)
                        //{
                        //    component3.charCtrller.enabled = true;
                        //}
                        //PlayerCtrlManager.Reset();
                    }
                }
            }
            else if (newPlayerIndex < 0 || newPlayerIndex >= PlayersManager.ActivePlayers.Count)
            {
                Debug.LogError("PlayersManager.SetPlayer: out of bound, newPlayerIndex = " + newPlayerIndex);
            }
            else
            {
                GameObject gameObject = null;
                Transform transform = null;
                GameObject player = PlayersManager.Player;
                //SlideDown slideDown = null;
                //if (PlayersManager.Player != null)
                //{
                //    PalNPC component4 = PlayersManager.Player.GetComponent<PalNPC>();
                //    if (component4 == null)
                //    {
                //        Debug.LogError("Error : " + PlayersManager.Player.name + " npc == null 690行");
                //    }
                //    SneakAttack component5 = component4.GetComponent<SneakAttack>();
                //    if (component5 != null)
                //    {
                //        component5.enabled = false;
                //    }
                //    if (component4 != null && component4.Data != null && component4.Data.CharacterID == 0)
                //    {
                //        if (component4.model == null)
                //        {
                //            Debug.LogError("Error : " + component4.name + " npc.model == null 707行");
                //        }
                //        AnimCtrlScript component6 = component4.model.GetComponent<AnimCtrlScript>();
                //        if (component6 != null)
                //        {
                //            component6.ActiveZhanDou(false, 1, true, true, true);
                //        }
                //        if (component4.Weapons == null)
                //        {
                //            Debug.LogError("Error : " + component4.name + " npc.Weapons == null 718行");
                //        }
                //        for (int i = 0; i < component4.Weapons.Count; i++)
                //        {
                //            GameObject gameObject2 = component4.Weapons[i];
                //            if (gameObject2 != null)
                //            {
                //                Animator componentInChildren = gameObject2.GetComponentInChildren<Animator>();
                //                if (componentInChildren != null)
                //                {
                //                    componentInChildren.enabled = false;
                //                    AnimatorListen componentInChildren2 = gameObject2.GetComponentInChildren<AnimatorListen>();
                //                    if (componentInChildren2 != null)
                //                    {
                //                        UnityEngine.Object.Destroy(componentInChildren2);
                //                    }
                //                }
                //                UtilFun.YueJinChaoShenSuo(gameObject2.transform, Vector3.zero);
                //            }
                //        }
                //    }
                //    gameObject = component4.model;
                //    if (gameObject != null)
                //    {
                //        if (gameObject.transform.parent != PlayersManager.Player.transform)
                //        {
                //            transform = gameObject.transform.parent;
                //            gameObject.transform.parent = PlayersManager.Player.transform;
                //        }
                //        PlayersManager.Player.tag = "Untagged";
                //        PlayersManager.Player.layer = 0;
                //        UtilFun.SetActive(gameObject, false);
                //        Agent component7 = gameObject.GetComponent<Agent>();
                //        if (component7 != null)
                //        {
                //            component7.curCtrlMode = ControlMode.ControlByAgent;
                //        }
                //        gameObject.SetHeadLight(false);
                //    }
                //    slideDown = gameObject.GetComponent<SlideDown>();
                //}
                PlayersManager.PlayerIndex = newPlayerIndex;
                if (PlayersManager.Player != null)
                {
                    //PalNPC component8 = PlayersManager.Player.GetComponent<PalNPC>();
                    //if (component8 == null)
                    //{
                    //    Debug.LogError("Error : " + PlayersManager.Player.name + "  npc==null  784行");
                    //}
                    //SneakAttack[] componentsInChildren2 = component8.GetComponentsInChildren<SneakAttack>(true);
                    //if (componentsInChildren2 != null && componentsInChildren2.Length > 0 && componentsInChildren2[0] != null)
                    //{
                    //    componentsInChildren2[0].enabled = true;
                    //}
                    //GameObject model2 = component8.model;
                    //if (model2 == null)
                    //{
                    //    Debug.LogError("Error : " + PlayersManager.Player.name + "  npc.model==null  799行");
                    //}
                    //model2.layer = 8;
                    //Agent component9 = model2.GetComponent<Agent>();
                    //if (component9 != null && component9.charCtrller != null && !component9.charCtrller.enabled)
                    //{
                    //    component9.charCtrller.enabled = true;
                    //}
                    //PlayersManager.Player.tag = "Player";
                    //PlayersManager.Player.layer = SmoothFollow2.IgnoreLayer;
                    //if (gameObject != null && SetPos)
                    //{
                    //    if (transform != null && transform.name != "7")
                    //    {
                    //        model2.transform.parent = transform;
                    //    }
                    //    UtilFun.SetPosition(model2.transform, gameObject.transform.position);
                    //    model2.transform.rotation = gameObject.transform.rotation;
                    //}
                    //UtilFun.SetActive(PlayersManager.Player, true);
                    //LateSetActive.DeleteKey(model2.name);
                    //if (!model2.activeSelf)
                    //{
                    //    UtilFun.SetActive(model2, true);
                    //}
                    //if (gameObject != null && SetPos)
                    //{
                    //    UtilFun.SetPosition(model2.transform, gameObject.transform.position);
                    //}
                    //Agent component10 = model2.GetComponent<Agent>();
                    //if (component10 != null)
                    //{
                    //    component10.curCtrlMode = ControlMode.None;
                    //}
                    //if (model2 != null)
                    //{
                    //    if (model2.GetComponent<BattleTrigger>() == null)
                    //    {
                    //        model2.AddComponent<BattleTrigger>();
                    //    }
                    //    model2.SetHeadLight(true);
                    //    TurnHead component11 = model2.GetComponent<TurnHead>();
                    //    if (component11 != null)
                    //    {
                    //        component11.enabled = false;
                    //    }
                    //    SkillSEPreviewAnimMove component12 = model2.GetComponent<SkillSEPreviewAnimMove>();
                    //    if (component12 != null)
                    //    {
                    //        UnityEngine.Object.Destroy(component12);
                    //    }
                    //}
                    //PlayerCtrlManager.Reset();
                    //if (player != null)
                    //{
                    //    PalNPC component13 = PlayersManager.Player.GetComponent<PalNPC>();
                    //    PalNPC component14 = player.GetComponent<PalNPC>();
                    //    if (component13 != null && component13.perception != null && component14 != null && component14.perception != null)
                    //    {
                    //        component13.perception.CopyData(component14.perception);
                    //    }
                    //}
                    //if (model2 != null && slideDown != null)
                    //{
                    //    SlideDown component15 = model2.GetComponent<SlideDown>();
                    //    if (component15 != null)
                    //    {
                    //        component15.enabled = slideDown.enabled;
                    //    }
                    //}
                }
            }
        }
    }

    public static GameObject GetPlayer(int ID)
    {
        GameObject result = null;
        foreach (GameObject current in PlayersManager.ActivePlayers)
        {
            if (current != null && current.name == ID.ToString())
            {
                result = current;
                break;
            }
        }
        return result;
    }

    public static GameObject FindMainChar(int ID, bool NeedAddToAllPlayers = true)
    {
        for (int i = 0; i < PlayersManager.AllPlayers.Count; i++)
        {
            GameObject gameObject = PlayersManager.AllPlayers[i];
            if (!(gameObject == null))
            {
                if (gameObject.name == ID.ToString())
                {
                    return gameObject;
                }
            }
        }

        GameObject gameObject2 = GameObject.Find("/" + ID.ToString());
        if (gameObject2 == null)
        {
            gameObject2 = GameObject.Find(ID.ToString());
        }
        if (NeedAddToAllPlayers && gameObject2 != null && ID != 6)
        {
            PlayersManager.AllPlayers.Add(gameObject2);
        }
        return gameObject2;
    }

    public static GameObject AddPlayer(int ID, bool bSetLevel = true)
    {
        GameObject player = PlayersManager.GetPlayer(ID);
        if (player != null)
        {
            Debug.Log("PlayersManager.AddPlayer 已经存在" + ID.ToString());
            return player;
        }

        GameObject gameObject = PlayersManager.FindMainChar(ID, true);
        if (gameObject != null && gameObject.GetComponent<PalNPC>() != null)
        {
            PlayersManager.AddPlayer(gameObject, bSetLevel);
            if (PlayersManager.ActivePlayers.Count == 1)
            {
                PlayersManager.SetPlayer(ID, false);
            }
            if (ID != 6 && PlayersManager.OnAddPlayer != null)
            {
                PlayersManager.OnAddPlayer(ID);
            }
            return gameObject;
        }

        GameObject gameObject2 = PlayersManager.LoadPlayer(ID);
        if (gameObject2 != null)
        {
            if (PlayersManager.AllPlayers.Count > ID + 1 && PlayersManager.AllPlayers[ID] == null)
            {
                PlayersManager.AllPlayers[ID] = gameObject2;
            }
            PlayersManager.AddPlayer(gameObject2, bSetLevel);
            if (ID == 4)
            {
                PlayersManager.AddPlayer(6, true);
            }
        }
        if (ID != 6 && PlayersManager.OnAddPlayer != null)
        {
            PlayersManager.OnAddPlayer(ID);
        }
        return gameObject2;
    }

    private static void SetLevel(GameObject go)
    {
        PalNPC component = go.GetComponent<PalNPC>();
        int level = component.Data.Level;
        GameObject gameObject = go;
        for (int i = 0; i < PlayersManager.ActivePlayers.Count; i++)
        {
            GameObject gameObject2 = PlayersManager.ActivePlayers[i];
            PalNPC component2 = gameObject2.GetComponent<PalNPC>();
            if (level < component2.Data.Level)
            {
                level = component2.Data.Level;
                gameObject = gameObject2;
            }
        }
        if (gameObject != go)
        {
            PalNPC component3 = go.GetComponent<PalNPC>();
            //component3.Data.Exp = PlayerBaseProperty.LevelData.GetLevelExp(level - 1);
            //Debug.Log(string.Concat(new string[]
            //{
            //        "Log : 对角色",
            //        go.name,
            //        "进行等级设置，参照了",
            //        gameObject.name,
            //        "的等级(",
            //        level.ToString(),
            //        "级)  其经验为:",
            //        component3.Data.Exp.ToString()
            //}));
        }
    }

    public static GameObject LoadPlayer(int ID)
    {
        string path = PlayersManager.PlayerTemplatePath + ID.ToString();
        GameObject gameObject = FileLoader.LoadObjectFromFile<GameObject>(path.ToAssetBundlePath(), true, true);
        gameObject.ExcludeCloneName();
        return gameObject;
    }

    public static void AddPlayer(GameObject newPlayer, bool bSetLevel = true)
    {
        if (newPlayer == null)
        {
            return;
        }
        newPlayer.ExcludeCloneName();
        PalNPC component = newPlayer.GetComponent<PalNPC>();
        if (component == null)
        {
            return;
        }
        if (!PlayersManager.ActivePlayers.Contains(newPlayer))
        {
            newPlayer.transform.parent = null;
            if (newPlayer.GetComponent<SavePrefabTarget>() == null)
            {
                SavePrefabTarget savePrefabTarget = newPlayer.AddComponent<SavePrefabTarget>();
            }

            if (component.model == null)
            {
                PalNPC palNPC = component;
                palNPC.OnLoadModelEnd = (PalNPC.void_fun_TF)Delegate.Combine(palNPC.OnLoadModelEnd, new PalNPC.void_fun_TF(PlayersManager.WaitLoadOverThanSetActiveFalse));
            }
            else
            {
                AnimCtrlScript component3 = component.model.GetComponent<AnimCtrlScript>();
                component3.ActiveAnimCrossFade("ZhanLi", false, 0f, true);
                if (component.Data.CharacterID == 2)
                {
                    if (!component.animator.GetCurrentAnimatorStateInfo(0).IsName("yidongState.ZhanLi"))
                    {
                        //LateSetActive.Init(component.model, false, 0.01f);
                    }
                    else
                    { 
                        UtilFun.SetActive(component.model, false);
                    }
                }
                else
                {
                    UtilFun.SetActive(component.model, false);
                }
            }

            if (bSetLevel)
            {
                PlayersManager.SetLevel(newPlayer);
            }

            PlayersManager.ActivePlayers.Add(newPlayer);

            if (component.Data != null)
            {
                FlagManager.SetBoolFlag((ulong)(34048L + (long)component.Data.CharacterID), true);
            }
        }
        else
        {
            Debug.Log("Log : PlayersManager.AddPlayer 已经存在 " + newPlayer.name);
        }
    }

    private static void WaitLoadOverThanSetActiveFalse(PalNPC npc)
    {
        if (PlayersManager.Player != null && npc != null && PlayersManager.Player != npc.gameObject)
        {
            UtilFun.SetActive(npc.model, false);
        }
    }

    //	public static void RemovePlayer(int ID, bool bActive = false)
    //	{
    //		if (ID == 4)
    //		{
    //			PlayersManager.RemovePlayer(6, bActive);
    //		}
    //		GameObject player = PlayersManager.GetPlayer(ID);
    //		if (!player)
    //		{
    //			Debug.Log("ActivePlayers不存在此ID");
    //			return;
    //		}
    //		PlayersManager.RemovePlayer(player, bActive);
    //		if (ID != 6 && PlayersManager.OnRemovePlayer != null)
    //		{
    //			PlayersManager.OnRemovePlayer(ID);
    //		}
    //	}

    //	public static void RemovePlayer(GameObject go, bool bActive = false)
    //	{
    //		if (PlayersManager.ActivePlayers.Count < 2)
    //		{
    //			Debug.LogError(go.name + "打算离队  只有一个人就别离队了");
    //			return;
    //		}
    //		int newPlayerIndex = 0;
    //		GameObject gameObject = null;
    //		if (PlayersManager.Player == go)
    //		{
    //			newPlayerIndex = PlayersManager.PlayerIndex % (PlayersManager.ActivePlayers.Count - 1);
    //		}
    //		else
    //		{
    //			gameObject = PlayersManager.Player;
    //		}
    //		PlayersManager.ActivePlayers.Remove(go);
    //		GameObject modelObj = go.GetModelObj(false);
    //		LateSetActive.Init(modelObj, bActive, 0.01f);
    //		if (gameObject == null)
    //		{
    //			PlayersManager.SetPlayer(newPlayerIndex, false);
    //		}
    //		else
    //		{
    //			newPlayerIndex = PlayersManager.ActivePlayers.IndexOf(gameObject);
    //			PlayersManager.SetPlayer(newPlayerIndex, false);
    //		}
    //	}

    //	public static GameObject SpawnPlayer(string path = null, string DestPosName = null, bool NeedSetCamera = false)
    //	{
    //		if (Application.loadedLevelName != "Start")
    //		{
    //			GameObject gameObject = PlayersManager.AddPlayer(0, true);
    //			PlayersManager.SetPlayer(0, true);
    //		}
    //		if (!string.IsNullOrEmpty(DestPosName))
    //		{
    //			PlayersManager.SetPlayerPosByDestObj(DestPosName);
    //		}
    //		return PlayersManager.Player;
    //	}

    //	public static void SetPlayerPosByDestObj(string DestName)
    //	{
    //		GameObject gameObject = GameObject.Find(DestName);
    //		if (gameObject == null)
    //		{
    //			Debug.LogError("没有找到 " + DestName);
    //			return;
    //		}
    //		if (PlayersManager.Player == null)
    //		{
    //			Debug.LogError("没有主控角色");
    //			return;
    //		}
    //		Transform transform;
    //		if (PlayerCtrlManager.agentObj != null)
    //		{
    //			transform = PlayerCtrlManager.agentObj.transform;
    //		}
    //		else
    //		{
    //			PalNPC component = PlayersManager.Player.GetComponent<PalNPC>();
    //			if (!(component.model != null))
    //			{
    //				PlayersManager.tempDestTF = gameObject.transform;
    //				PalNPC expr_9A = component;
    //				expr_9A.OnLoadModelEnd = (PalNPC.void_fun_TF)Delegate.Combine(expr_9A.OnLoadModelEnd, new PalNPC.void_fun_TF(PlayersManager.WaitForSpawn));
    //				return;
    //			}
    //			transform = component.model.transform;
    //		}
    //		if (transform != null)
    //		{
    //			Agent component2 = transform.GetComponent<Agent>();
    //			if (component2 != null && component2.charCtrller != null && !component2.charCtrller.enabled)
    //			{
    //				component2.charCtrller.enabled = true;
    //			}
    //		}
    //		Transform transform2 = gameObject.transform;
    //		Vector3 vector = transform2.position;
    //		vector.y += 1f;
    //		RaycastHit raycastHit;
    //		if (Physics.Raycast(vector, Vector3.down, out raycastHit))
    //		{
    //			vector = raycastHit.point;
    //		}
    //		UtilFun.SetPosition(transform, vector);
    //		transform.eulerAngles = new Vector3(0f, transform2.eulerAngles.y, 0f);
    //		SceneFall2.SetLastPointOnLoadOver();
    //	}

    //	private static void WaitForSpawn(PalNPC npc)
    //	{
    //		Vector3 vector = PlayersManager.tempDestTF.position;
    //		RaycastHit raycastHit;
    //		if (Physics.Raycast(vector, Vector3.down, out raycastHit))
    //		{
    //			vector = raycastHit.point;
    //		}
    //		Transform transform = npc.model.transform;
    //		UtilFun.SetPosition(transform, vector);
    //		transform.eulerAngles = new Vector3(0f, PlayersManager.tempDestTF.eulerAngles.y, 0f);
    //		SceneFall2.SetLastPointOnLoadOver();
    //	}

    //	public static string Save(string SaveName)
    //	{
    //		string text = SaveManager.GetStoreDirePath(SaveName);
    //		text += "/Player";
    //		List<SavePrefabTarget> players = PlayersManager.GetPlayers();
    //		using (BinaryWriter binaryWriter = new BinaryWriter(File.Open(text, FileMode.Create)))
    //		{
    //			PlayersManager.Save_FileStream(binaryWriter);
    //		}
    //		return string.Empty;
    //	}

    //	public static string Save_FileStream(BinaryWriter _writer)
    //	{
    //		List<SavePrefabTarget> players = PlayersManager.GetPlayers();
    //		_writer.Write(PlayersManager.PlayerIndex);
    //		_writer.Write(players.Count);
    //		foreach (SavePrefabTarget current in players)
    //		{
    //			if (!current.Save(_writer))
    //			{
    //				return current.name + "保存出错";
    //			}
    //		}
    //		int count = PlayersManager.ActivePlayers.Count;
    //		_writer.Write(count);
    //		for (int i = 0; i < count; i++)
    //		{
    //			GameObject gameObject = PlayersManager.ActivePlayers[i];
    //			PalNPC component = gameObject.GetComponent<PalNPC>();
    //			int characterID = component.Data.CharacterID;
    //			_writer.Write(characterID);
    //		}
    //		SmoothFollow2 component2 = Camera.main.GetComponent<SmoothFollow2>();
    //		if (component2 != null)
    //		{
    //			component2.Save(_writer);
    //		}
    //		WeatherManage.Save(_writer);
    //		TimeManager.Instance.SaveWeatherTime(_writer);
    //		return string.Empty;
    //	}

    //	public static string Load_FileStream(BinaryReader _reader)
    //	{
    //		for (int i = 0; i < PlayersManager.AllPlayers.Count; i++)
    //		{
    //			GameObject gameObject = PlayersManager.AllPlayers[i];
    //			if (!(gameObject == null))
    //			{
    //				GameObject modelObj = gameObject.GetModelObj(false);
    //				ShroudInstance component = modelObj.GetComponent<ShroudInstance>();
    //				if (component != null)
    //				{
    //					ShroudWeight component2 = modelObj.GetComponent<ShroudWeight>();
    //					if (component2 != null)
    //					{
    //						UnityEngine.Object.Destroy(component2);
    //					}
    //					component.HairWeightK = 100f;
    //					component.blendWeightK = 100f;
    //				}
    //			}
    //		}
    //		PlayersManager.ActivePlayers.Clear();
    //		PlayersManager.TempPlayerIndex = _reader.ReadInt32();
    //		int num = _reader.ReadInt32();
    //		if (num > 0)
    //		{
    //			PlayersManager.TempPlayersCount = 0;
    //			for (int j = 0; j < num; j++)
    //			{
    //				GameObject gameObject2 = SavePrefabTarget.Load(_reader, null);
    //				if (!gameObject2)
    //				{
    //					return "读取人物 " + j.ToString() + " 时报错";
    //				}
    //				PalNPC component3 = gameObject2.GetComponent<PalNPC>();
    //				if (component3.model == null && !component3.gameObject.activeSelf)
    //				{
    //					Debug.LogError("palNpc " + component3.name + " 这个东西没莫名的disable了，需查明愿意");
    //				}
    //			}
    //			if (PlayersManager.TempPlayersCount <= 0)
    //			{
    //				PlayersManager.ActivePlayers.Clear();
    //				int num2 = _reader.ReadInt32();
    //				for (int k = 0; k < num2; k++)
    //				{
    //					int iD = _reader.ReadInt32();
    //					GameObject newPlayer = PlayersManager.FindMainChar(iD, true);
    //					PlayersManager.AddPlayer(newPlayer, false);
    //				}
    //				PlayersManager.SetPlayer(PlayersManager.TempPlayerIndex, false);
    //			}
    //		}
    //		if (!SaveManager.IsErZhouMu)
    //		{
    //			SmoothFollow2 component4 = Camera.main.GetComponent<SmoothFollow2>();
    //			if (component4 != null)
    //			{
    //				component4.Load(_reader);
    //			}
    //			WeatherManage.Load(_reader);
    //			TimeManager.Instance.LoadWeatherTime(_reader);
    //		}
    //		return string.Empty;
    //	}

    //	private static List<SavePrefabTarget> GetPlayers()
    //	{
    //		List<SavePrefabTarget> list = new List<SavePrefabTarget>();
    //		for (int i = 0; i < PlayersManager.AllPlayers.Count; i++)
    //		{
    //			GameObject gameObject = PlayersManager.AllPlayers[i];
    //			if (!(gameObject == null))
    //			{
    //				SavePrefabTarget savePrefabTarget = gameObject.GetComponent<SavePrefabTarget>();
    //				if (savePrefabTarget == null)
    //				{
    //					savePrefabTarget = gameObject.AddComponent<SavePrefabTarget>();
    //				}
    //				list.Add(savePrefabTarget);
    //			}
    //		}
    //		if (list.Count < 1)
    //		{
    //			for (int j = 0; j < PlayersManager.ActivePlayers.Count; j++)
    //			{
    //				GameObject gameObject2 = PlayersManager.ActivePlayers[j];
    //				if (!(gameObject2 == null))
    //				{
    //					SavePrefabTarget savePrefabTarget2 = gameObject2.GetComponent<SavePrefabTarget>();
    //					if (savePrefabTarget2 == null)
    //					{
    //						savePrefabTarget2 = gameObject2.AddComponent<SavePrefabTarget>();
    //					}
    //					list.Add(savePrefabTarget2);
    //				}
    //			}
    //		}
    //		return list;
    //	}

    //	public static string Load(string LoadName)
    //	{
    //		string text = SaveManager.GetStoreDirePath(LoadName);
    //		text += "/Player";
    //		if (!File.Exists(text))
    //		{
    //			return "没找到 " + text;
    //		}
    //		string result = string.Empty;
    //		using (BinaryReader binaryReader = new BinaryReader(File.OpenRead(text)))
    //		{
    //			result = PlayersManager.Load_FileStream(binaryReader);
    //		}
    //		return result;
    //	}

    //	private static void WaitForSetPlayer(PalNPC npc)
    //	{
    //		UtilFun.SetActive(npc.model, false);
    //		PlayersManager.TempPlayersCount--;
    //		if (PlayersManager.TempPlayersCount <= 0)
    //		{
    //			PlayersManager.SetPlayer(PlayersManager.TempPlayerIndex, false);
    //		}
    //	}

    //	public static void AddExp(int exp)
    //	{
    //		for (int i = 0; i < PlayersManager.ActivePlayers.Count; i++)
    //		{
    //			GameObject gameObject = PlayersManager.ActivePlayers[i];
    //			if (gameObject == null)
    //			{
    //				Debug.LogError("ActivePlayers 有null 元素");
    //			}
    //			else
    //			{
    //				PalNPC component = gameObject.GetComponent<PalNPC>();
    //				if (component == null)
    //				{
    //					Debug.LogError(gameObject.name + "没找到PalNPC");
    //				}
    //				else if (component.Data == null)
    //				{
    //					Debug.LogError(gameObject.name + " npc.Data ==null");
    //				}
    //				else
    //				{
    //					component.Data.Exp += exp;
    //				}
    //			}
    //		}
    //	}

    //	public static void ResetPlayersInteract(bool bDLC = false)
    //	{
    //		for (int i = 0; i < PlayersManager.AllPlayers.Count; i++)
    //		{
    //			GameObject gameObject = PlayersManager.AllPlayers[i];
    //			if (!(gameObject == null))
    //			{
    //				Interact component = gameObject.GetComponent<Interact>();
    //				if (!(component == null))
    //				{
    //					string actionClassName = component.ActionClassName;
    //					if (!bDLC)
    //					{
    //						int num = actionClassName.IndexOf("_DLC");
    //						if (num > -1)
    //						{
    //							string str = actionClassName.Substring(0, num);
    //							component.ActionClassName = str + "_touch";
    //						}
    //					}
    //					else
    //					{
    //						int num2 = actionClassName.IndexOf("_DLC");
    //						if (num2 < 0)
    //						{
    //							int length = actionClassName.IndexOf("_touch");
    //							string str2 = actionClassName.Substring(0, length);
    //							component.ActionClassName = str2 + "_DLC_touch";
    //						}
    //					}
    //				}
    //			}
    //		}
    //	}

    //	private static void OnSetFlag(int idx, int flagValue)
    //	{
    //		if (idx == MissionManager.BranchLineToggleFlag)
    //		{
    //			PlayersManager.ResetPlayersInteract(flagValue > 0);
    //		}
    //	}

    public static void AddPlayerPerceptionRange(PalNPC npc)
    {
        PerceptionRange[] componentsInChildren = npc.model.GetComponentsInChildren<PerceptionRange>();
        for (int i = 0; i < componentsInChildren.Length; i++)
        {
            PlayersManager.AllPlayersPerceptionRange.Add(componentsInChildren[i]);
        }
    }

    public static void SetAllPlayersPerceptionRange(bool enable)
    {
        for (int i = 0; i < PlayersManager.AllPlayersPerceptionRange.Count; i++)
        {
            Collider component = PlayersManager.AllPlayersPerceptionRange[i].GetComponent<Collider>();
            component.enabled = enable;
        }
    }
}
