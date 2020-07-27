using SoftStar.Pal6;
using System;
using System.IO;
using UnityEngine;

public class SavePrefabTarget : SaveTarget
{
	public string PrefabPath;

	public int ID
	{
		get
		{
			PalNPC component = base.GetComponent<PalNPC>();
			if (component != null)
			{
				return component.Data.CharacterID;
			}
			return -1;
		}
	}

	private void Start()
	{
		this.GetPrefabPath();
	}

	private string GetPrefabPath()
	{
		if (string.IsNullOrEmpty(this.PrefabPath))
		{
			string text = base.gameObject.name;
			if (text.Contains("(Clone)"))
			{
				int length = text.IndexOf("(Clone)");
				text = text.Substring(0, length);
			}
			this.PrefabPath = "Template/Character/" + text;
		}
		return this.PrefabPath;
	}

	public override bool Save(BinaryWriter writer)
	{
		writer.Write(this.ID);
		writer.Write(this.GetPrefabPath());
		this.SaveTranData(writer);
		this.SaveTargetData(writer);
		return true;
	}

	public static GameObject Load(BinaryReader reader, Transform parent)
	{
		int num = reader.ReadInt32();
		string prefabPath = reader.ReadString();
		GameObject gameObject = null;
		if (num >= 0)
		{
			gameObject = PlayersManager.FindMainChar(num, true);
		}
		if (gameObject == null)
		{
			Debug.LogError("PlayersManager.FindMainChar not found, tempID = " + num);
		}
		SavePrefabTarget savePrefabTarget = gameObject.GetComponent<SavePrefabTarget>();
		if (savePrefabTarget == null)
		{
			savePrefabTarget = gameObject.AddComponent<SavePrefabTarget>();
		}
		savePrefabTarget.PrefabPath = prefabPath;
		gameObject.transform.parent = parent;
		savePrefabTarget.Load(reader);
		return gameObject;
	}
}
