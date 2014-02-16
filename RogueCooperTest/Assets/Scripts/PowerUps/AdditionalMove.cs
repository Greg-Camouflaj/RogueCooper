using UnityEngine;
using System.Collections;

public class AdditionalMove : PowerUp
{
	public AdditionalMove(Vector2Int pos) : base(pos)
	{
	}

	public override void GrantEffect(GameLogic logic)
	{
		logic.PlayerBonusMoves++;
	}
}
