using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SoftStar.Pal6
{
	public class DOBJLayer : MonoBehaviour
	{
		[NonSerialized]
		public int LayerIndex;

		public int orderIndex;

		public string layerName = string.Empty;

		public string prefabName = string.Empty;

		public List<int> enableNecessaryFlgs = new List<int>();

		//public List<ConditionalJudgment> enableNecessaryConditions = new List<ConditionalJudgment>();

		public List<int> disableSufficientFlags = new List<int>();

		//public List<ConditionalJudgment> disableSufficientConditions = new List<ConditionalJudgment>();

		public bool childActive = true;

		public List<PalGameObjectBase> palObjects = new List<PalGameObjectBase>();

		public string Remark;

		private TimeSpan loadTime;

		private List<PalGameObjectBase> palObjs = new List<PalGameObjectBase>();

		private bool isLoadOver;

		public bool IsLoadOver
		{
			get
			{
				return this.isLoadOver;
			}
		}

		//public static bool Judgment(GameFileStream stream)
		//{
		//	bool flag = false;
		//	int num = stream.ReadInt();
		//	if (num > 0)
		//	{
		//		flag = true;
		//	}
		//	for (int i = 0; i < num; i++)
		//	{
		//		//ConditionalJudgment.enum_JudgmentType type = (ConditionalJudgment.enum_JudgmentType)stream.ReadInt();
		//		//int value = stream.ReadInt();
		//		//int idx = stream.ReadInt();
		//		//int flag2 = FlagManager.GetFlag(idx);
		//		//string text = null;
		//		//if (!new ConditionalJudgment(type, value).Judgment(flag2, out text))
		//		//{
		//		//	flag = false;
		//		//}
		//	}
		//	num = stream.ReadInt();
		//	bool flag3 = true;
		//	for (int j = 0; j < num; j++)
		//	{
		//		//ConditionalJudgment.enum_JudgmentType type2 = (ConditionalJudgment.enum_JudgmentType)stream.ReadInt();
		//		//int value2 = stream.ReadInt();
		//		//int idx2 = stream.ReadInt();
		//		//int flag4 = FlagManager.GetFlag(idx2);
		//		//string text2 = null;
		//		//if (!new ConditionalJudgment(type2, value2).Judgment(flag4, out text2))
		//		//{
		//		//	flag3 = false;
		//		//}
		//	}
		//	return !flag && flag3;
		//}

		//public void WriteCondition(GameFileStream stream)
		//{
		//	stream.WriteInt(this.disableSufficientFlags.Count);
		//	for (int i = 0; i < this.disableSufficientFlags.Count; i++)
		//	{
		//		stream.WriteInt((int)this.disableSufficientConditions[i].type);
		//		stream.WriteInt(this.disableSufficientConditions[i].value);
		//		stream.WriteInt(this.disableSufficientFlags[i]);
		//	}
		//	stream.WriteInt(this.enableNecessaryFlgs.Count);
		//	for (int j = 0; j < this.enableNecessaryFlgs.Count; j++)
		//	{
		//		stream.WriteInt((int)this.enableNecessaryConditions[j].type);
		//		stream.WriteInt(this.enableNecessaryConditions[j].value);
		//		stream.WriteInt(this.enableNecessaryFlgs[j]);
		//	}
		//}

		public void SetActiveAllModels(bool bActive)
		{
			for (int i = 0; i < this.palObjects.Count; i++)
			{
				PalGameObjectBase palGameObjectBase = this.palObjects[i];
				if (!(palGameObjectBase == null))
				{
					//UtilFun.SetActive(palGameObjectBase.gameObject, bActive);
				}
			}
		}

		public void UnLoadAllModels()
		{
			foreach (PalGameObjectBase current in this.palObjects)
			{
				if (current.model != null)
				{
					UnityEngine.Object.DestroyImmediate(current.model);
				}
			}
		}

		public void LoadAllModels()
		{
			//if (Application.isPlaying && PalMain.IsLow)
			//{
			//	bool flag = true;
			//	int palMapLevel = UtilFun.GetPalMapLevel(SceneManager.GetActiveScene().buildIndex);
			//	string name = base.gameObject.name;
			//	if (palMapLevel == 1)
			//	{
			//		if (name.Contains("local_01") || name.Contains("AfterFly") || name.Contains("nonlocal_01") || name.Contains("guest") || name.Contains("animal") || name.Contains("LuRenNPC"))
			//		{
			//			flag = false;
			//		}
			//	}
			//	else if (palMapLevel == 13 && (name.Contains("JNnpcA09") || name.Contains("JNanimal") || name.Contains("afterqihun") || name.Contains("afterlangyin")))
			//	{
			//		flag = false;
			//	}
			//	if (!flag)
			//	{
			//		this.palObjs.Clear();
			//		this.palObjects.Clear();
			//		this.isLoadOver = true;
			//		PalGameObjectBase[] componentsInChildren = base.GetComponentsInChildren<PalGameObjectBase>(true);
			//		for (int i = 0; i < componentsInChildren.Length; i++)
			//		{
			//			PalGameObjectBase palGameObjectBase = componentsInChildren[i];
			//			UnityEngine.Object.Destroy(palGameObjectBase.gameObject);
			//		}
			//		//EntityManager.JudgeLoadOver(this);
			//		return;
			//	}
			//}
			this.loadTime = DateTime.Now.TimeOfDay;
			//DynamicObjsDataManager.Instance.AddLayer(this);
			this.palObjs.Clear();
			PalGameObjectBase[] componentsInChildren2 = base.GetComponentsInChildren<PalGameObjectBase>(true);
			this.palObjects.Clear();
			this.palObjects.AddRange(componentsInChildren2);
			if (this.palObjects.Count < 1)
			{
				Debug.LogWarning(base.name + "里面没有东西");
				this.LoadOver();
				return;
			}
			this.palObjs.AddRange(this.palObjects);
			if (this.palObjects.Count < 1)
			{
				Debug.LogWarning(base.name + "里面没有东西");
				this.LoadOver();
			}
			//if (!EntityManager.LoadTogether)
			//{
			//	PalGameObjectBase palGameObjectBase2 = this.palObjs[0];
			//	palGameObjectBase2.dobjLayer = this;
			//	palGameObjectBase2.LoadModel();
			//}
			//else
			//{
			//	for (int j = 0; j < this.palObjects.Count; j++)
			//	{
			//		PalGameObjectBase palGameObjectBase3 = this.palObjects[j];
			//		if (palGameObjectBase3 == null)
			//		{
			//			Debug.LogError(base.name + " palObjects " + j.ToString() + " == null");
			//		}
			//		else
			//		{
			//			palGameObjectBase3.dobjLayer = this;
			//			palGameObjectBase3.LoadModel();
			//		}
			//	}
			//}
		}

		public void GetNotLoadContent()
		{
			foreach (PalGameObjectBase current in this.palObjs)
			{
				Debug.Log(current.name);
			}
		}

		public void ContinueLoad()
		{
			if (this.palObjs.Count < 1)
			{
				return;
			}
			this.palObjs[0].LoadModelEnd(null);
		}

		public void ShowLoadingObjs()
		{
			for (int i = 0; i < this.palObjs.Count; i++)
			{
				PalGameObjectBase palGameObjectBase = this.palObjs[i];
				string text = "\t\t" + palGameObjectBase.name;
			}
		}

        /// <summary>
        /// 判断加载完毕
        /// </summary>
        /// <param name="obj"></param>
		public void JudgeLoadOver(PalGameObjectBase obj)
		{
			//if (!EntityManager.LoadTogether)
			//{
			//	if (this.palObjs.Count < 1)
			//	{
			//		this.LoadOver();
			//		return;
			//	}
			//	this.palObjs.RemoveAt(0);
			//	if (this.palObjs.Count < 1)
			//	{
			//		this.LoadOver();
			//		return;
			//	}
			//	PalGameObjectBase palGameObjectBase = this.palObjs[0];
			//	palGameObjectBase.dobjLayer = this;
			//	palGameObjectBase.LoadModel();
			//}
			//else
			//{
			//	this.palObjs.Remove(obj);
			//	if (this.palObjs.Count < 1)
			//	{
			//		this.LoadOver();
			//		return;
			//	}
			//}
		}

		public void LoadOver()
		{
			//foreach (Transform transform in base.transform)
			//{
			//	UtilFun.SetActive(transform.gameObject, true);
			//}
			//PalGameObjectBase[] componentsInChildren = base.GetComponentsInChildren<PalGameObjectBase>(true);
			//for (int i = 0; i < componentsInChildren.Length; i++)
			//{
			//	PalGameObjectBase palGameObjectBase = componentsInChildren[i];
			//	if (!(palGameObjectBase == null))
			//	{
			//		GameObject gameObject = palGameObjectBase.gameObject;
			//		if (!gameObject.activeSelf)
			//		{
			//			UtilFun.SetActive(gameObject, true);
			//		}
			//	}
			//}
			//UtilFun.SetActive(base.gameObject, true);
			//if (Application.isPlaying)
			//{
			//	DistanceCullManager.Instance.SetLayer(base.gameObject);
			//}
			//this.loadTime = DateTime.Now.TimeOfDay.Subtract(this.loadTime);
			//this.isLoadOver = true;
			//EntityManager.JudgeLoadOver(this);
		}

		public static int ComparionUp(DOBJLayer A, DOBJLayer B)
		{
			return (A.orderIndex < B.orderIndex) ? -1 : 1;
		}

		public void Clear()
		{
			Transform transform = base.transform;
			for (int i = 0; i < transform.childCount; i++)
			{
				Transform child = transform.GetChild(i);
				if (!(child == null))
				{
					PalGameObjectBase component = child.GetComponent<PalGameObjectBase>();
					if (!(component == null))
					{
						component.Clear();
					}
				}
			}
		}
	}
}
