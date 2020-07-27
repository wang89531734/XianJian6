using SoftStar.Pal6;
using System;
using System.IO;
using UnityEngine;

public class SmoothFollow2 : MonoBehaviour, ISaveInterface
{
	public bool bControl = true;

	public bool bInUI;

	public Transform target;

	public Transform targetRoot;

	private float baseHeight = 1.6f;

	public float height;

	public float damping = 5f;

	public float rotationDamping = 1E+08f;

	public Vector3 CenterOffset = new Vector3(0f, 0f, -1f);

	public float CamDistance = 1f;

	public float fieldOfView = 45f;

	[HideInInspector]
	[NonSerialized]
	public Vector3 CamPos;

	public Quaternion CamRot = new Quaternion(0f, 0f, 0f, 1f);

	private Vector3 FocalPos;

	private Vector3 LastTargetPos;

	private Quaternion LastWantedRotation = new Quaternion(0f, 0f, 0f, 1f);

	private CharacterController charCtrl;

	private static int ignorelayer = -100;

	private static int maskValue = -1;

	public bool CanScroll = true;

	public PalShake shakeScript;

	public ShakeType curShakeType;

	public static bool LeftCameraMove = false;

	private static Vector3 lastMousePos = Vector3.zero;

	private bool HVByLoadArchive;

	public Vector3 offsetScale = new Vector3(1f, 0.23f, 1f);

	public Quaternion TargetCamRot = new Quaternion(0f, 0f, 0f, 1f);

	public bool bJolt = true;

	[HideInInspector]
	[NonSerialized]
	public float lastAngleH;

	public bool bNeedReturn;

	public float ReturnSpeed = 2f;

	private bool bUseLeftReturn;

	public float CameraRadius = 0.13f;

	public static float CameraRadiusDefault = 0.16f;

	public Vector2 FOV_Range = new Vector2(35f, 60f);

	public Action InsideFun;

	private static string guiDisplayStr;

	public static RaycastHit mouseHit;

	public static GameObject mouseHitObject = null;

	public float horizontalRotSpeed = 200f;

	public float verticalRotSpeed = 250f;

	private float angleH;

	private float angleV;

	public float maxVerticalAngle = 60f;

	public float minVerticalAngle = -55f;

	private float m_maxVerticalAngle = 60f;

	private float m_minVerticalAngle = -55f;

	private float TargetCamDistance = 6f;

	public float MaxDistance = 12.3f;

	public float MinDistance = 0.8f;

	private float currentDistance = 5f;

	public float elasticSpeed = 4f;

	private Ray ray = new Ray(Vector3.zero, Vector3.zero);

	private bool cullPlayer;

	private Vector3 posOrig;

	private Vector3 posEnd;

	public float YSpeed = 5f;

	public bool UseYSpeed = true;

	private float curY;

	public float BigMap_maxVerticalAngle = 75f;

	public float BigMap_verticalRotSpeed = 800f;

	public float BigMap_CamDistanceSpeed = 40f;

	public float BigMap_maxVerticalAngleK = 2f;

	private float lastDistance;

	private float lastVerticalAngle;

	private Action<bool> MiddleEvent;

	public static int IgnoreLayer
	{
		get
		{
			if (SmoothFollow2.ignorelayer < -50)
			{
				SmoothFollow2.ignorelayer = LayerMask.NameToLayer("Ignore Camera Raycast");
			}
			return SmoothFollow2.ignorelayer;
		}
	}

	public static int MaskValue
	{
		get
		{
			return SmoothFollow2.maskValue;
		}
	}

	public float CamAngleH
	{
		get
		{
			return this.angleH;
		}
		set
		{
			this.angleH = value;
		}
	}

	public float CamAngleV
	{
		get
		{
			return this.angleV;
		}
		set
		{
			this.angleV = value;
		}
	}

