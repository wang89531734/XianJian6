using SoftStar;
using SoftStar.Pal6;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CharactersManager : MonoBehaviour
{
	private static CharactersManager instance;

	public List<Texture> MapIcons = new List<Texture>(5);

	public Texture T_Icon;

	public Dictionary<ObjType, Texture> TypeIcons = new Dictionary<ObjType, Texture>();

    public Dictionary<PalGameObjectBase, MapTarget> Characters = new Dictionary<PalGameObjectBase, MapTarget>(6);

    public List<PalGameObjectBase> Monsters = new List<PalGameObjectBase>();

	public List<PalGameObjectBase> Triggers = new List<PalGameObjectBase>();

	public List<PalGameObjectBase> Objs = new List<PalGameObjectBase>();

	public static CharactersManager Instance
	{
		get
		{
			if (CharactersManager.instance == null)
			{
				CharactersManager.Initialize();
			}
			return CharactersManager.instance;
		}
	}

	public static void Initialize()
	{
		PalMain gameMain = PalMain.GameMain;
		if (gameMain != null)
		{
			CharactersManager.instance = gameMain.GetComponent<CharactersManager>();
			if (CharactersManager.instance == null)
			{
				CharactersManager.instance = gameMain.gameObject.AddComponent<CharactersManager>();
			}
			CharactersManager.instance.InitTex();
		}
		//GameStateManager.AddInitStateFun(GameState.Cutscene, new GameStateManager.void_fun(CharactersManager.Instance.InitCutscene));
		//GameStateManager.AddEndStateFun(GameState.Cutscene, new GameStateManager.void_fun(CharactersManager.Instance.EndCutscene));
		//GameStateManager.AddInitStateFun(GameState.Battle, new GameStateManager.void_fun(CharactersManager.Instance.InitBattle));
		//GameStateManager.AddEndStateFun(GameState.Battle, new GameStateManager.void_fun(CharactersManager.Instance.EndBattle));
	}

	private void InitBattle()
	{
		this.SetAllMonsterActive(false);
		this.SetAllTriggerActive(false);
		this.SetAllObjsActive(false);
	}

	private void EndBattle()
	{
		this.SetAllMonsterActive(true);
		this.SetAllTriggerActive(true);
		this.SetAllObjsActive(true);
	}

	private void InitCutscene()
	{
		this.SetAllMonsterActive(false);
		this.SetAllTriggerActive(false);
		this.SetAllObjsActive(false);
	}

	private void EndCutscene()
	{
		this.SetAllMonsterActive(true);
		this.SetAllTriggerActive(true);
		this.SetAllObjsActive(true);
	}

	private void SetAllMonsterActive(bool bActive)
	{
		for (int i = 0; i < this.Monsters.Count; i++)
		{
			PalGameObjectBase palGameObjectBase = this.Monsters[i];
			if (!(palGameObjectBase == null))
			{
				palGameObjectBase.gameObject.SetActive(bActive);
			}
		}
	}

	private void SetAllTriggerActive(bool bActive)
	{
		for (int i = 0; i < this.Triggers.Count; i++)
		{
			PalGameObjectBase palGameObjectBase = this.Triggers[i];
			if (!(palGameObjectBase == null))
			{
				Collider[] componentsInChildren = palGameObjectBase.GetComponentsInChildren<Collider>(true);
				for (int j = 0; j < componentsInChildren.Length; j++)
				{
					Collider collider = componentsInChildren[j];
					collider.enabled = bActive;
				}
			}
		}
	}

	private void SetAllObjsActive(bool bActive)
	{
		for (int i = 0; i < this.Objs.Count; i++)
		{
			PalGameObjectBase palGameObjectBase = this.Objs[i];
			if (!(palGameObjectBase == null))
			{
				palGameObjectBase.gameObject.SetActive(bActive);
			}
		}
	}

	public void InitTex()
	{
		if (this.MapIcons.Count < 1)
		{
			string[] array = new string[]
			{
				string.Empty,
				string.Empty,
				string.Empty
			};
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i];
				this.MapIcons.Add(null);
			}
		}
		this.TypeIcons.Clear();
		for (int j = 0; j < this.MapIcons.Count; j++)
		{
			Texture value = this.MapIcons[j];
			if (j == 31 && Langue.IsLanguage2)
			{
				value = this.T_Icon;
			}
			this.TypeIcons.Add((ObjType)j, value);
		}
	}

	//public static bool ExistCharacter(PalGameObjectBase npc)
	//{
	//	//Dictionary<PalGameObjectBase, MapTarget> characters = CharactersManager.Instance.Characters;
	//	return characters.ContainsKey(npc);
	//}

	public static void AddCharacter(PalGameObjectBase npc)
	{
        Dictionary<PalGameObjectBase, MapTarget> characters = CharactersManager.Instance.Characters;
        if (characters.ContainsKey(npc))
        {
            Debug.LogError("已经在Characters中包含了");
            return;
        }
        MapTarget value = MapTarget.CreateNew(npc);
        characters.Add(npc, value);
        npc.DestroyEvent = (Action<PalGameObjectBase>)Delegate.Combine(npc.DestroyEvent, new Action<PalGameObjectBase>(CharactersManager.Instance.ObjdestroyEvent));
	}

	public static void AddMonster(PalGameObjectBase monster)
	{
		if (CharactersManager.Instance.Monsters.Contains(monster))
		{
			return;
		}
		//if (monster.GetComponent<DontSetActiveByState>() != null)
		//{
		//	return;
		//}
		CharactersManager.Instance.Monsters.Add(monster);
	}

	public static void AddObj(PalGameObjectBase obj)
	{
		CharactersManager.Instance.Objs.Add(obj);
	}

	public static void AddTrigger(PalGameObjectBase trigger)
	{
		if (CharactersManager.Instance.Triggers.Contains(trigger))
		{
			return;
		}
		//if (trigger.GetComponent<SetActiveByFlag>() != null)
		//{
		//	return;
		//}
		CharactersManager.Instance.Triggers.Add(trigger);
	}

	public static void Clear()
	{
		CharactersManager.Instance.Objs.Clear();
		CharactersManager.Instance.Monsters.Clear();
		CharactersManager.Instance.Triggers.Clear();
		//CharactersManager.Instance.Characters.Clear();
	}

	private void ObjdestroyEvent(PalGameObjectBase obj)
	{
	//	this.Characters.Remove(obj);
	}
}
