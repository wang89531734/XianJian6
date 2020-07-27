using Funfia.File;
//using Glow11;
//using SoftStar.BuffDebuff;
//using SoftStar.Item;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityStandardAssets.ImageEffects;

namespace SoftStar.Pal6
{
    public class PalMain : MonoBehaviour
    {
        //		public class SceneOptiDistFogParams
        //		{
        //			public int m_LevelID;

        //			public float m_CullDist_Hi;

        //			public float m_CullDist_Mid;

        //			public float m_CullDist_Low;

        //			public SceneOptiDistFogParams(int id, float hi, float mid, float low)
        //			{
        //				this.m_LevelID = id;
        //				this.m_CullDist_Hi = hi;
        //				this.m_CullDist_Mid = mid;
        //				this.m_CullDist_Low = low;
        //			}
        //		}

        //		public enum SETTING_ENUM
        //		{
        //			SHI_YE,
        //			YIN_YING,
        //			HDR,
        //			KANG_JU_CHI,
        //			TIE_TU_JING_DU,
        //			CHUI_ZHI_TONG_BU,
        //			TI_JI_GUANG,
        //			ZHI_BEI_FAN_WEI,
        //			WU_SE_JU_LI,
        //			JING_TONG_MEI_HUA,
        //			ZHAN_DOU_TE_XIAO,
        //			SHUI_MIAN_DAO_YING,
        //			GAO_JI_GUANG_YUAN,
        //			SETTING_MAX
        //		}

        //		public class PlayerConfigs
        //		{
        //			public int[] m_Settings = new int[13];

        //			public PlayerConfigs()
        //			{
        //				for (int i = 0; i < 13; i++)
        //				{
        //					this.m_Settings[i] = 0;
        //				}
        //			}
        //		}

        public enum WatchType
        {
            FirstPerson,
            ThirdPerson,
            Custem
        }

        public enum DISTANCE_CULL
        {
            LOW,
            MID,
            FULL,
            RESTORE
        }

        public enum POST_CAM
        {
            NONE,
            MID,
            FULL
        }

        public enum LIGHT
        {
            NONE,
            FULL
        }

        public enum UNLOADPROIR
        {
            IMMEDIATE,
            SHORT,
            LONG,
            VERYLONG
        }

        public delegate void void_func_void();

        public delegate void void_func_float_float(float currentTime, float deltaTime);

        public const int MoneyFlag = 2;

        private bool m_skillPreloading;

        private static float m_loadingValue = 0f;

        private static uint _MoneyID = 0u;

        public static int GameDifficulty = 0;

        //		public static int MapOffset = 4194304;

        //		public static float dyingRate = 0.1f;

        //		public static string dyingZhanLi = string.Empty;

        //		public static string normalZhanli = string.Empty;

        //		public static int IgnoreLayer = 2;

        //		public static int IgnoreMaskValue = (int)Mathf.Pow(2f, (float)PalMain.IgnoreLayer);

        //		public static List<string> dialogue = new List<string>();

        //		public BattleFormationManager CurBattleFormationManager = new BattleFormationManager();

        [NonSerialized]
        public static float GameBeginTime;

        [NonSerialized]
        public static float GameTotleTime;

        //		[SerializeField]
        //		private PalMain.WatchType mCurWatchType;

        //		public static string PlayerName = "Default";

        //		public static List<Landmark> landMarks = new List<Landmark>();

        //		public static string mapInfoPrefix = "/Resources/MapData/";

        //		public static GameObject mapinfo = null;

        //		public static Dictionary<int, int> ItemCount = new Dictionary<int, int>();

        //		public static Dictionary<int, List<IItem>> SceneItems = new Dictionary<int, List<IItem>>();

        //		public float minFPS = 10f;

        //		public static float MinFPS = 10f;

        //		public static float MinDeltaTime;

        //		public bool m_bIgnoreEnemy;

        //		public static BackgroundAudio backgroundAudio;

        //		public bool m_bVoiceBlance;

        //		private static GameObject mMainObj = null;

        private static GameObject m_MainCamera = null;

        public static Transform MainCameraTF = null;

        private static PalMain instance = null;

        //		public static bool IsXP = false;

        //		public static bool IsWin32 = false;

        //		public static bool ForceLow = false;

        private static bool initialized = false;

        //		public bool bPlayBeginMovie = true;

        //		private DateTime mLastConfigSave = DateTime.Now.AddSeconds(1.0);

        //		private static float CameraOptDistance = 60f;

        public static Action LoadOverEvent = null;

        //		private static Transform tempGameLayer = null;

        //		public QTEManager m_QTEManager = new QTEManager();

        //		private List<object> mTempObjects = new List<object>();

        //		private int mTempPosition;

        //		[NonSerialized]
        //		public float MoneyRate = 1f;

        //		[NonSerialized]
        //		public float DropAddRate;

        //		[NonSerialized]
        //		public float DPRate = 1f;

        //		public static PalMain.DISTANCE_CULL m_DistCull = PalMain.DISTANCE_CULL.RESTORE;

        //		public static PalMain.POST_CAM m_PostCam = PalMain.POST_CAM.FULL;

        //		public static PalMain.LIGHT m_Light = PalMain.LIGHT.FULL;

        //		private List<Behaviour> m_CamPosts = new List<Behaviour>();

        //		private List<Light> m_Lights = new List<Light>();

        //		private List<Light> m_LightsShadow = new List<Light>();

        //		private List<Camera> m_Cams = new List<Camera>();

        //		public float m_FarClipPlane;

        //		public bool m_bFog;

        //		public float m_FogStartDistance;

        //		public float m_FogEndDistance;

        //		public float m_FogDensity;

        //		public float m_TreeDistance;

        //		public float m_DetailObjectDistance;

        //		public float m_HeightmapPixelError;

        //		public float m_BasemapDistance;

        //		private float m_DualOffset = 0.2f;

        //		private int m_bDualCam;

        //		public Dictionary<int, PalMain.SceneOptiDistFogParams> m_LevelCullOpParams = new Dictionary<int, PalMain.SceneOptiDistFogParams>();

        //		private bool m_bIsIntel;

        //		private bool m_bIsGDI;

        //		public GameState m_lastGameState = GameState.None;

        //		public Camera m_SkyCam;

        //		private static PalMain.PLAYER_RECOMMANDATION m_PlayerRecommandation;

        //		public bool m_bZhuYuGame;

        //		public bool m_bGuHanJiang;

        //		public static PalMain.UNLOADPROIR m_CurPrior = PalMain.UNLOADPROIR.LONG;

        //		public static float m_CurUnloadTime = 0f;

        //		public static float m_FixImmediateTime = 1f;

        //		public static float m_FixShortTime = 5f;

        //		public static float m_FixLongTime = 15f;

        //		public static float m_FixVeryLongTime = 30f;

        //		public static bool m_bHasUnload = false;

        //		private static bool s_waitForActiveUserWarning = false;

        //		private static bool s_noControllerWarning = false;

        //		private static float s_noControllerTimeCounter = 0f;

        //		public int FOCUS_WAIT_FRAMES = 3;

        //		private int shouldRenewFrames;

        public event PalMain.void_func_float_float updateHandles;

        //		public event PalMain.void_func_void onInputHandles
        //		{
        //			[MethodImpl(MethodImplOptions.Synchronized)]
        //			add
        //			{
        //				this.onInputHandles = (PalMain.void_func_void)Delegate.Combine(this.onInputHandles, value);
        //			}
        //			[MethodImpl(MethodImplOptions.Synchronized)]
        //			remove
        //			{
        //				this.onInputHandles = (PalMain.void_func_void)Delegate.Remove(this.onInputHandles, value);
        //			}
        //		}

        //		public event PalMain.void_func_void onGUIHandles
        //		{
        //			[MethodImpl(MethodImplOptions.Synchronized)]
        //			add
        //			{
        //				this.onGUIHandles = (PalMain.void_func_void)Delegate.Combine(this.onGUIHandles, value);
        //			}
        //			[MethodImpl(MethodImplOptions.Synchronized)]
        //			remove
        //			{
        //				this.onGUIHandles = (PalMain.void_func_void)Delegate.Remove(this.onGUIHandles, value);
        //			}
        //		}

        //		public bool IsSkillPreloading
        //		{
        //			get
        //			{
        //				return this.m_skillPreloading;
        //			}
        //		}

        //		public static float LoadingValue
        //		{
        //			get
        //			{
        //				return PalMain.m_loadingValue;
        //			}
        //			set
        //			{
        //				PalMain.m_loadingValue = value;
        //			}
        //		}

        //		public static bool hasAssetBundleLoadOver
        //		{
        //			get
        //			{
        //				return false;
        //			}
        //		}

        //		public static uint MoneyID
        //		{
        //			get
        //			{
        //				if (PalMain._MoneyID == 0u)
        //				{
        //					PalMain._MoneyID = ItemManager.GetID(16u, 777u);
        //				}
        //				return PalMain._MoneyID;
        //			}
        //		}

        //		public PalMain.WatchType CurWatchType
        //		{
        //			get
        //			{
        //				return this.mCurWatchType;
        //			}
        //			set
        //			{
        //				if (this.mCurWatchType != value)
        //				{
        //					this.mCurWatchType = value;
        //					this.ResetWatch();
        //				}
        //			}
        //		}

        //		public static GameObject MapInfo
        //		{
        //			get
        //			{
        //				if (PalMain.mapinfo == null)
        //				{
        //					PalMain.mapinfo = GameObject.FindGameObjectWithTag("MapInfo");
        //					if (PalMain.mapinfo == null && SceneManager.GetActiveScene().name.ToLower() != "empty")
        //					{
        //						MapData data = MapData.GetData(UtilFun.GetPalMapLevel(SceneManager.GetActiveScene().buildIndex));
        //						if (data != null)
        //						{
        //							PalMain.mapinfo = FileLoader.LoadObjectFromFile<GameObject>((PalMain.mapInfoPrefix + data.MapInfoPath).ToAssetBundlePath(), true, true);
        //						}
        //					}
        //					if (PalMain.mapinfo == null && SceneManager.GetActiveScene().name.ToLower() != "empty")
        //					{
        //						UnityEngine.Debug.LogError("没有找到MapInfo，请在 地图csv中设置其相关路径");
        //						PalMain.mapinfo = new GameObject("MapInfo");
        //						PalMain.mapinfo.tag = "MapInfo";
        //					}
        //				}
        //				return PalMain.mapinfo;
        //			}
        //		}

        //		public static GameObject MainObj
        //		{
        //			get
        //			{
        //				if (PalMain.mMainObj == null)
        //				{
        //					PalMain.mMainObj = GameObject.Find("/Main");
        //					if (PalMain.mMainObj == null)
        //					{
        //						PalMain.mMainObj = new GameObject("Main");
        //					}
        //				}
        //				return PalMain.mMainObj;
        //			}
        //		}

        public static GameObject MainCamera
        {
            get
            {
                if (PalMain.m_MainCamera == null)
                {
                    GameObject gameObject = GameObject.Find("/Main Camera Pal");
                    if (gameObject != null)
                    {
                        PalMain.m_MainCamera = gameObject;
                    }
                    else if (UtilFun.GetMainCamera() != null)
                    {
                        PalMain.m_MainCamera = UtilFun.GetMainCamera().gameObject;
                    }
                    if (PalMain.m_MainCamera != null)
                    {
                        PalMain.MainCameraTF = PalMain.m_MainCamera.transform;
                    }
                }
                return PalMain.m_MainCamera;
            }
            set
            {
                PalMain.m_MainCamera = value;
                if (PalMain.m_MainCamera != null)
                {
                    PalMain.MainCameraTF = PalMain.m_MainCamera.transform;
                }
            }
        }

        public static PalMain GameMain
        {
            get
            {
                if (PalMain.instance == null)
                {
                    PalMain.CreateInstance();
                }
                return PalMain.instance;
            }
        }

        public static PalMain Instance
        {
            get
            {
                return PalMain.instance;
            }
        }

        //		public static bool IsLow
        //		{
        //			get
        //			{
        //				bool result = false;
        //				if (PalMain.ForceLow)
        //				{
        //					result = true;
        //				}
        //				else if (SystemInfo.graphicsMemorySize < 900)
        //				{
        //					result = true;
        //				}
        //				else if (PalMain.IsWin32)
        //				{
        //					result = true;
        //				}
        //				return result;
        //			}
        //		}

        //		public static bool MemoryLack
        //		{
        //			get
        //			{
        //				return false;
        //			}
        //		}

        //		public static Transform TempGameLayer
        //		{
        //			get
        //			{
        //				if (PalMain.tempGameLayer == null)
        //				{
        //					GameObject gameObject = GameObject.Find(SaveManager.TempGameLayerName);
        //					if (gameObject != null)
        //					{
        //						PalMain.tempGameLayer = gameObject.transform;
        //					}
        //					else
        //					{
        //						gameObject = new GameObject(SaveManager.TempGameLayerName);
        //						UtilFun.SetPosition(gameObject.transform, Vector3.zero);
        //						gameObject.transform.rotation = Quaternion.identity;
        //						PalMain.tempGameLayer = gameObject.transform;
        //					}
        //				}
        //				return PalMain.tempGameLayer;
        //			}
        //		}

        //		public void ResetWatch()
        //		{
        //			Transform transform = UtilFun.GetMainCamera().transform;
        //			FirstPersonEyesEffect component = transform.GetComponent<FirstPersonEyesEffect>();
        //			SmoothFollow2 component2 = transform.GetComponent<SmoothFollow2>();
        //			PalMain.WatchType watchType = this.mCurWatchType;
        //			if (watchType != PalMain.WatchType.FirstPerson)
        //			{
        //				if (watchType == PalMain.WatchType.ThirdPerson)
        //				{
        //					transform.parent = null;
        //					if (component != null)
        //					{
        //						component.enabled = false;
        //					}
        //					if (component2 != null)
        //					{
        //						component2.enabled = true;
        //					}
        //				}
        //			}
        //			else if (PlayersManager.Player)
        //			{
        //				transform.parent = GameObjectPath.GetHeadObj(PlayerCtrlManager.agentObj.transform);
        //				transform.localPosition = GameObjectPath.GetFirstPersonCamera(GameObjectPath.GetEyeObjs(PlayerCtrlManager.agentObj.transform));
        //				transform.localRotation = Quaternion.LookRotation(Vector3.up, -Vector3.right);
        //				if (component != null)
        //				{
        //					component.enabled = true;
        //				}
        //				if (component2 != null)
        //				{
        //					component2.enabled = false;
        //				}
        //			}
        //		}

