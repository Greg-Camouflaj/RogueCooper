using UnityEngine;
using System.Collections;

public abstract class PowerUp
{
	protected Vector2Int positionOnBoard;

	public PowerUp(Vector2Int position)
	{
		positionOnBoard = position;
	}

	public abstract void GrantEffect(GameLogic logic);
}
