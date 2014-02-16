using UnityEngine;
using System.Collections;

public class GameBoard : MonoBehaviour
{
    private const int GAME_BOARD_DIMENSION = 12;

    private GameCube[] _gameCubes = null;

    public void GenerateGameBoard()
    {
		CreateGameBoardCubes();
        PositionGameBoardCubes();
    }

    private void CreateGameBoardCubes()
    {
        _gameCubes = new GameCube[GAME_BOARD_DIMENSION * GAME_BOARD_DIMENSION];
    }

    private void PositionGameBoardCubes()
    {
        for (int y = 0; y < GAME_BOARD_DIMENSION; ++y)
        {
            for (int x = 0; x < GAME_BOARD_DIMENSION; ++x)
            {
                _gameCubes[y * GAME_BOARD_DIMENSION + x].transform.position = Vector3.zero;
            }
        }
    }
}