        public void ReStart()
        {
            //UIManager.Instance.DoNotOpenMainMenu = false;
            PlayerTeam.ReStart();
            //PlayersManager.Restart();
            //PlayerCtrlManager.Reset();
            //PalMain.Instance.CurBattleFormationManager.Clear();
            //BattleFormationManager.BattleFormationData battleFormationData = new BattleFormationManager.BattleFormationData();
            //battleFormationData.m_InFormationCharaDatas.AddRange(new BattleFormationManager.InFormationCharaData[9]);
            //PalMain.Instance.CurBattleFormationManager.AddFormation(battleFormationData);
            //PalBattleManager.Instance().Restart();
            //FlagManager.InitFlags();
            //ItemManager.GetInstance().ClearData();
            //for (int i = 0; i < PlayersManager.AllPlayers.Count; i++)
            //{
            //    try
            //    {
            //        PalNPC component = PlayersManager.AllPlayers[i].GetComponent<PalNPC>();
            //        if (component != null && component.Data != null && component.m_SkillIDs != null)
            //        {
            //            PlayersManager.AllPlayers[i].GetComponent<PalNPC>().Data.Exp = 0;
            //            PlayersManager.AllPlayers[i].GetComponent<PalNPC>().Data.Soul = 0;
            //            PlayersManager.AllPlayers[i].GetComponent<PalNPC>().m_SkillIDs.Clear();
            //            if (i < PlayersManager.AllPlayers.Count)
            //            {
            //                component = PlayersManager.AllPlayers[i].GetComponent<PalNPC>();
            //                if (component != null)
            //                {
            //                    BuffDebuffManager.BuffDebuffOwner buffDebuffData = PlayersManager.AllPlayers[i].GetComponent<PalNPC>().BuffDebuffData;
            //                    if (buffDebuffData != null)
            //                    {
            //                        buffDebuffData.ClearData();
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    catch
            //    {
            //    }
            //}
            //OrnamentItemTypeCache.ClearIsGet();
            //FashionClothItemTypeCache.ClearIsGet();
            //MonsterProperty.HideAll();
            //UIInformation_Help_Item.ReStart();
            //UIInformation_StrangeNews_Item.ReStart();
            //MissionManager.Restart();
            //RenownManager.Reset();
            //HuanHuaManager.Instance.Reset();
            //DynamicObjsDataManager.Instance.Clear();
        }

        //		public static bool GetPersonCtrl()
        //		{
        //			return PlayerCtrlManager.bControl;
        //		}

        //		public static void CheckLottery(out int result, out string outString)
        //		{
        //			result = 1;
        //			outString = null;
        //			StringBuilder stringBuilder = null;
        //			result = (int)SelfExtern.Lottery(out stringBuilder);
        //			outString = stringBuilder.ToString();
        //		}

        //		public static void SpawnLottery()
        //		{
        //			List<PalGameObjectBase> monsters = CharactersManager.Instance.Monsters;
        //			PalGameObjectBase palGameObjectBase = null;
        //			int i = monsters.Count;
        //			while (i > 0)
        //			{
        //				i--;
        //				int index = UnityEngine.Random.Range(0, monsters.Count - 1);
        //				PalGameObjectBase palGameObjectBase2 = monsters[index];
        //				if (palGameObjectBase2 != null)
        //				{
        //					palGameObjectBase = palGameObjectBase2;
        //					break;
        //				}
        //			}
        //			if (palGameObjectBase == null)
        //			{
        //				UnityEngine.Debug.LogError("没有找到怪物，SpawnLottery失败");
        //				return;
        //			}
        //			palGameObjectBase.gameObject.SetActive(false);
        //			GameObject gameObject = new GameObject("Lottery");
        //			Transform transform = gameObject.transform;
        //			transform.position = palGameObjectBase.transform.position;
        //			transform.parent = palGameObjectBase.transform.parent;
        //			if (Console.showConsole)
        //			{
        //				string text = "Log : 乐动宝箱的位置为 " + transform.position.ToString();
        //				UnityEngine.Debug.Log(text);
        //				Console.Log(text);
        //			}
        //			PalLottery palLottery = gameObject.AddComponent<PalLottery>();
        //			palLottery.PickItemID = UnityEngine.Random.Range(1008, 1020);
        //			palLottery.SetModelResourcePath("Character/Props/EXPH_SQ_baoxiang_jin.prefab", 1);
        //			Interact interact = gameObject.AddComponent<Interact>();
        //			interact.ActionClassName = "baoxiang_Lottery";
        //		}

        //		public static void SetGameState(GameState newState)
        //		{
        //			GameStateManager.CurGameState = newState;
        //		}

        //		public static void ChangeModel(int ID, GameObject newGo)
        //		{
        //			GameObject gameObject = PlayersManager.FindMainChar(ID, true);
        //			if (gameObject == null)
        //			{
        //				UnityEngine.Debug.LogError("没有找到此人");
        //				return;
        //			}
        //			ModelChangeScript component = gameObject.GetComponent<ModelChangeScript>();
        //			if (component != null)
        //			{
        //				if (component.curMode == ModelChangeScript.Mode.Another)
        //				{
        //					component.curMode = ModelChangeScript.Mode.Original;
        //				}
        //				else
        //				{
        //					component.curMode = ModelChangeScript.Mode.Another;
        //				}
        //			}
        //			else
        //			{
        //				UnityEngine.Debug.LogError(ID.ToString() + "没有找到ModelChangeScript,直接调用了PalNPC里面的函数ChangeModel");
        //				PalNPC component2 = gameObject.GetComponent<PalNPC>();
        //				component2.ChangeModel(newGo, true);
        //			}
        //		}

        //		public static void AddRenownValue(int ID, int AddValue)
        //		{
        //			RenownManager.AddRenownValue(ID, AddValue);
        //		}

        //		public static Renown.Manner GetRenownManner(int ID)
        //		{
        //			return RenownManager.GetRenownManner(ID);
        //		}

        //		public static GameObject GetPlayer(bool bAgent)
        //		{
        //			GameObject result;
        //			if (bAgent)
        //			{
        //				result = PlayerCtrlManager.agentObj.gameObject;
        //			}
        //			else
        //			{
        //				result = PlayersManager.Player;
        //			}
        //			return result;
        //		}

        //		public static int GetPlayerID()
        //		{
        //			string name = PlayersManager.Player.name;
        //			return int.Parse(name);
        //		}

        //		public static bool ExistInTeam(int id)
        //		{
        //			GameObject player = PlayersManager.GetPlayer(id);
        //			return player != null;
        //		}

        //		public static GameObject GetModelObj(GameObject go)
        //		{
        //			GameObject gameObject = null;
        //			PalGameObjectBase component = go.GetComponent<PalGameObjectBase>();
        //			if (component != null)
        //			{
        //				gameObject = component.model;
        //			}
        //			if (gameObject == null)
        //			{
        //				gameObject = go;
        //			}
        //			return gameObject;
        //		}

        //		public static GameObject AddPlayer(int ID)
        //		{
        //			return PlayersManager.AddPlayer(ID, true);
        //		}

        //		public static void SetPlayer(int Index)
        //		{
        //			PlayersManager.SetPlayer(Index, true);
        //		}

        public static int CreateInstance()
        {
            if (PalMain.instance != null)
            {
                return 1;
            }
            string path = "/Resources/Template/System/Main.prefab";
            GameObject gameObject = FileLoader.LoadObjectFromFile<GameObject>(path.ToAssetBundlePath(), true, true);
            string path2 = "/Resources/Template/System/Main Camera Pal.prefab";
            GameObject gameObject2 = FileLoader.LoadObjectFromFile<GameObject>(path2.ToAssetBundlePath(), true, true);
            gameObject.name = "Main";
            gameObject2.name = "Main Camera Pal";
            PalMain.instance = gameObject.GetComponent<PalMain>();
            if (PalMain.instance == null)
            {
                UnityEngine.Debug.LogError("Main Prefab 没有 PalMain");
                return 0;
            }
            PalMain.instance.Initialize();
            return 1;
        }

        public static bool Exist()
        {
            return PalMain.instance != null;
        }

        private void Awake()
        {
            if (PalMain.instance != null && PalMain.instance != this)
            {
                UnityEngine.Object.Destroy(base.gameObject);
                return;
            }
            this.Initialize();
        }

        private void OnDestroy()
        {
        }

        private void Initialize()
        {
            if (PalMain.initialized || !Application.isPlaying)
            {
                return;
            }

            //try
            //{
            //    if (ConfigManager.ReadWritePath == null)
            //    {
            //        UnityEngine.Debug.LogError("Read or write fail 0x2204.");
            //        UtilFun.WinMessageBox("read or write fail", "error", 8708);
            //        Application.Quit();
            //        return;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    UnityEngine.Debug.Log(ex.ToString() + " 0x2204.");
            //    UtilFun.WinMessageBox(ex.ToString(), "error", 8708);
            //    Application.Quit();
            //    return;
            //}

            //OptionConfig optionConfig = OptionConfig.GetInstance();
            //if (!ConfigManager.IsFileExist())
            //{
            //    optionConfig.SetAllQualityFirstTime(PalMain.FirstTimeLaunch103());
            //    optionConfig.Save();
            //}
            //optionConfig.Use_Start();
            //optionConfig.Use_Other();

            Physics.gravity = new Vector3(0f, -20f, 0f);
            Physics2D.queriesHitTriggers = false;
            PalMain.instance = this;

            //ItemManager.GetInstance().OnBeforeRemoveItem += new Action<IItem>(this.PutOffItem);
            //ItemPackage orCreatePackage = ItemManager.GetInstance().GetOrCreatePackage(1u);
            //orCreatePackage.OnItemAdded += delegate (IItem obj)
            //{
            //    SymbolNodeItemType symbolNodeItemType = obj.ItemType as SymbolNodeItemType;
            //    if (symbolNodeItemType != null)
            //    {
            //        SymbolNodeItemType.SetState(symbolNodeItemType.TypeID & 255u, true);
            //    }
            //    SymbolPanelItemType symbolPanelItemType = obj.ItemType as SymbolPanelItemType;
            //    if (symbolPanelItemType != null)
            //    {
            //        SymbolPanelItemType.SetState(symbolPanelItemType.TypeID & 255u, true);
            //    }
            //    AchievementManager.SingleAchievement[] achievements = PalBattleManager.Instance().m_Achievement.m_Achievements;
            //    OrnamentItemTypeCache.SetIsGet(obj.ItemType.TypeID, true);
            //    achievements[190].m_CurrentNum = ((OrnamentItemTypeCache.IsGetRate() < 1f) ? 0 : 1);
            //    FashionClothItemTypeCache.SetIsGet(obj.ItemType.TypeID, true);
            //    achievements[189].m_CurrentNum = ((FashionClothItemTypeCache.IsGetRate() < 1f) ? 0 : 1);
            //};

            //MouseEventManager.Initialize();
            //MessageProcess.Initialize();
            //SetActiveByFlagManager.Initialize();
            ScenesManager.Initialize();
            FlagManager.Initialize();
            //DistanceCullManager.Initialize();
            //CharactersManager.Initialize();
            InputManager.Initialize();
            PlayerCtrlManager.Initialize();
            //PalBattleManager.Initialize();
            //EntityManager.Initialize();
            //MissionTick.Initialize();
            PlayersManager.Initialize();
            //RenownManager.Initialize();
            //AnimWithoutClothSet.Initialize();
            //SlowLoopAnimSet.Initialize();
            //WaterEffectTrigger.Initialize();
            //CompoundItemList.CreateWatch();
            //SaveManager.OnLoadOver += delegate
            //{
            //    try
            //    {
            //        int num2 = 0;
            //        List<IEnumerator> list = new List<IEnumerator>(8);
            //        foreach (GameObject current in PlayersManager.AllPlayers)
            //        {
            //            if (current != null)
            //            {
            //                PalNPC component = current.GetComponent<PalNPC>();
            //                if (component != null)
            //                {
            //                    list.Add(component.Prepare());
            //                }
            //            }
            //        }
            //        list.Add(ItemManager.GetInstance().Prepare());
            //        list.Add(BuffDebuffManager.GetInstance().Prepare());
            //        while (list.Count > 0)
            //        {
            //            list.RemoveAll((IEnumerator curManager) => curManager == null || !curManager.MoveNext());
            //            num2++;
            //            if (num2 > 32)
            //            {
            //                UnityEngine.Debug.LogError("加载进程可能陷入了死循环");
            //                foreach (IEnumerator current2 in list)
            //                {
            //                    UnityEngine.Debug.LogError(current2.ToString());
            //                }
            //                break;
            //            }
            //        }
            //        CompoundItemList.CreateWatch();
            //        SoulStarData[] datasFromFile = SoulDataManager.GetDatasFromFile();
            //        for (int i = 0; i < datasFromFile.Length; i++)
            //        {
            //            SoulStarData soulStarData = datasFromFile[i];
            //            if (SoulDataManager.Instance != null)
            //            {
            //                if (soulStarData != null)
            //                {
            //                    if (SoulDataManager.Instance.IsSoulOpen(soulStarData.NodeID))
            //                    {
            //                        soulStarData.OpenScriptFun();
            //                    }
            //                }
            //            }
            //        }
            //        foreach (GameObject current3 in PlayersManager.AllPlayers)
            //        {
            //            PalNPC component2 = current3.GetComponent<PalNPC>();
            //            if (component2.Data.HPMPDP != null)
            //            {
            //                component2.Data.HPMPDP.SetWithoutEvents(component2.Data.LoadHP, component2.Data.LoadMP, component2.Data.LoadDP);
            //            }
            //        }
            //        try
            //        {
            //            if (ItemManager.GetInstance() != null)
            //            {
            //                ItemPackage orCreatePackage2 = ItemManager.GetInstance().GetOrCreatePackage(1u);
            //                if (orCreatePackage2 != null)
            //                {
            //                    foreach (IItem[] current4 in ItemManager.GetInstance().GetOrCreatePackage(1u).ForEachItemArrayInPackage())
            //                    {
            //                        if (current4 != null && current4.Length >= 0 && current4[0] != null)
            //                        {
            //                            if (0 < current4.Length && current4[0].ItemType != null)
            //                            {
            //                                CompoundItemList.ReCheck(current4[0].ItemType.TypeID);
            //                            }
            //                        }
            //                    }
            //                    int hadCreateCount = CompoundItemList.HadCreateCount;
            //                    AchievementManager.SingleAchievement[] achievements = PalBattleManager.Instance().m_Achievement.m_Achievements;
            //                    achievements[7].m_CurrentNum = hadCreateCount;
            //                    achievements[8].m_CurrentNum = hadCreateCount;
            //                    achievements[9].m_CurrentNum = hadCreateCount;
            //                }
            //            }
            //        }
            //        catch (Exception exception)
            //        {
            //            UnityEngine.Debug.LogException(exception);
            //        }
            //        try
            //        {
            //            for (int j = 0; j < 6; j++)
            //            {
            //                bool flag = true;
            //                int num3 = j * 84;
            //                for (int k = 0; k < 84; k++)
            //                {
            //                    if (SoulDataManager.GetData(num3 + k) != null)
            //                    {
            //                        if (!SoulDataManager.Instance.IsSoulOpen(num3 + k))
            //                        {
            //                            flag = false;
            //                            break;
            //                        }
            //                    }
            //                }
            //                if (flag)
            //                {
            //                    PalBattleManager.Instance().m_Achievement.m_Achievements[21].m_CurrentNum = ((!flag) ? 0 : 262143);
            //                    break;
            //                }
            //            }
            //        }
            //        catch (Exception exception2)
            //        {
            //            UnityEngine.Debug.LogException(exception2);
            //        }
            //        try
            //        {
            //            foreach (GameObject current5 in PlayersManager.AllPlayers)
            //            {
            //                if (!(current5 == null))
            //                {
            //                    PalNPC component3 = current5.GetComponent<PalNPC>();
            //                    if (!(component3 == null) && component3.Data != null)
            //                    {
            //                        if (component3.Data.CharacterID >= 0 && component3.Data.CharacterID <= 5)
            //                        {
            //                            IItem item = component3.GetSlot(EquipSlotEnum.Weapon);
            //                            if (item == null)
            //                            {
            //                                foreach (IItem[] current6 in ItemManager.GetInstance().GetOrCreatePackage(1u).ForEachItemArrayInPackage())
            //                                {
            //                                    if (current6 != null && current6.Length > 0)
            //                                    {
            //                                        if (current6[0] is WeaponItem)
            //                                        {
            //                                            IItem[] array = current6;
            //                                            for (int l = 0; l < array.Length; l++)
            //                                            {
            //                                                IItem item2 = array[l];
            //                                                if (item2 != null)
            //                                                {
            //                                                    WeaponItem weaponItem = item2 as WeaponItem;
            //                                                    if (weaponItem != null && !(weaponItem.GetOwner() != null))
            //                                                    {
            //                                                        WeaponItemType weaponItemType = weaponItem.ItemType as WeaponItemType;
            //                                                        if (weaponItemType != null)
            //                                                        {
            //                                                            if (((ulong)weaponItemType.CharacterMark & (ulong)(1L << (component3.Data.CharacterID & 31))) != 0uL)
            //                                                            {
            //                                                                if (item == null)
            //                                                                {
            //                                                                    item = weaponItem;
            //                                                                }
            //                                                                else if (ShowWeaponItemComparer.GetInstance().Compare(item, weaponItem) > 0)
            //                                                                {
            //                                                                    item = weaponItem;
            //                                                                }
            //                                                                break;
            //                                                            }
            //                                                        }
            //                                                    }
            //                                                }
            //                                            }
            //                                        }
            //                                    }
            //                                }
            //                                if (item != null)
            //                                {
            //                                    component3.PutOnEquip(item as WeaponItem);
            //                                }
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //        catch (Exception exception3)
            //        {
            //            UnityEngine.Debug.LogException(exception3);
            //        }
            //    }
            //    catch (Exception exception4)
            //    {
            //        UnityEngine.Debug.LogException(exception4);
            //    }
            //};
            //PlayersManager.OnAddPlayer = (Action<int>)Delegate.Combine(PlayersManager.OnAddPlayer, new Action<int>(delegate (int playerid)
            //{
            //    if (PalMain.Instance.CurBattleFormationManager.m_Formations.Count <= 0)
            //    {
            //        BattleFormationManager.BattleFormationData battleFormationData = new BattleFormationManager.BattleFormationData();
            //        PalMain.Instance.CurBattleFormationManager.AddFormation(battleFormationData);
            //        foreach (GameObject current in PlayersManager.ActivePlayers)
            //        {
            //            battleFormationData.AddOrChangeCharacter(current.GetCharacterID(), 0);
            //        }
            //        while (battleFormationData.m_InFormationCharaDatas.Count < 9)
            //        {
            //            battleFormationData.m_InFormationCharaDatas.Add(null);
            //        }
            //    }
            //    else
            //    {
            //        BattleFormationManager.BattleFormationData battleFormationData = PalMain.Instance.CurBattleFormationManager.m_Formations[0];
            //        while (battleFormationData.m_InFormationCharaDatas.Count < 9)
            //        {
            //            battleFormationData.m_InFormationCharaDatas.Add(null);
            //        }
            //        if (battleFormationData.GetPlayer(playerid) == null)
            //        {
            //            for (int i = 0; i < 9; i++)
            //            {
            //                if (battleFormationData.m_InFormationCharaDatas[i] == null)
            //                {
            //                    battleFormationData.m_InFormationCharaDatas[i] = new BattleFormationManager.InFormationCharaData(playerid, 0);
            //                    break;
            //                }
            //            }
            //        }
            //    }
            //}));
            //PlayersManager.OnRemovePlayer += delegate (int playerid)
            //{
            //    if (PalMain.Instance.CurBattleFormationManager.m_Formations.Count <= 0)
            //    {
            //        return;
            //    }
            //    BattleFormationManager.BattleFormationData battleFormationData = PalMain.Instance.CurBattleFormationManager.m_Formations[0];
            //    if (battleFormationData == null)
            //    {
            //        return;
            //    }
            //    if (battleFormationData.m_InFormationCharaDatas[0] != null && battleFormationData.m_InFormationCharaDatas[0].m_CharacterID != playerid)
            //    {
            //        return;
            //    }
            //    while (battleFormationData.m_InFormationCharaDatas.Count < 9)
            //    {
            //        battleFormationData.m_InFormationCharaDatas.Add(null);
            //    }
            //    for (int i = 1; i < 9; i++)
            //    {
            //        BattleFormationManager.InFormationCharaData inFormationCharaData = battleFormationData.m_InFormationCharaDatas[i];
            //        if (inFormationCharaData != null)
            //        {
            //            if (!(PlayersManager.GetPlayer(inFormationCharaData.m_CharacterID) == null))
            //            {
            //                battleFormationData.m_InFormationCharaDatas[i] = battleFormationData.m_InFormationCharaDatas[0];
            //                battleFormationData.m_InFormationCharaDatas[0] = inFormationCharaData;
            //                break;
            //            }
            //        }
            //    }
            //};
            //UIManager.Instance.Initialize();
            //EntityManager.OnLoadOver = (EntityManager.void_fun)Delegate.Combine(EntityManager.OnLoadOver, new EntityManager.void_fun(MiniMap.Instance.OnLoadOver));
            //PalMain.initialized = true;
            //MissionManager.Initialize();
            //HuanHuaManager.Instance.Initialize();
            //PanelFaceManager.Initialize();
            //PanelPositonManager.Initialize();
            //Cutscene2DManager.Initialize();
            //ScenesManager.Instance.RandomFlash();
            //TimeManager.Initialize();
            //SaveManager.OnLoadOver -= new SaveManager.void_fun(SceneFall2.SetLastPointOnLoadOver);
            //SaveManager.OnLoadOver += new SaveManager.void_fun(SceneFall2.SetLastPointOnLoadOver);
            //PalMain.MinFPS = this.minFPS;
            //PalMain.MinDeltaTime = 1f / this.minFPS;
            //PalMain.backgroundAudio = base.gameObject.GetComponent<BackgroundAudio>();
            //if (PalMain.backgroundAudio == null)
            //{
            //    PalMain.backgroundAudio = base.gameObject.AddComponent<BackgroundAudio>();
            //}
            //OptionConfig.GetInstance().Use_CharacterEmission();
            //SaveManager.LoadGlobalData();
            //FunfiaSteamManager funfiaSteamManager = FunfiaSteamManager.Instance;
            //if (SkillSEPreloader.s_preloadEnable && SkillSEPreloader.Instance == null)
            //{
            //    SkillSEPreloader.Initialize();
            //}
        }

