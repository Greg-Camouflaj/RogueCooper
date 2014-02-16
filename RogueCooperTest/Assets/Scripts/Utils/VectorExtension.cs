using UnityEngine;
using System.Collections;

static public class VectorExtension
{
	static public Vector2 ToVector2(this Vector3 v3)
	{
		return new Vector2(v3.x, v3.y);
	}
}
