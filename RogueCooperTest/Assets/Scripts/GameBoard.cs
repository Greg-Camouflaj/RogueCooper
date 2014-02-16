using UnityEngine;
using System.Collections;

public class GameBoard : MonoBehaviour
{
    public const int GAME_BOARD_DIMENSION = 12;

    public void GenerateGameBoard()
    {
        int halfDimension = GAME_BOARD_DIMENSION / 2;
        int startingIndex = -1 * halfDimension;
        int endingIndex = halfDimension;

        //This outer loop tracks our vertical position
        for (int j = startingIndex; j < endingIndex; j++)
        {
            //Create all our cubes in a horizontal line
            for (int i = startingIndex; i < endingIndex; i++)
            {
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
}