        //		private void PutOffItem(IItem inItem)
        //		{
        //			IItemAssemble<SymbolPanelItem> itemAssemble = inItem as IItemAssemble<SymbolPanelItem>;
        //			if (itemAssemble != null)
        //			{
        //				SymbolPanelItem owner = itemAssemble.GetOwner();
        //				if (owner != null)
        //				{
        //					owner.RemoveNode(itemAssemble as SymbolNodeItem);
        //				}
        //			}
        //			IItemAssemble<PalNPC> itemAssemble2 = inItem as IItemAssemble<PalNPC>;
        //			if (itemAssemble2 != null)
        //			{
        //				PalNPC owner2 = itemAssemble2.GetOwner();
        //				if (owner2 != null)
        //				{
        //					owner2.PutOffEquip(inItem);
        //				}
        //			}
        //		}

        //		private void PlayBeginMovie(float d, float a)
        //		{
        //			if (StartInit.Instance == null)
        //			{
        //				UnityEngine.Debug.LogError("StartInit.Instance==null 无法播放健康公告");
        //			}
        //			else
        //			{
        //				StartInit.Instance.ShowCore();
        //			}
        //			this.updateHandles = (PalMain.void_func_float_float)Delegate.Remove(this.updateHandles, new PalMain.void_func_float_float(this.PlayBeginMovie));
        //		}

        private void Start()
        {
            //this.m_LevelCullOpParams.Add(0, new PalMain.SceneOptiDistFogParams(0, -1f, 305f, 305f));
            //this.m_LevelCullOpParams.Add(1, new PalMain.SceneOptiDistFogParams(1, -1f, 200f, 105f));
            //this.m_LevelCullOpParams.Add(2, new PalMain.SceneOptiDistFogParams(2, -1f, 145f, 145f));
            //this.m_LevelCullOpParams.Add(3, new PalMain.SceneOptiDistFogParams(3, -1f, 150f, 75f));
            //this.m_LevelCullOpParams.Add(4, new PalMain.SceneOptiDistFogParams(4, -1f, 150f, 75f));
            //this.m_LevelCullOpParams.Add(5, new PalMain.SceneOptiDistFogParams(5, -1f, 200f, 75f));
            //this.m_LevelCullOpParams.Add(6, new PalMain.SceneOptiDistFogParams(6, -1f, 70f, 45f));
            //this.m_LevelCullOpParams.Add(7, new PalMain.SceneOptiDistFogParams(7, -1f, 150f, 95f));
            //this.m_LevelCullOpParams.Add(8, new PalMain.SceneOptiDistFogParams(8, -1f, 200f, 105f));
            //this.m_LevelCullOpParams.Add(9, new PalMain.SceneOptiDistFogParams(9, -1f, 100f, 65f));
            //this.m_LevelCullOpParams.Add(10, new PalMain.SceneOptiDistFogParams(10, -1f, 120f, 85f));
            //this.m_LevelCullOpParams.Add(11, new PalMain.SceneOptiDistFogParams(11, -1f, 70f, 35f));
            //this.m_LevelCullOpParams.Add(12, new PalMain.SceneOptiDistFogParams(12, -1f, 80f, 105f));
            //this.m_LevelCullOpParams.Add(13, new PalMain.SceneOptiDistFogParams(13, -1f, 150f, 75f));
            //this.m_LevelCullOpParams.Add(14, new PalMain.SceneOptiDistFogParams(14, -1f, 140f, 85f));
            //this.m_LevelCullOpParams.Add(15, new PalMain.SceneOptiDistFogParams(15, -1f, 100f, 75f));
            //this.m_LevelCullOpParams.Add(16, new PalMain.SceneOptiDistFogParams(16, -1f, 200f, 205f));
            //this.m_LevelCullOpParams.Add(17, new PalMain.SceneOptiDistFogParams(17, -1f, 140f, 75f));
            //this.m_LevelCullOpParams.Add(18, new PalMain.SceneOptiDistFogParams(18, -1f, 200f, 205f));
            //this.m_LevelCullOpParams.Add(19, new PalMain.SceneOptiDistFogParams(19, -1f, 250f, 155f));
            //this.m_LevelCullOpParams.Add(20, new PalMain.SceneOptiDistFogParams(20, -1f, 150f, 85f));
            //this.m_LevelCullOpParams.Add(21, new PalMain.SceneOptiDistFogParams(21, -1f, 300f, 305f));
            //this.m_LevelCullOpParams.Add(22, new PalMain.SceneOptiDistFogParams(22, -1f, 150f, 55f));
            //this.m_LevelCullOpParams.Add(23, new PalMain.SceneOptiDistFogParams(23, -1f, 120f, 55f));
            //this.m_LevelCullOpParams.Add(24, new PalMain.SceneOptiDistFogParams(24, -1f, 100f, 105f));
            //this.m_LevelCullOpParams.Add(25, new PalMain.SceneOptiDistFogParams(25, -1f, 120f, 45f));
            //this.m_LevelCullOpParams.Add(26, new PalMain.SceneOptiDistFogParams(26, -1f, 75f, 75f));
            //this.m_LevelCullOpParams.Add(27, new PalMain.SceneOptiDistFogParams(27, -1f, 200f, 75f));
            //this.m_LevelCullOpParams.Add(28, new PalMain.SceneOptiDistFogParams(28, -1f, 200f, 75f));
            //this.m_LevelCullOpParams.Add(29, new PalMain.SceneOptiDistFogParams(29, -1f, 105f, 105f));
            //this.m_LevelCullOpParams.Add(30, new PalMain.SceneOptiDistFogParams(30, -1f, 105f, 105f));
            //this.m_LevelCullOpParams.Add(31, new PalMain.SceneOptiDistFogParams(31, -1f, 150f, 75f));
            //this.m_LevelCullOpParams.Add(32, new PalMain.SceneOptiDistFogParams(32, -1f, 105f, 105f));
            //this.m_LevelCullOpParams.Add(33, new PalMain.SceneOptiDistFogParams(33, -1f, 200f, 105f));
            //this.m_LevelCullOpParams.Add(34, new PalMain.SceneOptiDistFogParams(34, -1f, 905f, 905f));
            //string text = SystemInfo.graphicsDeviceName;
            //string text2 = SystemInfo.graphicsDeviceVendor;
            //text = text.ToLower();
            //text2 = text2.ToLower();
            //if (text.Contains("intel"))
            //{
            //    this.m_bIsIntel = true;
            //}
            //if (text2.Contains("intel"))
            //{
            //    this.m_bIsIntel = true;
            //}
            //if (text.Contains("gdi"))
            //{
            //    this.m_bIsGDI = true;
            //}
            //if (text2.Contains("gdi"))
            //{
            //    this.m_bIsGDI = true;
            //}
            //int systemMemorySize = SystemInfo.systemMemorySize;
            //if (base.GetComponent<LoadFont>() == null)
            //{
            //    base.gameObject.AddComponent<LoadFont>();
            //}
            //if (base.GetComponent<AudioSource>() == null)
            //{
            //    base.gameObject.AddComponent<AudioSource>();
            //}
            //PalMain.backgroundAudio = base.GetComponent<BackgroundAudio>();
            //if (PalMain.backgroundAudio == null)
            //{
            //    PalMain.backgroundAudio = base.gameObject.AddComponent<BackgroundAudio>();
            //}
        }

        //		public static void RefreshAllLandMarks()
        //		{
        //			PalMain.landMarks.Clear();
        //			Landmark[] collection = (Landmark[])UnityEngine.Object.FindObjectsOfType(typeof(Landmark));
        //			PalMain.landMarks.AddRange(collection);
        //		}

        //		private void UpdateMinFPS()
        //		{
        //			if (PalMain.MinFPS != this.minFPS)
        //			{
        //				PalMain.MinFPS = this.minFPS;
        //				PalMain.MinDeltaTime = 1f / this.minFPS;
        //			}
        //		}

