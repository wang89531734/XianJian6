using Funfia.File;
using SoftStar.Pal6;
using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace SoftStar.Pal6
{
    public class ScenesManager : MonoBehaviour
    {
        private static ScenesManager instance;

        public int loadedLevel;

        public static bool IsChanging;

        //private static ShowLoading m_showLoading;

        private static int m_loadLevel = -1;

        public string flashPath = string.Empty;

        //private FlashStruct flashStruct;

        private string nextDestObjName = string.Empty;

        private int NextLevelIndex = -1;

        public static float curSceneBeforeTime;

        public static int CurLoadedLevel = -1;

        public event Action<int> OnLevelLoaded;

        public event Action<int> OnChangeMap;

        public static ScenesManager Instance
        {
            get
            {
                if (ScenesManager.instance == null)
                {
                    ScenesManager.Initialize();
                }
                return ScenesManager.instance;
            }
        }

        public string NextDestObjName
        {
            get
            {
                return this.nextDestObjName;
            }
            set
            {
                this.nextDestObjName = value;
            }
        }

        public static void Initialize()
        {
            PalMain gameMain = PalMain.GameMain;
            if (gameMain != null)
            {
                ScenesManager.instance = gameMain.GetComponent<ScenesManager>();
                if (ScenesManager.instance == null)
                {
                    ScenesManager.instance = gameMain.gameObject.AddComponent<ScenesManager>();
                }
            }
        }

        public void ChangeMap(string DestName, int LevelIndex, Action<int> _OnLevelLoaded)
        {
            //if (PalBattleManager.Instance() != null)
            //{
            //	PalBattleManager.Instance().bEnableGoToBattle = true;
            //	PalBattleManager.Instance().m_bEnterBattle = false;
            //}
            this.loadedLevel = LevelIndex;
            if (this.OnChangeMap != null)
            {
                this.OnChangeMap(LevelIndex);
            }
            int num = UnityEngine.Random.Range(0, 2);

            //ShowLoading showLoading;

            //showLoading = ShowLoading.Initialize("1");

            //if (showLoading != null)
            //{
            //	string text2 = this.flashPath.ToLanguagePath();
            //	System.Console.WriteLine("Play flash: " + text2);
            //	this.flashStruct = FlashManager.Instance.Play(text2, null, new Action<UnityEngine.Object, EventArgs>(this.flashLoadFinish), true, false, ShowLoading.LoadingCamera, null);
            //	EntityManager.OnLoadOverEnd = (EntityManager.void_fun)Delegate.Remove(EntityManager.OnLoadOverEnd, new EntityManager.void_fun(this.EntityLoadOver));
            //	EntityManager.OnLoadOverEnd = (EntityManager.void_fun)Delegate.Combine(EntityManager.OnLoadOverEnd, new EntityManager.void_fun(this.EntityLoadOver));
            //}
            this.OnLevelLoaded = (Action<int>)Delegate.Remove(this.OnLevelLoaded, _OnLevelLoaded);
            this.OnLevelLoaded = (Action<int>)Delegate.Combine(this.OnLevelLoaded, _OnLevelLoaded);
  
            this.NextDestObjName = DestName;

            ScenesManager.m_loadLevel = LevelIndex;
            //ScenesManager.m_showLoading = showLoading;
        }

        public void flashLoadFinish(UnityEngine.Object obj, EventArgs args)
        {
            //if (ScenesManager.m_showLoading != null)
            //{
            //	ScenesManager.m_showLoading.setDepth(obj, args);
            //}
            this.LoadLevel(ScenesManager.m_loadLevel);
        }

        public void RandomFlash()
        {
            bool flag = true;
            //int flag2 = PalMain.GetFlag(1);
            //do
            //{
            //	int num = UnityEngine.Random.Range(0, FlashManager.FlashScenePaths.Length);
            //	if ((num != 5 || flag2 >= 224020) && !this.flashPath.Equals(FlashManager.FlashScenePaths[num]))
            //	{
            //		this.flashPath = FlashManager.FlashScenePaths[num];
            //		flag = false;
            //	}
            //}
            //while (flag);
        }

        private void EntityLoadOver()
        {
            //EntityManager.OnLoadOverEnd = (EntityManager.void_fun)Delegate.Remove(EntityManager.OnLoadOverEnd, new EntityManager.void_fun(this.EntityLoadOver));
            //if (this.flashStruct != null)
            //{
            //	this.flashStruct.DestroyFlash();
            //	this.flashStruct = null;
            //}
            this.RandomFlash();
        }

        public void LoadLevel(int LevelIndex)
        {
            this.NextLevelIndex = LevelIndex;
            //base.StartCoroutine(UtilFun.LoadLevelAsync("empty"));
        }

        private void OnLevelWasLoaded(int level)
        {
            ScenesManager.curSceneBeforeTime = Time.time;
            if (SceneManager.GetActiveScene().name == "empty")
            {
                FileLoader.UnloadAllAssetBundles();
                //	base.StartCoroutine(UtilFun.LoadLevelAsync(this.NextLevelIndex));
                //PalMain.LoadingValue = 0.05f;
            }
            else
            {
                GC.Collect();
                //UtilFun.DestroyGhostCamera();
                //ScenesManager.CurLoadedLevel = UtilFun.GetPalMapLevel(level);
                //PalMain.LoadingValue = 0.15f;
                if (this.OnLevelLoaded != null)
                {
                    this.OnLevelLoaded(level);
                }
            }
        }
    }
}
