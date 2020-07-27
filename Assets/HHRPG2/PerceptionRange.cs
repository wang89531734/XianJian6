using System;
using System.Collections.Generic;
using UnityEngine;

public class PerceptionRange : MonoBehaviour
{
	private Perception m_perception;

	public Perception perception
	{
		get
		{
			if (this.m_perception == null && base.transform.parent != null)
			{
				this.m_perception = base.transform.parent.GetComponent<Perception>();
			}
			return this.m_perception;
		}
		set
		{
			this.m_perception = value;
		}
	}

	private void Start()
	{
		Rigidbody orAddComponent = base.gameObject.GetOrAddComponent<Rigidbody>();
		orAddComponent.isKinematic = true;
		base.gameObject.layer = 2;
	}

	private void OnTriggerEnter(Collider other)
	{
		PerceptionTarget component = other.GetComponent<PerceptionTarget>();
		if (component == null || this.perception == null)
		{
			return;
		}
		Transform host = component.host;
		if (!this.perception.targetsCanBePercept.ContainsKey(host))
		{
			List<PerceptionTarget> list = new List<PerceptionTarget>();
			list.Add(component);
			this.perception.targetsCanBePercept.Add(host, list);
		}
		else
		{
			this.perception.targetsCanBePercept[component.host].Add(component);
		}
	}

	private void OnTriggerExit(Collider other)
	{
		PerceptionTarget component = other.GetComponent<PerceptionTarget>();
		if (component == null || this.perception == null)
		{
			return;
		}
		Transform host = component.host;
		if (this.perception.targetsCanBePercept.ContainsKey(host))
		{
			this.perception.targetsCanBePercept[host].Remove(component);
			if (this.perception.targetsCanBePercept[host].Count < 1)
			{
				this.perception.hostsCanBeListened.Remove(host);
				this.perception.hostsCanBeSeen.Remove(host);
				this.perception.targetsCanBePercept.Remove(host);
				if (this.perception.OnBeNotSeenEvent != null)
				{
					this.perception.OnBeNotSeenEvent(host);
				}
				this.perception.SendMessageToUScript(host, false);
			}
		}
	}
}