        private void Update()
        {

            //if (this.onInputHandles != null)
            //{
            //    this.onInputHandles();
            //}
           
            if (this.updateHandles != null)
            {
                this.updateHandles(Time.time, Time.deltaTime);
            }
            //this.m_QTEManager.Update();
            //if (ScenesManager.IsChanging || ShowLoading.Instance != null)
            //{
            //    return;
            //}
            //this.UpdateOpCull();
            //PalMain.UpdateCheckUnload();
            //this.UpdateSpecialIssueForNonFocus();
            //if (WaitForSonyCheck.Instance == null)
            //{
            //}
            FileLoader.Instance.Update();
        }

        private void FixedUpdate()
        {
            //BuffDebuffManager.GetInstance().FixedUpdate(Time.fixedTime);
            //if (DateTime.Now > this.mLastConfigSave)
            //{
            //    this.mLastConfigSave = DateTime.Now.AddSeconds(1.0);
            //    bool flag = false;
            //    OptionConfig optionConfig = OptionConfig.GetInstance();
            //    if (optionConfig.IsDirty)
            //    {
            //        optionConfig.Save();
            //        flag = true;
            //    }
            //    if (flag)
            //    {
            //        SaveManager.SaveGlobalData();
            //    }
            //}
        }

        //		private void ReplaceMeshColliderFor7()
        //		{
        //			GameObject gameObject = GameObject.Find("/jianzhu");
        //			if (gameObject != null)
        //			{
        //				MeshCollider[] componentsInChildren = gameObject.GetComponentsInChildren<MeshCollider>();
        //				for (int i = 0; i < componentsInChildren.Length; i++)
        //				{
        //					MeshCollider meshCollider = componentsInChildren[i];
        //					if (!(meshCollider.name == "jz_ymh_shitou12") && !(meshCollider.name == "jz_fyy_st01") && !(meshCollider.name == "jz_fyy_st02") && !(meshCollider.name == "jz_fyy_st03") && !(meshCollider.name == "jz_fyy_st04") && !(meshCollider.name == "jz_fyy_shitou07") && !(meshCollider.name == "jz_fyy_shitou05") && !(meshCollider.name == "Object566") && !(meshCollider.name == "Object567") && !(meshCollider.name == "Object572") && !(meshCollider.name == "Object574") && !(meshCollider.name == "Object575") && !(meshCollider.name == "Object576"))
        //					{
        //						meshCollider.enabled = false;
        //					}
        //				}
        //			}
        //			FileLoader.LoadObjectFromFile<UnityEngine.Object>("Collider/pengzhuang7".ToAssetBundlePath(), true, true);
        //		}

        //		private void ReplaceMapElementFor25()
        //		{
        //			GameObject gameObject = FileLoader.LoadObjectFromFile<GameObject>("LevelParticles/EXPH_huanjing_PUBU_B".ToAssetBundlePath(), true, true);
        //			if (gameObject == null)
        //			{
        //				UnityEngine.Debug.LogError("Error : 没有找到 LevelParticles/EXPH_huanjing_PUBU_B");
        //				return;
        //			}
        //			string str = "/texiao/PUBU";
        //			for (int i = 1; i < 5; i++)
        //			{
        //				string name = str + i.ToString();
        //				GameObject gameObject2 = GameObject.Find(name);
        //				if (gameObject2 != null)
        //				{
        //					Transform[] componentsInChildren = gameObject2.GetComponentsInChildren<Transform>();
        //					Transform[] array = componentsInChildren;
        //					for (int j = 0; j < array.Length; j++)
        //					{
        //						Transform transform = array[j];
        //						if (transform.name == "EXPH_huanjing_PUBU_B")
        //						{
        //							transform.gameObject.SetActive(false);
        //							GameObject gameObject3 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
        //							Transform transform2 = gameObject3.transform;
        //							transform2.parent = transform.parent;
        //							transform2.position = transform.position;
        //							transform2.localScale = transform.localScale;
        //							transform2.localRotation = transform.localRotation;
        //						}
        //					}
        //				}
        //			}
        //			UnityEngine.Object.Destroy(gameObject);
        //		}

        //		public static void SetForOpt(bool bActive)
        //		{
        //			PalMain.SetEnableWaterReflect("/shui", !bActive);
        //			PalMain.SetEnableMeshCollide("/shitou", !bActive);
        //		}

        //		public static void SetEnableWaterReflect(string path, bool bEnable)
        //		{
        //			GameObject gameObject = GameObject.Find(path);
        //			if (gameObject == null)
        //			{
        //				UnityEngine.Debug.LogError("Error : SetEnableWaterReflect 没有找到 " + path);
        //				return;
        //			}
        //			PalMirrorReflection[] componentsInChildren = gameObject.GetComponentsInChildren<PalMirrorReflection>();
        //			for (int i = 0; i < componentsInChildren.Length; i++)
        //			{
        //				PalMirrorReflection palMirrorReflection = componentsInChildren[i];
        //				palMirrorReflection.enabled = bEnable;
        //			}
        //		}

        //		public static void SetEnableMeshCollide(string path, bool bEnable)
        //		{
        //			GameObject gameObject = GameObject.Find(path);
        //			if (gameObject == null)
        //			{
        //				UnityEngine.Debug.LogError("Error : SetEnableMeshCollide 没有找到 " + path);
        //				return;
        //			}
        //			MeshCollider[] componentsInChildren = gameObject.GetComponentsInChildren<MeshCollider>();
        //			for (int i = 0; i < componentsInChildren.Length; i++)
        //			{
        //				MeshCollider meshCollider = componentsInChildren[i];
        //				meshCollider.enabled = bEnable;
        //			}
        //		}

        //		public void SpecialProcessForLevel(int level)
        //		{
        //			Terrain activeTerrain = Terrain.activeTerrain;
        //			if (level == 11)
        //			{
        //				FlagManager.SetFlag(8, 0, false);
        //			}
        //			switch (level)
        //			{
        //			case 1:
        //				if (activeTerrain != null)
        //				{
        //					activeTerrain.basemapDistance = 100f;
        //					activeTerrain.castShadows = false;
        //					activeTerrain.treeBillboardDistance = 119f;
        //					activeTerrain.treeCrossFadeLength = 12f;
        //				}
        //				break;
        //			case 3:
        //				if (activeTerrain != null)
        //				{
        //					activeTerrain.heightmapPixelError = 33f;
        //					activeTerrain.basemapDistance = 130f;
        //					activeTerrain.castShadows = false;
        //					activeTerrain.treeBillboardDistance = 95f;
        //					activeTerrain.treeCrossFadeLength = 76f;
        //				}
        //				break;
        //			case 4:
        //				if (activeTerrain != null)
        //				{
        //					activeTerrain.basemapDistance = 40f;
        //					activeTerrain.castShadows = false;
        //					activeTerrain.treeBillboardDistance = 31f;
        //				}
        //				break;
        //			case 5:
        //				if (activeTerrain != null)
        //				{
        //					activeTerrain.heightmapPixelError = 63f;
        //					activeTerrain.basemapDistance = 117f;
        //					activeTerrain.treeBillboardDistance = 103f;
        //					activeTerrain.treeCrossFadeLength = 35f;
        //				}
        //				break;
        //			case 6:
        //				if (activeTerrain != null)
        //				{
        //					activeTerrain.heightmapPixelError = 44f;
        //					activeTerrain.basemapDistance = 128f;
        //					activeTerrain.castShadows = false;
        //				}
        //				if (PalShroudObjectManager.Instance != null)
        //				{
        //					Vector3 windDir = new Vector3(0.2f, 0f, -0.64f);
        //					PalShroudObjectManager.Instance.WindDir = windDir;
        //				}
        //				break;
        //			case 7:
        //				if (activeTerrain != null)
        //				{
        //					activeTerrain.heightmapPixelError = 60f;
        //					activeTerrain.basemapDistance = 160f;
        //					activeTerrain.castShadows = false;
        //				}
        //				break;
        //			case 8:
        //				if (activeTerrain != null)
        //				{
        //					activeTerrain.basemapDistance = 56f;
        //					activeTerrain.castShadows = false;
        //					activeTerrain.treeDistance = 291f;
        //					activeTerrain.treeBillboardDistance = 150f;
        //					activeTerrain.treeCrossFadeLength = 45f;
        //				}
        //				break;
        //			case 10:
        //				if (activeTerrain != null)
        //				{
        //					activeTerrain.heightmapPixelError = 18f;
        //					activeTerrain.basemapDistance = 97f;
        //					activeTerrain.castShadows = false;
        //					activeTerrain.treeBillboardDistance = 90f;
        //					activeTerrain.treeCrossFadeLength = 50f;
        //				}
        //				break;
        //			case 12:
        //			{
        //				GameObject gameObject = GameObject.Find("/texiao/EXPH_P_JQ44_HUNYU02");
        //				if (gameObject != null)
        //				{
        //					UtilFun.SetActive(gameObject, false);
        //				}
        //				break;
        //			}
        //			case 13:
        //				if (activeTerrain != null)
        //				{
        //					activeTerrain.heightmapPixelError = 100f;
        //					activeTerrain.basemapDistance = 38f;
        //					activeTerrain.castShadows = false;
        //					activeTerrain.treeBillboardDistance = 67f;
        //					activeTerrain.treeCrossFadeLength = 28f;
        //				}
        //				break;
        //			case 14:
        //				if (activeTerrain != null)
        //				{
        //					activeTerrain.heightmapPixelError = 44f;
        //					activeTerrain.basemapDistance = 114f;
        //					activeTerrain.castShadows = false;
        //					activeTerrain.treeBillboardDistance = 60f;
        //					activeTerrain.treeCrossFadeLength = 25f;
        //				}
        //				break;
        //			case 15:
        //			{
        //				RenderSettings.skybox = null;
        //				Camera mainCamera = UtilFun.GetMainCamera();
        //				if (mainCamera != null)
        //				{
        //					mainCamera.backgroundColor = Color.black;
        //				}
        //				break;
        //			}
        //			case 17:
        //				if (activeTerrain != null)
        //				{
        //					activeTerrain.heightmapPixelError = 16f;
        //					activeTerrain.basemapDistance = 159f;
        //					activeTerrain.castShadows = false;
        //					activeTerrain.treeBillboardDistance = 155f;
        //					activeTerrain.treeCrossFadeLength = 32f;
        //				}
        //				break;
        //			case 20:
        //				if (activeTerrain != null)
        //				{
        //					activeTerrain.heightmapPixelError = 28f;
        //					activeTerrain.basemapDistance = 129f;
        //					activeTerrain.castShadows = false;
        //					activeTerrain.treeBillboardDistance = 116f;
        //					activeTerrain.treeCrossFadeLength = 44f;
        //				}
        //				break;
        //			case 21:
        //				if (activeTerrain != null)
        //				{
        //					activeTerrain.basemapDistance = 626f;
        //					activeTerrain.castShadows = false;
        //					activeTerrain.treeBillboardDistance = 341f;
        //					activeTerrain.treeCrossFadeLength = 44f;
        //				}
        //				break;
        //			case 22:
        //				if (activeTerrain != null)
        //				{
        //					activeTerrain.heightmapPixelError = 37f;
        //					activeTerrain.basemapDistance = 113f;
        //					activeTerrain.castShadows = false;
        //					activeTerrain.treeBillboardDistance = 128f;
        //					activeTerrain.treeCrossFadeLength = 23f;
        //				}
        //				break;
        //			case 23:
        //				if (activeTerrain != null)
        //				{
        //					activeTerrain.castShadows = false;
        //					activeTerrain.treeBillboardDistance = 93f;
        //					activeTerrain.treeCrossFadeLength = 22f;
        //				}
        //				break;
        //			case 25:
        //			{
        //				if (activeTerrain != null)
        //				{
        //					activeTerrain.basemapDistance = 263f;
        //					activeTerrain.castShadows = false;
        //				}
        //				this.ReplaceMapElementFor25();
        //				GameObject gameObject2 = GameObject.Find("/Move_fuban");
        //				if (gameObject2 != null)
        //				{
        //					SetActiveByGameState.AddSetActiveByGameState(gameObject2, GameState.Battle, false);
        //				}
        //				break;
        //			}
        //			case 28:
        //				if (activeTerrain != null)
        //				{
        //					activeTerrain.heightmapPixelError = 50f;
        //					activeTerrain.basemapDistance = 213f;
        //					activeTerrain.castShadows = false;
        //					activeTerrain.treeDistance = 366f;
        //					activeTerrain.treeBillboardDistance = 200f;
        //					activeTerrain.treeCrossFadeLength = 52.8f;
        //				}
        //				break;
        //			case 31:
        //				if (activeTerrain != null)
        //				{
        //					activeTerrain.heightmapPixelError = 100f;
        //					activeTerrain.basemapDistance = 81f;
        //					activeTerrain.castShadows = false;
        //					activeTerrain.treeBillboardDistance = 320f;
        //					activeTerrain.treeCrossFadeLength = 32f;
        //				}
        //				break;
        //			case 33:
        //				if (activeTerrain != null)
        //				{
        //					activeTerrain.basemapDistance = 86f;
        //					activeTerrain.castShadows = false;
        //					activeTerrain.treeBillboardDistance = 119f;
        //					activeTerrain.treeCrossFadeLength = 12f;
        //				}
        //				break;
        //			}
        //		}

        private void OnLevelLoaded(int level)
        {
            level = ScenesManager.CurLoadedLevel;
            //PlayerCtrlManager.OnLevelLoaded(level);
            //PalMain.OnReadySpawn();
            //EntityManager.OnLoadOver = (EntityManager.void_fun)Delegate.Remove(EntityManager.OnLoadOver, new EntityManager.void_fun(PalMain.OnLoadOver));
            //EntityManager.OnLoadOver = (EntityManager.void_fun)Delegate.Combine(EntityManager.OnLoadOver, new EntityManager.void_fun(PalMain.OnLoadOver));
            //EntityManager.OnLevelWasLoaded(level);
            //OptionConfig.GetInstance().Use_OnLevelLoaded();
            //OptionConfig.GetInstance().Use_CharacterEmission();
            //MapWatch.Instance.SetMap();
        }

        //		[DebuggerHidden]
        //		public static IEnumerator Quit(float time)
        //		{
        //			PalMain.<Quit>c__Iterator53 <Quit>c__Iterator = new PalMain.<Quit>c__Iterator53();
        //			<Quit>c__Iterator.time = time;
        //			<Quit>c__Iterator.<$>time = time;
        //			return <Quit>c__Iterator;
        //		}

        //		public static void LoadLevel(int LevelIndex)
        //		{
        //			SceneManager.LoadSceneAsync(LevelIndex);
        //		}

        //		public static void LoadLevel(string levelName)
        //		{
        //			SceneManager.LoadSceneAsync(levelName);
        //		}