	public void CopyFrom(SmoothFollow2 sf2)
	{
		this.bControl = sf2.bControl;
		this.bInUI = sf2.bInUI;
		this.target = sf2.target;
		this.targetRoot = sf2.targetRoot;
		this.baseHeight = sf2.baseHeight;
		this.height = sf2.height;
		this.damping = sf2.damping;
		this.rotationDamping = sf2.rotationDamping;
		this.CenterOffset = sf2.CenterOffset;
		this.CamDistance = sf2.CamDistance;
		this.fieldOfView = sf2.fieldOfView;
		this.CamPos = sf2.CamPos;
		this.CamRot = sf2.CamRot;
		this.FocalPos = sf2.FocalPos;
		this.LastTargetPos = sf2.LastTargetPos;
		this.LastWantedRotation = sf2.LastWantedRotation;
		this.charCtrl = sf2.charCtrl;
		this.CanScroll = sf2.CanScroll;
		this.shakeScript = sf2.shakeScript;
		this.curShakeType = sf2.curShakeType;
		this.HVByLoadArchive = sf2.HVByLoadArchive;
		this.offsetScale = sf2.offsetScale;
		this.TargetCamRot = sf2.TargetCamRot;
		this.bJolt = sf2.bJolt;
		this.lastAngleH = sf2.lastAngleH;
		this.bNeedReturn = sf2.bNeedReturn;
		this.ReturnSpeed = sf2.ReturnSpeed;
		this.bUseLeftReturn = sf2.bUseLeftReturn;
		this.CameraRadius = sf2.CameraRadius;
		this.InsideFun = sf2.InsideFun;
		this.horizontalRotSpeed = sf2.horizontalRotSpeed;
		this.verticalRotSpeed = sf2.verticalRotSpeed;
		this.angleH = sf2.angleH;
		this.angleV = sf2.angleV;
		this.maxVerticalAngle = sf2.maxVerticalAngle;
		this.minVerticalAngle = sf2.minVerticalAngle;
		this.m_maxVerticalAngle = sf2.m_maxVerticalAngle;
		this.m_minVerticalAngle = sf2.m_minVerticalAngle;
		this.TargetCamDistance = sf2.TargetCamDistance;
		this.MaxDistance = sf2.MaxDistance;
		this.MinDistance = sf2.MinDistance;
		this.currentDistance = sf2.currentDistance;
		this.elasticSpeed = sf2.elasticSpeed;
		this.ray = sf2.ray;
		this.cullPlayer = sf2.cullPlayer;
		this.posOrig = sf2.posOrig;
		this.posEnd = sf2.posEnd;
		this.YSpeed = sf2.YSpeed;
		this.UseYSpeed = sf2.UseYSpeed;
		this.curY = sf2.curY;
		this.BigMap_maxVerticalAngle = sf2.BigMap_maxVerticalAngle;
		this.BigMap_verticalRotSpeed = sf2.BigMap_verticalRotSpeed;
		this.BigMap_CamDistanceSpeed = sf2.BigMap_CamDistanceSpeed;
		this.BigMap_maxVerticalAngleK = sf2.BigMap_maxVerticalAngleK;
		this.lastDistance = sf2.lastDistance;
		this.lastVerticalAngle = sf2.lastVerticalAngle;
		this.MiddleEvent = sf2.MiddleEvent;
	}

	private void Start()
	{
		this.InitTarget();
		SmoothFollow2.maskValue = (1 << SmoothFollow2.IgnoreLayer | 131072 | 262144 | 4 | 524288 | 512);
		SmoothFollow2.maskValue = ~SmoothFollow2.maskValue;
		if (!this.HVByLoadArchive && PlayersManager.Player != null)
		{
			this.InitAngle();
		}
		this.HVByLoadArchive = false;
		//GameStateManager.AddInitStateFun(GameState.Normal, new GameStateManager.void_fun(this.InNormal));
		//GameStateManager.AddEndStateFun(GameState.Normal, new GameStateManager.void_fun(this.OutNormal));
		PlayersManager.OnTabPlayer -= new Action<int>(this.OnTabPlayer);
		PlayersManager.OnTabPlayer += new Action<int>(this.OnTabPlayer);
	}

	private void OnDestroy()
	{
		//GameStateManager.RemoveInitStateFun(GameState.Normal, new GameStateManager.void_fun(this.InNormal));
		//GameStateManager.RemoveEndStateFun(GameState.Normal, new GameStateManager.void_fun(this.OutNormal));
		PlayersManager.OnTabPlayer -= new Action<int>(this.OnTabPlayer);
	}

	public void InNormal()
	{
		base.enabled = true;
		this.InitPlayerForward();
	}

	private void OutNormal()
	{
		base.enabled = false;
	}

	private void OnEnable()
	{
	}

	public void Init(GameObject go)
	{
		if (go == null)
		{
			Debug.LogError("SmoothFollow2 Init参数为null");
			return;
		}
		this.targetRoot = go.transform;
		this.target = null;
		this.InitTarget();
		if (!base.enabled)
		{
			base.enabled = true;
		}
		this.ResetData();
	}

