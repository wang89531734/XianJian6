using SoftStar;
using SoftStar.Pal6;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Funfia.File
{
	public class FileLoader
	{
		public class AssetBundleLoader
		{
			public static Dictionary<string, FileLoader.AssetBundleLoader> Loaders = new Dictionary<string, FileLoader.AssetBundleLoader>();

			private string m_path;

			private bool m_instantiate;

			private UnityEngine.Object m_mainAsset;

			private AssetBundle m_bundle;

            public event Action<UnityEngine.Object, string> OnLoadOver;

            public event Action<UnityEngine.Object, string> OnLoadOverInstantiate;

            public string Path
			{
				get
				{
					return this.m_path;
				}
			}

			public UnityEngine.Object MainAsset
			{
				get
				{
					return this.m_mainAsset;
				}
			}

			public AssetBundle Bundle
			{
				get
				{
					return this.m_bundle;
				}
			}

			private AssetBundleLoader()
			{
			}

			public AssetBundleLoader(string path, Action<UnityEngine.Object, string> onLoadOver, bool instantiate)
			{
				this.m_path = path;
				this.m_instantiate = instantiate;
				if (this.m_instantiate)
				{
					this.OnLoadOverInstantiate = (Action<UnityEngine.Object, string>)Delegate.Combine(this.OnLoadOverInstantiate, onLoadOver);
				}
				else
				{
					this.OnLoadOver = (Action<UnityEngine.Object, string>)Delegate.Combine(this.OnLoadOver, onLoadOver);
				}
				FileLoader.AssetBundleLoader.Loaders.Add(this.m_path, this);
				CoroutineRunner.Instance.StartCoroutine(this.LoadBundle());
			}

            public IEnumerator LoadBundle()
            {
                yield return new WaitForEndOfFrame();
                yield return FileLoader.Instance._loadAssetBundleFromFileAsync(this.m_path);
                this.m_bundle = FileLoader.Instance.loadAssetBundleFromCache(this.m_path, false);
                if (this.OnLoadOver != null && this.OnLoadOver.GetInvocationList().Length > 0)
                {
                    this.OnLoadOver(this.m_bundle.MainAsset5(), this.m_path);
                }
                if (this.OnLoadOverInstantiate != null)
                {
                    foreach (Action<UnityEngine.Object, string> func in this.OnLoadOverInstantiate.GetInvocationList())
                    {
                        UnityEngine.Object obj = null;
                        if (this.m_bundle.MainAsset5() == null)
                        {
                            UnityEngine.Debug.LogError("AssetBundleLoader.LoadBundle: object to instantiate is null, path = " + this.m_path);
                            obj = null;
                        }
                        else
                        {
                            obj = UnityEngine.Object.Instantiate(this.m_bundle.MainAsset5());
                        }
                        yield return null;
                        func(obj, this.m_path);
                        yield return null;
                    }
                    FileLoader.UnloadAssetBundle(this.m_path);
                }
                FileLoader.AssetBundleLoader.Loaders.Remove(this.m_path);
                yield break;
            }
        }

		private class AssetBundleInstance
		{
			public AssetBundle Bundle;

			public int References;

			public bool NoUnload;
		}

		public const string LANG_REPLACE_STRING = "{lang}";

		private static string s_assetBundlePath = null;

		private static string s_dataPath = null;

		private static string s_languagePath = null;

		private static string s_animationPath = null;

		public static readonly string UnitSEFolder = "/Assetbundles/UnitSEPrefab/";

		public static readonly string[] SneakAttackPaths = new string[]
		{
			"SkillSEs/3167_EXMT_SE_yuejinzhaoqixi.prefab",
			"SkillSEs/3174_EXMT_SE_yueqi.prefab",
			"SkillSEs/3166_EXMT_SE_xianqingqixi.prefab",
			"SkillSEs/3168_EXMT_SE_luowenrenqixi.prefab",
			"SkillSEs/3170_EXMT_SE_jushifang.prefab",
			"SkillSEs/3169_EXMT_SE_mingxiuqixi.prefab"
		};

		public static readonly string BattleEndEffect = "SkillSEs/44_EXF_SE_Screen_Taopaochenggong.prefab";

		public static readonly string BattleFailureEffect = "SkillSEs/19_EXF_SE_Screen_battlefailure.prefab";

		public static readonly string InBattleCameraPerform = "SkillSEs/37_EXF_SE_Screen_enterbattle.prefab";

		public static readonly string InBattleCameraPerformPost = "SkillSEs/38_EXF_SE_Screen_enterbattle_2.prefab";

		public static readonly string OutBattlePerformSmallGame = "SkillSEs/19_EXF_SE_Screen_battlefailure.prefab";

		public static readonly string GanZhiPath = "SkillSEs/18_EXF_SE_XT_XQ_ganzhi.prefab";

		public static readonly string GanZhiEndPath = "SkillSEs/39_EXF_SE_XT_XQ_ganzhi_end.prefab";

		public static readonly string path_up01 = "SkillSEs/1357_EXPH_SE_DT1.prefab";

		public static readonly string path_up02 = "SkillSEs/1358_EXPH_SE_DT2.prefab";

		public static readonly string path_up03 = "SkillSEs/1359_EXPH_SE_DT3.prefab";

		public static readonly string path_up04 = "SkillSEs/1360_EXPH_SE_DT4.prefab";

		public static readonly string path_up05 = "SkillSEs/1361_EXPH_SE_DT5.prefab";

		public static readonly string path_down01 = "SkillSEs/1362_EXPH_SE_DT1_TX.prefab";

		public static readonly string path_down02 = "SkillSEs/1363_EXPH_SE_DT2_TX.prefab";

		public static readonly string path_down03 = "SkillSEs/1364_EXPH_SE_DT3_TX.prefab";

		public static readonly string path_down04 = "SkillSEs/1365_EXPH_SE_DT4_TX.prefab";

		public static readonly string path_down05 = "SkillSEs/1366_EXPH_SE_DT5_TX.prefab";

		public static readonly string BossGHJJN1_1 = "SkillSEs/2104_EXL_SE_BOSS_GHJJN1_1.prefab";

		public static readonly string BossGHJJN1_2 = "SkillSEs/2105_EXL_SE_BOSS_GHJJN1_2.prefab";

		public static readonly string BossGHJJN1_3 = "SkillSEs/2106_EXL_SE_BOSS_GHJJN1_3.prefab";

		public static readonly string BossGHJJN2_1 = "SkillSEs/2113_EXL_SE_BOSS_GHJJN2_1.prefab";

		public static readonly string BossGHJJN2_2 = "SkillSEs/2114_EXL_SE_BOSS_GHJJN2_2.prefab";

		public static readonly string BossGHJJN2_3 = "SkillSEs/2115_EXL_SE_BOSS_GHJJN2_3.prefab";

		public static readonly string BossGHJJN3_4 = "SkillSEs/2110_EXL_SE_BOSS_GHJJN3_4.prefab";

		public static readonly string BossGHJJN3_5 = "SkillSEs/2111_EXL_SE_BOSS_GHJJN3_5.prefab";

		public static readonly string BossGHJJN3_6 = "SkillSEs/2112_EXL_SE_BOSS_GHJJN3_6.prefab";

		public static readonly string FuKongFongZhunXingPath = "SystemEffects/LDF/Zhunxing/EXF_XT_zhunxing_xiao.prefab";

		public static readonly string ZhunXing = "SystemEffects/LDF/Zhunxing/EXF_XT_zhunxing.prefab";

		public static readonly string MingXiuHuo = "SystemEffects/MT/zhuyudiyijieduan/EXMT_P_mingxiu_huo.prefab";

		public static readonly string Debuff = "SystemEffects/MT/zhuyudiyijieduan/EXMT_P_debuff.prefab";

		public static readonly string ShuiHuaZhuangShi = "SystemEffects/MT/zhuyudiyijieduan/EXMT_P_zhuyu_shuihuazhuangshi01.prefab";

		public static readonly string BaoDian = "SystemEffects/MT/EXMT_P_baodian.prefab";

		public static readonly string MingZhong = "SystemEffects/MT/zhuyudiyijieduan/EXMT_P_XG_HGY_mingzhong 1.prefab";

		public static readonly string MingZhong01 = "SystemEffects/MT/zhuyudiyijieduan/EXYJ_P_XG_FY_mingzhong01.prefab";

		public static readonly string MingXiuSheJi = "SystemEffects/MT/zhuyudiyijieduan/EXMT_P_mingxiushejiHUOQIU 2.prefab";

		public static readonly string MingXiuSheJi0 = "SystemEffects/MT/zhuyudiyijieduan/EXMT_P_mingxiushejiHUOQIU.prefab";

		public static readonly string MingXiuSheJi1 = "SystemEffects/MT/zhuyudiyijieduan/EXMT_P_mingxiushejiHUOQIU 1.prefab";

		public static readonly string TiaoKongXianPath = "SystemEffects/LDF/tiaokong/EXF_M_tiaokong_xian_F.prefab";

		public static readonly string ZhuYuGameLogic = "SystemEffects/MT/ZhuYuGameLogic.prefab";

		public static readonly string TiaoKongDiMianGuang = "SystemEffects/LDF/tiaokong/EXF_P_tiaokong_dimianguang.prefab";

		public static readonly string TiaoKongKongZhongGuang = "SystemEffects/LDF/tiaokong/EXF_P_tiaokong_kongzhongguang.prefab";

		public static readonly string DaFeiShuGuangHuan = "SystemEffects/PH/EXPH_XT/EXPH_XT_Dafeishu_guanghuan/EXPH_dafeishu_guanghuan.prefab";

		public static readonly string SongGuo01 = "SEObjects/MT/zhandouzhuangshitexiao/dafeishu/zuodichi/EXYJ_M_songguo01 1.prefab";

		public static readonly string BaiYan = "SEObjects/MT2/jiajuguai/EXMT_P_baiyan.prefab";

		public static readonly string HeiYan = "SEObjects/MT2/jiajuguai/EXMT_P_heiyan.prefab";

		public static readonly string LightningEmitter = "BattleEffects/Lightning Emitter.prefab";

		public static readonly string SFMO02 = "BattleEffects/EXL_P_HJ_YJZ_MX_SFMO02.prefab";

		public static readonly string SFMO03 = "BattleEffects/EXL_P_HJ_YJZ_MX_SFMO03.prefab";

		public static string MoviePanel = "UI/MoviePanel.unity3d";

		private static FileLoader s_instance = null;

		private static int s_assetbundlesToKeep = 1000;

		private Dictionary<string, FileLoader.AssetBundleInstance> m_loadedAssetBundles = new Dictionary<string, FileLoader.AssetBundleInstance>();

		public static List<string> s_loaderAssetList = new List<string>();

		private List<string> m_loadedOrder = new List<string>();

		private HashSet<string> m_toUnload = new HashSet<string>();

		private object m_lock = new object();

		public static string AssetBundlePath
		{
			get
			{
				if (FileLoader.s_assetBundlePath == null)
				{
					FileLoader.s_assetBundlePath = Path.Combine(Application.dataPath, "AssetBundles");
				}
				return FileLoader.s_assetBundlePath;
			}
		}

		public static string DataPath
		{
			get
			{
				if (FileLoader.s_dataPath == null)
				{
					FileLoader.s_dataPath = Path.Combine(Application.dataPath, "Data");
				}
				return FileLoader.s_dataPath;
			}
		}

		public static string LanguagePath
		{
			get
			{
				if (FileLoader.s_languagePath == null)
				{
                    FileLoader.s_languagePath = Path.Combine(Application.dataPath, Path.Combine("Langue", "0"));
                }
                return FileLoader.s_languagePath;
			}
		}

		public static string AnimationPath
		{
			get
			{
				if (FileLoader.s_animationPath == null)
				{
					FileLoader.s_animationPath = Path.Combine(FileLoader.AssetBundlePath, "AnimClips");
				}
				return FileLoader.s_animationPath;
			}
		}

		public static FileLoader Instance
		{
			get
			{
				if (FileLoader.s_instance == null)
				{
					FileLoader.s_instance = new FileLoader();
				}
				return FileLoader.s_instance;
			}
		}

		private FileLoader()
		{
		}

		public static void ResetLanguagePath()
		{
			FileLoader.s_languagePath = null;
		}

		private static int CheckDirectoryLevel(string path)
		{
			return path.Split(new char[]
			{
				Path.DirectorySeparatorChar
			}).Length - 2;
		}

		public bool CheckFileAvailability(string path)
		{
			if (path == string.Empty || path == null)
			{
				UnityEngine.Debug.LogError("File path is null");
				return false;
			}
			//if (!File.Exists(path))
			//{
			//	UnityEngine.Debug.LogError("File not found: path = " + path);
			//	return false;
			//}
			return true;
		}

		private void AddReference(string path, AssetBundle ab)
		{
			object @lock = this.m_lock;
			lock (@lock)
			{
				if (this.m_toUnload.Contains(path))
				{
					this.m_toUnload.Remove(path);
				}
				if (this.m_loadedAssetBundles.ContainsKey(path))
				{
					this.m_loadedAssetBundles[path].References++;
					int index = this.m_loadedOrder.FindIndex((string x) => x == path);
					this.m_loadedOrder.RemoveAt(index);
					this.m_loadedOrder.Add(path);
				}
				else
				{
					this.m_loadedAssetBundles.Add(path, new FileLoader.AssetBundleInstance
					{
						Bundle = ab,
						References = 1
					});
					this.m_loadedOrder.Add(path);
				}
			}
		}

		private void SubtractReference(string path)
		{
			object @lock = this.m_lock;
			lock (@lock)
			{
				if (this.m_loadedAssetBundles.ContainsKey(path))
				{
					this.m_loadedAssetBundles[path].References--;
					if (this.m_loadedAssetBundles[path].References == 0 && !this.m_loadedAssetBundles[path].NoUnload)
					{
						this.m_toUnload.Add(path);
					}
				}
			}
		}

		private AssetBundle _loadAssetBundleFromFile(string path)
		{
			if (!this.CheckFileAvailability(path))
			{
				return null;
			}
			if (this.m_loadedAssetBundles.ContainsKey(path))
			{
				this.AddReference(path, null);
				return this.m_loadedAssetBundles[path].Bundle;
			}
			AssetBundle assetBundle = AssetBundle.LoadFromFile(path);
			if (assetBundle == null)
			{
				UnityEngine.Debug.LogError("File not found: path = " + path);
			}
			else
			{
				this.AddReference(path, assetBundle);
			}
			return assetBundle;
		}

		public static AssetBundle LoadAssetBundleFromFile(string path)
		{
			System.Console.WriteLine("Static: LoadAssetBundleFromFile(" + path + ")");
			return FileLoader.Instance._loadAssetBundleFromFile(path);
		}

		public static T LoadObjectFromFile<T>(string path, bool instantiate, bool unloadAfterIns = true) where T : UnityEngine.Object
		{
			System.Console.WriteLine(string.Concat(new object[]
			{
				"Static: LoadObjectFromFile<",
				typeof(T).ToString(),
				">(",
				path,
				", ",
				instantiate,
				")"
			}));
			AssetBundle assetBundle = FileLoader.Instance._loadAssetBundleFromFile(path);
			T result = (T)((object)null);
			if (assetBundle != null)
			{
				if (instantiate)
				{
					if (assetBundle.MainAsset5() == null)
					{
						UnityEngine.Debug.LogError("FileLoader.LoadObjectFromFile<" + typeof(T).ToString() + ">: object to instantiate is null, path = " + path);
						result = (T)((object)null);
					}
					else
					{
						result = (UnityEngine.Object.Instantiate(assetBundle.MainAsset5()) as T);
					}
					if (unloadAfterIns)
					{
						FileLoader.UnloadAssetBundle(path);
					}
				}
				else
				{
					result = (assetBundle.MainAsset5() as T);
				}
			}
			return result;
		}

		public static T LoadObjectFromFileByLang<T>(string path, bool instantiate, bool unloadAfterIns = true, bool isLangue2 = false) where T : UnityEngine.Object
		{
			string text = path;
			if (path.Contains("{lang}"))
			{
				if (isLangue2)
				{
					text = text.Replace("{lang}", "_eng/");
				}
				else
				{
					text = text.Replace("{lang}", string.Empty);
				}
			}
			text = text.ToAssetBundlePath();
			return FileLoader.LoadObjectFromFile<T>(text, instantiate, unloadAfterIns);
		}

		public static T LoadComponentFromFile<T>(string path, bool instantiate) where T : Component
		{
			System.Console.WriteLine(string.Concat(new object[]
			{
				"Static: LoadComponentFromFile<",
				typeof(T).ToString(),
				">(",
				path,
				", ",
				instantiate,
				")"
			}));
			AssetBundle assetBundle = FileLoader.Instance._loadAssetBundleFromFile(path);
			GameObject gameObject = null;
			if (assetBundle != null)
			{
				if (instantiate)
				{
					if (assetBundle.MainAsset5() == null)
					{
						UnityEngine.Debug.LogError("FileLoader.LoadComponentFromFile<" + typeof(T).ToString() + ">: object to instantiate is null, path = " + path);
						gameObject = null;
					}
					else
					{
						gameObject = (UnityEngine.Object.Instantiate(assetBundle.MainAsset5()) as GameObject);
					}
					FileLoader.UnloadAssetBundle(path);
				}
				else
				{
					gameObject = (assetBundle.MainAsset5() as GameObject);
				}
			}
			T result = (T)((object)null);
			if (gameObject != null)
			{
				result = gameObject.GetComponent<T>();
			}
			return result;
		}

		public static T LoadComponentFromFileByLang<T>(string path, bool instantiate, bool isLangue2 = false) where T : Component
		{
			string text = path;
			if (path.Contains("{lang}"))
			{
				if (isLangue2)
				{
					text = text.Replace("{lang}", "_eng/");
				}
				else
				{
					text = text.Replace("{lang}", string.Empty);
				}
			}
			text = text.ToAssetBundlePath();
			return FileLoader.LoadComponentFromFile<T>(text, instantiate);
		}

        private IEnumerator _loadAssetBundleFromFileAsync(string path)
        {
            if (this.CheckFileAvailability(path))
            {
                if (this.m_loadedAssetBundles.ContainsKey(path))
                {
                    this.AddReference(path, null);
                }
                else
                {
                    AssetBundleCreateRequest abcr = AssetBundle.LoadFromFileAsync(path);
                    if (abcr == null)
                    {
                        UnityEngine.Debug.LogError("File not found: path = " + path);
                    }
                    else
                    {
                        yield return abcr;
                        AssetBundle ab = abcr.assetBundle;
                        this.AddReference(path, ab);
                    }
                }
            }
            yield break;
        }


        public static IEnumerator LoadAssetBundleFromFileAsync(string path)
        {
            System.Console.WriteLine("Static: LoadAssetBundleFromFileAsync(" + path + ")");
            yield return FileLoader.Instance._loadAssetBundleFromFileAsync(path);
            yield break;
        }

        public static void PreloadAssetBundleFromFileAsync(string path)
		{
			System.Console.WriteLine("Static: PreloadAssetBundleFromFileAsync(" + path + ")");
			PalMain.Instance.StartCoroutine(FileLoader.Instance._loadAssetBundleFromFileAsync(path));
		}

		private void loadAssetBundleFromFileAsync(string path, Action<UnityEngine.Object, string> onLoadOver, bool instantiate)
		{
			if (FileLoader.Instance.m_loadedAssetBundles.ContainsKey(path))
			{
				AssetBundle bundle = FileLoader.Instance.loadAssetBundleFromCache(path, true);
				UnityEngine.Object arg;
				if (instantiate)
				{
					if (bundle.MainAsset5() == null)
					{
						UnityEngine.Debug.LogError("FileLoader.loadAssetBundleFromFileAsync: object to instantiate is null, path = " + path);
						arg = null;
					}
					else
					{
						arg = UnityEngine.Object.Instantiate(bundle.MainAsset5());
					}
					FileLoader.UnloadAssetBundle(path);
				}
				else
				{
					arg = bundle.MainAsset5();
				}
				onLoadOver(arg, path);
			}
			else if (FileLoader.AssetBundleLoader.Loaders.ContainsKey(path))
			{
				if (instantiate)
				{
					FileLoader.AssetBundleLoader.Loaders[path].OnLoadOverInstantiate += onLoadOver;
				}
				else
				{
					FileLoader.AssetBundleLoader.Loaders[path].OnLoadOver += onLoadOver;
				}
			}
			else
			{
				FileLoader.AssetBundleLoader assetBundleLoader = new FileLoader.AssetBundleLoader(path, onLoadOver, instantiate);
			}
		}

		public static void LoadAssetBundleFromFileAsync(string path, Action<UnityEngine.Object, string> onLoadOver, bool instantiate)
		{
			System.Console.WriteLine(string.Concat(new object[]
			{
				"Static: LoadAssetBundleFromFileAsync(",
				path,
				", function, ",
				instantiate,
				")"
			}));
			FileLoader.Instance.loadAssetBundleFromFileAsync(path, onLoadOver, instantiate);
		}

		public AssetBundle loadAssetBundleFromCache(string path, bool addRef = true)
		{
			if (this.m_loadedAssetBundles.ContainsKey(path))
			{
				if (addRef)
				{
					this.AddReference(path, null);
				}
				return this.m_loadedAssetBundles[path].Bundle;
			}
			return null;
		}

		public static AssetBundle LoadAssetBundleFromCache(string path)
		{
			System.Console.WriteLine("Static: LoadAssetBundleFromCache(" + path + ")");
			return FileLoader.Instance.loadAssetBundleFromCache(path, true);
		}

		public static void UnloadAssetBundle(string path)
		{
			System.Console.WriteLine("Static: UnloadAssetBundle(" + path + ")");
			FileLoader.Instance.SubtractReference(path);
		}

		private void unloadAllAssetbundles()
		{
			object @lock = this.m_lock;
			lock (@lock)
			{
				Dictionary<string, FileLoader.AssetBundleInstance> dictionary = new Dictionary<string, FileLoader.AssetBundleInstance>();
				List<string> list = new List<string>();
				foreach (string current in this.m_loadedAssetBundles.Keys)
				{
					if (this.m_loadedAssetBundles[current] == null)
					{
						UnityEngine.Debug.LogError(string.Format("[Error] : unloadAllAssetbundles : key={0}, bundle=null", current));
					}
					if (this.m_loadedAssetBundles[current].NoUnload)
					{
						dictionary.Add(current, this.m_loadedAssetBundles[current]);
						list.Add(current);
					}
					else
					{
						this.m_loadedAssetBundles[current].Bundle.Unload(false);
					}
				}
				this.m_loadedAssetBundles.Clear();
				this.m_loadedOrder.Clear();
				this.m_toUnload.Clear();
				this.m_loadedAssetBundles = dictionary;
				this.m_loadedOrder = list;
			}
		}

		public static void UnloadAllAssetBundles()
		{
			System.Console.WriteLine("Static: UnloadAllAssetBundles");
			FileLoader.Instance.unloadAllAssetbundles();
		}

		private void setNoUnload(string path, bool noUnload)
		{
			object @lock = this.m_lock;
			lock (@lock)
			{
				if (this.m_loadedAssetBundles.ContainsKey(path))
				{
					this.m_loadedAssetBundles[path].NoUnload = noUnload;
				}
			}
		}

		public static void SetNoUnload(string path, bool noUnload)
		{
			FileLoader.Instance.setNoUnload(path, noUnload);
		}

		public static T LoadResourceFromFile<T>(string path) where T : UnityEngine.Object
		{
			T t = Resources.Load<T>(path);
			if (t == null)
			{
				UnityEngine.Debug.LogError("File not found: path = " + path);
			}
			return t;
		}

		public void Update()
		{
			object @lock = this.m_lock;
			lock (@lock)
			{
				foreach (string path in this.m_toUnload)
				{
					this.m_loadedAssetBundles[path].Bundle.Unload(false);
					this.m_loadedAssetBundles.Remove(path);
					int index = this.m_loadedOrder.FindIndex((string x) => x == path);
					this.m_loadedOrder.RemoveAt(index);
				}
				this.m_toUnload.Clear();
				if (this.m_loadedOrder.Count > FileLoader.s_assetbundlesToKeep)
				{
					int num = this.m_loadedOrder.Count - FileLoader.s_assetbundlesToKeep;
					int num2 = 0;
					int num3 = 0;
					while (num3 < num && num3 + num2 < this.m_loadedOrder.Count)
					{
						string text = this.m_loadedOrder[num2];
						if (this.m_loadedAssetBundles[text].NoUnload)
						{
							num3--;
							num2++;
						}
						else
						{
							System.Console.WriteLine(string.Concat(new object[]
							{
								"FileLoader.Update: Over + ",
								FileLoader.s_assetbundlesToKeep,
								", Release loaded: path = ",
								text
							}));
							this.m_loadedAssetBundles[text].Bundle.Unload(false);
							this.m_loadedAssetBundles.Remove(text);
							this.m_loadedOrder.RemoveAt(0);
						}
						num3++;
					}
				}
			}
		}

		public void ShowLoadedAssetBundle()
		{
			System.Console.WriteLine(string.Format("[Loaded AssetBundle] : AssetBundle count={0}", this.m_loadedAssetBundles.Count));
			foreach (KeyValuePair<string, FileLoader.AssetBundleInstance> current in this.m_loadedAssetBundles)
			{
				System.Console.WriteLine(string.Format("[Loaded AssetBundle] : path={0}", current.Key));
			}
		}
	}
}