        //		public static void ToSuitablePlace(GameObject o)
        //		{
        //			if (UtilFun.GetMainCamera() == null)
        //			{
        //				return;
        //			}
        //			if (o == null && UtilFun.GetMainCamera() == null)
        //			{
        //				return;
        //			}
        //			RaycastHit raycastHit = default(RaycastHit);
        //			Ray ray = UtilFun.GetMainCamera().ScreenPointToRay(new Vector2((float)UtilFun.GetMainCamera().pixelWidth * 0.5f, (float)UtilFun.GetMainCamera().pixelHeight * 0.5f));
        //			if (Physics.Raycast(ray, out raycastHit))
        //			{
        //				UtilFun.SetPosition(o.transform, raycastHit.point);
        //			}
        //			else
        //			{
        //				Plane plane = new Plane(Vector3.up, Vector3.zero);
        //				float distance;
        //				if (plane.Raycast(ray, out distance))
        //				{
        //					o.transform.position = ray.GetPoint(distance);
        //				}
        //				else
        //				{
        //					UtilFun.SetPosition(o.transform, ray.GetPoint(0f));
        //				}
        //			}
        //		}

        //		public static GameObject ABInstantiateToUscript(string path)
        //		{
        //			return FileLoader.LoadObjectFromFile<GameObject>(path.ToAssetBundlePath(), true, true);
        //		}

        //		public static void SetBianLuoHuanState()
        //		{
        //			UIManager.Instance.EndState_Normal();
        //			PlayerCtrlManager.bCanTab = false;
        //			PlayerCtrlManager.bCtrlOther = true;
        //			TimeManager.Initialize().selfUpdate -= new Action(TimeManager.Initialize().AutoSave);
        //		}

        //		public static void EndNPCWeatherInteract(GameObject npc)
        //		{
        //			NPCWeatherInteract component = npc.GetComponent<NPCWeatherInteract>();
        //			if (component != null)
        //			{
        //				component.EndActive();
        //			}
        //		}

        //		public static void SetFlyMapBtnEnabled(bool enabled)
        //		{
        //			UtilFun.SetFlyMapBtnEnabled(enabled);
        //		}

        //		public static void TeamRecoverHPAndMP()
        //		{
        //			UtilFun.TeamRecoverHPAndMP();
        //		}

        //		public static void ActiveBranch()
        //		{
        //			FlagManager.SetFlag(MissionManager.BranchLineToggleFlag, 1, true);
        //		}

        //		public static void InActiveBranch()
        //		{
        //			FlagManager.SetFlag(MissionManager.BranchLineToggleFlag, 0, true);
        //		}

        //		public static bool IsBranch()
        //		{
        //			int flag = FlagManager.GetFlag(MissionManager.BranchLineToggleFlag);
        //			return flag > 0;
        //		}

        public static void SetFlag(int idx, int value)
        {
            FlagManager.SetFlag(idx, value, true);
        }

        //		public static int GetFlag(int idx)
        //		{
        //			return FlagManager.GetFlag(idx);
        //		}

        //		public static void ChangeFlag(int idx, int value)
        //		{
        //			int flag = FlagManager.GetFlag(idx);
        //			FlagManager.SetFlag(idx, flag + value, true);
        //		}

        //		public static void ChangeMoney(int difmoney)
        //		{
        //			int num = PalMain.GetMoney();
        //			num += difmoney;
        //			PalMain.SetFlag(2, num);
        //		}

        //		public static int GetMoney()
        //		{
        //			return PalMain.GetFlag(2);
        //		}

        public static void SetMoney(int money)
        {
            PalMain.SetFlag(2, money);
        }

        //		public static void SetCameraDistanceForPot()
        //		{
        //			Camera mainCamera = UtilFun.GetMainCamera();
        //			mainCamera.layerCullSpherical = true;
        //			float[] array = new float[32];
        //			int num = LayerMask.NameToLayer("DistanceCull");
        //			if (num < 0 || num > array.Length - 1)
        //			{
        //				UnityEngine.Debug.LogError("请更新工程的ProjectSettings文件夹");
        //				return;
        //			}
        //			array[num] = PalMain.CameraOptDistance;
        //			mainCamera.layerCullDistances = array;
        //		}

        //		private static void AddGlowCamera()
        //		{
        //		}

        //		public static void KeepCameraOnly()
        //		{
        //			if (PalMain.MainCamera == null && UtilFun.GetMainCamera() != null)
        //			{
        //				PalMain.MainCamera = UtilFun.GetMainCamera().gameObject;
        //			}
        //			if (PalMain.MainCamera == null)
        //			{
        //				UnityEngine.Debug.Log("PalMain.KeepCameraOnly: MainCamera == null");
        //				return;
        //			}
        //			if (!PalMain.MainCamera.activeSelf)
        //			{
        //				UtilFun.SetActive(PalMain.MainCamera, true);
        //			}
        //			GameObject[] array = GameObject.FindGameObjectsWithTag("MainCamera");
        //			if (array.Length > 1)
        //			{
        //				GameObject[] array2 = array;
        //				for (int i = 0; i < array2.Length; i++)
        //				{
        //					GameObject gameObject = array2[i];
        //					if (PalMain.MainCamera != gameObject)
        //					{
        //						gameObject.SetActive(false);
        //						UnityEngine.Object.Destroy(gameObject);
        //					}
        //				}
        //			}
        //			DontDestroyOnLevelChange[] components = UtilFun.GetMainCamera().GetComponents<DontDestroyOnLevelChange>();
        //			if (components.Length > 0)
        //			{
        //				DontDestroyOnLevelChange[] array3 = components;
        //				for (int j = 0; j < array3.Length; j++)
        //				{
        //					DontDestroyOnLevelChange obj = array3[j];
        //					UnityEngine.Object.Destroy(obj);
        //				}
        //			}
        //		}

        //		public static void SetMainCamera(GameObject go)
        //		{
        //			go = go.GetModelObj(false);
        //			AudioReverbZoneTrigger.Owner = go;
        //			PalMain.KeepCameraOnly();
        //			PalMain.SetCameraDistanceForPot();
        //			PalMain.AddGlowCamera();
        //			if (UtilFun.GetMainCamera())
        //			{
        //				SmoothFollow2 orAddComponent = UtilFun.GetMainCamera().gameObject.GetOrAddComponent<SmoothFollow2>();
        //				orAddComponent.Init(go);
        //			}
        //			UtilFun.GetMainCamera().cullingMask = (-67108865 & UtilFun.GetMainCamera().cullingMask);
        //		}

        //		private void OnDrawGizmos()
        //		{
        //			if (PalBattleManager.Instance() != null)
        //			{
        //				PalBattleManager.Instance().DrawGizmos();
        //			}
        //		}

        //		public static void ClearManagerData()
        //		{
        //			GameObject gameObject = GameObject.Find("/FootMarks");
        //			if (gameObject != null)
        //			{
        //				UnityEngine.Object.Destroy(gameObject);
        //			}
        //			SetActiveByFlagManager.Clear();
        //			AnimCtrlScript.ClearAnimClipsDic();
        //			PalMain.mapinfo = null;
        //			DistanceCullManager.Instance.ClearMats();
        //			CharactersManager.Clear();
        //			DynamicObjsDataManager.Instance.ClearLayers();
        //			Footmark.Clear();
        //		}

        public static void ChangeMap(string DestName, int LevelIndex, bool PlayDefaultAudio = true, bool SaveDynamicObjs = true)
        {
            //if (!AssetBundleChecker.checkMapChange(DestName, LevelIndex))
            //{
            //    return;
            //}
            Time.timeScale = 1f;
            UnityEngine.Debug.Log(string.Format("ChangeMap : DestName={0}, LevelIndex={1}, PlayDefaultAudio={2}, SaveDynamicObjs={3}", new object[]
            {
                        DestName,
                        LevelIndex,
                        PlayDefaultAudio,
                        SaveDynamicObjs
            }));

            //PalBattleManager.Instance().OnSceneChangeClear();

            if (ScenesManager.CurLoadedLevel == 11 && LevelIndex != 11)
            {
                FlagManager.SetFlag(8, 1, false);
            }

            //if (Cutscene.current != null && (Cutscene.current.isPlaying || Cutscene.current.isPause))
            //{
            //    Cutscene.current.End(false);
            //}

            //Transform transform = UtilFun.GetMainCamera().transform;
            //if (transform != null)
            //{
            //    transform.parent = null;
            //}
            //UtilFun.GetMainCamera().cullingMask = 0;

            if (ScenesManager.IsChanging)
            {
                return;
            }

            //PlayersManager.RestoreLayer(true);
            //PlayersManager.ChangeHairShader(false);
            //if (SaveDynamicObjs)
            //{
            //    DynamicObjsDataManager.Instance.SaveCurObjsDataToMemory();
            //}
            //PalMain.ClearManagerData();
            ScenesManager.IsChanging = true;
            //if (PalMain.backgroundAudio != null)
            //{
            //    PalMain.backgroundAudio.PlayDefaultAudio = PlayDefaultAudio;
            //}
            //else
            //{
            //    UnityEngine.Debug.LogError("PalMain.backgroundAudio==null");
            //}

            //if (SkillSEPreloader.s_preloadEnable)
            //{
            //    SkillSEPreloader.Instance.unLoadAllSkillSE();
            //}
            ScenesManager.Instance.ChangeMap(DestName, LevelIndex, new Action<int>(PalMain.Instance.OnLevelLoaded));
        }

        //		public static void OnReadySpawn()
        //		{
        //			if (PlayersManager.Player == null)
        //			{
        //				PlayersManager.SpawnPlayer(null, null, false);
        //			}
        //			PlayersManager.SetPlayerPosByDestObj(ScenesManager.Instance.NextDestObjName);
        //			if (PlayersManager.Player != null)
        //			{
        //				PalMain.SetMainCamera(PlayersManager.Player);
        //			}
        //		}

        //		public static void OnLoadOver()
        //		{
        //			EntityManager.OnLoadOver = (EntityManager.void_fun)Delegate.Remove(EntityManager.OnLoadOver, new EntityManager.void_fun(PalMain.OnLoadOver));
        //			PalMain.RefreshAllLandMarks();
        //			DynamicObjsDataManager.Instance.LoadCurObjsDataFromMemory();
        //			GameStateManager.CurGameState = GameState.Normal;
        //			PalMain.Instance.SpecialProcessForLevel(ScenesManager.CurLoadedLevel);
        //			PalMain.ShowMemory();
        //			if (PalMain.LoadOverEvent != null)
        //			{
        //				PalMain.LoadOverEvent();
        //			}
        //		}

        //		public static void ShowMemory()
        //		{
        //			string value = "PalMain.ShowMemory: Current scene = " + SceneManager.GetActiveScene().buildIndex.ToString();
        //			System.Console.WriteLine(value);
        //		}

        //		public static GameObject CreatePermanentObject<T>(string path, bool bRelative)
        //		{
        //			Type typeFromHandle = typeof(T);
        //			UnityEngine.Object @object = UnityEngine.Object.FindObjectOfType(typeFromHandle);
        //			GameObject gameObject;
        //			if (@object == null)
        //			{
        //				UnityEngine.Object object2 = Resources.Load(path);
        //				if (object2 == null)
        //				{
        //					UnityEngine.Debug.LogError("没有找到" + path + "，建议更新所属文件夹");
        //					return null;
        //				}
        //				gameObject = (UnityEngine.Object.Instantiate(object2) as GameObject);
        //			}
        //			else
        //			{
        //				Component component = @object as Component;
        //				if (component == null)
        //				{
        //					UnityEngine.Debug.LogError(typeFromHandle.Name + "不是 Component");
        //					return null;
        //				}
        //				gameObject = component.gameObject;
        //			}
        //			if (gameObject != null && !gameObject.GetComponent<DontDestroyOnLevelChange>())
        //			{
        //				gameObject.AddComponent<DontDestroyOnLevelChange>();
        //			}
        //			return gameObject;
        //		}

        //		public static void SetCtrlModel(GameObject go)
        //		{
        //			PlayerCtrlManager.SetCtrlModel(go);
        //		}

        //		public static void RestoreModel()
        //		{
        //			PlayerCtrlManager.bCtrlOther = false;
        //			PlayerCtrlManager.RestoreModel();
        //		}

        //		public static void SetYanZhaoActive(int ID, bool bActive)
        //		{
        //			GameObject gameObject = PlayersManager.FindMainChar(ID, true);
        //			if (gameObject == null)
        //			{
        //				UnityEngine.Debug.LogError("SetYanZhaoActive 没有找到 " + ID.ToString());
        //				return;
        //			}
        //			if (gameObject == null)
        //			{
        //				UnityEngine.Debug.LogError("SetYanZhaoActive 没有找到 " + ID.ToString() + " 的模型");
        //				return;
        //			}
        //			SetActiveChildByFlag component = gameObject.GetComponent<SetActiveChildByFlag>();
        //			if (component != null)
        //			{
        //				component.SetActive(bActive);
        //			}
        //			else
        //			{
        //				UnityEngine.Debug.LogError("Error :" + gameObject.name + "没有找到 SetActiveChildByFlag");
        //			}
        //		}

        //		public static void SetActiveWeapon(GameObject go, bool bActive, bool bAssort)
        //		{
        //			PalNPC component = go.GetComponent<PalNPC>();
        //			if (component == null)
        //			{
        //				UnityEngine.Debug.LogError(go.name + " 没有PalNPC");
        //				return;
        //			}
        //			component.SetActiveWeaponAndAssort(bActive, bAssort, true);
        //		}

        //		public static GameObject GetChild(GameObject go, string path)
        //		{
        //			if (go == null)
        //			{
        //				UnityEngine.Debug.LogError("GetChild 里面go为 null");
        //				return null;
        //			}
        //			Transform transform = go.transform.Find(path);
        //			return (!(transform != null)) ? null : transform.gameObject;
        //		}

        //		public static void ToggleCtrl(bool bCon)
        //		{
        //			PlayerCtrlManager.bControl = bCon;
        //		}

        //		public static void BeginMoveToMap()
        //		{
        //			if (!BigMap.Instance.OnMap)
        //			{
        //				BigMap.Instance.BeginMoveToMap(true);
        //			}
        //		}

        //		public static void BeginMoveToGround()
        //		{
        //			if (BigMap.Instance.OnMap)
        //			{
        //				BigMap.Instance.BeginMoveToGround();
        //			}
        //		}

        //		public static void BackToStart()
        //		{
        //			PalMain.ChangeMap(null, 0, true, true);
        //		}

        //		public int CreateNewTempObjects(object newO)
        //		{
        //			while (this.mTempPosition < this.mTempObjects.Count && this.mTempObjects[this.mTempPosition] != null)
        //			{
        //				this.mTempPosition++;
        //			}
        //			if (this.mTempPosition >= this.mTempObjects.Count)
        //			{
        //				this.mTempObjects.Add(newO);
        //				this.mTempPosition = this.mTempObjects.Count;
        //				return this.mTempPosition - 1;
        //			}
        //			this.mTempObjects[this.mTempPosition] = newO;
        //			return this.mTempPosition++;
        //		}

        //		public object GetTempObject(int pos)
        //		{
        //			return this.mTempObjects[pos];
        //		}

        //		public void RemoveTempObject(int pos)
        //		{
        //			if (pos < 0 || pos >= this.mTempObjects.Count)
        //			{
        //				return;
        //			}
        //			if (this.mTempObjects[pos] == null && pos < this.mTempPosition)
        //			{
        //				this.mTempPosition = pos;
        //			}
        //			this.mTempObjects[pos] = null;
        //		}

