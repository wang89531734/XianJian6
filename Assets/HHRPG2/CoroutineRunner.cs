using System;
using UnityEngine;

public class CoroutineRunner : MonoBehaviour
{
	private static CoroutineRunner s_instance;

	public static CoroutineRunner Instance
	{
		get
		{
			if (CoroutineRunner.s_instance == null)
			{
				GameObject gameObject = new GameObject("CoroutineRunner", new Type[]
				{
					typeof(CoroutineRunner)
				});
				CoroutineRunner.s_instance = gameObject.GetComponent<CoroutineRunner>();
			}
			return CoroutineRunner.s_instance;
		}
	}

	private void Awake()
	{
		CoroutineRunner.s_instance = this;
	}

	private void OnDestroy()
	{
		CoroutineRunner.s_instance = null;
	}
}
