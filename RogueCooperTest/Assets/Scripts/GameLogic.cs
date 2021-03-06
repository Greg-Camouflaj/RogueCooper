using UnityEngine;
using System.Collections;
using System.Collections.Generic;   // for List.

public class GameLogic : MonoBehaviour
{
	private const int POWER_UP_SPAWN_INTERVAL = 3;

	private GUIText foo;

	public enum Owner
	{
        Neutral = 0,
		Contagion = 1,
		Player = 2,
        PowerUp = 3,
	}

	private Owner _currentTurnOwner;
	private int turnsSinceLastPowerUp;
	private int playerBonusMoves;
	private GameBoard _gameBoard = null;

	private bool gameOver;
	private bool playerIsDone;
	public int PlayerBonusMoves
	{
		get { return playerBonusMoves; }
		set { playerBonusMoves = value; }
	}
	public GameBoard GameBoard
	{
		get { return _gameBoard; }
	}

    private void Start()
    {
		Application.targetFrameRate = 60;
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
		gameOver = false;
		playerIsDone = false;
		UpdateScore();

		// Contagion picks first spot
		_currentTurnOwner = Owner.Contagion;
		turnsSinceLastPowerUp = 1;
		playerBonusMoves = 0;
		ContagionPicksInitialSpot();

        _currentTurnOwner = Owner.Player;
	}

	private void Update()
	{
        if (Input.GetKeyUp(KeyCode.R))
        {
            InitializeGameState();
        }

		if (!gameOver)
		{
			// Depending on whose turn it is, we do different behavior.
			if( _currentTurnOwner == Owner.Contagion )
			{
				DoContagionTurn();

				if (!playerIsDone)
				{
					// TODO also check if we can make a next move and if not then end hte game.
					if (_gameBoard.IsThereAnyValidPlayerMove())
					{
						_currentTurnOwner = Owner.Player;
					}
					else
					{
						playerIsDone = true;
					}
                }

                turnsSinceLastPowerUp++;
				if (turnsSinceLastPowerUp >= POWER_UP_SPAWN_INTERVAL)
				{
					SpawnPowerUp ();
					turnsSinceLastPowerUp = 0;
				}
			}
			else if ( _currentTurnOwner == Owner.Player )
			{
				// Else, this is the Player's turn.
				DoPlayerTurn();
				//@TODO: Wait for input of the Player selecting one of the valid blocks.

	            if (Input.GetKeyUp(KeyCode.P))
	            {
	                _currentTurnOwner = Owner.Contagion;
				}
			}
		}

		//Also remember to update our score every frame
		UpdateScore();
	}

	private void SpawnPowerUp()
	{
		List<Vector2Int> possibleSpawnPoints;
		_gameBoard.GetCubesOfType(Owner.Neutral, out possibleSpawnPoints);
		if (possibleSpawnPoints.Count > 0)
		{
			int index = UnityEngine.Random.Range(0, possibleSpawnPoints.Count);
			_gameBoard.SetOwner(possibleSpawnPoints[index], Owner.PowerUp);
		}
	}

	private void GenerateGameBoard()
	{
        if (_gameBoard == null)
        {
            GameObject gameBoardObj = new GameObject("GameBoard");
            _gameBoard = gameBoardObj.AddComponent<GameBoard>();
            _gameBoard.GenerateGameBoard();

            // Let's adjust the camera to make sure the board always fits.
            Camera.main.orthographicSize = GameBoard.GAME_BOARD_DIMENSION * 0.6f;
        }
		else
		{
			_gameBoard.Reset();
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

        int numCubes = contagionCubes.Count;
        int index;
        for (index = 0; index < numCubes; ++index)
        {
            //@TODO: Mark moves to make by doing left, right, up, and down checks.

            Vector2Int position = contagionCubes[index];

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

        int numMovesToMake = movesToMake.Count;
        for (index = 0; index < numMovesToMake; ++index)
        {
            _gameBoard.SetOwner(movesToMake[index], Owner.Contagion);
        }

		if (movesToMake.Count < 1)
		{
			// Games over!
			TriggerGameOver();
		}
	}

	private void TriggerGameOver()
	{
		gameOver = true;
		_currentTurnOwner = Owner.Neutral;
		_gameBoard.SetUnclaimedTilesToPlayer();
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
		int playerCount;
		int contagionCount;
		int otherCount;
		_gameBoard.GetOwnerCounts(out contagionCount, out playerCount, out otherCount);

		int score = playerCount;
		foo.text = "Score: " + score;
	}

	private void DoPlayerTurn()
	{
		const float touchRadius = 0.2f;
		if (Input.GetMouseButtonDown(0))
		{
			Vector3 mousePosition3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 mousePosition = mousePosition3D.ToVector2();

			Collider2D closest = null;
			float closestDistanceSqr = Mathf.Infinity;
			Collider2D[] colliders = Physics2D.OverlapCircleAll(mousePosition, touchRadius);
			for (int i = 0; i < colliders.Length; ++i)
			{
				float distanceSqr = Vector2.SqrMagnitude(colliders[i].transform.position.ToVector2() - mousePosition);
				if (distanceSqr < closestDistanceSqr)
				{
					closest = colliders[i];
					closestDistanceSqr = distanceSqr;
				}
			}

			// Tapped on something!
			if (closest != null)
			{
				GameCube clickedGameCube = closest.GetComponent<GameCube>();
				if (clickedGameCube != null)
				{
					if (_gameBoard.IsValidPlayerMove(clickedGameCube.PositionInt))
					{
						bool gotPowerUp = _gameBoard.GetOwner(clickedGameCube.PositionInt) == Owner.PowerUp;
						clickedGameCube.SetOwner(Owner.Player);  //Must do this before calculating powerups!

						if (gotPowerUp)
						{
							PowerUp powerUp = PowerUpFactory.GetRandomPowerUp(clickedGameCube.PositionInt);
							powerUp.GrantEffect(this);
						}

						if (playerBonusMoves == 0)
						{
							_currentTurnOwner = Owner.Contagion;
						}
						else
						{
							playerBonusMoves--;
							turnsSinceLastPowerUp++;
						}
					}
					// else TODO show some error thing
				}
			}
		}
	}
}