        //		public static void CreateCircleFile()
        //		{
        //		}

        //		public static void SaveCircleFile()
        //		{
        //			SaveManager.Save("100");
        //			SaveManager.GlobalData.mCircle = true;
        //			SaveManager.SaveGlobalData();
        //		}

        //		public static void PutOffSlot(int ID, EquipSlotEnum slot)
        //		{
        //			GameObject gameObject = PlayersManager.FindMainChar(ID, true);
        //			PalNPC component = gameObject.GetComponent<PalNPC>();
        //			component.PutOffEquip(slot);
        //		}

        //		public static void ActiveSSAO(bool bActiveSSAO)
        //		{
        //			if (UtilFun.GetMainCamera() != null)
        //			{
        //				ScreenSpaceAmbientOcclusion component = UtilFun.GetMainCamera().GetComponent<ScreenSpaceAmbientOcclusion>();
        //				if (component != null)
        //				{
        //					component.enabled = bActiveSSAO;
        //				}
        //			}
        //		}

        //		public static void SetDoReturnButtonEnabled(bool v)
        //		{
        //			UIConfig.DoReturnButtonEnabled = v;
        //		}

        //		public static void SetVisible(GameObject go, bool bVisible)
        //		{
        //			if (go == null)
        //			{
        //				UnityEngine.Debug.LogError("Error : SetVisible go == null");
        //				return;
        //			}
        //			go.SetVisible(bVisible);
        //		}

        //		public static void ChangeHairShader(bool bUseAlpha, string[] paths)
        //		{
        //			PlayersManager.ChangeHairShader(bUseAlpha);
        //			UtilFun.ChangeHairShader(bUseAlpha, paths);
        //		}

        //		public void SaveDistCull()
        //		{
        //			this.m_lastGameState = GameState.None;
        //			Camera mainCamera = UtilFun.GetMainCamera();
        //			Terrain activeTerrain = Terrain.activeTerrain;
        //			this.m_FarClipPlane = mainCamera.farClipPlane;
        //			this.m_bFog = RenderSettings.fog;
        //			this.m_FogStartDistance = RenderSettings.fogStartDistance;
        //			this.m_FogEndDistance = RenderSettings.fogEndDistance;
        //			this.m_FogDensity = RenderSettings.fogDensity;
        //			if (activeTerrain != null)
        //			{
        //				this.m_TreeDistance = activeTerrain.treeDistance;
        //				this.m_DetailObjectDistance = activeTerrain.detailObjectDistance;
        //				this.m_HeightmapPixelError = activeTerrain.heightmapPixelError;
        //				this.m_BasemapDistance = activeTerrain.basemapDistance;
        //			}
        //			this.CreateSkyCamera();
        //		}

        //		public void SetLayer(Transform tr, int layer)
        //		{
        //			tr.gameObject.layer = layer;
        //			for (int i = 0; i < tr.childCount; i++)
        //			{
        //				this.SetLayer(tr.GetChild(i), layer);
        //			}
        //		}

        //		public void SetDistCull(PalMain.DISTANCE_CULL dc, bool bFromInit, bool bNoSet = false)
        //		{
        //			if (!bFromInit)
        //			{
        //				Camera mainCamera = UtilFun.GetMainCamera();
        //				Terrain activeTerrain = Terrain.activeTerrain;
        //				if (dc == PalMain.DISTANCE_CULL.FULL)
        //				{
        //					if (GameStateManager.CurGameState == GameState.uScript)
        //					{
        //						bNoSet = true;
        //					}
        //					else
        //					{
        //						if (this.m_FarClipPlane <= 0f)
        //						{
        //							UnityEngine.Debug.LogWarning("m_FarClipPlane <= 0");
        //							this.m_FarClipPlane = 300f;
        //						}
        //						mainCamera.farClipPlane = this.m_FarClipPlane;
        //						RenderSettings.fog = this.m_bFog;
        //						RenderSettings.fogStartDistance = this.m_FogStartDistance;
        //						RenderSettings.fogDensity = this.m_FogDensity;
        //						RenderSettings.fogEndDistance = this.m_FogEndDistance;
        //						if (activeTerrain != null)
        //						{
        //							activeTerrain.treeDistance = this.m_TreeDistance;
        //							activeTerrain.detailObjectDistance = this.m_DetailObjectDistance;
        //						}
        //						if (this.m_SkyCam != null)
        //						{
        //							SkyCameraSync component = this.m_SkyCam.gameObject.GetComponent<SkyCameraSync>();
        //							if (component != null && component.m_Quad.activeSelf)
        //							{
        //								component.m_Quad.SetActive(false);
        //							}
        //							if (component.m_Sky != null)
        //							{
        //								this.SetLayer(component.m_Sky.transform, 0);
        //							}
        //							this.m_SkyCam.enabled = false;
        //						}
        //					}
        //				}
        //				else
        //				{
        //					if (this.m_SkyCam == null)
        //					{
        //						this.CreateSkyCamera();
        //					}
        //					if (this.m_SkyCam != null)
        //					{
        //						SkyCameraSync component2 = this.m_SkyCam.gameObject.GetComponent<SkyCameraSync>();
        //						if (component2 != null && !component2.m_Quad.activeSelf)
        //						{
        //							component2.m_Quad.SetActive(true);
        //						}
        //						if (component2.m_Sky != null)
        //						{
        //							this.SetLayer(component2.m_Sky.transform, 20);
        //						}
        //						this.m_SkyCam.enabled = true;
        //					}
        //					float num = 70f;
        //					PalMain.SceneOptiDistFogParams sceneOptiDistFogParams;
        //					if (this.m_LevelCullOpParams.TryGetValue(Application.loadedLevel, out sceneOptiDistFogParams))
        //					{
        //						if (dc == PalMain.DISTANCE_CULL.FULL)
        //						{
        //							if (sceneOptiDistFogParams.m_CullDist_Hi > 0f)
        //							{
        //								num = sceneOptiDistFogParams.m_CullDist_Hi;
        //							}
        //							else
        //							{
        //								num = this.m_FarClipPlane;
        //							}
        //						}
        //						else if (dc == PalMain.DISTANCE_CULL.LOW)
        //						{
        //							num = sceneOptiDistFogParams.m_CullDist_Low;
        //						}
        //						else if (dc == PalMain.DISTANCE_CULL.MID)
        //						{
        //							num = sceneOptiDistFogParams.m_CullDist_Mid;
        //						}
        //					}
        //					else if (dc == PalMain.DISTANCE_CULL.LOW)
        //					{
        //						num = 70f;
        //					}
        //					else if (dc == PalMain.DISTANCE_CULL.MID)
        //					{
        //						num = 140f;
        //					}
        //					else if (dc == PalMain.DISTANCE_CULL.FULL)
        //					{
        //						num = 300f;
        //					}
        //					mainCamera.farClipPlane = num;
        //					RenderSettings.fog = true;
        //					RenderSettings.fogStartDistance = num * 0.5f;
        //					RenderSettings.fogDensity = 1f;
        //					RenderSettings.fogMode = FogMode.Linear;
        //					RenderSettings.fogEndDistance = num * 0.8f;
        //					activeTerrain = Terrain.activeTerrain;
        //					if (activeTerrain != null)
        //					{
        //					}
        //				}
        //			}
        //			if (!bNoSet)
        //			{
        //				PalMain.m_DistCull = dc;
        //			}
        //		}

        //		public void SavePostCam()
        //		{
        //			this.m_CamPosts.Clear();
        //			Camera mainCamera = UtilFun.GetMainCamera();
        //			Behaviour behaviour;
        //			if (Application.loadedLevel != 11)
        //			{
        //				behaviour = (Behaviour)mainCamera.GetComponent("SunShafts");
        //				if (!(behaviour != null) || behaviour.enabled)
        //				{
        //				}
        //			}
        //			else
        //			{
        //				behaviour = (Behaviour)mainCamera.GetComponent("SunShafts");
        //				if (behaviour != null && behaviour.enabled)
        //				{
        //					behaviour.enabled = false;
        //				}
        //			}
        //			behaviour = (Behaviour)mainCamera.GetComponent("Bloom");
        //			if (behaviour != null && behaviour.enabled)
        //			{
        //				this.m_CamPosts.Add(behaviour);
        //			}
        //			behaviour = (Behaviour)mainCamera.GetComponent("ScreenSpaceAmbientOcclusion");
        //			if (behaviour != null && behaviour.enabled)
        //			{
        //				this.m_CamPosts.Add(behaviour);
        //			}
        //			behaviour = (Behaviour)mainCamera.GetComponent("TiltShift");
        //			if (behaviour != null && behaviour.enabled)
        //			{
        //				this.m_CamPosts.Add(behaviour);
        //			}
        //			behaviour = (Behaviour)mainCamera.GetComponent("GlowEffect");
        //			if (behaviour != null && behaviour.enabled)
        //			{
        //				this.m_CamPosts.Add(behaviour);
        //			}
        //			behaviour = (Behaviour)mainCamera.GetComponent("Vignetting");
        //			if (behaviour != null && behaviour.enabled)
        //			{
        //				this.m_CamPosts.Add(behaviour);
        //			}
        //			behaviour = (Behaviour)mainCamera.GetComponent("ContrastEnhance");
        //			if (behaviour != null && behaviour.enabled)
        //			{
        //				this.m_CamPosts.Add(behaviour);
        //			}
        //			behaviour = (Behaviour)mainCamera.GetComponent("ColorCorrectionCurves");
        //			if (behaviour != null && behaviour.enabled)
        //			{
        //				this.m_CamPosts.Add(behaviour);
        //			}
        //			behaviour = (Behaviour)mainCamera.GetComponent("GlobalFog");
        //			if (behaviour != null && behaviour.enabled)
        //			{
        //				this.m_CamPosts.Add(behaviour);
        //			}
        //			behaviour = (Behaviour)mainCamera.GetComponent("GlobalFog");
        //			if (behaviour != null && behaviour.enabled)
        //			{
        //				this.m_CamPosts.Add(behaviour);
        //			}
        //			Glow11 component = mainCamera.GetComponent<Glow11>();
        //			if (component != null && component.enabled)
        //			{
        //				this.m_CamPosts.Add(component);
        //			}
        //		}

        //		public void SetPostCam(PalMain.POST_CAM pc, bool bFromInit)
        //		{
        //			if (!bFromInit)
        //			{
        //				if (pc == PalMain.POST_CAM.FULL)
        //				{
        //					for (int i = 0; i < this.m_CamPosts.Count; i++)
        //					{
        //						if (this.m_CamPosts[i] != null)
        //						{
        //							this.m_CamPosts[i].enabled = true;
        //						}
        //					}
        //					Camera mainCamera = UtilFun.GetMainCamera();
        //					Behaviour behaviour = (Behaviour)mainCamera.GetComponent("GlowEffect");
        //					if (behaviour != null)
        //					{
        //						behaviour.enabled = false;
        //					}
        //				}
        //				else if (pc == PalMain.POST_CAM.MID)
        //				{
        //					for (int j = 0; j < this.m_CamPosts.Count; j++)
        //					{
        //						if (this.m_CamPosts[j] != null)
        //						{
        //							this.m_CamPosts[j].enabled = true;
        //						}
        //					}
        //					Camera mainCamera2 = UtilFun.GetMainCamera();
        //					Behaviour behaviour2 = (Behaviour)mainCamera2.GetComponent("ScreenSpaceAmbientOcclusion");
        //					if (behaviour2 != null)
        //					{
        //						behaviour2.enabled = false;
        //					}
        //					behaviour2 = (Behaviour)mainCamera2.GetComponent("TiltShift");
        //					if (behaviour2 != null)
        //					{
        //						behaviour2.enabled = false;
        //					}
        //					behaviour2 = (Behaviour)mainCamera2.GetComponent("GlowEffect");
        //					if (behaviour2 != null)
        //					{
        //						behaviour2.enabled = false;
        //					}
        //				}
        //				else
        //				{
        //					Camera mainCamera3 = UtilFun.GetMainCamera();
        //					Behaviour behaviour3 = (Behaviour)mainCamera3.GetComponent("Bloom");
        //					if (behaviour3 != null)
        //					{
        //						behaviour3.enabled = false;
        //					}
        //					behaviour3 = (Behaviour)mainCamera3.GetComponent("ScreenSpaceAmbientOcclusion");
        //					if (behaviour3 != null)
        //					{
        //						behaviour3.enabled = false;
        //					}
        //					behaviour3 = (Behaviour)mainCamera3.GetComponent("TiltShift");
        //					if (behaviour3 != null)
        //					{
        //						behaviour3.enabled = false;
        //					}
        //					behaviour3 = (Behaviour)mainCamera3.GetComponent("GlowEffect");
        //					if (behaviour3 != null)
        //					{
        //						behaviour3.enabled = false;
        //					}
        //					behaviour3 = (Behaviour)mainCamera3.GetComponent("Vignetting");
        //					if (behaviour3 != null)
        //					{
        //						behaviour3.enabled = false;
        //					}
        //					behaviour3 = (Behaviour)mainCamera3.GetComponent("HSL");
        //					if (behaviour3 != null)
        //					{
        //						behaviour3.enabled = false;
        //					}
        //					behaviour3 = (Behaviour)mainCamera3.GetComponent("ContrastEnhance");
        //					if (behaviour3 != null)
        //					{
        //						behaviour3.enabled = false;
        //					}
        //					behaviour3 = (Behaviour)mainCamera3.GetComponent("ColorCorrectionCurves");
        //					if (behaviour3 != null)
        //					{
        //						behaviour3.enabled = false;
        //					}
        //					behaviour3 = (Behaviour)mainCamera3.GetComponent("GlobalFog");
        //					if (behaviour3 != null)
        //					{
        //						behaviour3.enabled = false;
        //					}
        //					behaviour3 = (Behaviour)mainCamera3.GetComponent("GlobalFog");
        //					if (behaviour3 != null)
        //					{
        //						behaviour3.enabled = false;
        //					}
        //					Glow11 component = mainCamera3.GetComponent<Glow11>();
        //					if (component != null)
        //					{
        //						component.enabled = false;
        //					}
        //				}
        //			}
        //			PalMain.m_PostCam = pc;
        //		}

        //		public void SaveLight()
        //		{
        //			this.m_Lights.Clear();
        //			this.m_LightsShadow.Clear();
        //			Light[] array = UnityEngine.Object.FindObjectsOfType<Light>();
        //			for (int i = 0; i < array.Length; i++)
        //			{
        //				if (array[i].type != LightType.Directional)
        //				{
        //					if (array[i].enabled)
        //					{
        //						if (this.FindParentIsMainActor(array[i].gameObject.transform))
        //						{
        //							goto IL_92;
        //						}
        //						this.m_Lights.Add(array[i]);
        //					}
        //					array[i].shadows = LightShadows.None;
        //				}
        //				else if (array[i].shadows != LightShadows.None)
        //				{
        //					this.m_LightsShadow.Add(array[i]);
        //				}
        //				IL_92:;
        //			}
        //			this.m_Cams.Clear();
        //			Camera[] array2 = UnityEngine.Object.FindObjectsOfType<Camera>();
        //			for (int j = 0; j < array2.Length; j++)
        //			{
        //				UICamera component = array2[j].GetComponent<UICamera>();
        //				if (array2[j].tag != "MainCamera")
        //				{
        //					array2[j].useOcclusionCulling = false;
        //				}
        //				if (array2[j].tag != "MainCamera" && component == null && array2[j].name != "SkyCam")
        //				{
        //					array2[j].farClipPlane = 1000f;
        //				}
        //				if (array2[j].name == "MidCamera")
        //				{
        //					array2[j].gameObject.SetActive(false);
        //				}
        //			}
        //		}