	public void InitTarget()
	{
		if (this.targetRoot == null)
		{
			return;
		}
		Transform transform = this.targetRoot;
		Animator componentInChildren = this.targetRoot.GetComponentInChildren<Animator>();
		if (componentInChildren != null)
		{
			transform = componentInChildren.transform;
		}
		this.target = null;
		if (this.target == null)
		{
			this.target = transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Spine2/Bip01 Neck/Bip01 Head");
		}
		if (this.target == null)
		{
			this.target = transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 Head");
		}
		if (this.target == null)
		{
			this.target = transform.Find("Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Spine1/Bip001 Spine2/Bip001 Spine3/Bip001 Neck/Bip001 Head");
		}
		if (this.target == null)
		{
			this.target = transform.Find("Bip001/Bip001 Spine/Bip001 Spine1/Bip001 Neck/Bip001 Head");
		}
		if (this.target == null)
		{
			this.target = transform.Find("Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Spine1/Bip001 Neck/Bip001 Head");
		}
		if (this.target == null)
		{
			this.target = transform.Find("char_astrella_reference/char_astrella_Hips1/char_astrella_Spine/char_astrella_Spine1/char_astrella_Spine2/char_astrella_Neck/char_astrella_Head");
		}
		if (this.target == null)
		{
			this.target = this.FindHead();
		}
		if (this.target == null)
		{
			Debug.LogError("Error : 无法找到 Head");
			return;
		}
		this.curY = this.target.position.y;
		string name = this.targetRoot.name;
		switch (name)
		{
		case "YueJinChao":
			this.baseHeight = 1.559f;
			goto IL_280;
		case "YueQi":
			this.baseHeight = 1.3982f;
			goto IL_280;
		case "XianQing":
			this.baseHeight = 1.6169f;
			goto IL_280;
		case "LuoWenRen":
			this.baseHeight = 1.5489f;
			goto IL_280;
		case "JuShiFang":
			this.baseHeight = 1.5183f;
			goto IL_280;
		case "MingXiu":
			this.baseHeight = 1.4548f;
			goto IL_280;
		}
		this.baseHeight = 1.56f;
		IL_280:
		this.target = this.targetRoot;
		base.GetComponent<Camera>().fieldOfView = this.fieldOfView;
		base.GetComponent<Camera>().nearClipPlane = 0.1f;
		this.CamPos = base.transform.position;
		this.FocalPos = this.target.position;
        Vector3 lastTargetPos = Util.RelativeMatrix(this.target, this.targetRoot).MultiplyPoint(Vector3.zero);
        this.LastTargetPos = lastTargetPos;
    }

	public void InitAngle()
	{
		this.CamAngleH = PlayersManager.Player.GetModelObj(true).transform.eulerAngles.y;
		this.CamAngleV = 10f;
		this.LateUpdate();
		PlayerCtrlManager.LastForward = base.transform.forward;
	}

	public void InitPlayerForward()
	{
		this.LateUpdate();
		PlayerCtrlManager.LastForward = base.transform.forward;
	}

	public void ResetData()
	{
		this.YSpeed = 5f;
		this.CameraRadius = SmoothFollow2.CameraRadiusDefault;
		this.horizontalRotSpeed = 170f;
		this.verticalRotSpeed = 250f;
		this.rotationDamping = 1E+08f;
	}

