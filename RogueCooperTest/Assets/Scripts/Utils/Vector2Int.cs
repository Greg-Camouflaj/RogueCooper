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

    public Vector2Int GetLeft()
    {
        return new Vector2Int(_x - 1, _y);
    }

    public Vector2Int GetRight()
    {
        return new Vector2Int(_x + 1, _y);
    }

    public Vector2Int GetUp()
    {
        return new Vector2Int(_x, _y - 1);
    }

    public Vector2Int GetDown()
    {
        return new Vector2Int(_x, _y + 1);
    }
}