        //		public void SetLight(PalMain.LIGHT lt, bool bFromInit)
        //		{
        //			if (!bFromInit)
        //			{
        //				if (lt == PalMain.LIGHT.FULL)
        //				{
        //					for (int i = 0; i < this.m_Lights.Count; i++)
        //					{
        //						if (this.m_Lights[i] != null)
        //						{
        //							this.m_Lights[i].enabled = true;
        //						}
        //					}
        //					for (int j = 0; j < this.m_LightsShadow.Count; j++)
        //					{
        //						if (this.m_LightsShadow[j] != null)
        //						{
        //							this.m_LightsShadow[j].shadows = LightShadows.Soft;
        //						}
        //					}
        //					for (int k = 0; k < this.m_Cams.Count; k++)
        //					{
        //						if (this.m_Cams[k] != null)
        //						{
        //							this.m_Cams[k].gameObject.SetActive(true);
        //						}
        //					}
        //				}
        //				else
        //				{
        //					Light[] array = UnityEngine.Object.FindObjectsOfType<Light>();
        //					for (int l = 0; l < array.Length; l++)
        //					{
        //						if (array[l].type != LightType.Directional)
        //						{
        //							if (array[l].enabled)
        //							{
        //								array[l].enabled = false;
        //							}
        //						}
        //						else if (array[l].shadows != LightShadows.None)
        //						{
        //							array[l].shadows = LightShadows.None;
        //						}
        //					}
        //					Camera[] array2 = UnityEngine.Object.FindObjectsOfType<Camera>();
        //					for (int m = 0; m < array2.Length; m++)
        //					{
        //						UICamera component = array2[m].GetComponent<UICamera>();
        //						if (array2[m].tag != "MainCamera")
        //						{
        //							array2[m].useOcclusionCulling = false;
        //						}
        //						if (array2[m].tag != "MainCamera" && component == null && array2[m].name != "SkyCam")
        //						{
        //							array2[m].farClipPlane = 1000f;
        //						}
        //					}
        //				}
        //			}
        //			PalMain.m_Light = lt;
        //		}

        //		public void UpdateOpCull()
        //		{
        //			if (PalMain.m_DistCull == PalMain.DISTANCE_CULL.RESTORE)
        //			{
        //				if (GameStateManager.CurGameState != this.m_lastGameState)
        //				{
        //					if (GameStateManager.CurGameState == GameState.Battle && this.m_lastGameState != GameState.Battle)
        //					{
        //						if (PalMain.m_DistCull != PalMain.DISTANCE_CULL.FULL)
        //						{
        //							this.SetDistCull(PalMain.DISTANCE_CULL.MID, false, true);
        //						}
        //					}
        //					else if (GameStateManager.CurGameState != GameState.Cutscene)
        //					{
        //						this.SetDistCull(PalMain.DISTANCE_CULL.MID, false, false);
        //					}
        //				}
        //			}
        //			else if (GameStateManager.CurGameState != this.m_lastGameState)
        //			{
        //				if (GameStateManager.CurGameState == GameState.Cutscene && this.m_lastGameState != GameState.Cutscene && !this.m_bZhuYuGame && !this.m_bGuHanJiang)
        //				{
        //					this.SetDistCull(PalMain.DISTANCE_CULL.FULL, false, true);
        //				}
        //				if (GameStateManager.CurGameState != GameState.Prompt || this.m_lastGameState == GameState.Prompt || this.m_lastGameState == GameState.uScript)
        //				{
        //					if (GameStateManager.CurGameState == GameState.SmallGame && this.m_lastGameState != GameState.SmallGame && !this.m_bZhuYuGame && !this.m_bGuHanJiang)
        //					{
        //						if (SceneManager.GetActiveScene().buildIndex == 25 && Fly_YuJieShu.fly_flag == 1)
        //						{
        //							this.SetDistCull(PalMain.DISTANCE_CULL.FULL, false, true);
        //						}
        //						else
        //						{
        //							this.SetDistCull(PalMain.DISTANCE_CULL.MID, false, true);
        //						}
        //					}
        //					else if (GameStateManager.CurGameState == GameState.Battle && this.m_lastGameState != GameState.Battle)
        //					{
        //						if (PalMain.m_DistCull == PalMain.DISTANCE_CULL.MID)
        //						{
        //							this.SetDistCull(PalMain.DISTANCE_CULL.MID, false, true);
        //						}
        //						else if (PalMain.m_DistCull == PalMain.DISTANCE_CULL.MID)
        //						{
        //							this.SetDistCull(PalMain.DISTANCE_CULL.LOW, false, true);
        //						}
        //					}
        //					else if (GameStateManager.CurGameState != GameState.Cutscene && GameStateManager.CurGameState != GameState.uScript)
        //					{
        //						this.SetDistCull(PalMain.m_DistCull, false, false);
        //					}
        //				}
        //			}
        //			this.m_lastGameState = GameStateManager.CurGameState;
        //		}

        //		public void UpdateDualCamera()
        //		{
        //			if (SceneManager.GetActiveScene().buildIndex == 0)
        //			{
        //				return;
        //			}
        //			if (this.m_bDualCam != 0)
        //			{
        //				DualCamera dualCamera = UtilFun.GetMainCamera().gameObject.GetComponent<DualCamera>();
        //				if (dualCamera == null)
        //				{
        //					dualCamera = UtilFun.GetMainCamera().gameObject.AddComponent<DualCamera>();
        //				}
        //				if (this.m_bDualCam == 1)
        //				{
        //					dualCamera.SetDualCamera(true, this.m_DualOffset, 0);
        //				}
        //				else
        //				{
        //					dualCamera.SetDualCamera(true, this.m_DualOffset, 1);
        //				}
        //			}
        //		}

        //		public bool GetDualCam()
        //		{
        //			return this.m_bDualCam != 0;
        //		}

        //		public bool IsIntelGraphicCard()
        //		{
        //			return this.m_bIsIntel;
        //		}

        //		public bool IsGDI()
        //		{
        //			return this.m_bIsGDI;
        //		}

        //		public void CreateSkyCamera()
        //		{
        //			GameObject gameObject = GameObject.Find("/palUniStorm1.6v");
        //			if (gameObject == null)
        //			{
        //				gameObject = GameObject.Find("/PalUniStorm");
        //			}
        //			if (gameObject == null)
        //			{
        //				gameObject = GameObject.Find("/palUniStorm1.6v_ljc");
        //			}
        //			if (gameObject != null)
        //			{
        //				this.SetLayer(gameObject.transform, 20);
        //			}
        //			Camera component = PalMain.MainCamera.GetComponent<Camera>();
        //			if (component != null)
        //			{
        //				int num = -1048577;
        //				component.cullingMask &= num;
        //				Transform transform = component.transform.FindChild("SkyCam");
        //				GameObject gameObject2;
        //				Camera camera;
        //				if (transform == null)
        //				{
        //					gameObject2 = new GameObject("SkyCam");
        //					camera = gameObject2.AddComponent<Camera>();
        //					this.m_SkyCam = camera;
        //				}
        //				else
        //				{
        //					gameObject2 = transform.gameObject;
        //					camera = gameObject2.GetComponent<Camera>();
        //					if (camera == null)
        //					{
        //						camera = gameObject2.AddComponent<Camera>();
        //					}
        //					this.m_SkyCam = camera;
        //				}
        //				camera.renderingPath = RenderingPath.Forward;
        //				camera.useOcclusionCulling = false;
        //				camera.farClipPlane = 20000f;
        //				camera.cullingMask = 1048576;
        //				camera.clearFlags = CameraClearFlags.Skybox;
        //				camera.fieldOfView = component.fieldOfView;
        //				camera.transform.parent = component.gameObject.transform;
        //				camera.transform.localPosition = Vector3.zero;
        //				camera.transform.localRotation = Quaternion.identity;
        //				camera.depth = -2f;
        //				camera.backgroundColor = Color.black;
        //				gameObject2.SetActive(true);
        //				transform = component.transform.FindChild("Quad");
        //				GameObject gameObject3;
        //				if (transform == null)
        //				{
        //					string path = "SEObjects/SkyQuad".ToAssetBundlePath();
        //					gameObject3 = FileLoader.LoadObjectFromFile<GameObject>(path, true, true);
        //					FileLoader.UnloadAssetBundle(path);
        //				}
        //				else
        //				{
        //					gameObject3 = transform.gameObject;
        //				}
        //				if (gameObject3 != null)
        //				{
        //					gameObject3.name = "Quad";
        //				}
        //				else
        //				{
        //					UnityEngine.Debug.LogError("Quad is null");
        //				}
        //				gameObject3.transform.parent = component.gameObject.transform;
        //				gameObject3.transform.localPosition = Vector3.zero;
        //				gameObject3.transform.localRotation = Quaternion.identity;
        //				if (SceneManager.GetActiveScene().buildIndex == 18)
        //				{
        //					Renderer component2 = gameObject3.GetComponent<Renderer>();
        //					if (component2 != null)
        //					{
        //						SkyQuadScript component3 = gameObject3.GetComponent<SkyQuadScript>();
        //						if (component3 != null)
        //						{
        //							component2.material = component3.m_Diffuse;
        //						}
        //					}
        //				}
        //				else if (SceneManager.GetActiveScene().buildIndex == 15)
        //				{
        //					Renderer component4 = gameObject3.GetComponent<Renderer>();
        //					if (component4 != null)
        //					{
        //						SkyQuadScript component5 = gameObject3.GetComponent<SkyQuadScript>();
        //						if (component5 != null)
        //						{
        //							component4.material = component5.m_Diffuse;
        //						}
        //					}
        //				}
        //				Vector3 zero = Vector3.zero;
        //				zero.z = 1999f;
        //				gameObject3.transform.localPosition = zero;
        //				float num2 = UtilFun.GetMainCamera().fieldOfView / 2f;
        //				float num3 = Mathf.Tan(0.0174532924f * num2) * zero.z;
        //				float num4 = (float)Screen.width / (float)Screen.height;
        //				gameObject3.transform.localScale = new Vector3(num3 * 2f * num4, num3 * 2f, 1f);
        //				Renderer component6 = gameObject3.GetComponent<Renderer>();
        //				if (component6 != null)
        //				{
        //					camera.targetTexture = (RenderTexture)component6.sharedMaterial.mainTexture;
        //				}
        //				else
        //				{
        //					UnityEngine.Debug.LogError("r is null");
        //				}
        //				SkyCameraSync skyCameraSync = camera.gameObject.AddComponent<SkyCameraSync>();
        //				skyCameraSync.m_Quad = gameObject3;
        //				skyCameraSync.m_Sky = gameObject;
        //			}
        //		}

        //		public void SaveDynamicLight()
        //		{
        //			Light[] array = UnityEngine.Object.FindObjectsOfType<Light>();
        //			for (int i = 0; i < array.Length; i++)
        //			{
        //				if (array[i].type != LightType.Directional)
        //				{
        //					if (!this.FindParentIsMainActor(array[i].gameObject.transform))
        //					{
        //						if (SceneManager.GetActiveScene().buildIndex != 19 || array[i].type != LightType.Spot)
        //						{
        //							if (SceneManager.GetActiveScene().buildIndex == 15 && array[i].type == LightType.Spot)
        //							{
        //								array[i].shadows = LightShadows.Hard;
        //							}
        //							else if (array[i].gameObject.GetComponent<PointLightOptimizer>() == null)
        //							{
        //								array[i].gameObject.AddComponent<PointLightOptimizer>();
        //							}
        //						}
        //					}
        //				}
        //			}
        //		}

        //		private bool FindParentIsMainActor(Transform obj)
        //		{
        //			while (obj != null)
        //			{
        //				PalNPC component = obj.GetComponent<PalNPC>();
        //				if (component != null)
        //				{
        //					if (component.Data != null && component.Data.CharacterID <= 6)
        //					{
        //						return true;
        //					}
        //					obj = obj.parent;
        //				}
        //				else
        //				{
        //					obj = obj.parent;
        //				}
        //			}
        //			return false;
        //		}

        //		public void UpdateDynamicLight()
        //		{
        //		}

        //		public static PalMain.PLAYER_RECOMMANDATION GetPlayerRecommandation()
        //		{
        //			return PalMain.m_PlayerRecommandation;
        //		}

        //		public static void SetPlayerRecommandation(PalMain.PLAYER_RECOMMANDATION rec)
        //		{
        //			PalMain.m_PlayerRecommandation = rec;
        //		}

        //		public static PalMain.PlayerConfigs GetSettingsByLevel(PalMain.PLAYER_RECOMMANDATION rec)
        //		{
        //			PalMain.PlayerConfigs playerConfigs = new PalMain.PlayerConfigs();
        //			if (rec == PalMain.PLAYER_RECOMMANDATION.HIGH)
        //			{
        //				playerConfigs.m_Settings[0] = 2;
        //				playerConfigs.m_Settings[1] = 3;
        //				playerConfigs.m_Settings[2] = 1;
        //				playerConfigs.m_Settings[3] = 1;
        //				playerConfigs.m_Settings[4] = 0;
        //				playerConfigs.m_Settings[5] = 0;
        //				playerConfigs.m_Settings[6] = 1;
        //				playerConfigs.m_Settings[7] = 2;
        //				playerConfigs.m_Settings[8] = 2;
        //				playerConfigs.m_Settings[9] = 2;
        //				playerConfigs.m_Settings[10] = 1;
        //				playerConfigs.m_Settings[11] = 1;
        //				playerConfigs.m_Settings[12] = 1;
        //				return playerConfigs;
        //			}
        //			if (rec == PalMain.PLAYER_RECOMMANDATION.MID)
        //			{
        //				playerConfigs.m_Settings[0] = 1;
        //				playerConfigs.m_Settings[1] = 1;
        //				playerConfigs.m_Settings[2] = 1;
        //				playerConfigs.m_Settings[3] = 1;
        //				playerConfigs.m_Settings[4] = 1;
        //				playerConfigs.m_Settings[5] = 0;
        //				playerConfigs.m_Settings[6] = 1;
        //				playerConfigs.m_Settings[7] = 2;
        //				playerConfigs.m_Settings[8] = 1;
        //				playerConfigs.m_Settings[9] = 1;
        //				playerConfigs.m_Settings[10] = 1;
        //				playerConfigs.m_Settings[11] = 0;
        //				playerConfigs.m_Settings[12] = 1;
        //				return playerConfigs;
        //			}
        //			playerConfigs.m_Settings[0] = 0;
        //			playerConfigs.m_Settings[1] = 0;
        //			playerConfigs.m_Settings[2] = 0;
        //			playerConfigs.m_Settings[3] = 0;
        //			playerConfigs.m_Settings[4] = 3;
        //			playerConfigs.m_Settings[5] = 0;
        //			playerConfigs.m_Settings[6] = 0;
        //			playerConfigs.m_Settings[7] = 0;
        //			playerConfigs.m_Settings[8] = 0;
        //			playerConfigs.m_Settings[9] = 0;
        //			playerConfigs.m_Settings[10] = 1;
        //			playerConfigs.m_Settings[11] = 0;
        //			playerConfigs.m_Settings[12] = 0;
        //			return playerConfigs;
        //		}

