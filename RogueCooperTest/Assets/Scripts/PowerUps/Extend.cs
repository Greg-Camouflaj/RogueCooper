using UnityEngine;
using System.Collections;

public class Extend : PowerUp
{
	public Extend(Vector2Int pos) : base(pos)
	{
	}

	public override void GrantEffect (GameLogic logic)
	{
		Vector2Int[] directions = Vector2Int.GetCardinalDirections();
		
		for (int i = 0; i < directions.Length; i++)
		{
			if (CanClaimDirection(logic, directions[i]))
			{
				ClaimInDirection(logic, directions[i]);
			}
		}
	}

	private void ClaimInDirection(GameLogic logic, Vector2Int direction)
	{
		Vector2Int toClaim = positionOnBoard + direction;
		while (logic.GameBoard.IsValidPlayerMove2(toClaim))
		{
			logic.GameBoard.SetOwner(toClaim, GameLogic.Owner.Player);
			toClaim += direction;
		}
	}

	private bool CanClaimDirection(GameLogic logic, Vector2Int direction)
	{
		Vector2Int opposite = positionOnBoard - direction;
		return logic.GameBoard.GetOwner(opposite) == GameLogic.Owner.Player;
	}
}
