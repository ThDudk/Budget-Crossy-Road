using System;
using UnityEngine;

/// <summary>
/// Represents a cardinal direction (up, down, left right). 
/// </summary>
public enum CardinalDirection
{
    Left,
    Right,
    Up,
    Down
}

public static class AxisExtensions {
    public static Vector2 ToVector2(this CardinalDirection cardinalDirection) {
        return cardinalDirection switch {
            CardinalDirection.Left => Vector2.left,
            CardinalDirection.Right => Vector2.right,
            CardinalDirection.Up => Vector2.up,
            CardinalDirection.Down => Vector2.down,
            
            _ => throw new Exception("Axis not supported")
        };
    }

    /// <returns>the cardinal direction with a z-value of 0</returns>
    public static Vector3 ToVector3(this CardinalDirection cardinalDirection) {
        var vector2 = cardinalDirection.ToVector2();
        return new Vector3(vector2.x, vector2.y, 0);
    }

    private static CardinalDirection CastToCardinalDirection(Vector2 cardinalVector) {
        if(cardinalVector == Vector2.left) return CardinalDirection.Left;
        if(cardinalVector == Vector2.right) return CardinalDirection.Right;
        if(cardinalVector == Vector2.up) return CardinalDirection.Up;
        if(cardinalVector == Vector2.down) return CardinalDirection.Down;
        
        throw new Exception("Vector is not a cardinal direction.");
    }
    private static Vector2 ToCardinalDirectionVector(this Vector2 vector) {
        return (Mathf.Abs(vector.x) > Mathf.Abs(vector.y))
            ? Vector2.right * Mathf.Sign(vector.x)
            : Vector2.up * Mathf.Sign(vector.y);
    }

    public static CardinalDirection ToCardinalDirection(this Vector2 vector) {
        return CastToCardinalDirection(vector.ToCardinalDirectionVector());
    }
}