using SoftStar.Pal6;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public class AnimCtrlScript : MonoBehaviour
{
    public enum OverrideAnim
    {
        OverridableAnim,
        OverridableAnim_L,
        OverridableAnim_Q,
        OverridableAnim_Z,
        OverridableAnim_H
    }

    private const string stateName = "OverridableAnim";

    private Animator m_animator;

    //protected Dictionary<int, ResetStateBool> remainingRSBs = new Dictionary<int, ResetStateBool>();

    public bool bInBattleState;

    private RuntimeAnimatorController oriCtrl;

    private AnimatorOverrideController overrideCtrl;

    private List<AnimationClipPair> pairs = new List<AnimationClipPair>(50);

    private bool m_Override = true;

    private string m_CtrlName;

    private bool hasInit;

    private bool hasOverride;

    private static List<AnimatorOverrideController> OverrideCtrlList = new List<AnimatorOverrideController>();

    private int tempHash;

    private float curTime;

    private List<float> LayerWeights = new List<float>();

    //public static Dictionary<string, Dictionary<string, AnimClipData>> AnimClipsDic = new Dictionary<string, Dictionary<string, AnimClipData>>(7);

    private Dictionary<AnimCtrlScript.OverrideAnim, float> animCrossFades = new Dictionary<AnimCtrlScript.OverrideAnim, float>();

    public bool m_bPlayFromBegining;

    private List<int> NeedRemoveKey = new List<int>(2);

    //	public Animator animator
    //	{
    //		get
    //		{
    //			if (this.m_animator == null)
    //			{
    //				this.m_animator = base.GetComponent<Animator>();
    //			}
    //			return this.m_animator;
    //		}
    //	}

    //	private bool IsMainChar
    //	{
    //		get
    //		{
    //			string text = base.transform.name;
    //			text = text.ToLower();
    //			return text == "yuejinchao" || text == "yueqi" || text == "xianqing" || text == "luowenren" || text == "luowenren_nv" || text == "jushifang" || text == "mingxiu";
    //		}
    //	}

    //	public bool Override
    //	{
    //		get
    //		{
    //			return this.m_Override;
    //		}
    //		set
    //		{
    //			this.m_Override = value;
    //		}
    //	}

    //	public string CtrlName
    //	{
    //		get
    //		{
    //			return this.m_CtrlName;
    //		}
    //		set
    //		{
    //			this.m_CtrlName = value;
    //		}
    //	}

    //	public bool HasOverride
    //	{
    //		get
    //		{
    //			return this.hasOverride;
    //		}
    //	}

    //	public string GetOriActorName(string ActorName = null)
    //	{
    //		string ctrlName = this.CtrlName;
    //		int length = ctrlName.LastIndexOf("Ctrl");
    //		string text = ctrlName.Substring(0, length);
    //		if (!string.IsNullOrEmpty(ActorName))
    //		{
    //			if (ActorName.Equals("DLC_MoHuai", StringComparison.OrdinalIgnoreCase))
    //			{
    //				UnityEngine.Debug.Log("DLC_MoHuai GetOriActorName, just return XianQing back");
    //				return text;
    //			}
    //			if (!ActorName.Contains(text))
    //			{
    //				text = ActorName;
    //			}
    //		}
    //		return text;
    //	}

    //	public void OverrideController()
    //	{
    //		if (this.hasOverride)
    //		{
    //			return;
    //		}
    //		this.hasOverride = true;
    //		try
    //		{
    //			if (this.animator != null && this.animator.runtimeAnimatorController != null)
    //			{
    //				this.overrideCtrl = new AnimatorOverrideController();
    //				this.oriCtrl = this.animator.runtimeAnimatorController;
    //				if (this.oriCtrl != null && this.overrideCtrl != null)
    //				{
    //					this.CtrlName = this.oriCtrl.name;
    //					this.overrideCtrl.runtimeAnimatorController = this.oriCtrl;
    //					this.animator.runtimeAnimatorController = this.overrideCtrl;
    //					this.animator.Rebind();
    //				}
    //			}
    //		}
    //		catch (Exception ex)
    //		{
    //			UnityEngine.Debug.Log("Exception catched:");
    //			UnityEngine.Debug.Log(ex.Message);
    //			UnityEngine.Debug.Log(ex.StackTrace);
    //		}
    //	}

    //	public void Start()
    //	{
    //		if (this.hasInit)
    //		{
    //			return;
    //		}
    //		this.hasInit = true;
    //		if (this.Override)
    //		{
    //			InitOverrideController.Init(this);
    //		}
    //	}

    //	private void OnDestroy()
    //	{
    //		if (this.hasOverride)
    //		{
    //			this.animator.runtimeAnimatorController = null;
    //			this.oriCtrl = null;
    //			this.overrideCtrl = null;
    //		}
    //	}

    //	private static void ClearOverrideReference()
    //	{
    //		for (int i = 0; i < AnimCtrlScript.OverrideCtrlList.Count; i++)
    //		{
    //			AnimatorOverrideController animatorOverrideController = AnimCtrlScript.OverrideCtrlList[i];
    //			if (animatorOverrideController != null)
    //			{
    //				AnimCtrlScript.ClearAllOverrideClip(animatorOverrideController);
    //			}
    //			UnityEngine.Object.DestroyImmediate(animatorOverrideController);
    //		}
    //		AnimCtrlScript.OverrideCtrlList.Clear();
    //	}

    //	private static void ClearAllOverrideClip(AnimatorOverrideController overrideCtrl)
    //	{
    //		if (overrideCtrl != null)
    //		{
    //			overrideCtrl[AnimCtrlScript.OverrideAnim.OverridableAnim.ToString()] = null;
    //			overrideCtrl[AnimCtrlScript.OverrideAnim.OverridableAnim_H.ToString()] = null;
    //			overrideCtrl[AnimCtrlScript.OverrideAnim.OverridableAnim_L.ToString()] = null;
    //			overrideCtrl[AnimCtrlScript.OverrideAnim.OverridableAnim_Q.ToString()] = null;
    //			overrideCtrl[AnimCtrlScript.OverrideAnim.OverridableAnim_Z.ToString()] = null;
    //		}
    //	}

    //	private int GetIndex(AnimatorOverrideController overrideCtrl, string name = "XiuXian")
    //	{
    //		int result = -1;
    //		AnimationClipPair[] clips = overrideCtrl.clips;
    //		for (int i = 0; i < clips.Length; i++)
    //		{
    //			if (clips[i].originalClip.name == name)
    //			{
    //				result = i;
    //				break;
    //			}
    //		}
    //		return result;
    //	}

    //	public void ActiveZhanDou(bool bActive, int mode = 1, bool NeedBindWeapon = true, bool NeedSetAssort = true, bool bZhanLi = true)
    //	{
    //		if (this.animator == null)
    //		{
    //			UnityEngine.Debug.LogError(base.name + " animator==null");
    //			return;
    //		}
    //		this.bInBattleState = bActive;
    //		if (this.animator.layerCount < 2)
    //		{
    //			return;
    //		}
    //		if (bActive)
    //		{
    //			if (mode == 1)
    //			{
    //				if (this.animator.layerCount > 1)
    //				{
    //					this.animator.SetLayerWeight(1, 1f);
    //				}
    //				if (this.animator.layerCount > 2)
    //				{
    //					this.animator.SetLayerWeight(2, 0f);
    //				}
    //				if (this.animator.layerCount > 4)
    //				{
    //					this.animator.SetLayerWeight(3, 0f);
    //					this.animator.SetLayerWeight(4, 0f);
    //				}
    //			}
    //			else if (mode == 2 && this.animator.layerCount > 4)
    //			{
    //				this.animator.SetLayerWeight(3, 1f);
    //				this.animator.SetLayerWeight(4, 1f);
    //				this.animator.SetLayerWeight(1, 0f);
    //			}
    //			this.animator.SetBool("ZhanDou", true);
    //			if (this.animator.GetFloat("Speed") > 4f)
    //			{
    //				this.animator.Play("WeiLeQianChong", 2);
    //			}
    //		}
    //		else
    //		{
    //			ActiveZhanDouScript component = this.animator.GetComponent<ActiveZhanDouScript>();
    //			if (component != null)
    //			{
    //				UnityEngine.Object.Destroy(component);
    //			}
    //			this.animator.SetLayerWeight(1, 0f);
    //			if (this.animator.layerCount > 2)
    //			{
    //				this.animator.SetLayerWeight(2, 1f);
    //			}
    //			if (this.animator.layerCount > 4)
    //			{
    //				this.animator.SetLayerWeight(3, 0f);
    //				this.animator.SetLayerWeight(4, 0f);
    //			}
    //			this.animator.SetBool("ZhanDou", false);
    //			if (bZhanLi)
    //			{
    //				this.animator.CrossFade("ZhanLi", 0.05f);
    //			}
    //			if (this.animator.layerCount > 2)
    //			{
    //				this.animator.Play("New State", 2);
    //			}
    //		}
    //		Transform transform = base.transform;
    //		if (NeedBindWeapon && !base.name.ToLower().Contains("jiguanxiong") && !base.name.Contains("JuShiFang"))
    //		{
    //			UtilFun.BindWeaponToProp(transform, UtilFun.BindSlot.Default);
    //		}
    //		if (NeedSetAssort)
    //		{
    //			if (bActive)
    //			{
    //				UtilFun.SetActiveWeaponInBattle(transform);
    //			}
    //			else
    //			{
    //				UtilFun.SetActiveWeaponInNormal(transform);
    //			}
    //		}
    //	}

    //	public void ActiveBattle(bool bActive)
    //	{
    //		float crossTime = 0.12f;
    //		string text = base.name.ToLower();
    //		if (!text.Contains("yuejinchao") && !text.Contains("luowenren"))
    //		{
    //			this.ActiveZhanDou(bActive, 2.5f, 1);
    //			return;
    //		}
    //		if (base.gameObject.GetComponent<TakePlace>() == null)
    //		{
    //			base.gameObject.AddComponent<TakePlace>();
    //		}
    //		if (this.animator.GetFloat("Speed") < 0.3f)
    //		{
    //			if (bActive)
    //			{
    //				if (this.animator.GetLayerWeight(1) < 0.5f)
    //				{
    //					this.bInBattleState = true;
    //					this.ActiveAnimCrossFade("ChaJian 1", false, 0.12f, true);
    //				}
    //			}
    //			else if (this.animator.GetLayerWeight(1) > 0.5f)
    //			{
    //				this.bInBattleState = false;
    //				this.ActiveAnimCrossFade("New State", true, 0.05f, true);
    //				this.ActiveAnimCrossFade("ChaJian 0", false, crossTime, true);
    //			}
    //		}
    //		else if (bActive)
    //		{
    //			if (this.animator.GetLayerWeight(1) < 0.5f)
    //			{
    //				this.bInBattleState = true;
    //				this.ActiveAnimCrossFade("BaJian", true, 0.12f, true);
    //			}
    //		}
    //		else if (this.animator.GetLayerWeight(1) > 0.5f)
    //		{
    //			this.bInBattleState = false;
    //			this.ActiveAnimCrossFade("New State", true, 0.05f, true);
    //			this.ActiveAnimCrossFade("ChaJian 0", false, crossTime, true);
    //		}
    //	}

    //	public void ActiveZhanDou(bool bActive, float velocity, int mode)
    //	{
    //		if (base.name.ToLower().Contains("yuejinchao") && !bActive)
    //		{
    //			Transform[] props = GameObjectPath.GetProps(base.transform);
    //			if (props != null)
    //			{
    //				for (int i = 0; i < props.Length; i++)
    //				{
    //					Animator componentInChildren = props[i].GetComponentInChildren<Animator>();
    //					if (componentInChildren != null)
    //					{
    //						componentInChildren.enabled = true;
    //						componentInChildren.CrossFade("weapon_M1_01_change 0", 0.05f);
    //						AnimatorListen.Listen(componentInChildren, "weapon_M1_01_change 0", 1f, new Action<object>(TakePlace.BaChaEnd));
    //					}
    //				}
    //			}
    //		}
    //		this.bInBattleState = bActive;
    //		ActiveZhanDouScript.InitAnim(base.gameObject, bActive, mode, velocity, this.animator, this);
    //	}

    //	public void ActiveAnimFromClip(AnimationClip clip, AnimCtrlScript.OverrideAnim anim = AnimCtrlScript.OverrideAnim.OverridableAnim, float crossTime = 0.1f, bool useUp = false)
    //	{
    //		this.SetOverrideClip(clip, anim, true);
    //		base.StartCoroutine(this.WaitFrameAndPlayAnim(clip, anim, crossTime, useUp));
    //	}

    //	private void StoreLayerWeights()
    //	{
    //		this.LayerWeights.Clear();
    //		int layerCount = this.animator.layerCount;
    //		for (int i = 0; i < layerCount; i++)
    //		{
    //			float layerWeight = this.animator.GetLayerWeight(i);
    //			this.LayerWeights.Add(layerWeight);
    //		}
    //	}

    //	private void RestoreLayerWeights()
    //	{
    //		int layerCount = this.animator.layerCount;
    //		if (GameStateManager.CurGameState != GameState.Battle)
    //		{
    //			for (int i = 0; i < layerCount; i++)
    //			{
    //				if (i <= this.LayerWeights.Count - 1)
    //				{
    //					this.animator.SetLayerWeight(i, this.LayerWeights[i]);
    //				}
    //			}
    //		}
    //		else if (layerCount < 4)
    //		{
    //			if (layerCount >= 1)
    //			{
    //				this.animator.SetLayerWeight(0, 0f);
    //			}
    //			if (layerCount >= 2)
    //			{
    //				this.animator.SetLayerWeight(1, 1f);
    //			}
    //			if (layerCount >= 3)
    //			{
    //				this.animator.SetLayerWeight(2, 0f);
    //			}
    //		}
    //		else
    //		{
    //			int yueJinZhaoSkillGroup = PalBattleManager.Instance().GetYueJinZhaoSkillGroup();
    //			if (yueJinZhaoSkillGroup < 1)
    //			{
    //				this.animator.SetLayerWeight(0, 0f);
    //				this.animator.SetLayerWeight(1, 1f);
    //				this.animator.SetLayerWeight(2, 0f);
    //				this.animator.SetLayerWeight(3, 0f);
    //				if (layerCount >= 5)
    //				{
    //					this.animator.SetLayerWeight(4, 0f);
    //				}
    //			}
    //			else
    //			{
    //				this.animator.SetLayerWeight(0, 0f);
    //				this.animator.SetLayerWeight(1, 0f);
    //				this.animator.SetLayerWeight(2, 0f);
    //				this.animator.SetLayerWeight(3, 1f);
    //				if (layerCount >= 5)
    //				{
    //					this.animator.SetLayerWeight(4, 0f);
    //				}
    //			}
    //		}
    //	}

    //	public void SetOverrideClip(AnimationClip clip, AnimCtrlScript.OverrideAnim anim = AnimCtrlScript.OverrideAnim.OverridableAnim, bool WaitNextFrame = false)
    //	{
    //		AnimatorStateInfo currentAnimatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
    //		this.tempHash = currentAnimatorStateInfo.fullPathHash;
    //		this.curTime = currentAnimatorStateInfo.normalizedTime;
    //		if (this.overrideCtrl[anim.ToString()] != clip)
    //		{
    //			this.StoreLayerWeights();
    //			this.overrideCtrl[anim.ToString()] = clip;
    //		}
    //		if (!WaitNextFrame)
    //		{
    //			this.RestoreLayerWeights();
    //			this.animator.Play(this.tempHash, 0, this.curTime);
    //		}
    //	}

    //	public void SetOverrideClip(AnimationClip clip, string animName)
    //	{
    //		this.overrideCtrl[animName] = clip;
    //	}

    //	public static void ClearAnimClipsDic()
    //	{
    //		foreach (KeyValuePair<string, Dictionary<string, AnimClipData>> current in AnimCtrlScript.AnimClipsDic)
    //		{
    //			foreach (KeyValuePair<string, AnimClipData> current2 in current.Value)
    //			{
    //				AnimClipData value = current2.Value;
    //				value.Clear();
    //			}
    //		}
    //		AnimCtrlScript.AnimClipsDic.Clear();
    //	}

    //	public bool ActiveAnimFromAssetBundle(string actorName, string AnimName, out bool IsEndToH, float crossTime, bool useUp = false, bool Normalize = false)
    //	{
    //		this.m_bPlayFromBegining = Normalize;
    //		actorName = this.GetOriActorName(actorName);
    //		IsEndToH = false;
    //		if (string.IsNullOrEmpty(actorName))
    //		{
    //			actorName = this.name;
    //		}
    //		if (!AnimCtrlScript.AnimClipsDic.ContainsKey(actorName))
    //		{
    //			AnimCtrlScript.AnimClipsDic.Add(actorName, new Dictionary<string, AnimClipData>());
    //		}
    //		Dictionary<string, AnimClipData> dictionary = AnimCtrlScript.AnimClipsDic[actorName];
    //		if (!dictionary.ContainsKey(AnimName))
    //		{
    //			dictionary.Add(AnimName, new AnimClipData(actorName, AnimName));
    //		}
    //		AnimClipData animClipData = dictionary[AnimName];
    //		animClipData.TryLoad();
    //		AnimCtrlScript.OverrideAnim overrideAnim = AnimCtrlScript.OverrideAnim.OverridableAnim;
    //		if (AnimName[AnimName.Length - 2] == '_')
    //		{
    //			int num = AnimName.Length - 2;
    //			if (num == AnimName.IndexOf("_Q"))
    //			{
    //				overrideAnim = AnimCtrlScript.OverrideAnim.OverridableAnim_Q;
    //			}
    //			else if (num == AnimName.IndexOf("_L"))
    //			{
    //				overrideAnim = AnimCtrlScript.OverrideAnim.OverridableAnim_L;
    //			}
    //			else if (num == AnimName.IndexOf("_Z"))
    //			{
    //				overrideAnim = AnimCtrlScript.OverrideAnim.OverridableAnim_Z;
    //			}
    //			else if (num == AnimName.IndexOf("_H"))
    //			{
    //				overrideAnim = AnimCtrlScript.OverrideAnim.OverridableAnim_H;
    //			}
    //		}
    //		if (!this.animCrossFades.ContainsKey(overrideAnim))
    //		{
    //			this.animCrossFades.Add(overrideAnim, 0.01f);
    //		}
    //		this.animCrossFades[overrideAnim] = crossTime;
    //		int num2 = AnimName.LastIndexOf("_Q");
    //		if (num2 > -1 && num2 == AnimName.Length - 2)
    //		{
    //			string str = AnimName.Substring(0, num2);
    //			string animName = str + "_Z";
    //			string animName2 = str + "_H";
    //			if (!this.OverrideAnimClip(dictionary, actorName, animName, AnimCtrlScript.OverrideAnim.OverridableAnim_Z, true))
    //			{
    //				UnityEngine.Debug.LogError("没有_Z动作！！！！");
    //				return false;
    //			}
    //			bool flag = this.OverrideAnimClip(dictionary, actorName, animName2, AnimCtrlScript.OverrideAnim.OverridableAnim_H, true);
    //			if (flag)
    //			{
    //				IsEndToH = true;
    //			}
    //		}
    //		if (animClipData.AnimClip == null)
    //		{
    //			animClipData.LoadAnimClip(AnimClipData.Mode.ChangeAndPlay, this, overrideAnim, crossTime, useUp);
    //		}
    //		else
    //		{
    //			this.ActiveAnimFromClip(animClipData.AnimClip, overrideAnim, crossTime, useUp);
    //		}
    //		return true;
    //	}

    //	public static AnimClipData LoadClipFromAssetBundle(AnimCtrlScript acs, string actorName, string AnimName)
    //	{
    //		bool flag = false;
    //		if (string.IsNullOrEmpty(actorName) && acs != null)
    //		{
    //			actorName = acs.name;
    //		}
    //		actorName = acs.GetOriActorName(actorName);
    //		string animFilePath = UtilFun.GetAnimFilePath(actorName, AnimName);
    //		if (File.Exists(animFilePath))
    //		{
    //			flag = true;
    //		}
    //		if (!flag)
    //		{
    //			if (!animFilePath.Contains("_H"))
    //			{
    //				string message = string.Concat(new string[]
    //				{
    //					"Error : ",
    //					acs.name,
    //					" 没有找到 ",
    //					animFilePath,
    //					" 这个动作"
    //				});
    //				UnityEngine.Debug.LogError(message);
    //			}
    //			return null;
    //		}
    //		if (!AnimCtrlScript.AnimClipsDic.ContainsKey(actorName))
    //		{
    //			AnimCtrlScript.AnimClipsDic.Add(actorName, new Dictionary<string, AnimClipData>());
    //		}
    //		Dictionary<string, AnimClipData> dictionary = AnimCtrlScript.AnimClipsDic[actorName];
    //		if (!dictionary.ContainsKey(AnimName))
    //		{
    //			dictionary.Add(AnimName, new AnimClipData(actorName, AnimName));
    //		}
    //		AnimClipData animClipData = dictionary[AnimName];
    //		AnimCtrlScript.OverrideAnim overrideAnim = AnimCtrlScript.OverrideAnim.OverridableAnim;
    //		if (AnimName.Contains("_Q"))
    //		{
    //			overrideAnim = AnimCtrlScript.OverrideAnim.OverridableAnim_Q;
    //		}
    //		else if (AnimName.Contains("_L"))
    //		{
    //			overrideAnim = AnimCtrlScript.OverrideAnim.OverridableAnim_L;
    //		}
    //		else if (AnimName.Contains("_H"))
    //		{
    //			overrideAnim = AnimCtrlScript.OverrideAnim.OverridableAnim_H;
    //		}
    //		else if (AnimName.Contains("_Z"))
    //		{
    //			overrideAnim = AnimCtrlScript.OverrideAnim.OverridableAnim_Z;
    //		}
    //		int num = AnimName.LastIndexOf("_Q");
    //		if (num > -1)
    //		{
    //			string str = AnimName.Substring(0, num);
    //			string animName = str + "_Z";
    //			string animName2 = str + "_H";
    //			if (AnimCtrlScript.LoadClipFromAssetBundle(acs, actorName, animName) == null)
    //			{
    //				UnityEngine.Debug.LogError(AnimName + "没有_Z动作！！！！");
    //				return null;
    //			}
    //			AnimCtrlScript.LoadClipFromAssetBundle(acs, actorName, animName2);
    //		}
    //		if (animClipData.AnimClip == null)
    //		{
    //			animClipData.LoadAnimClip(AnimClipData.Mode.None, acs, overrideAnim, 0.1f, false);
    //		}
    //		return animClipData;
    //	}

    //	public static void UnLoadClipFromAssetBundle(AnimCtrlScript acs, string AnimName)
    //	{
    //		string oriActorName = acs.GetOriActorName(null);
    //		if (!AnimCtrlScript.AnimClipsDic.ContainsKey(oriActorName))
    //		{
    //			UnityEngine.Debug.LogError("不存在 人名key " + oriActorName);
    //			return;
    //		}
    //		Dictionary<string, AnimClipData> dictionary = AnimCtrlScript.AnimClipsDic[oriActorName];
    //		if (!dictionary.ContainsKey(AnimName))
    //		{
    //			UnityEngine.Debug.LogError("不存在 动作key " + AnimName);
    //			return;
    //		}
    //		AnimClipData animClipData = dictionary[AnimName];
    //		if (animClipData.TryUnload())
    //		{
    //			dictionary.Remove(AnimName);
    //		}
    //	}

    //	private bool OverrideAnimClip(Dictionary<string, AnimClipData> dic, string actorName, string animName, AnimCtrlScript.OverrideAnim overAnim, bool NeedOverride = true)
    //	{
    //		bool flag = false;
    //		if (string.IsNullOrEmpty(actorName))
    //		{
    //			actorName = base.name;
    //		}
    //		string animFilePath = UtilFun.GetAnimFilePath(actorName, animName);
    //		if (File.Exists(animFilePath))
    //		{
    //			flag = true;
    //		}
    //		if (!flag)
    //		{
    //			return false;
    //		}
    //		if (!dic.ContainsKey(animName))
    //		{
    //			dic.Add(animName, new AnimClipData(actorName, animName));
    //		}
    //		AnimClipData animClipData = dic[animName];
    //		if (NeedOverride)
    //		{
    //			if (animClipData.AnimClip == null)
    //			{
    //				animClipData.LoadAnimClip(AnimClipData.Mode.Change, this, overAnim, 0.1f, false);
    //			}
    //			else
    //			{
    //				this.SetOverrideClip(animClipData.AnimClip, overAnim, false);
    //			}
    //		}
    //		return flag;
    //	}

    //	[DebuggerHidden]
    //	private IEnumerator WaitFrameAndPlayAnim(AnimationClip clip, AnimCtrlScript.OverrideAnim anim = AnimCtrlScript.OverrideAnim.OverridableAnim, float crossTime = 0.1f, bool useUp = false)
    //	{
    //		AnimCtrlScript.<WaitFrameAndPlayAnim>c__Iterator3A <WaitFrameAndPlayAnim>c__Iterator3A = new AnimCtrlScript.<WaitFrameAndPlayAnim>c__Iterator3A();
    //		<WaitFrameAndPlayAnim>c__Iterator3A.useUp = useUp;
    //		<WaitFrameAndPlayAnim>c__Iterator3A.anim = anim;
    //		<WaitFrameAndPlayAnim>c__Iterator3A.crossTime = crossTime;
    //		<WaitFrameAndPlayAnim>c__Iterator3A.<$>useUp = useUp;
    //		<WaitFrameAndPlayAnim>c__Iterator3A.<$>anim = anim;
    //		<WaitFrameAndPlayAnim>c__Iterator3A.<$>crossTime = crossTime;
    //		<WaitFrameAndPlayAnim>c__Iterator3A.<>f__this = this;
    //		return <WaitFrameAndPlayAnim>c__Iterator3A;
    //	}

    //	private int GetUpLayerIndex(ref bool hasUp)
    //	{
    //		int num = 0;
    //		if (this.animator.layerCount > 2)
    //		{
    //			num = 2;
    //		}
    //		else if (this.animator.layerCount > 1)
    //		{
    //			num = 1;
    //		}
    //		hasUp = (num >= 1);
    //		return num;
    //	}

    //	private int GetLayerNum(bool useUp)
    //	{
    //		int result = 0;
    //		if (useUp)
    //		{
    //			if (this.animator.layerCount > 2)
    //			{
    //				result = 2;
    //			}
    //			else if (this.animator.layerCount > 1)
    //			{
    //				result = 1;
    //			}
    //		}
    //		return result;
    //	}

    //	public void ActiveAnim(string stateBoolName, bool useUp = false, bool NeedSample = false, bool ForceActive = false)
    //	{
    //		if (!this.animator)
    //		{
    //			return;
    //		}
    //		bool flag = false;
    //		if (!ForceActive)
    //		{
    //			flag = AnimCtrlScript.IsActive(this.animator, stateBoolName, NeedSample);
    //			if (stateBoolName == "BeiZhan" && useUp)
    //			{
    //				flag = false;
    //			}
    //		}
    //		if (!flag)
    //		{
    //			int key = (!useUp) ? 0 : 1;
    //			if (this.remainingRSBs.ContainsKey(key) && this.remainingRSBs[key] != null)
    //			{
    //				this.animator.SetBool(this.remainingRSBs[key].stateBoolName, false);
    //			}
    //			if (!stateBoolName.Contains("Zuo"))
    //			{
    //				this.animator.SetBool("Up", useUp);
    //			}
    //			this.animator.SetBool(stateBoolName, true);
    //			ResetStateBool value = new ResetStateBool(stateBoolName);
    //			this.remainingRSBs[key] = value;
    //			if (NeedSample)
    //			{
    //				base.StartCoroutine(this.ActiveAndSample(stateBoolName, 0));
    //			}
    //		}
    //	}

    public void ActiveAnimCrossFade(string stateName, bool useUp = false, float crossTime = 0.1f, bool NeedChangeCrossTime = true)
    {
        //if (!this.animator)
        //{
        //    return;
        //}
        //int layerNum = this.GetLayerNum(useUp);
        //if (layerNum >= this.animator.layerCount || layerNum <= -1)
        //{
        //    return;
        //}
        //bool flag = true;
        //int upLayerIndex = this.GetUpLayerIndex(ref flag);
        //if (!useUp)
        //{
        //    if (flag)
        //    {
        //        this.animator.SetLayerWeight(upLayerIndex, 0f);
        //    }
        //}
        //else
        //{
        //    if (layerNum == 2)
        //    {
        //    }
        //    this.animator.SetLayerWeight(layerNum, 1f);
        //}
        //if (crossTime < 0f)
        //{
        //    crossTime = 0.1f;
        //}
        //if (NeedChangeCrossTime)
        //{
        //    AnimatorStateInfo currentAnimatorStateInfo = this.animator.GetCurrentAnimatorStateInfo(0);
        //    if (!this.IsMainChar)
        //    {
        //        bool flag2 = true;
        //        if (!useUp && currentAnimatorStateInfo.IsName("yidongState.ZhanLi"))
        //        {
        //            crossTime *= 0.1f;
        //            flag2 = false;
        //        }
        //        if (flag2)
        //        {
        //            SlowLoopAnimSet.Instance.ChangedCrossTime(stateName, ref crossTime);
        //        }
        //    }
        //    else if (!currentAnimatorStateInfo.IsName("yidongState.ZhanLi"))
        //    {
        //        AnimatorClipInfo[] currentAnimatorClipInfo = this.animator.GetCurrentAnimatorClipInfo(0);
        //        for (int i = 0; i < currentAnimatorClipInfo.Length; i++)
        //        {
        //            string name = currentAnimatorClipInfo[i].clip.name;
        //            if (SlowLoopAnimSet.Instance.SlowAnims.ContainsKey(name))
        //            {
        //                SlowLoopAnimSet.Instance.ChangedCrossTime(name, ref crossTime);
        //                break;
        //            }
        //        }
        //    }
        //}
        //if (crossTime > 0.0001f)
        //{
        //    if (this.m_bPlayFromBegining)
        //    {
        //        this.m_bPlayFromBegining = false;
        //        this.animator.CrossFade(stateName, crossTime, layerNum, 0f);
        //    }
        //    else
        //    {
        //        this.animator.CrossFade(stateName, crossTime, layerNum);
        //    }
        //}
        //else
        //{
        //    this.animator.Play(stateName, layerNum);
        //}
        //AnimWithoutClothSet.Instance.SetClothByDic(base.gameObject, stateName, 0.32f, 0f);
    }

    //	public void CancelAnim(string stateBoolName, bool useUp = false, float crossTime = -1f, bool isEndToH = false, bool DynamicAnim = false)
    //	{
    //		if (!this.animator)
    //		{
    //			return;
    //		}
    //		string text = string.Empty;
    //		if (isEndToH)
    //		{
    //			if (!DynamicAnim)
    //			{
    //				if (string.IsNullOrEmpty(stateBoolName))
    //				{
    //					UnityEngine.Debug.LogError("Error : AnimCtrlScript.CancelAnim  stateBoolName == null");
    //					return;
    //				}
    //				int num = stateBoolName.LastIndexOf("_Q");
    //				if (num > 0)
    //				{
    //					string str = stateBoolName.Substring(0, num);
    //					text = str + "_H";
    //				}
    //				else
    //				{
    //					text = stateBoolName;
    //				}
    //			}
    //			else
    //			{
    //				text = AnimCtrlScript.OverrideAnim.OverridableAnim_H.ToString();
    //			}
    //		}
    //		else if (!useUp)
    //		{
    //			text = "ZhanLi";
    //		}
    //		else
    //		{
    //			text = "New State";
    //		}
    //		if (crossTime < 0f)
    //		{
    //			crossTime = 0.1f;
    //		}
    //		if (!this.IsMainChar)
    //		{
    //			if (!useUp)
    //			{
    //				SlowLoopAnimSet.Instance.ChangedCrossTime(stateBoolName, ref crossTime);
    //			}
    //		}
    //		else
    //		{
    //			SlowLoopAnimSet.Instance.ChangedCrossTime(stateBoolName, ref crossTime);
    //		}
    //		this.ActiveAnimCrossFade(text, useUp, crossTime, false);
    //		ShroudWeight.Active(base.gameObject, 0.23f, 100f);
    //	}

    //	private void LateUpdate()
    //	{
    //		if (this.remainingRSBs.Count < 1)
    //		{
    //			return;
    //		}
    //		this.NeedRemoveKey.Clear();
    //		foreach (KeyValuePair<int, ResetStateBool> current in this.remainingRSBs)
    //		{
    //			if (current.Value.SetFalse(this.animator))
    //			{
    //				this.NeedRemoveKey.Add(current.Key);
    //			}
    //		}
    //		foreach (int current2 in this.NeedRemoveKey)
    //		{
    //			this.remainingRSBs.Remove(current2);
    //		}
    //	}

    //	[DebuggerHidden]
    //	public IEnumerator ActiveAndSample(string stateBoolName, int LayerIndex = 0)
    //	{
    //		AnimCtrlScript.<ActiveAndSample>c__Iterator3B <ActiveAndSample>c__Iterator3B = new AnimCtrlScript.<ActiveAndSample>c__Iterator3B();
    //		<ActiveAndSample>c__Iterator3B.stateBoolName = stateBoolName;
    //		<ActiveAndSample>c__Iterator3B.<$>stateBoolName = stateBoolName;
    //		<ActiveAndSample>c__Iterator3B.<>f__this = this;
    //		return <ActiveAndSample>c__Iterator3B;
    //	}

    //	public static bool IsActive(Animator animator, string stateBoolName)
    //	{
    //		bool result = false;
    //		List<AnimatorClipInfo> list = new List<AnimatorClipInfo>();
    //		for (int i = 0; i < animator.layerCount; i++)
    //		{
    //			AnimatorClipInfo[] currentAnimatorClipInfo = animator.GetCurrentAnimatorClipInfo(i);
    //			list.AddRange(currentAnimatorClipInfo);
    //		}
    //		stateBoolName = stateBoolName.ToLower();
    //		foreach (AnimatorClipInfo current in list)
    //		{
    //			if (current.clip.name.ToLower().Contains(stateBoolName))
    //			{
    //				result = true;
    //				break;
    //			}
    //		}
    //		return result;
    //	}

    //	public static bool IsActive(Animator animator, string stateBoolName, out int layerIndex, out AnimatorClipInfo outAnifo)
    //	{
    //		layerIndex = -1;
    //		outAnifo = default(AnimatorClipInfo);
    //		if (string.IsNullOrEmpty(stateBoolName))
    //		{
    //			return false;
    //		}
    //		bool flag = false;
    //		Dictionary<int, AnimatorClipInfo[]> dictionary = new Dictionary<int, AnimatorClipInfo[]>();
    //		for (int i = 0; i < animator.layerCount; i++)
    //		{
    //			AnimatorClipInfo[] currentAnimatorClipInfo = animator.GetCurrentAnimatorClipInfo(i);
    //			dictionary[i] = currentAnimatorClipInfo;
    //		}
    //		if (stateBoolName[stateBoolName.Length - 1] == 'Q' && stateBoolName[stateBoolName.Length - 2] == '_')
    //		{
    //			stateBoolName = stateBoolName.Substring(0, stateBoolName.Length - 2);
    //		}
    //		foreach (KeyValuePair<int, AnimatorClipInfo[]> current in dictionary)
    //		{
    //			AnimatorClipInfo[] value = current.Value;
    //			for (int j = 0; j < value.Length; j++)
    //			{
    //				AnimatorClipInfo animatorClipInfo = value[j];
    //				if (animatorClipInfo.clip.name.Contains(stateBoolName))
    //				{
    //					flag = true;
    //					outAnifo = animatorClipInfo;
    //					break;
    //				}
    //			}
    //			if (flag)
    //			{
    //				layerIndex = current.Key;
    //				break;
    //			}
    //		}
    //		return flag;
    //	}

    //	public static bool IsActive(Animator animator, string stateBoolName, bool NeedSample)
    //	{
    //		bool flag = AnimCtrlScript.IsActive(animator, stateBoolName);
    //		if (flag && NeedSample)
    //		{
    //			animator.speed = 0f;
    //		}
    //		return flag;
    //	}

    //	public static bool IsActive(Animator animator, string stateBoolName, out int layerIndex, out AnimatorClipInfo outAnifo, bool NeedSample)
    //	{
    //		bool flag = AnimCtrlScript.IsActive(animator, stateBoolName, out layerIndex, out outAnifo);
    //		if (flag && NeedSample)
    //		{
    //			animator.speed = 0f;
    //		}
    //		return flag;
    //	}
}
