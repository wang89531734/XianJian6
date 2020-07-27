using System;
using System.Collections.Generic;
using UnityEngine;

public class PalShake : MonoBehaviour
{
	public List<ShakeType> typeList = new List<ShakeType>();

	private ShakeType curShakeType;

	public object belong;

	private bool GonaDestroy;

	public Vector3 referPos = Vector3.zero;

	public float intensity = 1f;

	public Vector3 shakeAmount;

	public float shakeTime;

	public float shakeSpeed;

	private Vector3 lastRandomVector;

	private Vector3 offset;

	private Vector3 destOffset;

	private float lifeSpan;

	public bool permanent;

	private Transform m_tf;

	private int minSpaceTime;

	private int maxSpaceTime;

	private int minShakeTime;

	private int maxShakeTime;

	private float curSpaceTime;

	private bool isSpace;

	public float handheldSpeed = 1f;

	public Vector3 handheldDesPos = Vector3.zero;

	public Vector3 handheldCurPos = Vector3.zero;

	public Vector3 handheldIntensity = Vector3.one;

	private ShakeType CurShakeType
	{
		set
		{
			this.curShakeType = value;
			SmoothFollow2 component = base.GetComponent<SmoothFollow2>();
			if (component != null)
			{
				component.curShakeType = value;
				component.shakeScript = this;
			}
		}
	}

	private Transform m_transform
	{
		get
		{
			if (this.m_tf == null)
			{
				this.m_tf = base.transform;
			}
			return this.m_tf;
		}
	}

	public static PalShake StartShake(GameObject go, Vector3 amount, float speed, float time, bool permanent = false, float intensity = 1f)
	{
		PalShake palShake = go.GetComponent<PalShake>();
		if (palShake == null || palShake.GonaDestroy)
		{
			palShake = go.AddComponent<PalShake>();
		}
		palShake.CurShakeType = ShakeType.Shake;
		if (palShake.typeList.Contains(ShakeType.Shake))
		{
			palShake.typeList.Remove(ShakeType.Shake);
		}
		palShake.typeList.Add(ShakeType.Shake);
		palShake.StartShake(amount, speed, time, permanent, intensity);
		palShake.referPos = go.transform.position;
		return palShake;
	}

	public void StartShake(Vector3 amount, float speed, float time, bool permanent = false, float intensity = 1f)
	{
		this.shakeAmount = amount;
		this.shakeSpeed = speed * 2f;
		this.shakeTime = time;
		this.lastRandomVector = UnityEngine.Random.onUnitSphere;
		this.destOffset = new Vector3(0.1f * this.shakeAmount.x * this.lastRandomVector.x, 0.1f * this.shakeAmount.y * this.lastRandomVector.y, 0.1f * this.shakeAmount.z * this.lastRandomVector.z);
		this.offset = Vector3.zero;
		this.lifeSpan = this.shakeTime;
		this.permanent = permanent;
		this.intensity = intensity;
	}

	public static PalShake StartShake(GameObject go, Vector3 amount, float speed, int minSpaceTime, int maxSpaceTime, int minShakeTime, int maxShakeTime, float intensity = 1f)
	{
		PalShake palShake = go.GetComponent<PalShake>();
		if (palShake == null || palShake.GonaDestroy)
		{
			palShake = go.AddComponent<PalShake>();
		}
		palShake.CurShakeType = ShakeType.InterimShake;
		if (palShake.typeList.Contains(ShakeType.InterimShake))
		{
			palShake.typeList.Remove(ShakeType.InterimShake);
		}
		palShake.typeList.Add(ShakeType.InterimShake);
		palShake.StartShake(amount, speed, 0f, true, intensity);
		palShake.referPos = go.transform.position;
		palShake.minSpaceTime = minSpaceTime;
		palShake.maxSpaceTime = maxSpaceTime;
		palShake.minShakeTime = minShakeTime;
		palShake.maxShakeTime = maxShakeTime;
		palShake.curSpaceTime = (float)UnityEngine.Random.Range(minShakeTime, maxShakeTime);
		return palShake;
	}

