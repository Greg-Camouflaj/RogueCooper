using UnityEngine;
using System.Collections;

public class AdditionalMove : PowerUp
{
	public AdditionalMove()
	{
	}

	public AdditionalMove(Vector2Int pos) : base(pos)
	{
	}

	public override void GrantEffect(GameLogic logic)
	{
		logic.PlayerBonusMoves++;
	}

	public override PowerUp Create ()
	{
		return new AdditionalMove();
	}
}
