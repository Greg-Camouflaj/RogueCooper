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
                GameObject newGameCubeObj = new GameObject();
                GameCube newGameCube = newGameCubeObj.AddComponent<GameCube>();
                newGameCube.Initialize(i, j);
            }
        }
    }
}