	public static PalShake StartShake(GameObject go, Vector3 hhIntensity, float hhSpeed)
	{
		PalShake palShake = go.GetComponent<PalShake>();
		if (palShake == null || palShake.GonaDestroy)
		{
			palShake = go.AddComponent<PalShake>();
		}
		palShake.CurShakeType = ShakeType.Hand_held;
		if (palShake.typeList.Contains(ShakeType.Hand_held))
		{
			palShake.typeList.Remove(ShakeType.Hand_held);
		}
		palShake.typeList.Add(ShakeType.Hand_held);
		palShake.handheldIntensity = hhIntensity;
		palShake.handheldSpeed = hhSpeed;
		palShake.referPos = go.transform.position;
		palShake.permanent = true;
		palShake.lifeSpan = 0f;
		return palShake;
	}

	public void StopShake()
	{
		if (this.typeList.Contains(this.curShakeType))
		{
			this.typeList.Remove(this.curShakeType);
		}
		if (this.typeList.Count > 0)
		{
			this.CurShakeType = this.typeList[this.typeList.Count - 1];
			return;
		}
		this.CurShakeType = ShakeType.None;
		this.m_transform.position = this.referPos;
		this.GonaDestroy = true;
		UnityEngine.Object.DestroyImmediate(this);
	}

	private void LateUpdate()
	{
		if (this.permanent || this.lifeSpan > 0f)
		{
			switch (this.curShakeType)
			{
			case ShakeType.Shake:
				this.m_transform.position = this.Shake(this.referPos);
				break;
			case ShakeType.InterimShake:
				if (!this.isSpace)
				{
					this.m_transform.position = this.Shake(this.referPos);
				}
				else
				{
					this.m_transform.position = this.referPos;
				}
				this.curSpaceTime -= Time.deltaTime;
				if (this.curSpaceTime <= 0f)
				{
					this.isSpace = !this.isSpace;
					this.curSpaceTime = (float)((!this.isSpace) ? UnityEngine.Random.Range(this.minShakeTime, this.maxShakeTime) : UnityEngine.Random.Range(this.minSpaceTime, this.maxSpaceTime));
				}
				break;
			case ShakeType.Hand_held:
				this.m_transform.position = this.HandheldCamera(this.referPos);
				break;
			}
		}
		else
		{
			this.StopShake();
		}
		this.lifeSpan -= Time.deltaTime;
	}

	public Vector3 Shake(Vector3 pos)
	{
		if (this.offset == this.destOffset)
		{
			Vector3 a = UnityEngine.Random.insideUnitSphere;
			Vector3 vector = a - this.lastRandomVector;
			if (vector.sqrMagnitude < 0.25f)
			{
				a = this.lastRandomVector + vector.normalized * 0.25f;
			}
			this.lastRandomVector = a;
			this.destOffset = new Vector3(0.1f * this.shakeAmount.x * a.x, 0.1f * this.shakeAmount.y * a.y, 0.1f * this.shakeAmount.z * a.z);
		}
		Vector3 a2 = this.destOffset - this.offset;
		if (a2.sqrMagnitude < this.shakeSpeed * Time.deltaTime * Time.deltaTime)
		{
			this.offset = this.destOffset;
		}
		else
		{
			a2.Normalize();
			this.offset += a2 * this.shakeSpeed * Time.deltaTime;
		}
		pos += this.offset * this.intensity;
		return pos;
	}

	public Vector3 HandheldCamera(Vector3 pos)
	{
		float num = this.handheldSpeed * Time.deltaTime;
		if (Vector3.SqrMagnitude(this.handheldCurPos - this.handheldDesPos) < num * num)
		{
			this.handheldCurPos = this.handheldDesPos;
			this.handheldDesPos = UnityEngine.Random.insideUnitSphere + UnityEngine.Random.insideUnitSphere;
			this.handheldDesPos.Set(this.handheldDesPos.x * this.handheldIntensity.x, this.handheldDesPos.y * this.handheldIntensity.y, this.handheldDesPos.z * this.handheldIntensity.z);
			this.handheldDesPos = base.transform.rotation * this.handheldDesPos;
		}
		else
		{
			this.handheldCurPos += Vector3.Normalize(this.handheldDesPos - this.handheldCurPos) * num;
		}
		return pos + this.handheldCurPos;
	}
}