	private void LateUpdate()
	{
		if (this.target == null)
		{
			return;
		}

		if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
		{
			SmoothFollow2.LeftCameraMove = false;
			SmoothFollow2.lastMousePos = Input.mousePosition;
		}
		else if (Input.GetMouseButton(0) && Vector3.SqrMagnitude(Input.mousePosition - SmoothFollow2.lastMousePos) > 4f)
		{
			SmoothFollow2.LeftCameraMove = true;
		}
	
		if (this.InsideFun != null)
		{
			this.InsideFun();
			return;
		}
		Vector3 position = Vector3.zero;

		if (this.curShakeType != ShakeType.None)
		{
			position = base.transform.position;
			base.transform.position = this.CamPos;
		}

		if (this.bControl && !this.bInUI)
		{
			if (this.CanScroll)
			{
				this.TargetCamDistance = this.GetTargetCamDistance();
				this.CamDistance = this.TargetCamDistance;
			}

			this.AdjustFOV(this.CamDistance);

            if (InputManager.GetKey(KEY_ACTION.CAMERA_LEFT, false))
            {
                this.CamAngleH -= this.horizontalRotSpeed * Time.smoothDeltaTime;
            }
            else if (InputManager.GetKey(KEY_ACTION.CAMERA_RIGHT, false))
            {
                this.CamAngleH += this.horizontalRotSpeed * Time.smoothDeltaTime;
            }
 
            if (Input.GetMouseButton(1))
			{
                this.GetHV();
				this.bNeedReturn = false;
			}
			else if (Input.GetMouseButton(0))
			{
				this.GetHV();
			}

			if (this.bUseLeftReturn)
			{
				if (Input.GetMouseButtonDown(0))
				{
					if (!this.bNeedReturn)
					{
						this.lastAngleH = this.CamAngleH;
					}
					this.bNeedReturn = false;
				}
				else if (Input.GetMouseButtonUp(0))
				{
					this.bNeedReturn = true;
				}

				if (this.bNeedReturn)
				{
					this.CamAngleH = Mathf.LerpAngle(this.CamAngleH, this.lastAngleH, Time.deltaTime * this.ReturnSpeed);
					if (Mathf.Abs(this.CamAngleH - this.lastAngleH) < 0.5f)
					{
						this.bNeedReturn = false;
					}
				}
			}

			if (this.bJolt)
			{
				this.MakeJolt();
			}
			this.TargetCamRot = Quaternion.Euler(this.angleV, this.CamAngleH, 0f);
			this.CamRot = Quaternion.Lerp(this.CamRot, this.TargetCamRot, Time.deltaTime * this.rotationDamping);
		}
		this.FocalPos = this.GetLookAtPos(this.UseYSpeed);
		this.CamPos = this.FocalPos + this.CamRot * this.CenterOffset * this.CamDistance;
		this.CheckForCollision(ref this.CamPos, this.FocalPos, 0f, this.CameraRadius);
		if (this.curShakeType != ShakeType.None)
		{
			if (this.shakeScript != null)
			{
				this.shakeScript.referPos = this.CamPos;
			}
			base.transform.position = position;
		}
		else
		{
			base.transform.position = this.CamPos;
		}
		base.transform.rotation = this.CamRot;
		SmoothFollow2.CastRay(base.GetComponent<Camera>());
	}

	public void AdjustFOV()
	{
		this.AdjustFOV((base.transform.position - this.GetLookAtPos(false)).magnitude);
	}

	public void AdjustFOV(float camDis)
	{
		float t = (camDis - this.MinDistance) / (this.MaxDistance - this.MinDistance);
		float num = Mathf.Lerp(this.FOV_Range.x, this.FOV_Range.y, t);
		if (base.GetComponent<Camera>().fieldOfView != num)
		{
			base.GetComponent<Camera>().fieldOfView = num;
		}
	}

	public void SetCurCamera()
	{
		Vector3 eulerAngles = base.transform.rotation.eulerAngles;
		this.angleV = eulerAngles.x;
		this.CamAngleH = eulerAngles.y;
		this.TargetCamDistance = (this.CamDistance = (base.transform.position - this.GetLookAtPos(false)).magnitude);
	}

	public static void CastRay(Camera camera)
	{
	}

	private Quaternion GetTargetCamRot()
	{
		this.CamAngleH += Mathf.Clamp(Input.GetAxis("Mouse X"), -1f, 1f) * this.horizontalRotSpeed * Time.smoothDeltaTime;
		this.angleV -= Mathf.Clamp(Input.GetAxis("Mouse Y"), -1f, 1f) * this.verticalRotSpeed * Time.smoothDeltaTime;
		this.angleV = Mathf.Clamp(this.angleV, this.m_minVerticalAngle, this.m_maxVerticalAngle);
		return Quaternion.Euler(this.angleV, this.CamAngleH, 0f);
	}

	private void GetHV()
	{
		this.CamAngleH += Mathf.Clamp(Input.GetAxis("Mouse X"), -1f, 1f) * this.horizontalRotSpeed * Time.smoothDeltaTime;
		this.angleV -= Mathf.Clamp(Input.GetAxis("Mouse Y"), -1f, 1f) * this.verticalRotSpeed * Time.smoothDeltaTime;
		if (this.angleV < -270f)
		{
			this.angleV += 360f;
		}
		this.angleV = Mathf.Clamp(this.angleV, this.m_minVerticalAngle, this.m_maxVerticalAngle);
	}

