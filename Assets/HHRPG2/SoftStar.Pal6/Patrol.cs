using System;
using UnityEngine;

namespace SoftStar.Pal6
{
	public class Patrol : MonoBehaviour
	{
		//private PalPatrolRoute route;

		//[SerializeField]
		//private string routePath;

		//private PalNavAgent agent;

		//private float timer;

		//public int currentRoutePointIdx;

		//public PalPatrolRoute Route
		//{
		//	get
		//	{
		//		if (this.route == null)
		//		{
		//			GameObject gameObject = PalUtility.FindGameObjectByPath(this.routePath);
		//			if (gameObject != null)
		//			{
		//				this.route = gameObject.GetComponent<PalPatrolRoute>();
		//			}
		//		}
		//		return this.route;
		//	}
		//	set
		//	{
		//		if (value != this.route)
		//		{
		//			this.route = value;
		//			if (this.route != null)
		//			{
		//				this.routePath = PalUtility.GetPathInScene(this.route.gameObject);
		//			}
		//			else
		//			{
		//				this.routePath = string.Empty;
		//			}
		//		}
		//	}
		//}

		//private void Start()
		//{
		//	this.agent = base.GetComponent<PalNavAgent>();
		//	if (this.agent != null && this.route != null)
		//	{
		//		if (this.currentRoutePointIdx < 0 || this.currentRoutePointIdx > this.Route.Count - 1)
		//		{
		//			Debug.LogWarning("Patrol Exception, start route point index out of range.");
		//			return;
		//		}
		//		this.agent.SetDestination(this.route.positions[this.currentRoutePointIdx]);
		//	}
		//}

		//private void Update()
		//{
		//	if (this.agent == null || this.Route == null)
		//	{
		//		return;
		//	}
		//	if (this.currentRoutePointIdx == -1)
		//	{
		//		return;
		//	}
		//	if (this.agent.finished)
		//	{
		//		if (this.timer == 0f && this.route.stayTime[this.currentRoutePointIdx] != 0f && this.route.motions[this.currentRoutePointIdx] != string.Empty)
		//		{
		//			base.GetComponent<AnimCtrlScript>().ActiveAnim(this.route.motions[this.currentRoutePointIdx], false, false, false);
		//		}
		//		if (this.timer >= this.route.stayTime[this.currentRoutePointIdx])
		//		{
		//			if (this.route.stayTime[this.currentRoutePointIdx] != 0f)
		//			{
		//				base.GetComponent<AnimCtrlScript>().ActiveAnim("ZhanLi", false, false, false);
		//			}
		//			int randomNextPoint = this.Route.GetRandomNextPoint(this.currentRoutePointIdx);
		//			if (randomNextPoint != -1)
		//			{
		//				this.agent.SetDestination(this.Route.positions[randomNextPoint]);
		//			}
		//			this.currentRoutePointIdx = randomNextPoint;
		//			this.timer = 0f;
		//		}
		//		else
		//		{
		//			this.timer += Time.deltaTime;
		//		}
		//	}
		//}
	}
}
