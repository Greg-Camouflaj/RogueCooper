using UnityEngine;
using System.Collections;

public class GameLogic : MonoBehaviour
{
	enum Owner
	{
		Contagion = 0,
		Player = 1,
	}

	private Owner _currentTurnOwner = Owner.Contagion;

	private GameBoard _gameBoard = null;

    private void Start()
    {
        InitializeGameState();
    }

	private void InitializeGameState()
	{
		//@TODO: Generate game board first.
		GenerateGameBoard();

		// Contagion picks first spot
		ContagionPicksInitialSpot();
	}

	private void Update()
	{
		// Depending on whose turn it is, we do different behavior.
		if( _currentTurnOwner == Owner.Contagion )
		{
			DoContagionTurn();
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
		//@TODO:
	}

	private void DoContagionTurn()
	{
		//@TODO: We look for all spots on the board where the Contagion currently is and then spread to all adjacent squares.
	}
}
