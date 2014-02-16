using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour
{
	public enum Owner
	{
        Nuetral = 0,
		Contagion = 1,
		Player = 2,
	}

	private Owner _currentTurnOwner = Owner.Contagion;

	private GameBoard _gameBoard = null;

    private void Start()
    {
        CreateDependencies();

        InitializeGameState();
    }

    private void CreateDependencies()
    {
        GameCube.CreateMaterials();
    }

	private void InitializeGameState()
	{
		//@TODO: Generate game board first.
		GenerateGameBoard();

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
		}
	}

	private void GenerateGameBoard()
	{
        if (_gameBoard == null)
        {
            _gameBoard = new GameBoard();
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
	}
}
