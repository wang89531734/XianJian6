using Funfia.File;
using SoftStar;
using SoftStar.Pal6;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace SoftStar.Pal6
{
    public class StartInit : MonoBehaviour
    {
        private static StartInit mInstance;

        public bool OverrideMainLine;

        public static bool IsFirstStart = true;

        public static StartInit Instance
        {
            get
            {
                return StartInit.mInstance;
            }
        }

        private void Awake()
        {
            StartInit.mInstance = this;
            this.Init();
        }

        private void Update()
        {

        }

        private void OnLevelLoadOver()
        {
            PalMain.LoadOverEvent = (Action)Delegate.Remove(PalMain.LoadOverEvent, new Action(this.OnLevelLoadOver));
        }

        private void Init()
        {
            if (SceneManager.GetActiveScene().buildIndex != 0)
            {
                return;
            }
            this.LoadMain();
            PalMain.Instance.ReStart();
            if (StartInit.IsFirstStart)
            {
  
            }
            else
            {
                PalMain.LoadOverEvent = (Action)Delegate.Remove(PalMain.LoadOverEvent, new Action(this.OnLevelLoadOver));
                PalMain.LoadOverEvent = (Action)Delegate.Combine(PalMain.LoadOverEvent, new Action(this.OnLevelLoadOver));
            }
        }

        private void LoadMain()
        {
            GameObject x = GameObject.Find("/Main");
            if (x == null)
            {
                PalMain.CreateInstance();
            }
        }
    }
}
