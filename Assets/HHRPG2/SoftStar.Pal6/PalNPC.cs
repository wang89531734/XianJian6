//using SoftStar.BuffDebuff;
using SoftStar.Item;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SoftStar.Pal6
{
    public class PalNPC : PalAnimatorObject, ISaveInterface
    {
        [Serializable]
        public class CharacterData : ISaveInterface
        {
            [NonSerialized]
            private GameObject mOwner;

            //[NonSerialized]
            //private PlayerBaseProperty mPlayerBase;

            //[NonSerialized]
            //private HPMPDPProperty mHPMPDP;

            //[NonSerialized]
            //private FightProperty mFight;

            //[NonSerialized]
            //private CharacterProperty mCharacter;

            //[NonSerialized]
            //private PlayerProperty mPlayer;

            //[NonSerialized]
            //private SocialNPCProperty mSocialNPC;

            //[NonSerialized]
            //private MonsterProperty mMonster;

            [SerializeField]
            private int mCharacterID;

            [SerializeField]
            private int mLevel;

            [NonSerialized]
            private int mExp;

            [NonSerialized]
            private int mSoul;

            [NonSerialized]
            public int LoadHP;

            [NonSerialized]
            public int LoadMP;

            [NonSerialized]
            public int LoadDP;

            public GameObject Owner
            {
                get
                {
                    return this.mOwner;
                }
                set
                {
                    this.mOwner = value;
                }
            }

            //public PlayerBaseProperty PlayerBase
            //{
            //    get
            //    {
            //        return this.mPlayerBase;
            //    }
            //}

            //public HPMPDPProperty HPMPDP
            //{
            //    get
            //    {
            //        return this.mHPMPDP;
            //    }
            //}

            //public FightProperty Fight
            //{
            //    get
            //    {
            //        return this.mFight;
            //    }
            //}

            //public CharacterProperty CharacterCommon
            //{
            //    get
            //    {
            //        return this.mCharacter;
            //    }
            //}

            //public PlayerProperty Player
            //{
            //    get
            //    {
            //        return this.mPlayer;
            //    }
            //}

            //public SocialNPCProperty SocialNPC
            //{
            //    get
            //    {
            //        if (this.mSocialNPC == null)
            //        {
            //            string message = "Error : 严重错误！！！ npc[" + ((!(this.Owner != null)) ? this.CharacterID.ToString() : this.Owner.name) + "] SocialNPC==null";
            //            UnityEngine.Debug.LogError(message);
            //        }
            //        return this.mSocialNPC;
            //    }
            //}

            //public MonsterProperty Monster
            //{
            //    get
            //    {
            //        return this.mMonster;
            //    }
            //}

            public int CharacterID
            {
                get
                {
                    return this.mCharacterID;
                }
            }

            public int Level
            {
                get
                {
                    return this.mLevel;
                }
                private set
                {
                    if (this.mLevel != value)
                    {
                        //if (this.mPlayerBase != null)
                        //{
                        //    uint characterID = (uint)this.mCharacterID;
                        //    int oldLevel = this.mLevel;
                        //    this.mLevel = value;
                        //    this.mPlayerBase.ChangeLevel(characterID, this.mLevel);
                        //    ChangeLevelScript.OnChangeLevel(characterID, oldLevel, this.mLevel, this.mOwner);
                        //}
                        //else
                        //{
                        //    this.mLevel = value;
                        //}
                        //if (this.mHPMPDP != null)
                        //{
                        //    this.mHPMPDP.HP = this.mHPMPDP.HPRange;
                        //    this.mHPMPDP.MP = this.mHPMPDP.MPRange;
                        //}
                    }
                }
            }

            public int Exp
            {
                get
                {
                    return this.mExp;
                }
                set
                {
                    if (this.mExp != value)
                    {
                        try
                        {
                            this.mExp = value;
                           // this.Level = PlayerBaseProperty.LevelData.FindLevel(this.mExp);
                        }
                        catch (Exception ex)
                        {
                            UnityEngine.Debug.LogException(ex);
                          //  UIDialogManager.Instance.ShowNoForceInfoDialog(ex.ToString(), 60f);
                        }
                    }
                }
            }

            //public int NeedExp
            //{
            //    get
            //    {
            //        return PlayerBaseProperty.LevelData.GetLevelExp(this.mLevel) - this.mExp;
            //    }
            //}

            public int Soul
            {
                get
                {
                    return this.mSoul;
                }
                set
                {
                    this.mSoul = value;
                }
            }

            public void initialization(GameObject inOwner, int inCharacterID, int inLevel)
            {
                this.mOwner = inOwner;
                this.mCharacterID = inCharacterID;
                this.Reset();
                this.Level = inLevel;
            }

            public void initialization(GameObject inOwner, uint inCharacterID, int inLevel)
            {
                this.initialization(inOwner, (int)inCharacterID, inLevel);
            }

            public void Reset()
            {
                uint num = (uint)this.mCharacterID;
                //if (this.mPlayerBase != null)
                //{
                //    this.mPlayerBase = null;
                //}
                //PlayerBaseProperty.PlayerBaseData data = PlayerBaseProperty.GetData(num, this.Level);
                //if (data != null)
                //{
                //    this.mPlayerBase = new PlayerBaseProperty(data);
                //}
                //if (this.mHPMPDP != null)
                //{
                //    this.mHPMPDP.UnLink();
                //    this.mHPMPDP = null;
                //}
                //HPMPDPProperty.StaticData data2 = HPMPDPProperty.StaticData.GetData(num);
                //if (data2 != null)
                //{
                //    this.mHPMPDP = new HPMPDPProperty(data2);
                //}
                //if (this.mFight != null)
                //{
                //    this.mFight.UnLink();
                //    this.mFight = null;
                //}
                //FightProperty.StaticData data3 = FightProperty.StaticData.GetData(num);
                //if (data3 != null)
                //{
                //    this.mFight = new FightProperty(data3);
                //}
                //if (this.mHPMPDP != null)
                //{
                //    this.mHPMPDP.LinkPlayerBase = this.mPlayerBase;
                //    this.mHPMPDP.SetWithoutEvents(this.mHPMPDP.HPRange, 0, 0);
                //}
                //if (this.mFight != null)
                //{
                //    this.mFight.LinkPlayerBase = this.mPlayerBase;
                //}
                //CharacterProperty.StaticData data4 = CharacterProperty.StaticData.GetData(num);
                //if (data4 != null)
                //{
                //    this.mCharacter = new CharacterProperty(data4);
                //}
                //else
                //{
                //    this.mCharacter = null;
                //}
                //PlayerProperty.StaticData data5 = PlayerProperty.StaticData.GetData(num);
                //if (data5 != null)
                //{
                //    this.mPlayer = new PlayerProperty(data5);
                //}
                //else
                //{
                //    this.mPlayer = null;
                //}
                //SocialNPCProperty.StaticData data6 = SocialNPCProperty.StaticData.GetData(num);
                //if (data6 != null)
                //{
                //    this.mSocialNPC = new SocialNPCProperty(data6);
                //}
                //else
                //{
                //    this.mSocialNPC = null;
                //}
                //MonsterProperty.StaticData data7 = MonsterProperty.StaticData.GetData(num);
                //if (data7 != null)
                //{
                //    this.mMonster = new MonsterProperty(data7);
                //}
                //else
                //{
                //    this.mMonster = null;
                //}
            }

            public void Save(BinaryWriter writer)
            {
                writer.Write(this.mCharacterID);
                //if (this.mHPMPDP != null)
                //{
                //    writer.Write(this.mHPMPDP.HP);
                //    writer.Write(this.mHPMPDP.MP);
                //    writer.Write(this.mHPMPDP.DP);
                //}
                //else
                //{
                //    writer.Write(0);
                //    writer.Write(0);
                //    writer.Write(0);
                //}
                writer.Write(this.mExp);
                writer.Write(this.mSoul);
            }

            public void Load(BinaryReader reader)
            {
                this.mCharacterID = reader.ReadInt32();
                this.LoadHP = reader.ReadInt32();
                this.LoadMP = reader.ReadInt32();
                this.LoadDP = reader.ReadInt32();
                //if (SaveManager.VersionNum < 21u)
                //{
                //    this.mLevel = reader.ReadInt32();
                //}
                this.mExp = reader.ReadInt32();
              //  this.mLevel = PlayerBaseProperty.LevelData.FindLevel(this.mExp);
                this.mSoul = reader.ReadInt32();
                this.Reset();
            }
        }

        [Serializable]
        public class SkillInfo
        {
            public int m_ID;

            public bool m_bOpen = true;

            public int m_CurrentExp;
        }

        public enum NPCState
        {
            Default,
            Patrol,
            Guard,
            Die
        }

        public delegate void void_fun_TF(PalNPC npc);

        [HideInInspector, SerializeField]
        public PalNPC.CharacterData Data;

        //[NonSerialized]
        //public BuffDebuffManager.BuffDebuffOwner BuffDebuffData;

        public int[] MonsterGroups;

        public int mBattleFieldID;

        public AudioClip m_BeHitSound;

        private static int[] mAdvanceSkillIDs = new int[]
        {
                    101,
                    3088,
                    104,
                    2078,
                    107,
                    2084,
                    114,
                    3090,
                    2102,
                    116,
                    3097,
                    3096
        };

        public List<PalNPC.SkillInfo> m_SkillIDs = new List<PalNPC.SkillInfo>();

        [NonSerialized]
        public float ExpRate = 1f;

        [NonSerialized]
        public int ImmunityAllBadBattleStates;

        [NonSerialized]
        public float FengYinAddRate;

        [NonSerialized]
        public float MiaoShouAddRate;

        [NonSerialized]
        public float BreakAddRate;

        [NonSerialized]
        public List<GameObject> Weapons = new List<GameObject>(2);

        [NonSerialized]
        public GameObject ornament;

        [NonSerialized]
        public Texture oriMainTex;

        [NonSerialized]
        public Texture oriSpecTex;

        private Material m_oriAssortMat;

        private GameObject weaponAssortObj;

        [NonSerialized]
        private uint[] mLoadEquipSlots;

        //[NonSerialized]
        //private Dictionary<EquipSlotEnum, ItemWatcher> mEquipSlots = new Dictionary<EquipSlotEnum, ItemWatcher>();

        //private AchievementManager.ACHIEVEMENT_INDEX mCurrentAchievement = (AchievementManager.ACHIEVEMENT_INDEX)(-1);

        protected PalNPC.NPCState state;

        public PalNPC.void_fun_TF OnLoadModelEnd;

        public bool bEnableOnLoadModelEnd = true;

        private Animator m_animator;

        public Patrol patrol;

        //public NPCMode curNPCMode = NPCMode.fight;

        //public NPCPersonalityType personalityType;

        public Perception perception;

        public float hatred = 50f;

        public float CurFightDistance = 0.5f;

        public string PrefabID = string.Empty;

        private bool IsDataInit;

        //private Footmark m_FootMark;

        public float BattleColR = 1.67f;

        public float BattleColH = 0.1f;

        //public List<Interact> interActs = new List<Interact>();

        private int m_SkillGroup;

        private bool m_bDontLoadModel;

        private Action ProcessCore;

        public static float interval = 1f;

        private float curTime = 0.23f;

        private Transform modelTF;

        private bool isMonster;

        public static bool SevereCull = true;

        public static float CullCamOffsetRatio = 0.35f;

        private List<string> uScriptsName = new List<string>();

        private string curAnimName;

        //		public string ShowName
        //		{
        //			get
        //			{
        //				string result = string.Empty;
        //				if (this.Data != null)
        //				{
        //					if (this.Data.CharacterCommon == null)
        //					{
        //						this.Data.Reset();
        //					}
        //					CharacterProperty characterCommon = this.Data.CharacterCommon;
        //					if (characterCommon != null)
        //					{
        //						if (characterCommon.ShowName != null)
        //						{
        //							result = characterCommon.ShowName.get_string();
        //						}
        //						else
        //						{
        //							UnityEngine.Debug.LogError("Error : PalNPC.Data ShowName  Data.CharacterCommon.ShowName == null");
        //						}
        //					}
        //					else
        //					{
        //						UnityEngine.Debug.LogError("Error : PalNPC.Data ShowName  Data.CharacterCommon == null");
        //					}
        //				}
        //				else
        //				{
        //					UnityEngine.Debug.LogError("Error : PalNPC.Data ShowName  Data == null");
        //				}
        //				return result;
        //			}
        //		}

        //		public Material oriAssortMat
        //		{
        //			get
        //			{
        //				return this.m_oriAssortMat;
        //			}
        //			set
        //			{
        //				this.m_oriAssortMat = value;
        //			}
        //		}

        //		public GameObject WeaponAssortObj
        //		{
        //			get
        //			{
        //				if (base.name == "2")
        //				{
        //					return base.gameObject.GetWeaponAssortObj(false, -1);
        //				}
        //				if (this.weaponAssortObj == null)
        //				{
        //					this.weaponAssortObj = base.gameObject.GetWeaponAssortObj(false, -1);
        //				}
        //				return this.weaponAssortObj;
        //			}
        //		}

        //		public AchievementManager.ACHIEVEMENT_INDEX CurrentAchievement
        //		{
        //			get
        //			{
        //				return this.mCurrentAchievement;
        //			}
        //			set
        //			{
        //				if (this.mCurrentAchievement == value)
        //				{
        //					return;
        //				}
        //				NicknameBuffDebuffType data = NicknameBuffDebuffTypeCache.GetData((uint)this.mCurrentAchievement);
        //				if (data != null)
        //				{
        //					this.BuffDebuffData.RemoveOneBuffDebuffByType(data.TypeID);
        //				}
        //				this.mCurrentAchievement = value;
        //				NicknameBuffDebuffType data2 = NicknameBuffDebuffTypeCache.GetData((uint)this.mCurrentAchievement);
        //				if (data2 != null)
        //				{
        //					this.BuffDebuffData.AddBuffDebuffByType(data2.TypeID);
        //				}
        //			}
        //		}

        //		public PalNPC.NPCState State
        //		{
        //			get
        //			{
        //				return this.state;
        //			}
        //			set
        //			{
        //				if (value != this.state)
        //				{
        //					if (value != PalNPC.NPCState.Default)
        //					{
        //						if (value == PalNPC.NPCState.Patrol)
        //						{
        //							this.patrol.enabled = true;
        //						}
        //					}
        //					else
        //					{
        //						this.patrol.enabled = false;
        //					}
        //				}
        //			}
        //		}

        public Animator animator
        {
            get
            {
                if (this.m_animator == null && this.model != null)
                {
                    this.m_animator = this.model.GetComponent<Animator>();
                }
                return this.m_animator;
            }
        }

        //		public Footmark footMark
        //		{
        //			get
        //			{
        //				return this.m_FootMark;
        //			}
        //			set
        //			{
        //				this.m_FootMark = value;
        //			}
        //		}

        //		public override string[] AvailableComponentNames
        //		{
        //			get
        //			{
        //				return new string[]
        //				{
        //					"Patrol",
        //					"MoveByPalCurve"
        //				};
        //			}
        //		}

        //		public bool CanLookAt()
        //		{
        //			return this.Data != null && this.Data.SocialNPC != null && this.Data.SocialNPC.LookAt;
        //		}

        //		public bool CanBeLooked()
        //		{
        //			return this.Data != null && this.Data.SocialNPC != null && this.Data.SocialNPC.BeLooked;
        //		}

        //		public float ChangeHP(PalNPC Source, ElementPhase curPhase, float add, float scale, bool IsCrit, bool IsDodge, bool IsBlock)
        //		{
        //			BuffDebuffManager.BuffDebuffOwner.ActionContainer actionContainer = new BuffDebuffManager.BuffDebuffOwner.ActionContainer(Source, this);
        //			actionContainer.IsCrit = IsCrit;
        //			actionContainer.IsDodge = IsDodge;
        //			actionContainer.IsBlock = IsBlock;
        //			DynamicFloat orCreateHPChange = actionContainer.GetOrCreateHPChange(curPhase);
        //			if (add >= 0f)
        //			{
        //				orCreateHPChange.MinValue = 0f;
        //				orCreateHPChange.MaxValue = 3.40282347E+38f;
        //			}
        //			else
        //			{
        //				orCreateHPChange.MinValue = -3.40282347E+38f;
        //				orCreateHPChange.MaxValue = 0f;
        //			}
        //			orCreateHPChange.SetAdd(add);
        //			orCreateHPChange.SetScale(scale);
        //			BuffDebuffManager.BuffDebuffOwner.BuffDebuffAction(actionContainer);
        //			actionContainer.UseChange();
        //			return actionContainer.GetHPChangeSum();
        //		}

        //		public void AddSkillNoRepeat(PalNPC.SkillInfo newSkill)
        //		{
        //			int num = 0;
        //			MonsterSkillDataManager.SkillData data = PalBattleManager.Instance().GetMonsterSkillDataManager().GetData(newSkill.m_ID);
        //			if (data != null)
        //			{
        //				num = data.mID;
        //			}
        //			foreach (PalNPC.SkillInfo current in this.m_SkillIDs)
        //			{
        //				if (current.m_ID == newSkill.m_ID)
        //				{
        //					return;
        //				}
        //			}
        //			int index = 0;
        //			bool flag = false;
        //			for (int i = 0; i < this.m_SkillIDs.Count; i++)
        //			{
        //				index = i;
        //				MonsterSkillDataManager.SkillData data2 = PalBattleManager.Instance().GetMonsterSkillDataManager().GetData(this.m_SkillIDs[i].m_ID);
        //				if (data2 != null && data2.mID >= num)
        //				{
        //					flag = true;
        //					break;
        //				}
        //			}
        //			if (flag)
        //			{
        //				this.m_SkillIDs.Insert(index, newSkill);
        //			}
        //			else
        //			{
        //				this.m_SkillIDs.Add(newSkill);
        //			}
        //			if (Array.Exists<int>(PalNPC.mAdvanceSkillIDs, (int Cur) => Cur == newSkill.m_ID))
        //			{
        //				ulong idx = 32002uL;
        //				FlagManager.SetBoolFlag(idx, true);
        //				try
        //				{
        //					if (UIInformation_Help_Item.Items != null && 1 < UIInformation_Help_Item.Items.Length && 2 < UIInformation_Help_Item.Items[1].SubItems.Length && 0 < UIInformation_Help_Item.Items[1].SubItems[2].Text.Length)
        //					{
        //						UIDialogManager.SetOpen(UIInformation_Help_Item.Items[1].SubItems[2].Text[0]);
        //					}
        //				}
        //				catch (Exception exception)
        //				{
        //					UnityEngine.Debug.LogException(exception);
        //				}
        //			}
        //		}

        //		public void ResetOrnament()
        //		{
        //			UtilFun.BindOrnamentToProp(this.model.transform, this.ornament.transform, false);
        //		}

        public void GetOriRes()
        {
            //this.model.GetOriTex(out this.oriMainTex, out this.oriSpecTex);
            //this.oriAssortMat = this.WeaponAssortObj.GetMat();
        }

        //		public void RestoreAssortMat()
        //		{
        //			if (this.oriAssortMat != null)
        //			{
        //				this.WeaponAssortObj.SetMat(this.oriAssortMat);
        //			}
        //		}

        //		public void RestoreTex()
        //		{
        //			if (this.model == null)
        //			{
        //				UtilFun.ConsoleLog("Log : model=null RestoreTex 返回", false);
        //				return;
        //			}
        //			if (!this.model.name.Contains("LuoWenRen"))
        //			{
        //				this.model.SetColorTexDecal(this.oriMainTex, this.oriSpecTex, null, Color.white);
        //			}
        //			else
        //			{
        //				ModelChangeScript component = base.gameObject.GetComponent<ModelChangeScript>();
        //				if (component == null || component.curMode == ModelChangeScript.Mode.Original)
        //				{
        //					this.model.SetColorTexDecal(this.oriMainTex, this.oriSpecTex, null, Color.white);
        //				}
        //				else
        //				{
        //					GameObject gameObject = PlayersManager.FindMainChar(7, true);
        //					PalNPC component2 = gameObject.GetComponent<PalNPC>();
        //					component2.RestoreTex();
        //				}
        //			}
        //		}

        //		public IItem GetSlot(EquipSlotEnum position)
        //		{
        //			ItemWatcher itemWatcher;
        //			if (this.mEquipSlots.TryGetValue(position, out itemWatcher))
        //			{
        //				return itemWatcher.Target;
        //			}
        //			return null;
        //		}

        //		public void PutOnEquip(IItemAssemble<PalNPC> curEquip)
        //		{
        //			IItem item = curEquip as IItem;
        //			if (item == null)
        //			{
        //				return;
        //			}
        //			foreach (KeyValuePair<EquipSlotEnum, ItemWatcher> current in this.mEquipSlots)
        //			{
        //				if (current.Value.Target == item)
        //				{
        //					return;
        //				}
        //			}
        //			PalNPC.PutOffEquip_Internal(this, item);
        //			IItemMerger itemMerger = item as IItemMerger;
        //			if (itemMerger != null && itemMerger.Count > 1)
        //			{
        //				itemMerger.SetCountWithoutEvent(itemMerger.Count - 1);
        //				List<IItem> list = new List<IItem>();
        //				list.Add(item);
        //				item = item.ItemType.CloneWithoutEvent(item);
        //				itemMerger = (item as IItemMerger);
        //				curEquip = (item as IItemAssemble<PalNPC>);
        //				itemMerger.SetCountWithoutEvent(1);
        //				ItemManager.GetInstance().DoAfterCountChange(list.ToArray());
        //				ItemManager.GetInstance().DoOnAfterAddItem(item);
        //				if (item.OwnerPackage != null)
        //				{
        //					item.OwnerPackage.DoOnItemAdded(item);
        //				}
        //			}
        //			EquipSlotEnum equipSlotEnum = (EquipSlotEnum)(item.ItemType.TypeID >> 24);
        //			if (!Enum.IsDefined(typeof(EquipSlotEnum), equipSlotEnum))
        //			{
        //				ItemPackage ownerPackage = item.OwnerPackage;
        //				if (ownerPackage == null)
        //				{
        //					return;
        //				}
        //				ownerPackage.MergerItemType(item.ItemType.TypeID);
        //				return;
        //			}
        //			else
        //			{
        //				ItemWatcher itemWatcher;
        //				if (!this.mEquipSlots.TryGetValue(equipSlotEnum, out itemWatcher))
        //				{
        //					itemWatcher = new ItemWatcher();
        //					this.mEquipSlots.Add(equipSlotEnum, itemWatcher);
        //				}
        //				uint typeID = 0u;
        //				ItemPackage itemPackage = null;
        //				try
        //				{
        //					if (itemWatcher != null && itemWatcher.Target != null && itemWatcher.Target.ItemType != null)
        //					{
        //						typeID = itemWatcher.Target.ItemType.TypeID;
        //						itemPackage = itemWatcher.Target.OwnerPackage;
        //					}
        //				}
        //				catch (Exception exception)
        //				{
        //					UnityEngine.Debug.LogException(exception);
        //				}
        //				PalNPC.PutOffEquip_Internal(this, itemWatcher.Target);
        //				curEquip.Link(this);
        //				itemWatcher.SetTarget(item);
        //				if (itemPackage != null)
        //				{
        //					itemPackage.MergerItemType(typeID);
        //				}
        //				if (item.OwnerPackage != null)
        //				{
        //					item.OwnerPackage.MergerItemType(item.ItemType.TypeID);
        //				}
        //			}
        //		}

        //		private void LoadLink(IItemAssemble<PalNPC> curEquip)
        //		{
        //			IItem item = curEquip as IItem;
        //			if (item == null)
        //			{
        //				return;
        //			}
        //			EquipSlotEnum equipSlotEnum = (EquipSlotEnum)(item.ItemType.TypeID >> 24);
        //			if (!Enum.IsDefined(typeof(EquipSlotEnum), equipSlotEnum))
        //			{
        //				return;
        //			}
        //			ItemWatcher itemWatcher = new ItemWatcher();
        //			if (!this.mEquipSlots.ContainsKey(equipSlotEnum))
        //			{
        //				this.mEquipSlots.Add(equipSlotEnum, itemWatcher);
        //			}
        //			else
        //			{
        //				this.mEquipSlots[equipSlotEnum] = itemWatcher;
        //			}
        //			curEquip.Link(this);
        //			itemWatcher.SetTarget(item);
        //		}

        //		public static void PutOffEquip_Internal(PalNPC OldPalNPC, IItem SlotItem)
        //		{
        //			IItemAssemble<PalNPC> itemAssemble = SlotItem as IItemAssemble<PalNPC>;
        //			if (itemAssemble == null || itemAssemble.GetOwner() == null)
        //			{
        //				foreach (KeyValuePair<EquipSlotEnum, ItemWatcher> current in OldPalNPC.mEquipSlots)
        //				{
        //					if (current.Value.Target == SlotItem)
        //					{
        //						current.Value.SetTarget(null);
        //					}
        //				}
        //				return;
        //			}
        //			if (itemAssemble.GetOwner() != OldPalNPC)
        //			{
        //				foreach (KeyValuePair<EquipSlotEnum, ItemWatcher> current2 in OldPalNPC.mEquipSlots)
        //				{
        //					if (current2.Value.Target == SlotItem)
        //					{
        //						current2.Value.SetTarget(null);
        //					}
        //				}
        //				PalNPC.PutOffEquip_Internal(itemAssemble.GetOwner(), SlotItem);
        //				return;
        //			}
        //			foreach (KeyValuePair<EquipSlotEnum, ItemWatcher> current3 in OldPalNPC.mEquipSlots)
        //			{
        //				if (current3.Value.Target == SlotItem)
        //				{
        //					current3.Value.SetTarget(null);
        //				}
        //			}
        //			itemAssemble.UnLink();
        //		}

        //		public void PutOffEquip(EquipSlotEnum SlotPosition)
        //		{
        //			ItemWatcher itemWatcher;
        //			if (!this.mEquipSlots.TryGetValue(SlotPosition, out itemWatcher))
        //			{
        //				return;
        //			}
        //			this.PutOffEquip(itemWatcher.Target);
        //		}

        //		public void PutOffEquip(IItem SlotItem)
        //		{
        //			if (SlotItem == null)
        //			{
        //				return;
        //			}
        //			PalNPC.PutOffEquip_Internal(this, SlotItem);
        //			ItemPackage ownerPackage = SlotItem.OwnerPackage;
        //			if (ownerPackage == null)
        //			{
        //				return;
        //			}
        //			ownerPackage.MergerItemType(SlotItem.ItemType.TypeID);
        //		}

        //		public void RePutOnEquip(EquipSlotEnum SlotPosition)
        //		{
        //			ItemWatcher itemWatcher;
        //			if (!this.mEquipSlots.TryGetValue(SlotPosition, out itemWatcher))
        //			{
        //				return;
        //			}
        //			IItem target = itemWatcher.Target;
        //			if (target == null)
        //			{
        //				return;
        //			}
        //			PalNPC.PutOffEquip_Internal(this, target);
        //			this.PutOnEquip(target as IItemAssemble<PalNPC>);
        //		}

        //		public void ReBattleItemFun(int oldv)
        //		{
        //			if (this.Data.HPMPDP.HP > 0 || GameStateManager.CurGameState != GameState.Battle)
        //			{
        //				return;
        //			}
        //			SymbolPanelItem symbolPanelItem = this.GetSlot(EquipSlotEnum.SymbolPanel) as SymbolPanelItem;
        //			if (symbolPanelItem == null)
        //			{
        //				return;
        //			}
        //			SymbolNodeItem nodeByType = symbolPanelItem.GetNodeByType(ItemManager.GetID(32u, 58u));
        //			if (nodeByType == null)
        //			{
        //				return;
        //			}
        //			ItemManager.GetInstance().RemoveItem(nodeByType.ID);
        //			this.Data.HPMPDP.HP = this.Data.HPMPDP.HPRange;
        //			BattleScriptInterface.RevivePlayer(this.Data.CharacterID);
        //		}

        //		public bool IsCanEquipThisItem_ForLuoZhaoYan(IItem curOb)
        //		{
        //			FashionClothItem fashionClothItem = curOb as FashionClothItem;
        //			if (fashionClothItem == null)
        //			{
        //				return true;
        //			}
        //			uint characterMark = (fashionClothItem.ItemType as FashionClothItemType).CharacterMark;
        //			int num = 3;
        //			if (FlagManager.GetFlag(3) != 0)
        //			{
        //				num = 7;
        //			}
        //			int num2 = 1 << num;
        //			long num3 = (long)((ulong)characterMark & (ulong)((long)num2));
        //			return num3 != 0L;
        //		}

        public override void Awake()
        {
            if (this.m_bDontLoadModel)
            {
                return;
            }
            base.Awake();
            //if (base.gameObject.IsMonster())
            //{
            //    CharactersManager.AddMonster(this);
            //}
        }

        public override void Start()
        {
            //if (this.IsMainCharAndExist())
            //{
            //    UnityEngine.Object.DestroyImmediate(base.gameObject);
            //    return;
            //}

            if (this.IsDataInit)
            {
                return;
            }

            base.Start();
            if (this.Data != null)
            {
                this.Data.Owner = base.gameObject;
                this.Data.Reset();
            }

            if (this.model == null)
            {
                this.LoadModel();
            }

            //this.BuffDebuffData = new BuffDebuffManager.BuffDebuffOwner();
            //this.BuffDebuffData.Owner = this;
            this.patrol = base.GetComponent<Patrol>();
            this.IsDataInit = true;
        }

        //		private bool IsMainCharAndExist()
        //		{
        //			if (StartInit.IsFirstStart)
        //			{
        //				return false;
        //			}
        //			int num = 0;
        //			if (int.TryParse(base.name, out num) && num >= 0 && num <= 7)
        //			{
        //				for (int i = 0; i < PlayersManager.AllPlayers.Count; i++)
        //				{
        //					GameObject gameObject = PlayersManager.AllPlayers[i];
        //					if (!(gameObject == null))
        //					{
        //						if (gameObject.name == base.name && gameObject != base.gameObject)
        //						{
        //							return true;
        //						}
        //					}
        //				}
        //				if (num == 6 && SceneManager.GetActiveScene().buildIndex == 0)
        //				{
        //					UnityEngine.Debug.LogError(string.Format("[Error] : Try to load JiGuanXiong again in Title, don't load and exist.", new object[0]));
        //					return true;
        //				}
        //			}
        //			return false;
        //		}

        //		private void AddColliderForGoToBattle()
        //		{
        //			SphereCollider sphereCollider = this.model.GetComponent<SphereCollider>();
        //			if (sphereCollider == null)
        //			{
        //				sphereCollider = this.model.AddComponent<SphereCollider>();
        //				CharacterController component = this.model.GetComponent<CharacterController>();
        //				if (component != null)
        //				{
        //					sphereCollider.radius = component.radius * this.BattleColR;
        //				}
        //				else
        //				{
        //					sphereCollider.radius = 1f;
        //				}
        //				sphereCollider.center += new Vector3(0f, sphereCollider.radius + this.BattleColH, 0f);
        //			}
        //			sphereCollider.isTrigger = true;
        //		}

        public override void LoadModelEnd(UnityEngine.Object obj)
        {
            base.LoadModelEnd(obj);
            //if (this.MonsterGroups.Length == 0 && this.model != null && base.gameObject.GetComponent<Interact>() != null && this.model.GetComponent<MouseEnterCursor>() == null)
            //{
            //    //MouseEnterCursor mouseEnterCursor = this.model.AddComponent<MouseEnterCursor>();
            //    //mouseEnterCursor.curState = CursorTextureState.Talk;
            //}

            if (this.model != null)
            {
                //this.model.layer = SmoothFollow2.IgnoreLayer;
                this.model.ExcludeCloneName();
                Agent component = this.model.GetComponent<Agent>();
                if (component != null)
                {
                    component.palNPC = this;
                    if (component.agent != null)//&& this.MonsterGroups.Length < 1
                    {
                        UnityEngine.Debug.Log("执行");
                        component.agent.baseOffset = -0.1f;
                        //if (!NPCHeight.Instance.SetHeight(component.agent))
                        //{
                        //    component.gameObject.AddComponent<AdjustNavAgentOffset>();
                        //}
                    }
                }

                if (Application.isPlaying && this.Data.CharacterID > -1 && this.Data.CharacterID < 8)
                {
                    this.GetOriRes();
                }
                //this.Weapons.Clear();
                //Transform[] props = GameObjectPath.GetProps(this.model.transform);
                //for (int i = 0; i < props.Length; i++)
                //{
                //    Transform transform = props[i];
                //    if (!(transform == null))
                //    {
                //        if (transform.childCount < 1)
                //        {
                //            this.Weapons.Add(null);
                //        }
                //        else
                //        {
                //            this.Weapons.Add(transform.GetChild(0).gameObject);
                //        }
                //    }
                //}

                //if (this.Weapons.Count < 1 || (this.Weapons[0] == null && this.Weapons.Count > 1 && this.Weapons[1] == null))
                //{
                //    this.Weapons.Clear();
                //    Transform[] boneProps = GameObjectPath.GetBoneProps(this.model.transform);
                //    for (int j = 0; j < boneProps.Length; j++)
                //    {
                //        Transform transform2 = boneProps[j];
                //        if (!(transform2 == null))
                //        {
                //            if (transform2.childCount < 1)
                //            {
                //                this.Weapons.Add(null);
                //            }
                //            else
                //            {
                //                this.Weapons.Add(transform2.GetChild(0).gameObject);
                //            }
                //        }
                //    }
                //}

                //if (this.Data != null && this.Data.CharacterID < 8 && this.Data.CharacterID > -1)
                //{
                //    this.SetActiveWeaponInNormal(true);
                //}
                //else if (base.GetComponent<SaveTarget>() == null)
                //{
                //    this.InitDisCull();
                //}

                //AnimCtrlScript component2 = this.model.GetComponent<AnimCtrlScript>();
                //if (component2 != null)
                //{
                //    component2.Start();
                //}

                //if (!OptionConfig.NeedOpt || (!string.IsNullOrEmpty(this.modelResourcePath) && this.modelResourcePath.Contains("NpcP6")))
                //{
                //    ShroudInstance.Init(this.model);
                //}
                //else
                //{
                //    ShroudInstance component3 = this.model.GetComponent<ShroudInstance>();
                //    UnityEngine.Object.Destroy(component3);
                //}

                //UtilFun.SetActive(this.model, this.bEnableOnLoadModelEnd);
                //Vector3 position = this.model.transform.position;
                //position.y += 0.5f;
                //RaycastHit raycastHit;
                //if (Physics.Raycast(position, Vector3.down, out raycastHit, 200f, SmoothFollow2.MaskValue))
                //{
                //    UtilFun.SetPosition(this.model.transform, raycastHit.point);
                //}
                //int characterID = this.Data.CharacterID;
                //if (characterID > -1 && characterID < 8 && characterID != 6)
                //{
                //    this.footMark = this.model.AddComponent<Footmark>();
                //    string text = this.model.name;
                //    int num = text.IndexOf(' ');
                //    if (num > -1)
                //    {
                //        text = text.Substring(0, num + 1);
                //        this.model.name = text;
                //    }
                //}

                //if (((characterID < 8 && characterID != 6) || (characterID > 3999 && characterID < 4020) || this.MonsterGroups.Length > 0) && (characterID != 0 || this.modelResourcePath.Contains("YueJinChao")))
                //{
                //    Perception.ActivePerception(this);
                //}

                //if (characterID < 8 && characterID != 6 && SceneManager.GetActiveScene().buildIndex == 0)
                //{
                //    PlayersManager.AddPlayerPerceptionRange(this);
                //}

                //if (this.MonsterGroups.Length > 0 && this.model != null)
                //{
                //    this.AddColliderForGoToBattle();
                //    MonsterStateScript.SetState(this.model, MonsterStateScript.MonsterState.None);
                //}

                if (this.OnLoadModelEnd != null)
                {
                    this.OnLoadModelEnd(this);
                }
            }
        }

        public override void LoadModel()
        {
            //if (SkillSEPreloader.s_preloadEnable && ScenesManager.IsChanging && SkillSEPreloader.Instance != null)
            //{
            //    if (this.Data != null)
            //    {
            //        this.Data.Owner = base.gameObject;
            //        this.Data.Reset();
            //    }
            //    FightProperty fight = this.Data.Fight;
            //    if (fight != null)
            //    {
            //        int num = -1;
            //        int.TryParse(fight.BattleAIScript, out num);
            //        if (SkillSEPreloader.s_battleAISkillDic.ContainsKey(num))
            //        {
            //            List<int> list = SkillSEPreloader.s_battleAISkillDic[num];
            //            for (int i = 0; i < list.Count; i++)
            //            {
            //                System.Console.WriteLine(string.Format("[PreLoad Skill] : npc={0}, scriptID={1}, skillID={2}", base.gameObject.ToString(), num, list[i]));
            //                SkillSEPreloader.Instance.loadSkillSE(list[i]);
            //            }
            //        }
            //        SkillSEPreloader.Instance.m_preloadThisScene = true;
            //    }
            //    else if (this.MonsterGroups.Length != 0)
            //    {
            //        PalBattleManager palBattleManager = PalBattleManager.Instance();
            //        if (palBattleManager == null)
            //        {
            //            return;
            //        }
            //        MonsterGroupDataManager monsterGroupDataManager = palBattleManager.GetMonsterGroupDataManager();
            //        if (monsterGroupDataManager == null)
            //        {
            //            return;
            //        }
            //        int[] monsterGroups = this.MonsterGroups;
            //        for (int j = 0; j < monsterGroups.Length; j++)
            //        {
            //            int id = monsterGroups[j];
            //            MonsterGroupDataManager.MonsterGroupData data = monsterGroupDataManager.GetData(id);
            //            foreach (int current in data.mMonsters)
            //            {
            //                if (SkillSEPreloader.s_battleAISkillDic.ContainsKey(current))
            //                {
            //                    List<int> list2 = SkillSEPreloader.s_battleAISkillDic[current];
            //                    for (int k = 0; k < list2.Count; k++)
            //                    {
            //                        System.Console.WriteLine(string.Format("[PreLoad Skill] : npc={0}, scriptID={1}, skillID={2}", base.gameObject.ToString(), current, list2[k]));
            //                        SkillSEPreloader.Instance.loadSkillSE(list2[k]);
            //                    }
            //                }
            //            }
            //            SkillSEPreloader.Instance.m_preloadThisScene = true;
            //        }
            //    }
            //}

            if (this.m_bDontLoadModel)
            {
                this.LoadModelEnd(this);
                return;
            }

            Animator componentInChildren = base.GetComponentInChildren<Animator>();
            if (componentInChildren != null)
            {
                this.model = componentInChildren.gameObject;
            }

            if (this.model == null)
            {
                base.LoadModel();
            }
            else
            {
                this.LoadModelEnd(this);
            }
        }

        //		private void OnDrawGizmos()
        //		{
        //			if (this.model != null)
        //			{
        //				Gizmos.DrawIcon(this.model.transform.position, "npcicon", true);
        //			}
        //			else if (base.GetComponentInChildren<Animator>() != null)
        //			{
        //				Gizmos.DrawIcon(base.GetComponentInChildren<Animator>().transform.position, "npcicon", true);
        //			}
        //			else
        //			{
        //				Gizmos.DrawIcon(base.transform.position, "npcicon", true);
        //			}
        //		}

        //		public static Type GetComponentTypeByName(string name)
        //		{
        //			if (string.IsNullOrEmpty(name))
        //			{
        //				return null;
        //			}
        //			Type typeFromHandle = typeof(Component);
        //			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
        //			for (int i = 0; i < assemblies.Length; i++)
        //			{
        //				Assembly assembly = assemblies[i];
        //				Type[] types = assembly.GetTypes();
        //				for (int j = 0; j < types.Length; j++)
        //				{
        //					Type type = types[j];
        //					if (typeFromHandle.IsAssignableFrom(type) && type.Name == name)
        //					{
        //						return type;
        //					}
        //				}
        //			}
        //			return null;
        //		}

        //		public override void AddComponentByName(string componentName)
        //		{
        //			if (base.GetComponent(componentName) != null)
        //			{
        //				UnityEngine.Debug.LogWarning("AddComponentByName Exception, component " + componentName + " already exist.");
        //			}
        //			if (componentName != null)
        //			{
        //				if (PalNPC.<>f__switch$map17 == null)
        //				{
        //					PalNPC.<>f__switch$map17 = new Dictionary<string, int>(1)
        //					{
        //						{
        //							"Patrol",
        //							0
        //						}
        //					};
        //				}
        //				int num;
        //				if (PalNPC.<>f__switch$map17.TryGetValue(componentName, out num))
        //				{
        //					if (num != 0)
        //					{
        //					}
        //				}
        //			}
        //			Component x = base.gameObject.AddComponent(PalNPC.GetComponentTypeByName(componentName));
        //			if (x == null)
        //			{
        //				UnityEngine.Debug.LogWarning("AddComponentByName Exception, create " + componentName + " failed.");
        //			}
        //			if (componentName != null)
        //			{
        //				if (PalNPC.<>f__switch$map18 == null)
        //				{
        //					PalNPC.<>f__switch$map18 = new Dictionary<string, int>(1)
        //					{
        //						{
        //							"Patrol",
        //							0
        //						}
        //					};
        //				}
        //				int num;
        //				if (PalNPC.<>f__switch$map18.TryGetValue(componentName, out num))
        //				{
        //					if (num != 0)
        //					{
        //					}
        //				}
        //			}
        //		}

        //		public override void RemoveComponentByName(string componentName)
        //		{
        //			Component component = base.GetComponent(componentName);
        //			if (component == null)
        //			{
        //				UnityEngine.Debug.LogWarning("RemoveComponentByName Exception, cant find " + componentName + ".");
        //			}
        //			if (componentName != null)
        //			{
        //				if (PalNPC.<>f__switch$map19 == null)
        //				{
        //					PalNPC.<>f__switch$map19 = new Dictionary<string, int>(1)
        //					{
        //						{
        //							"Patrol",
        //							0
        //						}
        //					};
        //				}
        //				int num;
        //				if (PalNPC.<>f__switch$map19.TryGetValue(componentName, out num))
        //				{
        //					if (num != 0)
        //					{
        //					}
        //				}
        //			}
        //			UnityEngine.Object.DestroyImmediate(component);
        //			if (componentName != null)
        //			{
        //				if (PalNPC.<>f__switch$map1A == null)
        //				{
        //					PalNPC.<>f__switch$map1A = new Dictionary<string, int>(1)
        //					{
        //						{
        //							"Patrol",
        //							0
        //						}
        //					};
        //				}
        //				int num;
        //				if (PalNPC.<>f__switch$map1A.TryGetValue(componentName, out num))
        //				{
        //					if (num != 0)
        //					{
        //					}
        //				}
        //			}
        //		}

        //		public static PalNPC FindTheNPC(Transform transform)
        //		{
        //			Transform transform2 = transform;
        //			while (transform2 != null)
        //			{
        //				PalNPC component = transform2.GetComponent<PalNPC>();
        //				if (component != null)
        //				{
        //					return component;
        //				}
        //				transform2 = transform2.parent;
        //			}
        //			return null;
        //		}

        //		public static float GetRadius(GameObject npcObj)
        //		{
        //			float result = 3f;
        //			NavMeshAgent component = npcObj.GetComponent<NavMeshAgent>();
        //			if (component != null)
        //			{
        //				result = component.radius;
        //			}
        //			else
        //			{
        //				CharacterController component2 = npcObj.GetComponent<CharacterController>();
        //				if (component2 != null)
        //				{
        //					result = component2.radius;
        //				}
        //				else if (npcObj.GetComponent<Collider>() != null)
        //				{
        //					SphereCollider component3 = npcObj.GetComponent<SphereCollider>();
        //					if (component3 != null)
        //					{
        //						result = component3.radius;
        //					}
        //					else
        //					{
        //						CapsuleCollider component4 = npcObj.GetComponent<CapsuleCollider>();
        //						if (component4 != null)
        //						{
        //							result = component4.radius;
        //						}
        //						else
        //						{
        //							result = npcObj.GetComponent<Collider>().bounds.extents.magnitude;
        //						}
        //					}
        //				}
        //			}
        //			return result;
        //		}

        //		public Vector3 GetAimPos(GameObject npcObj)
        //		{
        //			float radius = PalNPC.GetRadius(npcObj);
        //			Vector3 vector = base.transform.position - npcObj.transform.position;
        //			vector.Normalize();
        //			switch (this.personalityType)
        //			{
        //			case NPCPersonalityType.subtle:
        //				vector = npcObj.transform.rotation * Vector3.back;
        //				break;
        //			}
        //			NavMeshAgent component = base.GetComponent<NavMeshAgent>();
        //			if (component == null)
        //			{
        //				UnityEngine.Debug.LogWarning(base.name + "没有NavMeshAgent");
        //				return base.transform.position;
        //			}
        //			vector *= radius + component.radius + this.CurFightDistance;
        //			return npcObj.transform.position + vector;
        //		}

        public void Save(BinaryWriter writer)
        {
            //this.Data.Save(writer);
            //this.BuffDebuffData.Save(writer);
            //List<uint> list = new List<uint>();
            //int count = this.mEquipSlots.Values.Count;
            //if (count < 1 && this.Data != null && this.Data.CharacterID > -1 && this.Data.CharacterID < 6)
            //{
            //    UnityEngine.Debug.LogError("警告：" + base.name + " 的 mEquipSlots.Values.Count==0");
            //}
            //UtilFun.ConsoleLog("Log : " + base.name + "  PalNPC.Save  mEquipSlots.Values.Count ==" + count.ToString(), false);
            //foreach (KeyValuePair<EquipSlotEnum, ItemWatcher> current in this.mEquipSlots)
            //{
            //    if (current.Value == null || current.Value.Target == null)
            //    {
            //        if (this.Data != null && this.Data.CharacterID > -1 && this.Data.CharacterID < 6)
            //        {
            //            UnityEngine.Debug.LogError(string.Concat(new string[]
            //            {
            //                        "警告：",
            //                        base.name,
            //                        " 的 ",
            //                        current.Key.ToString(),
            //                        " 的 ",
            //                        (current.Value != null) ? "cur.Value.Target == null" : "cur.Value == null"
            //            }));
            //        }
            //    }
            //    else
            //    {
            //        list.Add(current.Value.Target.ID);
            //    }
            //}
            //UtilFun.ConsoleLog("Log : " + base.name + " PalNPC.Save 装备链接数=" + list.Count.ToString(), false);
            //if (list.Count < 1 && this.Data != null && this.Data.CharacterID > -1 && this.Data.CharacterID < 6)
            //{
            //    UnityEngine.Debug.LogError("警告：" + base.name + " 的 装备链接数 = 0 ！！！！！！！！！！！！！！！！！！！！");
            //}
            //writer.Write(list.Count);
            //using (List<uint>.Enumerator enumerator2 = list.GetEnumerator())
            //{
            //    while (enumerator2.MoveNext())
            //    {
            //        int current2 = (int)enumerator2.Current;
            //        writer.Write(current2);
            //    }
            //}
            //writer.Write((int)this.mCurrentAchievement);
            //int count2 = this.m_SkillIDs.Count;
            //writer.Write(count2);
            //for (int i = 0; i < count2; i++)
            //{
            //    writer.Write(this.m_SkillIDs[i].m_ID);
            //    writer.Write(this.m_SkillIDs[i].m_bOpen);
            //    writer.Write(this.m_SkillIDs[i].m_CurrentExp);
            //}
            //writer.Write((int)this.m_curObjType);
        }

        public void Load(BinaryReader reader)
        {
            //this.Data.Load(reader);
            //this.ExpRate = 1f;
            //this.ImmunityAllBadBattleStates = 0;
            //this.FengYinAddRate = 0f;
            //this.MiaoShouAddRate = 0f;
            //this.BreakAddRate = 0f;
            //this.BuffDebuffData = new BuffDebuffManager.BuffDebuffOwner();
            //this.BuffDebuffData.Owner = this;
            //this.BuffDebuffData.Load(reader);
            //int num = reader.ReadInt32();
            //UtilFun.ConsoleLog("Log : " + base.name + " PalNPC.Load 装备链接数=" + num.ToString(), false);
            //if (!SaveManager.IsErZhouMu || SaveManager.inheritStruct.Item)
            //{
            //    this.mLoadEquipSlots = new uint[num];
            //    for (int i = 0; i < this.mLoadEquipSlots.Length; i++)
            //    {
            //        this.mLoadEquipSlots[i] = reader.ReadUInt32();
            //    }
            //}
            //else
            //{
            //    this.mLoadEquipSlots = new uint[0];
            //    for (int j = 0; j < num; j++)
            //    {
            //        reader.ReadUInt32();
            //    }
            //}
            //int num2 = reader.ReadInt32();
            //try
            //{
            //    if (num2 >= 0)
            //    {
            //        if (num2 < PalBattleManager.Instance().m_Achievement.m_Achievements.Length && PalBattleManager.Instance().m_Achievement.m_Achievements[num2] == null)
            //        {
            //            num2 = -1;
            //        }
            //    }
            //    else
            //    {
            //        num2 = -1;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    num2 = -1;
            //    UnityEngine.Debug.LogError(ex.ToString());
            //}
            //this.mCurrentAchievement = (AchievementManager.ACHIEVEMENT_INDEX)num2;
            //if (SaveManager.VersionNum >= 9u)
            //{
            //    int num3 = reader.ReadInt32();
            //    this.m_SkillIDs.Clear();
            //    for (int k = 0; k < num3; k++)
            //    {
            //        this.AddSkillNoRepeat(new PalNPC.SkillInfo
            //        {
            //            m_ID = reader.ReadInt32(),
            //            m_bOpen = reader.ReadBoolean(),
            //            m_CurrentExp = reader.ReadInt32()
            //        });
            //    }
            //}
            //this.m_curObjType = (ObjType)reader.ReadInt32();
            //if (this.Data.HPMPDP != null)
            //{
            //    this.Data.HPMPDP.SetWithoutEvents(this.Data.LoadHP, this.Data.LoadMP, this.Data.LoadDP);
            //}
        }

        //		[DebuggerHidden]
        //		public IEnumerator Prepare()
        //		{
        //			PalNPC.<Prepare>c__Iterator3C <Prepare>c__Iterator3C = new PalNPC.<Prepare>c__Iterator3C();
        //			<Prepare>c__Iterator3C.<>f__this = this;
        //			return <Prepare>c__Iterator3C;
        //		}

        //		public int GetSkillGroup()
        //		{
        //			return this.m_SkillGroup;
        //		}

        //		public void SetSkillGroup(int sg)
        //		{
        //			this.m_SkillGroup = sg;
        //		}

        //		public void SetDontLoadModel(bool b)
        //		{
        //			this.m_bDontLoadModel = b;
        //		}

        //		private void Update()
        //		{
        //			if (this.ProcessCore != null)
        //			{
        //				this.ProcessCore();
        //			}
        //		}

        //		private void InitDisCull()
        //		{
        //			this.modelTF = this.model.transform;
        //			this.ProcessCore = new Action(this.DisCull);
        //			this.curTime = UnityEngine.Random.Range(3f, 3f + PalNPC.interval);
        //			if (this.MonsterGroups.Length > 0)
        //			{
        //				this.isMonster = true;
        //			}
        //			else
        //			{
        //				this.isMonster = false;
        //			}
        //		}

        //		public void RemoveDisCull()
        //		{
        //			this.ProcessCore = null;
        //		}

        //		private void DisCull()
        //		{
        //			this.curTime -= Time.deltaTime;
        //			if (this.curTime > 0f)
        //			{
        //				return;
        //			}
        //			this.curTime = PalNPC.interval;
        //			if (GameStateManager.CurGameState != GameState.Normal)
        //			{
        //				return;
        //			}
        //			if (PalBattleManager.Instance() != null && !PalBattleManager.Instance().IsLoadingFinished)
        //			{
        //				UnityEngine.Debug.Log("Wait for battle loading");
        //				return;
        //			}
        //			if (this.modelTF == null)
        //			{
        //				return;
        //			}
        //			Vector3 position = this.modelTF.position;
        //			Vector3 vector = PalMain.MainCameraTF.position;
        //			float num = 0f;
        //			float num2 = DistanceCullManager.Instance.GetCameraDistanceValue();
        //			if (PalNPC.SevereCull)
        //			{
        //				num = num2 * PalNPC.CullCamOffsetRatio;
        //			}
        //			num2 = num2 + 2f - num;
        //			if (PalNPC.SevereCull)
        //			{
        //				Vector3 forward = PalMain.MainCameraTF.forward;
        //				forward.y = 0f;
        //				vector += forward.normalized * num;
        //			}
        //			Vector3 vector2 = position - vector;
        //			float magnitude = vector2.magnitude;
        //			if (magnitude > num2 || vector2.y < -20f || vector2.y > 20f)
        //			{
        //				if (this.model.activeSelf)
        //				{
        //					this.SetActiveModel(false);
        //				}
        //			}
        //			else if (!this.model.activeSelf)
        //			{
        //				this.SetActiveModel(true);
        //			}
        //		}

        //		private void SetActiveModel(bool bValue)
        //		{
        //			if (!bValue)
        //			{
        //				this.uScriptsName.Clear();
        //				Animator animator = this.animator;
        //				if (animator != null)
        //				{
        //					float @float = animator.GetFloat("Speed");
        //					bool @bool = animator.GetBool("Move");
        //					if (!@bool || @float <= 0f)
        //					{
        //						AnimatorClipInfo[] currentAnimatorClipInfo = animator.GetCurrentAnimatorClipInfo(0);
        //						if (currentAnimatorClipInfo.Length > 0)
        //						{
        //							AnimatorClipInfo animatorClipInfo = currentAnimatorClipInfo[0];
        //							this.curAnimName = animatorClipInfo.clip.name;
        //						}
        //					}
        //					else
        //					{
        //						this.curAnimName = null;
        //					}
        //				}
        //				this.model.SetActive(bValue);
        //			}
        //			else
        //			{
        //				if (this.isMonster)
        //				{
        //					ReSpawnMonster component = base.GetComponent<ReSpawnMonster>();
        //					if (component != null)
        //					{
        //						return;
        //					}
        //				}
        //				this.model.SetActive(bValue);
        //				if (!string.IsNullOrEmpty(this.curAnimName))
        //				{
        //					AnimCtrlScript component2 = this.model.GetComponent<AnimCtrlScript>();
        //					if (component2 != null)
        //					{
        //						component2.ActiveAnimCrossFade(this.curAnimName, false, 0f, true);
        //					}
        //					this.curAnimName = null;
        //				}
        //			}
        //			uScriptCode[] components = base.GetComponents<uScriptCode>();
        //			for (int i = 0; i < components.Length; i++)
        //			{
        //				uScriptCode uScriptCode = components[i];
        //				string name = uScriptCode.GetType().Name;
        //				if (bValue)
        //				{
        //					if (this.uScriptsName.Contains(name))
        //					{
        //						uScriptCode.enabled = true;
        //					}
        //				}
        //				else if (uScriptCode.enabled)
        //				{
        //					this.uScriptsName.Add(name);
        //					uScriptCode.enabled = false;
        //				}
        //			}
        //			FollowTarget component3 = base.GetComponent<FollowTarget>();
        //			if (component3 != null)
        //			{
        //				component3.enabled = bValue;
        //			}
        //		}
    }
}
