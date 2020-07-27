using SoftStar.Pal6;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Perception : MonoBehaviour
{
	private bool useShowLog;

	private float HighLevelP;

	public float relayP = 0.01f;

	public Action<Transform> OnBeSeenEvent;

	public Action<Transform> OnBeNotSeenEvent;

	public float RangeWidthAngle = 30f;

	public float RangeHeightAngle = 18.5f;

	public float watchDistance = 8f;

	public Material mat;

	private Material matClone;

	public Color FrustumColor = new Color(0.5f, 0.2f, 0.2f, 0.4f);

	private MeshFilter meshFilter;

	private MeshRenderer meshRenderer;

	private List<Vector3> vertices = new List<Vector3>();

	private List<Vector2> uvs = new List<Vector2>();

	private List<int> triangles = new List<int>();

	private List<Vector3> normals = new List<Vector3>();

	public bool bShowFrustum;

	public float listenRadius = 4f;

	public Color ListenRangeColor = new Color(0.5f, 0.7f, 0.56f, 0.23f);

	public bool bShowListenRange = true;

	public PalNPC npc;

	private Transform _host;

	private Transform modelTF;

	private int IgnoreLayer = -100;

	private int IgnoreMask;

	private Transform range;

	private PerceptionRange perceptionRange;

	public Dictionary<Transform, List<PerceptionTarget>> targetsCanBePercept = new Dictionary<Transform, List<PerceptionTarget>>();

	public List<Transform> hostsCanBeSeen = new List<Transform>();

	public List<Transform> hostsCanBeListened = new List<Transform>();

	private float lastTime;

	public float prosInterval = 0.3f;

	private float curInterval;

	private bool bActive;

	private Transform myTransform;

	public Transform host
	{
		get
		{
			if (this._host == null)
			{
				//PalNPC palNPC = PalNPC.FindTheNPC(base.transform);
				//this._host = ((!(palNPC == null)) ? palNPC.transform : base.transform);
			}
			return this._host;
		}
	}

	public void CopyData(Perception other)
	{
		if (other == null)
		{
			return;
		}
		this.targetsCanBePercept = other.targetsCanBePercept;
		this.hostsCanBeSeen = other.hostsCanBeSeen;
		this.hostsCanBeListened = other.hostsCanBeListened;
	}

	public static void ActivePerception(PalNPC npc)
	{
		Perception[] componentsInChildren = npc.model.GetComponentsInChildren<Perception>(true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Perception perception = componentsInChildren[i];
			if (perception != null)
			{
				perception.Init(npc);
			}
		}
	}

	public void Init(PalNPC npc)
	{
		if (npc == null)
		{
			return;
		}
		this.myTransform = base.transform;
		this.npc = npc;
		this._host = npc.transform;
		PerceptionDate perceptionDate = GameObjectPath.FindSpecParent(base.transform, typeof(PerceptionDate)) as PerceptionDate;
		if (perceptionDate != null)
		{
			this.RangeWidthAngle = perceptionDate.RangeWidthAngle;
			this.watchDistance = perceptionDate.watchDistance;
			this.listenRadius = perceptionDate.listenRadius;
			UnityEngine.Object.Destroy(perceptionDate);
		}
		npc.perception = this;
		GameObject model = npc.model;
		if (model != null)
		{
			Agent component = model.GetComponent<Agent>();
			component.perception = this;
			this.modelTF = model.transform;
		}
		int characterID = npc.Data.CharacterID;
		if (characterID < 8 && characterID != 6)
		{
			this.prosInterval = 0.07f;
			this.watchDistance = 15f;
			this.RangeWidthAngle = 27f;
		}
		if (this.relayP < 0.01f)
		{
			this.relayP = 0.01f;
		}
		this.IgnoreLayer = 2;
		this.IgnoreMask = (1 << this.IgnoreLayer | 131072 | 262144 | 256);
		this.IgnoreMask = ~this.IgnoreMask;
		base.gameObject.layer = this.IgnoreLayer;
		this.InitFrustumMesh();
		this.InitRange();
		this.lastTime = Time.realtimeSinceStartup;
		this.bShowFrustum = false;
		this.bActive = true;
		base.enabled = true;
	}

	private void Start()
	{
		if (!this.bActive)
		{
			base.enabled = false;
		}
	}

	private void InitRange()
	{
		Transform transformByType = GameObjectPath.GetTransformByType<Animator>(base.transform);
		if (transformByType == null)
		{
			Debug.LogError(this.myTransform.name + "往上面没有找到 Animator");
			return;
		}
		this.range = transformByType.FindChild("Range");
		if (this.range == null)
		{
			this.range = new GameObject("Range")
			{
				transform = 
				{
					parent = transformByType
				}
			}.transform;
		}
		SphereCollider sphereCollider = this.range.GetComponent<SphereCollider>();
		if (sphereCollider == null)
		{
			sphereCollider = this.range.gameObject.AddComponent<SphereCollider>();
		}
		sphereCollider.radius = (this.watchDistance + this.listenRadius) / 2f;
		sphereCollider.isTrigger = true;
		this.range.localPosition = new Vector3(0f, 0f, this.watchDistance - sphereCollider.radius);
		this.perceptionRange = this.range.GetComponent<PerceptionRange>();
		if (this.perceptionRange == null)
		{
			this.perceptionRange = this.range.gameObject.AddComponent<PerceptionRange>();
		}
		this.perceptionRange.perception = this;
		this.range.gameObject.layer = this.IgnoreLayer;
	}

	private void InitFrustumMesh()
	{
		this.meshFilter = base.GetComponent<MeshFilter>();
		if (this.meshFilter == null)
		{
			this.meshFilter = base.gameObject.AddComponent<MeshFilter>();
		}
		this.meshRenderer = base.GetComponent<MeshRenderer>();
		if (this.meshRenderer == null)
		{
			this.meshRenderer = base.gameObject.AddComponent<MeshRenderer>();
		}
		if (this.mat != null)
		{
			this.matClone = UnityEngine.Object.Instantiate<Material>(this.mat);
			this.meshRenderer.material = this.matClone;
			this.meshRenderer.material.color = this.FrustumColor;
			this.meshRenderer.material.SetFloat("_Distance", this.watchDistance);
		}
		this.meshRenderer.shadowCastingMode = ShadowCastingMode.Off;
		this.meshRenderer.receiveShadows = false;
		this.meshRenderer.enabled = false;
		this.SetFrustumMesh(this.meshFilter);
	}

	private void SetFrustumMesh(MeshFilter meshFilter)
	{
		float f = this.RangeWidthAngle / 180f * 3.14159274f;
		float f2 = this.RangeHeightAngle / 180f * 3.14159274f;
		float num = Mathf.Tan(f) * this.watchDistance;
		float num2 = Mathf.Tan(f2) * this.watchDistance;
		float z = this.watchDistance;
		this.vertices.Add(new Vector3(0f, 0f, 0f));
		this.vertices.Add(new Vector3(num, num2, z));
		this.vertices.Add(new Vector3(num, -num2, z));
		this.vertices.Add(new Vector3(-num, -num2, z));
		this.vertices.Add(new Vector3(-num, num2, z));
		for (int i = 0; i < this.vertices.Count; i++)
		{
			this.uvs.Add(new Vector2(0f, 0f));
		}
		int[] collection = new int[]
		{
			0,
			4,
			1,
			0,
			1,
			2,
			0,
			3,
			4,
			0,
			2,
			3,
			1,
			4,
			3,
			1,
			3,
			2
		};
		this.triangles.AddRange(collection);
		Vector3 item = new Vector3(0f, 0f, -1f);
		Vector3 item2 = new Vector3(1f, 1f, 1f);
		Vector3 item3 = new Vector3(1f, -1f, 1f);
		Vector3 item4 = new Vector3(-1f, -1f, 1f);
		Vector3 item5 = new Vector3(-1f, 1f, 1f);
		this.normals.Add(item);
		this.normals.Add(item2);
		this.normals.Add(item3);
		this.normals.Add(item4);
		this.normals.Add(item5);
		meshFilter.mesh = new Mesh
		{
			vertices = this.vertices.ToArray(),
			triangles = this.triangles.ToArray(),
			uv = this.uvs.ToArray(),
			normals = this.normals.ToArray()
		};
	}

	public void ShowFrustum(bool key)
	{
		this.meshRenderer.enabled = key;
		this.bShowFrustum = key;
	}

	private void Update()
	{
		if (!this.bActive)
		{
			base.enabled = false;
			return;
		}
		this.curInterval -= Time.deltaTime;
		float num = Time.realtimeSinceStartup - this.lastTime - this.HighLevelP;
		//if (num > PalMain.MinDeltaTime)
		//{
		//	this.lastTime = Time.realtimeSinceStartup;
		//	this.HighLevelP += this.relayP;
		//	return;
		//}
		this.HighLevelP = 0f;
		this.lastTime = Time.realtimeSinceStartup;
		if (this.curInterval < 0f)
		{
			this.curInterval = this.prosInterval;
			if (this.targetsCanBePercept.Count > 0)
			{
				bool flag = false;
				foreach (KeyValuePair<Transform, List<PerceptionTarget>> current in this.targetsCanBePercept)
				{
					Transform key = current.Key;
					if (key == null)
					{
						Transform transformByType = GameObjectPath.GetTransformByType<Agent>(base.transform);
						if (transformByType != null)
						{
							Debug.LogWarning(transformByType.name + " 没有找到 目标宿主 进行一次情况");
						}
						flag = true;
						break;
					}
					bool flag2 = false;
					foreach (PerceptionTarget current2 in current.Value)
					{
						if (!(current2 == null))
						{
							if (this.IsExsitInFrustum(current2.transform) && this.IsCanSeen(current2))
							{
								flag2 = true;
								break;
							}
						}
					}
					if (flag2)
					{
						if (!this.hostsCanBeSeen.Contains(key))
						{
							this.hostsCanBeSeen.Add(key);
							if (this.OnBeSeenEvent != null)
							{
								this.OnBeSeenEvent(key);
							}
							this.SendMessageToUScript(key, true);
						}
					}
					else if (this.hostsCanBeSeen.Contains(key))
					{
						this.hostsCanBeSeen.Remove(key);
						if (this.OnBeNotSeenEvent != null)
						{
							this.OnBeNotSeenEvent(key);
						}
						this.SendMessageToUScript(key, false);
					}
					int num2 = this.hostsCanBeListened.IndexOf(key);
					if (this.IsExsitInListenRange(key))
					{
						if (num2 < 0)
						{
							this.hostsCanBeListened.Add(key);
						}
					}
					else if (num2 > -1)
					{
						this.hostsCanBeListened.RemoveAt(num2);
					}
				}
				if (flag)
				{
					this.targetsCanBePercept.Clear();
				}
			}
			else
			{
				this.hostsCanBeSeen.Clear();
				this.hostsCanBeListened.Clear();
			}
		}
		if (this.bShowFrustum && this.meshRenderer != null && this.meshRenderer.material != null)
		{
			Vector3 position = this.myTransform.position;
			this.meshRenderer.material.SetFloat("_Distance", this.watchDistance);
			this.meshRenderer.material.SetVector("_ObjPos", new Vector4(position.x, position.y, position.z, 1f));
		}
		if (this.bShowFrustum != this.meshRenderer.enabled)
		{
			this.meshRenderer.enabled = this.bShowFrustum;
		}
	}

	private bool IsExsitInFrustum(Transform tf)
	{
		Vector3 forward = this.myTransform.forward;
		Vector3 point;
		if (this.modelTF != null)
		{
			point = tf.position - this.modelTF.position;
		}
		else
		{
			point = tf.position - this.myTransform.position;
		}
		if (point.magnitude > this.watchDistance)
		{
			return false;
		}
		forward.Normalize();
		point.Normalize();
		Quaternion rotation = Quaternion.LookRotation(forward);
		Quaternion rotation2 = Quaternion.Inverse(rotation);
		Vector3 vector = rotation2 * point;
		Vector2 from = new Vector2(vector.z, -vector.x);
		float num = Vector2.Angle(from, Vector2.right);
		return num <= this.RangeWidthAngle;
	}

	private bool IsExsitInListenRange(Transform tf)
	{
		float magnitude = (tf.position - this.myTransform.position).magnitude;
		return magnitude <= this.listenRadius;
	}

	private bool IsCanSeen(PerceptionTarget pt)
	{
		bool flag = false;
		pt.host.gameObject.GetModelObj(false);
		Vector3 direction = Vector3.zero;
		if (!flag)
		{
			direction = pt.transform.position - this.myTransform.position;
			if (this.useShowLog)
			{
				RaycastHit raycastHit;
				flag = !Physics.Raycast(this.myTransform.position, direction, out raycastHit, direction.magnitude, this.IgnoreMask);
			}
			else if (!Physics.Raycast(this.myTransform.position, direction, direction.magnitude, this.IgnoreMask))
			{
				flag = true;
			}
		}
		return flag;
	}

	//public static bool IsCanBeSeen(Transform tf, Transform Target)
	//{
	//	//GameObject nPCObj = tf.gameObject.GetNPCObj();
	//	//PalNPC component = nPCObj.GetComponent<PalNPC>();
	//	return Perception.IsCanBeSeen(component, Target);
	//}

	public static bool IsCanBeSeen(PalNPC npc, Transform Target)
	{
		if (npc == null)
		{
			Debug.LogError("Error : IsCanBeSeen npc==null");
			return false;
		}
		if (npc.perception == null)
		{
			Debug.LogError("Error : IsCanBeSeen " + npc.name + " npc.perception = null");
			return false;
		}
		return npc.perception.hostsCanBeSeen.Contains(Target);
	}

	public static void RemoveCanBeSeen(Transform tf, Transform Target)
	{
		if (tf == null)
		{
			Debug.LogError("Error : RemoveCanBeSeen tf = null");
			return;
		}
		if (Target == null)
		{
			Debug.LogError("Error : RemoveCanBeSeen Target = null");
			return;
		}
		//GameObject nPCObj = tf.gameObject.GetNPCObj();
		//PalNPC component = nPCObj.GetComponent<PalNPC>();
		//Perception.RemoveCanBeSeen(component, Target);
	}

	public static void RemoveCanBeSeen(PalNPC npc, Transform Target)
	{
		if (npc == null)
		{
			Debug.LogError("Error : RemoveCanBeSeen npc = null");
			return;
		}
		Perception perception = npc.perception;
		if (perception == null)
		{
			Debug.LogError("Error : RemoveCanBeSeen perception = null");
			return;
		}
		perception.hostsCanBeListened.Remove(Target);
		perception.hostsCanBeSeen.Remove(Target);
		perception.targetsCanBePercept.Remove(Target);
	}

	private void OnDrawGizmos()
	{
		if (this.bShowListenRange)
		{
			Gizmos.color = this.ListenRangeColor;
			Gizmos.DrawSphere(this.myTransform.position, this.listenRadius);
		}
	}

	public void SendMessageToUScript(Transform curHost, bool OnBeSeen)
	{
		//if (!curHost.gameObject.IsPlayer())
		//{
		//	return;
		//}
		//if (this.npc == null)
		//{
		//	Debug.LogError("npc为空");
		//	return;
		//}
		//uScriptCustomEvent.CustomEventData value = new uScriptCustomEvent.CustomEventData((!OnBeSeen) ? "OnBeNotSeen" : "OnBeSeen", null, this.npc.gameObject);
		//this.npc.SendMessage("CustomEvent", value, SendMessageOptions.DontRequireReceiver);
	}

	private void OnEnable()
	{
	}

	public void Clear()
	{
		this.modelTF = null;
		foreach (KeyValuePair<Transform, List<PerceptionTarget>> current in this.targetsCanBePercept)
		{
			List<PerceptionTarget> value = current.Value;
			if (value != null)
			{
				value.Clear();
			}
		}
		this.targetsCanBePercept.Clear();
		this.hostsCanBeSeen.Clear();
		this.hostsCanBeListened.Clear();
	}

	private void OnDestroy()
	{
		this.Clear();
	}

	public void SetActive(bool bActive)
	{
		base.gameObject.SetActive(bActive);
		this.range.SetActive(bActive);
	}
}