	private void MakeJolt()
	{
        Vector3 vector = Util.RelativeMatrix(this.target, this.targetRoot).MultiplyPoint(Vector3.zero);
        Vector3 vector2 = vector - this.LastTargetPos;
        vector2.x *= this.offsetScale.x;
        vector2.y *= this.offsetScale.y;
        vector2.z *= this.offsetScale.z;
        this.LastTargetPos = vector;
        Vector3 vector3 = new Vector3(0f, 0f, this.CamDistance);
        vector2 *= this.CamDistance;
        Quaternion quaternion = Quaternion.FromToRotation(vector3, vector3 + vector2);
        float num = quaternion.eulerAngles.x;
        if (num > 350f)
        {
            num -= 360f;
        }
        this.angleV -= num;
        this.CamAngleH += quaternion.eulerAngles.y;
    }

	private float GetTargetCamDistance()
	{
		return Mathf.Clamp(this.TargetCamDistance - Input.GetAxis("Mouse ScrollWheel"), this.MinDistance, this.MaxDistance);
	}

	private void CheckForCollision(ref Vector3 posEnd, Vector3 posOrig, float OrigR, float radius)
	{
		this.posEnd = posEnd;
		this.posOrig = posOrig;
		Vector3 direction = posEnd - posOrig;
		this.ray.origin = posOrig;
		this.ray.direction = direction;
		RaycastHit raycastHit;
		if (Physics.SphereCast(this.ray, radius, out raycastHit, direction.magnitude, SmoothFollow2.maskValue))
		{
			if (this.currentDistance < raycastHit.distance)
			{
				this.currentDistance = Mathf.Clamp(this.currentDistance + this.elasticSpeed * Time.deltaTime, this.currentDistance, raycastHit.distance);
			}
			else
			{
				this.currentDistance = raycastHit.distance;
			}
		}
		else if (this.currentDistance < this.CamDistance)
		{
			this.currentDistance = Mathf.Clamp(this.currentDistance + this.elasticSpeed * Time.deltaTime, this.currentDistance, this.CamDistance);
		}
		else
		{
			this.currentDistance = this.CamDistance;
		}
		posEnd = this.FocalPos + this.CamRot * this.CenterOffset * this.currentDistance;
		GameObject modelObj = PlayersManager.Player.GetModelObj(false);
		if (modelObj == null)
		{
			return;
		}
		CharacterController component = modelObj.GetComponent<CharacterController>();
		if (!this.cullPlayer && this.currentDistance <= component.radius + base.GetComponent<Camera>().nearClipPlane)
		{
		//	modelObj.SetVisible(false);
			this.cullPlayer = true;
		}
		else if (this.cullPlayer && this.currentDistance > component.radius + base.GetComponent<Camera>().nearClipPlane)
		{
		//	modelObj.SetVisible(true);
			this.cullPlayer = false;
		}
	}

	private void CheckNearClipPlane()
	{
		GameObject modelObj = PlayersManager.Player.GetModelObj(false);
		if (modelObj == null)
		{
			return;
		}
		CharacterController component = modelObj.GetComponent<CharacterController>();
		if (this.currentDistance <= component.radius + base.GetComponent<Camera>().nearClipPlane)
		{
		//	modelObj.SetVisible(false);
			this.cullPlayer = true;
		}
		else
		{
			//modelObj.SetVisible(true);
			this.cullPlayer = false;
		}
	}

	private void OnTabPlayer(int i)
	{
		if (base.GetComponent<Camera>() == null)
		{
			Debug.LogError("Error : SmoothFollow2.OnTabPlayer camera == null");
			return;
		}
		GameObject modelObj = PlayersManager.Player.GetModelObj(false);
		CharacterController component = modelObj.GetComponent<CharacterController>();
		if (this.currentDistance <= component.radius + base.GetComponent<Camera>().nearClipPlane)
		{
		//	modelObj.SetVisible(false);
			this.cullPlayer = true;
		}
		else if (this.currentDistance > component.radius + base.GetComponent<Camera>().nearClipPlane)
		{
		//	modelObj.SetVisible(true);
			this.cullPlayer = false;
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(this.posOrig, this.posEnd);
	}

	private Transform FindHead()
	{
		Transform[] componentsInChildren = this.targetRoot.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Transform transform = componentsInChildren[i];
			if (transform.name.ToLower().Contains("head") && transform.name.ToLower() != "b head")
			{
				return transform;
			}
		}
		Transform[] componentsInChildren2 = this.targetRoot.GetComponentsInChildren<Transform>();
		for (int j = 0; j < componentsInChildren2.Length; j++)
		{
			Transform transform2 = componentsInChildren2[j];
			if (transform2.name.ToLower().Contains("Focus"))
			{
				return transform2;
			}
		}
		return this.targetRoot;
	}

