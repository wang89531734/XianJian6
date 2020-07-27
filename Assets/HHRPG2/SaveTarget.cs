using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

public class SaveTarget : MonoBehaviour
{
	[SerializeField]
	private bool m_TransDataSkip;

	public virtual bool Save(BinaryWriter writer)
	{
		string path = GameObjectPath.GetPath(base.transform);
		writer.Write(path);
		this.SaveTranData(writer);
		this.SaveTargetData(writer);
		return true;
	}

	public virtual bool Load(BinaryReader reader)
	{
		this.LoadTranData(reader);
		this.LoadTargetData(reader);
		return true;
	}

	public static bool LOAD(BinaryReader reader)
	{
		string text = reader.ReadString();
		GameObject gameObject = GameObject.Find(text);
		if (gameObject == null)
		{
			int length = text.IndexOf("/", 1);
			string name = text.Substring(0, length);
			GameObject gameObject2 = GameObject.Find(name);
			if (gameObject2 != null)
			{
				int startIndex = text.LastIndexOf("/") + 1;
				string b = text.Substring(startIndex);
				SaveTarget[] componentsInChildren = gameObject2.GetComponentsInChildren<SaveTarget>(true);
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					SaveTarget saveTarget = componentsInChildren[i];
					if (saveTarget.name == b)
					{
						gameObject = saveTarget.gameObject;
						break;
					}
				}
			}
		}
		if (gameObject == null)
		{
			return false;
		}
		SaveTarget saveTarget2 = gameObject.GetComponent<SaveTarget>();
		if (saveTarget2 == null)
		{
			saveTarget2 = gameObject.AddComponent<SaveTarget>();
		}
		saveTarget2.Load(reader);
		return true;
	}

	protected virtual void SaveTranData(BinaryWriter writer)
	{
		Transform transform = base.gameObject.GetModelObj(false).transform;
		try
		{
			writer.Write(transform.position.x);
			writer.Write(transform.position.y);
			writer.Write(transform.position.z);
			writer.Write(transform.rotation.w);
			writer.Write(transform.rotation.x);
			writer.Write(transform.rotation.y);
			writer.Write(transform.rotation.z);
			writer.Write(transform.gameObject.activeSelf);
		}
		catch (Exception ex)
		{
			string path = GameObjectPath.GetPath(base.transform);
			Debug.LogError("Error : " + path + " " + ex.ToString());
		}
	}

	protected virtual void LoadTranData(BinaryReader reader)
	{
		bool flag = base.gameObject.name == "XianJing01" || base.gameObject.name == "XianJing02" || base.gameObject.name == "XianJing03" || this.m_TransDataSkip;
		Transform transform = base.gameObject.GetModelObj(false).transform;
		Vector3 position;
		position.x = reader.ReadSingle();
		position.y = reader.ReadSingle();
		position.z = reader.ReadSingle();
		if (!flag)
		{
			transform.position = position;
		}
		Quaternion rotation;
		rotation.w = reader.ReadSingle();
		rotation.x = reader.ReadSingle();
		rotation.y = reader.ReadSingle();
		rotation.z = reader.ReadSingle();
		if (!flag)
		{
			transform.rotation = rotation;
		}
		bool bActive = reader.ReadBoolean();
		transform.SetActive(bActive);
	}

	protected virtual void SaveTargetData(BinaryWriter writer)
	{
		Dictionary<Type, List<ISaveInterface>> dictionary = new Dictionary<Type, List<ISaveInterface>>();
		Component[] components = base.gameObject.GetComponents<Component>();
		for (int i = 0; i < components.Length; i++)
		{
			Component component = components[i];
			ISaveInterface saveInterface = component as ISaveInterface;
			if (saveInterface != null)
			{
				Type type = component.GetType();
				if (!dictionary.ContainsKey(type))
				{
					dictionary.Add(type, new List<ISaveInterface>
					{
						saveInterface
					});
				}
				else
				{
					dictionary[type].Add(saveInterface);
				}
			}
		}
		writer.Write(dictionary.Keys.Count);
		foreach (KeyValuePair<Type, List<ISaveInterface>> current in dictionary)
		{
			writer.Write(current.Key.FullName);
			List<ISaveInterface> value = current.Value;
			writer.Write(value.Count);
			for (int j = 0; j < value.Count; j++)
			{
				ISaveInterface saveInterface2 = value[j];
				saveInterface2.Save(writer);
			}
		}
	}

	protected virtual void LoadTargetData(BinaryReader reader)
	{
		int num = reader.ReadInt32();
		GameObject gameObject = base.gameObject;
		object[] args = new object[]
		{
			reader
		};
		for (int i = 0; i < num; i++)
		{
			string typeName = reader.ReadString();
			Type type = Type.GetType(typeName);
			int num2 = reader.ReadInt32();
			Component[] components = base.GetComponents(type);
			for (int j = 0; j < components.Length; j++)
			{
				Component target = components[j];
				type.InvokeMember("Load", BindingFlags.Public | BindingFlags.InvokeMethod, null, target, args);
			}
			for (int k = 0; k < num2 - components.Length; k++)
			{
				Component target2 = gameObject.AddComponent(type);
				type.InvokeMember("Load", BindingFlags.Public | BindingFlags.InvokeMethod, null, target2, args);
			}
		}
	}
}
