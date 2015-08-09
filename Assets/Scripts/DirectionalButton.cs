using UnityEngine;
using System.Collections;

public enum Direction { Up, Down, Right, Left };
public class DirectionalButton : MonoBehaviour {
    public Direction direction;

    public GridMove playerMovementController;

    void OnMouseUp()
    {
        Vector2 directionVector = Vector2.zero;
        switch (direction)
        {
            case Direction.Up:
                directionVector = Vector2.up;
                break;
            case Direction.Down:
                directionVector = Vector2.down;
                break;
            case Direction.Right:
                directionVector = Vector2.right;
                break;
            case Direction.Left:
                directionVector = Vector2.left;
                break;
        }
        playerMovementController.Move(directionVector);
    }
}
