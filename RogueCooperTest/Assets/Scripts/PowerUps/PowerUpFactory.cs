using UnityEngine;
using System.Collections;

public class PowerUpFactory 
{
	private static PowerUp[] powerUps;

	public static PowerUp GetRandomPowerUp(Vector2Int position)
	{
		if (powerUps == null)
		{
			InitPowerUps();
		}

		int index = UnityEngine.Random.Range(0, 3);
		PowerUp powerUp = powerUps[index].Create();
		powerUp.PositionOnBoard = position;
		return powerUp;
	}

	private static void InitPowerUps()
	{
		powerUps = new PowerUp[3];
		powerUps[0] = new AdditionalMove();
		powerUps[1] = new Expand();
		powerUps[2] = new Extend();
	}
}
