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

		const int startingIndex = 0;
		const int endingIndex = GAME_BOARD_DIMENSION;
        
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

        // Let's adjust the camera to make sure the board always fits.
        Camera.main.orthographicSize = GameBoard.GAME_BOARD_DIMENSION * 0.6f;
        const int halfDimension = GAME_BOARD_DIMENSION / 2;
		Camera.main.transform.position = new Vector3(halfDimension, halfDimension, Camera.main.transform.position.z);
    }

    public void Reset()
    {
        if (_gameCubes != null)
        {
            foreach (GameCube gameCube in _gameCubes)
            {
                gameCube.Reset();
            }
        }
        else
        {
            Debug.LogWarning("[GameBoard] Reset - Expected valid list of GameCubes but none were found. Will attempt to generate it all from scratch.", this);

            GenerateGameBoard();
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

    public void GetOwnerCounts(out int contagionCount, out int playerCount, out int otherCount)
    {
        contagionCount = 0;
        playerCount = 0;
		otherCount = 0;

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
					{
				++otherCount;
					}
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
		GameCube gameCube = IsInBounds(x, y)? GetGameCube(x, y) : null;
		return (gameCube != null && gameCube.Owner != GameLogic.Owner.Player && gameCube.Owner != GameLogic.Owner.Contagion);
    }

    public bool IsValidContagionMove(Vector2Int position)
    {
        return IsValidContagionMove(position.x, position.y);
    }

	public bool IsValidPlayerMove(Vector2Int position)
	{
		bool isValid = false;
		if ( IsInBounds(position) && (GetGameCube(position).Owner == GameLogic.Owner.Neutral ||
		     GetGameCube(position).Owner == GameLogic.Owner.PowerUp))
		{
			List<Vector2Int> cubes;
			GetCubesOfType(GameLogic.Owner.Player, out cubes);

			if (cubes.Count > 0)
			{
				foreach (Vector2Int cube in cubes)
		        {
					if (IsAdjacent(position, cube))
					{
						isValid = true;
					}
				}
			}
			else
			{
				// There are no player ownewd cubes so the player can place anywheres.
				isValid = true;
			}
		}

		return isValid;
	}

	public bool IsAdjacent(Vector2Int a, Vector2Int b)
	{
		if (a.x - b.x == 1 || a.x - b.x == -1)
		{
			return a.y == b.y;
		}

		if (a.y - b.y == 1 || a.y - b.y == -1)
		{
			return a.x == b.x;
		}

		return false;
	}

	public bool IsThereAnyValidPlayerMove()
	{
		bool valid = false;
		foreach(GameCube cube in _gameCubes)
		{
			if (IsValidPlayerMove(cube.PositionInt))
			{
				valid = true;
				break;
			}
		}

		return valid;
	}

	public void SetUnclaimedTilesToPlayer()
	{
		for (int i = 0; i < _gameCubes.Count; ++i)
		{
			GameCube cube = _gameCubes[i];
			if (cube.Owner == GameLogic.Owner.Neutral || cube.Owner == GameLogic.Owner.PowerUp)
			{
				cube.SetOwner(GameLogic.Owner.Player);
            }
        }
	}
}
