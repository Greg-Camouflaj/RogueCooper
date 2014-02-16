using UnityEngine;
using System.Collections;
using System.Collections.Generic;   // for List.

public class GameBoard : MonoBehaviour
{
    public const int GAME_BOARD_DIMENSION = 12;

    private List<GameCube> _gameCubes = null;

    public void GenerateGameBoard()
    {
        _gameCubes = new List<GameCube>(GAME_BOARD_DIMENSION * GAME_BOARD_DIMENSION);

        int halfDimension = GAME_BOARD_DIMENSION / 2;
        int startingIndex = -1 * halfDimension;
        int endingIndex = halfDimension;

        //This outer loop tracks our vertical position
        for (int y = startingIndex; y < endingIndex; y++)
        {
            //Create all our cubes in a horizontal line
            for (int x = startingIndex; x < endingIndex; x++)
            {
                GameObject newGameCubeObj = new GameObject();
                GameCube newGameCube = newGameCubeObj.AddComponent<GameCube>();
                newGameCube.Initialize(x, y);

                _gameCubes.Add(newGameCube);
            }
        }
    }

    public void SetOwner(int x, int y, GameLogic.Owner owner)
    {
        _gameCubes[y * GAME_BOARD_DIMENSION + x].SetOwner(owner);
    }

    public void GetOwnerCounts(out int contagionOut, out int playerCount)
    {
        contagionOut = 0;
        playerCount = 0;

        foreach( GameCube gameCube in _gameCubes )
        {
            if (gameCube.Owner == GameLogic.Owner.Player)
            {
                ++playerCount;
            }
            else
            {
                ++contagionOut;
            }
        }
    }
}
