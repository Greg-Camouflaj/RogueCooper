using UnityEngine;
using System.Collections;

public class Expand : PowerUp
{
	public Expand(Vector2Int pos) : base(pos)
	{
	}

	public override void GrantEffect(GameLogic logic)
	{
		GameBoard board = logic.GameBoard;
		Vector2Int[] spotsToClaim = positionOnBoard.GetAdjacent();

		for (int i = 0; i < spotsToClaim.Length; i++)
		{
			if (board.IsValidPlayerMove(spotsToClaim[i]))
			{
				board.SetOwner(spotsToClaim[i], GameLogic.Owner.Player);
			}
		}
	}
}