        //		public static PalMain.PlayerConfigs FirstTimeLaunch103()
        //		{
        //			return PalMain.GetSettingsByLevel(PalMain.DetectMachine());
        //		}

        //		public static bool CheckNeedRiaseWarnning(PalMain.SETTING_ENUM setting, int value)
        //		{
        //			PalMain.PlayerConfigs settingsByLevel = PalMain.GetSettingsByLevel(PalMain.DetectMachine());
        //			if (setting == PalMain.SETTING_ENUM.TIE_TU_JING_DU)
        //			{
        //				return settingsByLevel.m_Settings[(int)setting] >= value;
        //			}
        //			return settingsByLevel.m_Settings[(int)setting] < value;
        //		}

        //		public static void UpdateCheckUnload()
        //		{
        //			PalMain.m_CurUnloadTime += Time.unscaledDeltaTime;
        //			float num = 0f;
        //			switch (PalMain.m_CurPrior)
        //			{
        //			case PalMain.UNLOADPROIR.IMMEDIATE:
        //				num = PalMain.m_FixImmediateTime;
        //				break;
        //			case PalMain.UNLOADPROIR.SHORT:
        //				num = PalMain.m_FixShortTime;
        //				break;
        //			case PalMain.UNLOADPROIR.LONG:
        //				num = PalMain.m_FixLongTime;
        //				break;
        //			case PalMain.UNLOADPROIR.VERYLONG:
        //				num = PalMain.m_FixVeryLongTime;
        //				break;
        //			}
        //			if (PalMain.m_CurUnloadTime > num)
        //			{
        //				PalMain.m_CurUnloadTime = 0f;
        //				if (PalMain.m_bHasUnload)
        //				{
        //					Resources.UnloadUnusedAssets();
        //					PalMain.m_bHasUnload = false;
        //				}
        //			}
        //		}

        //		public static void UnloadUnusedAssets(PalMain.UNLOADPROIR proir = PalMain.UNLOADPROIR.LONG)
        //		{
        //			Resources.UnloadUnusedAssets();
        //		}

        //		public static void AddFlagValue(int idx, [DefaultValue(1)] int AddValue = 1)
        //		{
        //			int num = FlagManager.GetFlag(idx);
        //			num += AddValue;
        //			FlagManager.SetFlag(idx, num, false);
        //		}

        //		public static void SetPosition(GameObject go, Vector3 pos)
        //		{
        //			NavMeshAgent componentInChildren = go.GetComponentInChildren<NavMeshAgent>();
        //			if (componentInChildren != null)
        //			{
        //				componentInChildren.updatePosition = false;
        //				componentInChildren.updateRotation = false;
        //			}
        //			UtilFun.SetPosition(go.transform, pos);
        //		}

        //		public static string GetLangueStr(string str0, string str1)
        //		{
        //			uint curLangue = Langue.CurLangue;
        //			return (curLangue >= 1u) ? str1 : str0;
        //		}

        //		public static void NoActiveUserWarning(bool backToTitle = false)
        //		{
        //			if (PalMain.s_waitForActiveUserWarning)
        //			{
        //				return;
        //			}
        //			PalMain.s_waitForActiveUserWarning = true;
        //			string info;
        //			if (backToTitle)
        //			{
        //				info = ((Langue.CurLangue != 0u) ? "無使用者登入，確認後返回開頭" : "无使用者登入，确认后返回开头");
        //			}
        //			else
        //			{
        //				info = ((Langue.CurLangue != 0u) ? "請登入一名使用者，否則將無法讀存檔案" : "请登入一名使用者，否则将无法读存档案");
        //			}
        //			UIDialogManager.Instance.ShowInfoDialog(info, UIDialogManager.ButtonEnum.Yes, delegate
        //			{
        //				if ((int)UIDialogManager.Instance.Result == 2 && backToTitle)
        //				{
        //					PalMain.BackToStart();
        //				}
        //				PalMain.s_waitForActiveUserWarning = false;
        //				InputManager.LockThisFrame();
        //			});
        //		}

        //		public static void NoControllerWarning()
        //		{
        //			if (PalMain.s_noControllerWarning)
        //			{
        //				return;
        //			}
        //			PalMain.s_noControllerWarning = true;
        //			string text = "Warning, no controller connected.";
        //			UIDialogManager.Instance.ShowTopWarnInfo(text, delegate
        //			{
        //				PalMain.s_noControllerWarning = false;
        //				InputManager.LockThisFrame();
        //			});
        //		}

        //		[DebuggerHidden]
        //		public IEnumerator waitSkillPreload(Action _action, int level)
        //		{
        //			PalMain.<waitSkillPreload>c__Iterator54 <waitSkillPreload>c__Iterator = new PalMain.<waitSkillPreload>c__Iterator54();
        //			<waitSkillPreload>c__Iterator._action = _action;
        //			<waitSkillPreload>c__Iterator.<$>_action = _action;
        //			<waitSkillPreload>c__Iterator.<>f__this = this;
        //			return <waitSkillPreload>c__Iterator;
        //		}

        //		private void OnApplicationFocus(bool hasFocus)
        //		{
        //			if (hasFocus)
        //			{
        //				this.shouldRenewFrames = this.FOCUS_WAIT_FRAMES;
        //			}
        //		}

        //		public void ForceRenew()
        //		{
        //			this.shouldRenewFrames = this.FOCUS_WAIT_FRAMES;
        //		}

        //		private void UpdateSpecialIssueForNonFocus()
        //		{
        //			if (this.shouldRenewFrames > 0)
        //			{
        //				this.shouldRenewFrames--;
        //				if (this.shouldRenewFrames == 0)
        //				{
        //					Terrain activeTerrain = Terrain.activeTerrain;
        //					if (activeTerrain != null)
        //					{
        //						activeTerrain.gameObject.SetActive(false);
        //						activeTerrain.gameObject.SetActive(true);
        //					}
        //					GameObject uIRoot = UIManager.Instance.UIRoot;
        //					if (uIRoot != null)
        //					{
        //						uIRoot.SetActive(false);
        //						uIRoot.SetActive(true);
        //					}
        //				}
        //			}
        //		}

        //		public void ChangeLanguage(uint next)
        //		{
        //			Langue.ClearLanguageDictionary();
        //			PalMain.ReleaseLangueUIAtlas("StringImage0");
        //			Langue.CurLangue = next;
        //			PalBattleManager.Instance().GetMonsterSkillDataManager().ReParseLanguageData();
        //			PalBattleManager.Instance().GetStatusDataManager().ReParseLanguageData();
        //			PalMain.ReloadLangueUIAtlas("StringImage0");
        //			NicknameBuffDebuffType[] datasFromFile = NicknameBuffDebuffTypeCache.GetDatasFromFile();
        //			for (int i = 0; i < datasFromFile.Length; i++)
        //			{
        //				NicknameBuffDebuffType nicknameBuffDebuffType = datasFromFile[i];
        //				if (nicknameBuffDebuffType != null)
        //				{
        //					nicknameBuffDebuffType.mName.ReLoad();
        //					nicknameBuffDebuffType.mHistoryDesc.ReLoad();
        //					nicknameBuffDebuffType.mFunctionDesc.ReLoad();
        //				}
        //			}
        //			ClothDecal[] datasFromFile2 = ClothDecal.GetDatasFromFile();
        //			for (int j = 0; j < datasFromFile2.Length; j++)
        //			{
        //				ClothDecal clothDecal = datasFromFile2[j];
        //				if (clothDecal != null)
        //				{
        //					clothDecal.TextureName.ReLoad();
        //				}
        //			}
        //			ClothTexture[] datasFromFile3 = ClothTexture.GetDatasFromFile();
        //			for (int k = 0; k < datasFromFile3.Length; k++)
        //			{
        //				ClothTexture clothTexture = datasFromFile3[k];
        //				if (clothTexture != null)
        //				{
        //					clothTexture.ColorName.ReLoad();
        //				}
        //			}
        //			SymbolNodeItemType[] datasFromFile4 = SymbolNodeItemType.GetDatasFromFile();
        //			for (int l = 0; l < datasFromFile4.Length; l++)
        //			{
        //				SymbolNodeItemType symbolNodeItemType = datasFromFile4[l];
        //				if (symbolNodeItemType != null)
        //				{
        //					symbolNodeItemType.mName.ReLoad();
        //					symbolNodeItemType.mDesc.ReLoad();
        //				}
        //			}
        //			SymbolPanelItemType[] datasFromFile5 = SymbolPanelItemType.GetDatasFromFile();
        //			for (int m = 0; m < datasFromFile5.Length; m++)
        //			{
        //				SymbolPanelItemType symbolPanelItemType = datasFromFile5[m];
        //				if (symbolPanelItemType != null)
        //				{
        //					symbolPanelItemType.mName.ReLoad();
        //					symbolPanelItemType.mDesc.ReLoad();
        //				}
        //			}
        //			MapData[] datasFromFile6 = MapData.GetDatasFromFile();
        //			for (int n = 0; n < datasFromFile6.Length; n++)
        //			{
        //				MapData mapData = datasFromFile6[n];
        //				if (mapData != null)
        //				{
        //					mapData.Name.ReLoad();
        //				}
        //			}
        //			MusicData[] datasFromFile7 = MusicData.GetDatasFromFile();
        //			for (int num = 0; num < datasFromFile7.Length; num++)
        //			{
        //				MusicData musicData = datasFromFile7[num];
        //				if (musicData != null)
        //				{
        //					musicData.Name.ReLoad();
        //				}
        //			}
        //			PlayerProperty.StaticData[] datasFromFile8 = PlayerProperty.StaticData.GetDatasFromFile();
        //			for (int num2 = 0; num2 < datasFromFile8.Length; num2++)
        //			{
        //				PlayerProperty.StaticData staticData = datasFromFile8[num2];
        //				if (staticData != null)
        //				{
        //					staticData.SkillGroupName0.ReLoad();
        //					staticData.SkillGroupName1.ReLoad();
        //				}
        //			}
        //			SoulStarData[] datasFromFile9 = SoulDataManager.GetDatasFromFile();
        //			for (int num3 = 0; num3 < datasFromFile9.Length; num3++)
        //			{
        //				SoulStarData soulStarData = datasFromFile9[num3];
        //				if (soulStarData != null)
        //				{
        //					soulStarData.NodeName.ReLoad();
        //					soulStarData.NodeDesc.ReLoad();
        //				}
        //			}
        //			UIInformation_Help_Item.ItemClass[] items = UIInformation_Help_Item.Items;
        //			for (int num4 = 0; num4 < items.Length; num4++)
        //			{
        //				UIInformation_Help_Item.ItemClass itemClass = items[num4];
        //				itemClass.TitleLangue.ReLoad();
        //				UIInformation_Help_Item.ItemClass[] subItems = itemClass.SubItems;
        //				for (int num5 = 0; num5 < subItems.Length; num5++)
        //				{
        //					UIInformation_Help_Item.ItemClass itemClass2 = subItems[num5];
        //					itemClass2.TitleLangue.ReLoad();
        //				}
        //			}
        //			UIInformation_StrangeNews_Item.TypeData[] items2 = UIInformation_StrangeNews_Item.Items;
        //			for (int num6 = 0; num6 < items2.Length; num6++)
        //			{
        //				UIInformation_StrangeNews_Item.TypeData typeData = items2[num6];
        //				typeData.TextLangue.ReLoad();
        //				UIInformation_StrangeNews_Item.TitleData[] titleDatas = typeData.TitleDatas;
        //				for (int num7 = 0; num7 < titleDatas.Length; num7++)
        //				{
        //					UIInformation_StrangeNews_Item.TitleData titleData = titleDatas[num7];
        //					titleData.TextLangue.ReLoad();
        //					UIInformation_StrangeNews_Item.ItemData[] itemDatas = titleData.ItemDatas;
        //					for (int num8 = 0; num8 < itemDatas.Length; num8++)
        //					{
        //						UIInformation_StrangeNews_Item.ItemData itemData = itemDatas[num8];
        //						itemData.TextLangue.ReLoad();
        //					}
        //				}
        //			}
        //			foreach (KeyValuePair<uint, CharacterProperty.StaticData> current in CharacterProperty.StaticData.GetDatasFromFile())
        //			{
        //				current.Value.ShowName.ReLoad();
        //			}
        //			foreach (KeyValuePair<uint, ClothItemType> current2 in ClothItemTypeCache.GetDatasFromFile())
        //			{
        //				current2.Value.mName.ReLoad();
        //				current2.Value.mHistoryDesc.ReLoad();
        //				current2.Value.mFunctionDesc.ReLoad();
        //			}
        //			foreach (KeyValuePair<uint, FashionClothItemType> current3 in FashionClothItemTypeCache.GetDatasFromFile())
        //			{
        //				current3.Value.mName.ReLoad();
        //				current3.Value.mHistoryDesc.ReLoad();
        //				current3.Value.mFunctionDesc.ReLoad();
        //			}
        //			foreach (KeyValuePair<uint, NormalItemType> current4 in NormalItemTypeCache.GetDatasFromFile())
        //			{
        //				current4.Value.mName.ReLoad();
        //				current4.Value.mHistoryDesc.ReLoad();
        //				current4.Value.mFunctionDesc.ReLoad();
        //			}
        //			foreach (KeyValuePair<uint, OrnamentItemType> current5 in OrnamentItemTypeCache.GetDatasFromFile())
        //			{
        //				current5.Value.mName.ReLoad();
        //				current5.Value.mHistoryDesc.ReLoad();
        //				current5.Value.mFunctionDesc.ReLoad();
        //			}
        //			foreach (KeyValuePair<uint, ShoesItemType> current6 in ShoesItemTypeCache.GetDatasFromFile())
        //			{
        //				current6.Value.mName.ReLoad();
        //				current6.Value.mHistoryDesc.ReLoad();
        //				current6.Value.mFunctionDesc.ReLoad();
        //			}
        //			foreach (KeyValuePair<uint, WeaponItemType> current7 in WeaponItemTypeCache.GetDatasFromFile())
        //			{
        //				current7.Value.mName.ReLoad();
        //				current7.Value.mHistoryDesc.ReLoad();
        //				current7.Value.mFunctionDesc.ReLoad();
        //			}
        //		}
    }
}