	public Vector3 GetLookAtPos(bool bLerp = false)
	{
		Vector3 position = this.target.position;
		position.y += this.baseHeight + this.height;
		if (bLerp)
		{
			this.curY = Mathf.Lerp(this.curY, position.y, Time.deltaTime * this.YSpeed);
			position.y = this.curY;
		}
		return position;
	}

	private void BaseCalculate()
	{
		this.TargetCamRot = Quaternion.Euler(this.angleV, this.CamAngleH, 0f);
		this.CamRot = Quaternion.Lerp(this.CamRot, this.TargetCamRot, Time.deltaTime * this.rotationDamping);
		this.FocalPos = this.target.position;
		this.FocalPos.y = this.FocalPos.y + (this.baseHeight + this.height);
		this.CamPos = this.FocalPos + this.CamRot * this.CenterOffset * this.CamDistance;
		base.transform.position = this.CamPos;
		base.transform.rotation = this.CamRot;
	}

	public void BeginMoveToMap()
	{
		this.lastVerticalAngle = this.angleV;
		this.lastDistance = this.CamDistance;
		this.InsideFun = new Action(this.ToBigMapCorePartA);
		//CtrlScreenBlend.Blend(BigMap.Instance.CurCamera, 2.22222233f, 45f, false, false, new Action(this.BlendFinishA), false);
	}

	private void ToBigMapCorePartA()
	{
		if (this.angleV < this.BigMap_maxVerticalAngle)
		{
			this.angleV += this.BigMap_verticalRotSpeed * Time.smoothDeltaTime;
		}
		else
		{
			if (this.MiddleEvent != null)
			{
				this.MiddleEvent(true);
				this.MiddleEvent = null;
			}
			this.CamDistance += this.BigMap_CamDistanceSpeed * Time.deltaTime;
		}
		this.BaseCalculate();
	}

	private void BlendFinishA()
	{
		base.enabled = false;
		this.InsideFun = null;
	}

	public void BeginMoveToGround()
	{
		this.InsideFun = new Action(this.ToBigMapCorePartB);
		base.enabled = true;
		//CtrlScreenBlend.Blend(BigMap.Instance.CurCamera, 2.22222233f, 45f, true, false, new Action(this.BlendFinishB), false);
	}

	private void ToBigMapCorePartB()
	{
		if (this.CamDistance > this.lastDistance)
		{
			this.CamDistance -= this.BigMap_CamDistanceSpeed * Time.deltaTime;
		}
		else if (this.angleV > this.lastVerticalAngle)
		{
			this.angleV -= this.BigMap_verticalRotSpeed * this.BigMap_maxVerticalAngleK * Time.smoothDeltaTime;
		}
		this.BaseCalculate();
	}

	private void BlendFinishB()
	{
		this.InsideFun = null;
	}

	public void Save(BinaryWriter writer)
	{
		writer.Write(this.CamDistance);
		writer.Write(this.CamAngleH);
		writer.Write(this.angleV);
		writer.Write(this.CamPos.x);
		writer.Write(this.CamPos.y);
		writer.Write(this.CamPos.z);
		writer.Write(this.CamRot.x);
		writer.Write(this.CamRot.y);
		writer.Write(this.CamRot.z);
		writer.Write(this.CamRot.w);
	}

	public void Load(BinaryReader reader)
	{
		//if (SaveManager.VersionNum >= 14u)
		//{
		//	this.HVByLoadArchive = true;
		//	this.CamDistance = reader.ReadSingle();
		//	this.CamAngleH = reader.ReadSingle();
		//	this.angleV = reader.ReadSingle();
		//	this.CamPos.x = reader.ReadSingle();
		//	this.CamPos.y = reader.ReadSingle();
		//	this.CamPos.z = reader.ReadSingle();
		//	this.CamRot.x = reader.ReadSingle();
		//	this.CamRot.y = reader.ReadSingle();
		//	this.CamRot.z = reader.ReadSingle();
		//	this.CamRot.w = reader.ReadSingle();
		//	this.lastAngleH = this.CamAngleH;
		//	this.TargetCamRot = this.CamRot;
		//	this.bNeedReturn = false;
		//	this.LateUpdate();
		//	this.CheckNearClipPlane();
		//	PlayerCtrlManager.LastForward = base.transform.forward;
		//}
	}
}
