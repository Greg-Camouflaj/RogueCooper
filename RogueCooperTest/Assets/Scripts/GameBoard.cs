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
                GameObject newGameCubeObj = new GameObject(string.Format("GameCube [x:{0},y:{1}]", x, y));
                GameCube newGameCube = newGameCubeObj.AddComponent<GameCube>();
                newGameCube.transform.parent = this.transform;
                newGameCube.Initialize(x, y);

                _gameCubes.Add(newGameCube);
            }
        }
    }

    public GameCube GetGameCube(int x, int y)
    {
        return _gameCubes[y * GAME_BOARD_DIMENSION + x];
    }

    public GameCube GetGameCube(Vector2Int position)
    {
        return GetGameCube(position.x, position.y);
    }

    public void SetOwner(int x, int y, GameLogic.Owner owner)
    {
        GetGameCube(x, y).SetOwner(owner);
    }

    public void SetOwner(Vector2Int position, GameLogic.Owner owner)
    {
        SetOwner(position.x, position.y, owner);
    }

    public bool IsInBounds(int x, int y)
    {
        return
            (   0 <= x && x < GAME_BOARD_DIMENSION &&
                0 <= y && y < GAME_BOARD_DIMENSION);
    }

    public bool IsInBounds(Vector2Int position)
    {
        return IsInBounds(position.x, position.y);
    }

    public GameLogic.Owner GetOwner(int x, int y)
    {
        return GetGameCube(x, y).Owner;
    }

    public GameLogic.Owner GetOwner(Vector2Int position)
    {
        return GetOwner(position.x, position.y);
    }

    public void GetOwnerCounts(out int contagionCount, out int playerCount)
    {
        contagionCount = 0;
        playerCount = 0;

        foreach( GameCube gameCube in _gameCubes )
        {
            switch (gameCube.Owner)
            {
                case GameLogic.Owner.Contagion:
                    {
                        ++contagionCount;
                    }
                    break;
                case GameLogic.Owner.Player:
                    {
                        ++playerCount;
                    }
                    break;
                default:
                    break;
            }
        }
    }

	public void GetCubesOfType(GameLogic.Owner owner, out List<Vector2Int> cubes)
	{
		cubes = new List<Vector2Int>();
		
		for (int y = 0; y < GAME_BOARD_DIMENSION; y++)
		{
			for (int x = 0; x < GAME_BOARD_DIMENSION; x++)
			{
				if (_gameCubes[y * GAME_BOARD_DIMENSION + x].Owner == owner)
				{
					cubes.Add(new Vector2Int(x, y));
				}
			}
		}
	}
	
	public void GetContagionCubes(out List<Vector2Int> contagionCubes)
	{
		GetCubesOfType (GameLogic.Owner.Contagion, out contagionCubes);
    }

    public bool IsValidContagionMove(int x, int y)
    {
        return (IsInBounds(x, y) && GetGameCube(x, y).Owner != GameLogic.Owner.Player);
    }

    public bool IsValidContagionMove(Vector2Int position)
    {
        return IsValidContagionMove(position.x, position.y);
    }
}
