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

        // Move
        while (turnStatus == TurnStatus.Idle)
        {
            yield return null;
        }

        Move(CalculateMovementDirection());
        while (turnStatus == TurnStatus.Moving)
        {
            yield return null;
        }

        // Check for effects movement and ambient

        InTurn = false;
    }

    public void Move(Vector2 movement)
    {
        // Update position
        Position += movement;

        ExploreGUI.Instance.AlterBar(-1, PlayerBars.Energy);
        turnStatus = TurnStatus.Moving; // Lock, is released by animator
    }

    public Vector2 CalculateMovementDirection()
    {
        Vector2 targetDirection = Vector2.zero;

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

        return targetDirection;
    }
}
