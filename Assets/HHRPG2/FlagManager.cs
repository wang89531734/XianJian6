using SoftStar.Pal6;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

public class FlagManager
{
	public static Dictionary<int, int> flags = new Dictionary<int, int>();

	public static Action<int, int> OnSetFlag = null;

	public static Action<int, int> AfterSetFlag = null;

    public static event Action<int, int> OnSetFlagEveryT;

    public static void Initialize()
	{
		FlagManager.flags.Clear();
		FlagManager.InitFlags();
	}

	public static void InitFlags()
	{
		FlagManager.flags.Clear();
		FlagManager.flags.Add(1000, 0);
		for (int i = 1024; i < 1032; i++)
		{
			FlagManager.flags.Add(i, 0);
		}
		for (int j = 1032; j < 1040; j++)
		{
			FlagManager.flags.Add(j, 0);
		}
		for (int k = 1040; k < 1048; k++)
		{
			FlagManager.flags.Add(k, 0);
		}
		for (int l = 1048; l < 1064; l++)
		{
			FlagManager.flags.Add(l, 0);
		}
		FlagManager.flags.Add(1064, 0);
		//FlagManager.SetFlag(MissionManager.MainLineFlag, MissionManager.MainLineFlagInitValue, true);
		//FlagManager.SetFlag(MissionManager.BranchLineToggleFlag, MissionManager.BranchLineToggleFlagInitValue, true);
		FlagManager.InitMapFlag();
		FlagManager.Init2DCutsceneFlag();
        PlayerTeam.Instance.InitPlayerTeamFlags();
        FlagManager.SetFlag(4, 0, true);
		FlagManager.SetFlag(3, 0, true);
		FlagManager.SetFlag(6, 1, true);
		FlagManager.SetFlag(7, 0, true);
	}

	public static void InitMapFlag()
	{
		//MapData[] datasFromFile = MapData.GetDatasFromFile();
		//for (int i = 0; i < datasFromFile.Length; i++)
		//{
		//	if (datasFromFile[i] != null && datasFromFile[i].mapFlag > 0)
		//	{
		//		int value = datasFromFile[i].CanClickMidMapBtn | datasFromFile[i].CanShowMidMapBtn | datasFromFile[i].MiniMapFlagValue;
		//		PalMain.SetFlag(datasFromFile[i].mapFlag, value);
		//	}
		//}
	}

	public static void Init2DCutsceneFlag()
	{
		//Cutscene2DCondition[] datasFromFile = Cutscene2DCondition.GetDatasFromFile();
		//for (int i = 0; i < datasFromFile.Length; i++)
		//{
		//	if (datasFromFile[i] != null)
		//	{
		//		string s = datasFromFile[i].cutsceneName.Replace("csmh_", string.Empty);
		//		int num = 0;
		//		if (int.TryParse(s, out num))
		//		{
		//			FlagManager.SetFlag(Cutscene2DManager.Offset2DCS + num, 0, true);
		//		}
		//	}
		//}
	}

	public static void ClearFlag()
	{
		FlagManager.flags.Clear();
	}

	public static int GetFlag(int idx)
	{
		if (FlagManager.flags.ContainsKey(idx))
		{
			return FlagManager.flags[idx];
		}
		return -1;
	}

	public static void SetFlag(int idx, int flagValue, bool bEvent = true)
	{
		if (bEvent && FlagManager.OnSetFlag != null)
		{
			FlagManager.OnSetFlag(idx, flagValue);
		}
		if (FlagManager.OnSetFlagEveryT != null)
		{
			FlagManager.OnSetFlagEveryT(idx, flagValue);
		}
		if (FlagManager.flags.ContainsKey(idx))
		{
			FlagManager.flags[idx] = flagValue;
		}
		else
		{
			FlagManager.flags.Add(idx, flagValue);
		}
		if (bEvent && FlagManager.AfterSetFlag != null)
		{
			FlagManager.AfterSetFlag(idx, flagValue);
		}
	}

	public static bool GetBoolFlag(ulong idx)
	{
		uint key = (uint)(idx / 32uL);
		int num;
		return !FlagManager.flags.TryGetValue((int)key, out num) || (num & 1 << (int)((byte)(idx % 32uL))) != 0;
	}

	public static void SetBoolFlag(ulong idx, bool newValue)
	{
		uint key = (uint)(idx / 32uL);
		int num;
		if (!FlagManager.flags.TryGetValue((int)key, out num))
		{
			if (!newValue)
			{
				FlagManager.flags[(int)key] = ~(1 << (int)((byte)(idx % 32uL)));
			}
			return;
		}
		uint num2 = (uint)idx % 32u;
		if (newValue)
		{
			FlagManager.flags[(int)key] = (num | 1 << (int)((byte)num2));
		}
		else
		{
			FlagManager.flags[(int)key] = (num & ~(1 << (int)((byte)num2)));
		}
	}

	public static void Save(BinaryWriter writer)
	{
		writer.Write(FlagManager.flags.Count);
		foreach (KeyValuePair<int, int> current in FlagManager.flags)
		{
			writer.Write(current.Key);
			writer.Write(current.Value);
		}
	}

	public static void Load(BinaryReader reader)
	{
		FlagManager.flags.Clear();
		FlagManager.InitMapFlag();
		FlagManager.Init2DCutsceneFlag();
		int num = reader.ReadInt32();
		for (int i = 0; i < num; i++)
		{
			int idx = reader.ReadInt32();
			int flagValue = reader.ReadInt32();
			FlagManager.SetFlag(idx, flagValue, false);
		}
	}
}
