using UnityEngine;
using System.Collections;

/// <summary>
/// Describes the player behaviour inside the map
/// </summary>
public class SerpentEntity : MapEntity, IMovable
{
    /// <summary>
    /// Function activated upon this entity's turn
    /// </summary>
    protected override IEnumerator PlayTurnRoutine()
    {
        InTurn = true;
        // Item usage
        //while (turnStatus == TurnStatus.WaitingBagUse)
        //{
        //   yield return null;
        //}

        // Move
        // Turn off directional buttons

        UpdateMoveButtons();
        turnStatus = TurnStatus.WaitingMove;
        while (turnStatus == TurnStatus.WaitingMove)
        {
            yield return null;
        }

        //Move();
        while (turnStatus == TurnStatus.Moving)
        {
            yield return null;
        }

        // Check winning condition

        // Check for effects movement and ambient

        InTurn = false;
    }

    public void Move(Vector2 movement)
    {
        // Update position
        Position += movement;

        ExploreGUI.Instance.AlterHealthbar(-1, PlayerBars.Energy);
        turnStatus = TurnStatus.Moving;
    }

    public void UpdateMoveButtons()
    {
        print(Position);
        MapTile tile;

        // Up
        tile = MapController.Instance.GetTile(Position + Vector2.up);
        if (tile == null || !tile.Passable) // tile not found
        {
        }
        else
        {
        }

        // Down
        tile = MapController.Instance.GetTile(Position + Vector2.down);
        if (tile == null || !tile.Passable) // tile not found
        {
        }
        else
        {
        }

        // Left
        tile = MapController.Instance.GetTile(Position + Vector2.left);
        if (tile == null || !tile.Passable) // tile not found
        {
        }
        else
        {
        }

        // Right
        tile = MapController.Instance.GetTile(Position + Vector2.right);
        if (tile == null || !tile.Passable) // tile not found
        {
        }
        else
        {
        }
    }
}
