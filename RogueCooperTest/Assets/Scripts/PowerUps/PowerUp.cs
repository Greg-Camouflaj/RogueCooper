using UnityEngine;
using System.Collections;

public abstract class PowerUp
{
	protected Vector2Int positionOnBoard;

	public PowerUp()
	{
		positionOnBoard = null;
	}

	public PowerUp(Vector2Int position)
	{
		positionOnBoard = position;
	}

	public Vector2Int PositionOnBoard
	{
		get { return positionOnBoard; }
		set { positionOnBoard = value; }
	}

	public abstract void GrantEffect(GameLogic logic);

	public abstract PowerUp Create();
}
