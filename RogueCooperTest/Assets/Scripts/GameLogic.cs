using UnityEngine;
using System.Collections;
using System.Collections.Generic;   // for List.

public class GameLogic : MonoBehaviour
{
	private const int POWER_UP_SPAWN_INTERVAL = 3;

	private GUIText foo;

	public enum Owner
	{
        Nuetral = 0,
		Contagion = 1,
		Player = 2,
        PowerUp = 3,
	}

	private Owner _currentTurnOwner = Owner.Contagion;
	private int turnsSinceLastPowerUp = 0;
	private GameBoard _gameBoard = null;

    private void Start()
    {
        CreateDependencies();

        InitializeGameState();
    }

    private void CreateDependencies()
    {
        GameCube.CreateMaterials();
		CreateScore();
    }

	private void InitializeGameState()
	{
		GenerateGameBoard();
		UpdateScore();

		// Contagion picks first spot
		ContagionPicksInitialSpot();

        _currentTurnOwner = Owner.Player;
	}

	private void Update()
	{
		// Depending on whose turn it is, we do different behavior.
		if( _currentTurnOwner == Owner.Contagion )
		{
			DoContagionTurn();

            _currentTurnOwner = Owner.Player;
		}
		else
		{
			// Else, this is the Player's turn.

			//@TODO: Wait for input of the Player selecting one of the valid blocks.

            if (Input.GetKeyUp(KeyCode.P))
            {
                _currentTurnOwner = Owner.Contagion;
            }
		}

		//Also remember to update our score every frame
		UpdateScore();
	}

	private void GenerateGameBoard()
	{
        if (_gameBoard == null)
        {
            GameObject gameBoardObj = new GameObject("GameBoard");
            _gameBoard = gameBoardObj.AddComponent<GameBoard>();
            _gameBoard.GenerateGameBoard();
        }
	}

	private void ContagionPicksInitialSpot()
	{
		//@TODO: Contagion picks random spot among the grid.
        int x = Random.Range(0, GameBoard.GAME_BOARD_DIMENSION - 1);
        int y = Random.Range(0, GameBoard.GAME_BOARD_DIMENSION - 1);

        _gameBoard.SetOwner(x, y, Owner.Contagion);
	}

	private void DoContagionTurn()
	{
        //@TODO: We look for all spots on the board where the Contagion currently is and then spread to all adjacent squares.

        List<Vector2Int> movesToMake = new List<Vector2Int>();

        List<Vector2Int> contagionCubes;
        _gameBoard.GetContagionCubes(out contagionCubes);

        foreach (Vector2Int position in contagionCubes)
        {
            //@TODO: Mark moves to make by doing left, right, up, and down checks.

            // Left:
            Vector2Int leftMove = position.GetLeft();
            if( _gameBoard.IsValidContagionMove( leftMove ) )
            {
                movesToMake.Add(leftMove);
            }

            // Right:
            Vector2Int rightMove = position.GetRight();
            if (_gameBoard.IsValidContagionMove(rightMove))
            {
                movesToMake.Add(rightMove);
            }

            // Up:
            Vector2Int upMove = position.GetUp();
            if (_gameBoard.IsValidContagionMove(upMove))
            {
                movesToMake.Add(upMove);
            }

            // Down:
            Vector2Int downMove = position.GetDown();
            if (_gameBoard.IsValidContagionMove(downMove))
            {
                movesToMake.Add(downMove);
            }
        }

        foreach (Vector2Int position in movesToMake)
        {
            _gameBoard.SetOwner(position, Owner.Contagion);
        }
	}

	private void CreateScore()
	{
		GameObject score = new GameObject("Score");
		score.transform.position = new Vector3(0, 1, 0);
		foo = score.AddComponent<GUIText>();
		foo.color = Color.red;
		foo.fontSize = 22;
	}

	private void UpdateScore()
	{
		int score;
		int contagionCount;
		_gameBoard.GetOwnerCounts(out contagionCount, out score);
		foo.text = "Score: " + contagionCount;
	}
}
