using System;
using UnityEngine;

public static class GameObjectPath
{
	public static string GetPath(Transform tf)
	{
		if (tf == null)
		{
			return string.Empty;
		}
		string text = tf.name;
		Transform parent = tf.transform.parent;
		while (parent != null)
		{
			text = parent.name + "/" + text;
			parent = parent.parent;
		}
		return "/" + text;
	}

	public static Component FindSpecParent(Transform tf, Type type)
	{
		if (tf == null)
		{
			return null;
		}
		Transform parent = tf.parent;
		if (parent == null)
		{
			return null;
		}
		while (parent != null)
		{
			Component component = parent.GetComponent(type);
			if (component != null)
			{
				return component;
			}
			parent = parent.parent;
		}
		return null;
	}

	public static T FindSpecParent<T>(Transform tf) where T : Component
	{
		if (tf == null)
		{
			return (T)((object)null);
		}
		Transform transform = tf;
		if (transform == null)
		{
			return (T)((object)null);
		}
		while (transform != null)
		{
			T component = transform.GetComponent<T>();
			if (component != null)
			{
				return component;
			}
			transform = transform.parent;
		}
		return (T)((object)null);
	}

	public static bool IsTheParent(Transform tf, Transform Parent)
	{
		if (tf == null)
		{
			return false;
		}
		Transform parent = tf.transform.parent;
		if (parent == Parent)
		{
			return true;
		}
		while (parent != null)
		{
			if (parent == Parent)
			{
				return true;
			}
			parent = parent.parent;
		}
		return false;
	}

	public static Transform GetTransformByType<T>(Transform transf)
	{
		Transform transform = transf;
		while (transform != null)
		{
			Component component = transform.GetComponent(typeof(T));
			if (component != null)
			{
				return component.transform;
			}
			transform = transform.parent;
		}
		return null;
	}

	public static T GetComponentByType<T>(Transform transf) where T : Component
	{
		T result = (T)((object)null);
		Transform transform = transf;
		while (transform != null)
		{
			T component = transform.GetComponent<T>();
			if (component != null)
			{
				result = component;
				break;
			}
			transform = transform.parent;
		}
		return result;
	}

