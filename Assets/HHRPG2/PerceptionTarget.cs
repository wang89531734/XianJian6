using SoftStar.Pal6;
using System;
using UnityEngine;

[AddComponentMenu("Perception/Target")]
public class PerceptionTarget : MonoBehaviour
{
	private Transform m_host;

	public Transform host
	{
		get
		{
			if (this.m_host == null)
			{
				this.InitHost();
			}
			return this.m_host;
		}
	}

	private void InitHost()
	{
		Transform transformByType = GameObjectPath.GetTransformByType<PalGameObjectBase>(base.transform);
		if (transformByType != null)
		{
			this.m_host = transformByType.transform;
		}
		else
		{
			this.m_host = GameObjectPath.GetTransformByType<Animator>(base.transform);
		}
		if (this.m_host == null)
		{
			this.m_host = base.transform;
		}
	}

	private void Start()
	{
		if (base.GetComponent<CharacterController>())
		{
			return;
		}
		BoxCollider component = base.GetComponent<BoxCollider>();
		if (component != null)
		{
			base.gameObject.layer = 17;
			GameObject gameObject = new GameObject("PerceptionTarget");
			gameObject.AddComponent<PerceptionTarget>();
			Transform transform = gameObject.transform;
			transform.parent = base.transform;
			transform.localPosition = new Vector3(0f, 0.2f, 0f);
			SphereCollider sphereCollider = gameObject.AddComponent<SphereCollider>();
			sphereCollider.radius = component.bounds.size.x / 3f;
			UnityEngine.Object.Destroy(this);
			return;
		}
		if (this.host != null)
		{
			Transform parent = base.transform.parent;
			if (parent != null)
			{
				Collider component2 = parent.GetComponent<Collider>();
				if (component2 != null)
				{
					parent.gameObject.layer = 17;
				}
			}
		}
		if (base.GetComponent<Rigidbody>() == null)
		{
			base.gameObject.AddComponent<Rigidbody>();
		}
		base.GetComponent<Rigidbody>().isKinematic = true;
		this.InitHost();
		if (this.host != null && this.host.GetComponent<PalNPC>() != null && base.GetComponent<Collider>())
		{
			base.GetComponent<Collider>().isTrigger = true;
		}
		base.gameObject.layer = 17;
	}

	private void OnEnable()
	{
		if (base.GetComponent<CharacterController>())
		{
			return;
		}
		if (this.m_host == null)
		{
			return;
		}
		Transform transformByType = GameObjectPath.GetTransformByType<PalGameObjectBase>(base.transform);
		if (transformByType == null)
		{
			transformByType = GameObjectPath.GetTransformByType<Animator>(base.transform);
		}
		if (transformByType != null && this.m_host != null && this.m_host != transformByType)
		{
			this.InitHost();
			Transform parent = base.transform.parent;
			if (parent != null)
			{
				Collider component = parent.GetComponent<Collider>();
				if (component != null)
				{
					parent.gameObject.layer = 17;
				}
			}
			if (this.m_host.GetComponent<PalNPC>() != null && base.GetComponent<Collider>())
			{
				base.GetComponent<Collider>().isTrigger = true;
			}
		}
	}
}
