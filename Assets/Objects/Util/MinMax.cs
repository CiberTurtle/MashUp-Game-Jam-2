using System;
using UnityEngine;

[Serializable]
public struct MinMax
{
	public float min;
	public float max;

	public float random { get => UnityEngine.Random.Range(this.min, this.max); }

	public MinMax(float m_min, float m_max)
	{
		this.min = m_min;
		this.max = m_max;
	}

	public float Clamp(float value) => Mathf.Clamp(value, this.min, this.max);
	public bool IsInRange(float value) => value > min && value < max;
}