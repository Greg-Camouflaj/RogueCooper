using UnityEngine;
using System.Collections;

public class GameCube : MonoBehaviour
{
    GameObject _visualCube = null;

	private void Start()
	{
        _visualCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        _visualCube.transform.parent = this.transform;
	}
}
