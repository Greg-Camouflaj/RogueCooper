using UnityEngine;
using System.Collections;

public class GenerateBoard : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		generateGrid();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	private static void generateGrid () {
		//This outer loop tracks our vertical position
		for (int j = -5; j < 5; j++ ) {
			//Create all our cubes in a horizontal line
			for (int i = -5; i < 5; i++ ) {
				GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
				//NOTE: The following function creates a NEW material on every call
				//TODO: Specifically create these materials before hand and reference them here
				//cube.renderer.sharedMaterial = ...
				cube.renderer.material.shader = Shader.Find("Self-Illumin/Diffuse");
				cube.renderer.material.SetColor("_Color", Color.red);
				cube.transform.position = new Vector3(0.5F + i + (i * 0.1F), 0.5F + j + (j * 0.1F), 0);
			}
		}
	}
	
	private static void changeColor () {
		
	}
}