using UnityEngine;
using System.Collections;

public class Vector2Int
{
    private int _x;
    public int x
    {
        get { return _x; }
        set { _x = value; }
    }

    private int _y;
    public int y
    {
        get { return _y; }
        set { _y = value; }
    }

    public Vector2Int(int X, int Y)
    {
        _x = X;
        _y = Y;
    }

	public static Vector2Int operator+(Vector2Int lhs, Vector2Int rhs)
	{
		Vector2Int sum = new Vector2Int(lhs.x + rhs.x, lhs.y + rhs.y);
		return sum;
	}

	public static Vector2Int operator-(Vector2Int lhs, Vector2Int rhs)
	{
		Vector2Int difference = new Vector2Int(lhs.x - rhs.x, lhs.y - rhs.y);
		return difference;
	}

    public Vector2Int GetLeft()
    {
		return this + Left;
    }

    public Vector2Int GetRight()
    {
		return this + Right;
    }

    public Vector2Int GetUp()
    {
		return this + Up;
    }

    public Vector2Int GetDown()
    {
		return this + Down;
    }

	public static Vector2Int Left
	{
		get { return new Vector2Int(-1, 0); }
	}

	public static Vector2Int Right
	{
		get { return new Vector2Int(1, 0); }
	}

	public static Vector2Int Up
	{
		get { return new Vector2Int(0, -1); }
	}

	public static Vector2Int Down
	{
		get { return new Vector2Int(0, 1); }
	}

	public Vector2Int[] GetAdjacent()
	{
		Vector2Int[] cardinalDirections = new Vector2Int[4];
		cardinalDirections[0] = GetLeft();
		cardinalDirections[1] = GetRight();
		cardinalDirections[2] = GetUp();
		cardinalDirections[3] = GetDown();

		return cardinalDirections;
	}

	public static Vector2Int[] GetCardinalDirections()
	{
		Vector2Int[] directions = new Vector2Int[4];
		directions[0] = Left;
		directions[1] = Right;
		directions[2] = Up;
		directions[3] = Down;

		return directions;
	}
}