	public static Transform[] GetEyeObjs(Transform transf)
	{
		Transform transform = transf.Find("Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Spine1/Bip001 Neck/Bip001 Head/Bone_L_eyes");
		Transform transform2 = transf.Find("Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Spine1/Bip001 Neck/Bip001 Head/Bone_R_eyes");
		if (transform == null || transform2 == null)
		{
			transform = transf.Find("Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Spine1/Bip001 Neck/Bip001 Head/Bone_LeftEyes");
			transform2 = transf.Find("Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Spine1/Bip001 Neck/Bip001 Head/Bone_RightEyes");
		}
		if (transform == null || transform2 == null)
		{
			Transform[] componentsInChildren = transf.GetComponentsInChildren<Transform>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform3 = componentsInChildren[i];
				string text = transform3.name.ToLower();
				if (text.Contains("eyes"))
				{
					if (text.Contains("l"))
					{
						transform = transform3;
					}
					else if (text.Contains("r"))
					{
						transform2 = transform3;
					}
				}
				if (transform && transform2)
				{
					break;
				}
			}
		}
		return new Transform[]
		{
			transform,
			transform2
		};
	}

	public static Vector3 GetFirstPersonCamera(Transform[] eyes)
	{
		if (eyes == null || eyes.Length <= 0)
		{
			return Vector3.zero;
		}
		Vector3 a = Vector3.zero;
		int num = 0;
		for (int i = 0; i < eyes.Length; i++)
		{
			Transform transform = eyes[i];
			if (transform != null)
			{
				num++;
				a += transform.localPosition;
			}
		}
		if (num <= 0)
		{
			return Vector3.zero;
		}
		return a / (float)eyes.Length;
	}

	public static Transform[] GetHandsObj(Transform transf)
	{
		Transform transform = transf.Find("Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Spine1/Bip001 L Clavicle/Bip001 L UpperArm/Bip001 L Forearm/Bip001 L Hand");
		if (transform == null)
		{
			transform = transf.Find("Bip001/Bip001 Spine/Bip001 Spine1/Bip001 L Clavicle/Bip001 L UpperArm/Bip001 L Forearm/Bip001 L Hand");
		}
		Transform transform2 = transf.Find("Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Spine1/Bip001 R Clavicle/Bip001 R UpperArm/Bip001 R Forearm/Bip001 R Hand");
		if (transform2 == null)
		{
			transform2 = transf.Find("Bip001/Bip001 Spine/Bip001 Spine1/Bip001 R Clavicle/Bip001 R UpperArm/Bip001 R Forearm/Bip001 R Hand");
		}
		if (transform == null || transform2 == null)
		{
			Transform[] componentsInChildren = transf.GetComponentsInChildren<Transform>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Transform transform3 = componentsInChildren[i];
				if (transform3.name.ToLower().Contains("lefthand") || transform3.name.ToLower().Contains("l hand"))
				{
					transform = transform3;
				}
				else if (transform3.name.ToLower().Contains("righthand") || transform3.name.ToLower().Contains("r hand"))
				{
					transform2 = transform3;
				}
				if (transform && transform2)
				{
					break;
				}
			}
		}
		return new Transform[]
		{
			transform,
			transform2
		};
	}

	public static Transform GetBackObj(Transform transf)
	{
		Transform transform = transf.Find("Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Spine1");
		if (transform == null)
		{
			transform = transf.Find("Bip001/Bip001 Spine/Bip001 Spine1");
		}
		return transform;
	}

	public static Transform[] GetTakePoint(Transform transf)
	{
		Transform[] handsObj = GameObjectPath.GetHandsObj(transf);
		Transform transform = (!(handsObj[0] == null)) ? handsObj[0].FindChild("[@b]") : null;
		Transform transform2 = (!(handsObj[1] == null)) ? handsObj[1].FindChild("[@a]") : null;
		return new Transform[]
		{
			transform,
			transform2
		};
	}

	public static Transform[] GetProps(Transform transf)
	{
		Transform[] result;
		if (!transf.name.ToLower().Contains("jiguan"))
		{
			Transform[] handsObj = GameObjectPath.GetHandsObj(transf);
			Transform transform = (!(handsObj[0] == null)) ? handsObj[0].FindChild("Bip001 Prop2") : null;
			Transform transform2 = (!(handsObj[1] == null)) ? handsObj[1].FindChild("Bip001 Prop1") : null;
			if (transform == null && handsObj[0] != null)
			{
				transform = handsObj[0].FindChild("[@b]");
			}
			result = new Transform[]
			{
				transform,
				transform2
			};
		}
		else
		{
			Transform transform3 = transf.Find("Bip001/Bip001 Pelvis/Bip001 Spine/Bone_Prop");
			result = new Transform[]
			{
				null,
				transform3
			};
		}
		return result;
	}

	public static Transform[] GetBoneProps(Transform transf)
	{
		Transform transform = transf.Find("Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Spine1/Bone_Prop2/[@wuqi2]");
		Transform transform2 = transf.Find("Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Spine1/Bone_Prop1/[@wuqi1]");
		Transform[] array = null;
		if (transform != null || transform2 != null)
		{
			array = new Transform[]
			{
				transform,
				transform2
			};
		}
		else
		{
			Transform transform3 = transf.Find("Bip001/Bip001 Pelvis/Bip001 Spine/Bone_Prop");
			if (transform3 == null)
			{
				transform3 = transf.Find("Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Spine1/Bone_WuQiGuaZai/[@wuqi1]");
			}
			if (transform3 != null)
			{
				array = new Transform[]
				{
					transform3
				};
			}
		}
		if (array == null)
		{
			array = GameObjectPath.GetProps(transf);
		}
		return array;
	}

	public static Transform GetOrnamentProp(Transform transf)
	{
		if (transf.name.Contains("JuShiFang"))
		{
			Transform transform = transf.Find("Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Spine1/[@wuqi1]");
			if (transform != null)
			{
				if (transform.childCount < 1)
				{
					Debug.LogError(transform.name + " 没有子对象");
					return null;
				}
				transform = transform.GetChild(0);
				if (transform != null && transform.gameObject.activeSelf)
				{
					transform = transform.FindChild("[@zhuangshi2]");
					if (transform != null)
					{
						return transform;
					}
				}
			}
		}
		Transform transform2 = null;
		if (transform2 == null)
		{
			transform2 = transf.Find("Bip001/Bip001 Pelvis/Bip001 Spine/[@zhuangshi]");
			if (transform2 == null)
			{
				transform2 = transf.Find("Bip001/Bip001 Pelvis/[@zhuangshi]");
				if (transform2 == null)
				{
					transform2 = transf.Find("Bip001/Bip001 Spine/[@zhuangshi]");
					if (transform2 == null)
					{
						transform2 = transf.Find("Bip001/Bip001 Spine/[@zhuangshi]");
						if (transform2 == null)
						{
							transform2 = transf.Find("[@zhuangshi]");
						}
					}
				}
			}
		}
		return transform2;
	}

	public static Transform GetHeadObj(Transform transf)
	{
		Transform transform = transf.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Spine2/Bip01 Neck/Bip01 Head");
		if (transform != null)
		{
			return transform;
		}
		transform = transf.Find("U/joint_Char/joint_Pelvis/joint_TorsoA/joint_TorsoB/joint_TorsoC/joint_Neck/joint_Head");
		if (transform != null)
		{
			return transform;
		}
		transform = transf.Find("RootJoint01/ChestJoint01/HeadJoint01");
		if (transform != null)
		{
			return transform;
		}
		transform = transf.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 Head");
		if (transform != null)
		{
			return transform;
		}
		transform = transf.Find("Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Spine1/Bip001 Spine2/Bip001 Spine3/Bip001 Neck/Bip001 Head");
		if (transform != null)
		{
			return transform;
		}
		transform = transf.Find("Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Spine1/Bip001 Neck/Bip001 Head");
		if (transform != null)
		{
			return transform;
		}
		transform = transf.Find("char_astrella_reference/char_astrella_Hips1/char_astrella_Spine/char_astrella_Spine1/char_astrella_Spine2/char_astrella_Neck/char_astrella_Head");
		if (transform != null)
		{
			return transform;
		}
		Transform[] componentsInChildren = transf.GetComponentsInChildren<Transform>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Transform transform2 = componentsInChildren[i];
			if (transform2.name.ToLower().Contains("head"))
			{
				return transform2;
			}
		}
		Transform[] componentsInChildren2 = transf.GetComponentsInChildren<Transform>();
		for (int j = 0; j < componentsInChildren2.Length; j++)
		{
			Transform transform3 = componentsInChildren2[j];
			if (transform3.name.ToLower().Contains("@n"))
			{
				return transform3;
			}
		}
		return null;
	}

	public static Transform GetHeadProp(Transform transf)
	{
		Transform headObj = GameObjectPath.GetHeadObj(transf);
		return (!(headObj == null)) ? headObj.Find("[@h]") : null;
	}

	public static Transform GetChild(Transform transf, string childName)
	{
		Transform result = null;
		Transform[] componentsInChildren = transf.GetComponentsInChildren<Transform>();
		Transform[] array = componentsInChildren;
		for (int i = 0; i < array.Length; i++)
		{
			Transform transform = array[i];
			if (transform.name == childName)
			{
				result = transform;
				break;
			}
		}
		return result;
	}

	public static string GetResourcePath(string old)
	{
		if (!old.Contains("Assets/Resources/"))
		{
			return old;
		}
		string[] separator = new string[]
		{
			"Assets/Resources/"
		};
		string[] array = old.Split(separator, StringSplitOptions.None);
		if (array.Length != 2)
		{
			return null;
		}
		return array[1].Split(new char[]
		{
			'.'
		})[0];
	}

	public static void ExcludeCloneName(this GameObject go)
	{
		if (go == null)
		{
			Debug.LogError("Error : ExcludeCloneName go==null");
			return;
		}
		string name = go.name;
		int num = name.IndexOf("(Clone)");
		if (num < 0)
		{
			return;
		}
		go.name = name.Substring(0, num);
	}

	public static string ExcludeCloneName(this string goName)
	{
		int num = goName.IndexOf("(Clone)");
		if (num < 0)
		{
			return goName;
		}
		return goName.Substring(0, num);
	}

	public static Transform SearchSubNode(Transform target, string name)
	{
		if (target.name == name)
		{
			return target;
		}
		for (int i = 0; i < target.childCount; i++)
		{
			Transform transform = GameObjectPath.SearchSubNode(target.GetChild(i), name);
			if (transform != null)
			{
				return transform;
			}
		}
		return null;
	}
}
