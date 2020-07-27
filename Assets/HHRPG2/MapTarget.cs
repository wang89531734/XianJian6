using Funfia.File;
using SoftStar.Pal6;
using System;
using UnityEngine;

public class MapTarget : MonoBehaviour
{
	public const float BaseRate = 140f;

	public PalGameObjectBase Gob;

	public Texture Tex;

	public Transform targetTF;

	private Material m_material;

	private bool m_isMain;

	private Transform mapInfoTF;

	private Transform MapInfoTF
	{
		get
		{
			//if (this.mapInfoTF == null && PalMain.MapInfo != null)
			//{
			//	this.mapInfoTF = PalMain.MapInfo.transform;
			//}
			return this.mapInfoTF;
		}
	}

	public static MapTarget CreateNew(PalGameObjectBase Gob)
	{
        //if (Gob == null)
        //{
        //	return null;
        //}
        ////Transform transform = MapWatch.Instance.Points.transform;
        //string name = Gob.name + "_MT";
        ////Transform transform2 = transform.Find(name);
        ////if (transform2 != null)
        ////{
        ////	MapTarget component = transform2.GetComponent<MapTarget>();
        ////	if (component != null && component.Gob == Gob)
        ////	{
        ////		Debug.LogError("Error : 已经存在一个对应 " + Gob.name + " 的MapTarget 删除掉");
        ////		UnityEngine.Object.Destroy(transform2.gameObject);
        ////	}
        ////}
        //GameObject gameObject = FileLoader.LoadObjectFromFile<GameObject>("UI/MapTarget".ToAssetBundlePath(), true, true);
        //GameObject gameObject2;
        //if (gameObject == null)
        //{
        //	Debug.LogError("Error : MapTarget UI/MapTarget 没有找到");
        //	gameObject2 = new GameObject(name);
        //}
        //else
        //{
        //	gameObject2 = gameObject;
        //}
        //gameObject2.name = name;
        ////if (MapWatch.Instance != null)
        ////{
        ////	gameObject2.transform.parent = MapWatch.Instance.Points.transform;
        ////}
        ////else
        ////{
        ////	Debug.LogError("没有找到MapWatch.Instance");
        ////}
        //gameObject2.transform.localScale = new Vector3(100f, 100f, 1f);
        //MapTarget mapTarget = gameObject2.GetComponent<MapTarget>();
        //if (mapTarget == null)
        //{
        //	mapTarget = gameObject2.AddComponent<MapTarget>();
        //}
        //mapTarget.Init(Gob);
        //return mapTarget;
        return null;
    }

	private void ObjdestroyEvent(PalGameObjectBase obj)
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	private void OnCurObjTypeChange(PalGameObjectBase obj)
	{
		if (obj.CurObjType == ObjType.none)
		{
			base.gameObject.SetActive(false);
			return;
		}
		base.gameObject.SetActive(true);
		this.ChangeTex(obj.CurObjType);
	}

	private void ChangeTex(ObjType type)
	{
		if (base.GetComponent<Renderer>() == null)
		{
			Debug.LogError(base.gameObject.name + "没有 renderer");
			return;
		}
		if (CharactersManager.Instance.TypeIcons.ContainsKey(type))
		{
			this.Tex = CharactersManager.Instance.TypeIcons[type];
			if (this.m_material == null)
			{
				this.m_material = base.GetComponent<Renderer>().material;
			}
			this.m_material.SetTexture("_MainTex", this.Tex);
			if (type == ObjType.MainLine)
			{
				this.m_isMain = true;
			}
			else
			{
				this.m_isMain = false;
			}
		}
	}

	public void Init(PalGameObjectBase goBase)
	{
		this.m_material = base.GetComponent<Renderer>().material;
		this.Gob = goBase;
		//UIManager.SetAllToUILayer(base.transform);
		if (this.Gob.model == null)
		{
			if (this.Gob.transform.childCount > 0)
			{
				this.targetTF = this.Gob.transform.GetChild(0);
			}
			else
			{
				this.targetTF = this.Gob.transform;
			}
		}
		else
		{
			this.targetTF = this.Gob.model.transform;
		}
		PalGameObjectBase expr_9D = this.Gob;
		expr_9D.DestroyEvent = (Action<PalGameObjectBase>)Delegate.Combine(expr_9D.DestroyEvent, new Action<PalGameObjectBase>(this.ObjdestroyEvent));
		PalGameObjectBase expr_C4 = this.Gob;
		expr_C4.OnCurObjTypeChange = (Action<PalGameObjectBase>)Delegate.Combine(expr_C4.OnCurObjTypeChange, new Action<PalGameObjectBase>(this.OnCurObjTypeChange));
		this.ChangeTex(this.Gob.CurObjType);
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (this.MapInfoTF != null && this.targetTF != null)
		{
			Vector3 vector = this.targetTF.position - this.MapInfoTF.position;
			if (this.m_isMain)
			{
				base.transform.localPosition = new Vector3(vector.x, vector.z, -0.2f);
			}
			else
			{
				base.transform.localPosition = new Vector3(vector.x, vector.z, -0.1f);
			}
		}
	}

	private void OnDestroy()
	{
		if (this.Gob != null)
		{
			PalGameObjectBase expr_17 = this.Gob;
			expr_17.DestroyEvent = (Action<PalGameObjectBase>)Delegate.Remove(expr_17.DestroyEvent, new Action<PalGameObjectBase>(this.ObjdestroyEvent));
			PalGameObjectBase expr_3E = this.Gob;
			expr_3E.OnCurObjTypeChange = (Action<PalGameObjectBase>)Delegate.Remove(expr_3E.OnCurObjTypeChange, new Action<PalGameObjectBase>(this.OnCurObjTypeChange));
		}
		if (this.m_material != null)
		{
			UnityEngine.Object.Destroy(this.m_material);
		}
	}
}
