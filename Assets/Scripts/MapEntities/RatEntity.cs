﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Describes the player behaviour inside the map
/// </summary>
public class RatEntity : MapEntity, IMovable
{

    public GridMove MovementController;

    /// <summary>
    /// Function activated upon this entity's turn
    /// </summary>
    protected override IEnumerator PlayTurnRoutine()
    {
        InTurn = true;

        // Move
        MovementController.Move(CalculateMovementDirection());
        while (turnStatus == TurnStatus.Moving)
        {
            yield return null;
        }

        // Check for effects movement and ambient

        InTurn = false;
    }

    /// <summary>
    /// Activated when other entity walks onto this entity
    /// </summary>
    public override void ActivateEffect(MapEntity otherEntity)
    {

    }

    public void Move(Vector2 movement)
    {
        // Update position
        Position += movement;
        //turnStatus = TurnStatus.Idle; // Release moving lock, should be handled by animator
    }

    public Vector2 CalculateMovementDirection()
    {
        Vector2 targetDirection = Vector2.zero;
        bool canMoveUp, canMoveDown, canMoveLeft, canMoveRight;

        MapTile tile;

        // Check available directions
         // Up
        tile = MapController.Instance.GetTile(Position + Vector2.up);
        canMoveUp = (tile != null) && (tile.Passable) && (!tile.Occupied || tile.OccupiedByPlayer);

        // Down
        tile = MapController.Instance.GetTile(Position + Vector2.down);
        canMoveDown = (tile != null) && (tile.Passable) && (!tile.Occupied || tile.OccupiedByPlayer);

        // Left
        tile = MapController.Instance.GetTile(Position + Vector2.left);
        canMoveLeft = (tile != null) && (tile.Passable) && (!tile.Occupied || tile.OccupiedByPlayer);

        // Right
        tile = MapController.Instance.GetTile(Position + Vector2.right);
        canMoveRight = (tile != null) && (tile.Passable) && (!tile.Occupied || tile.OccupiedByPlayer);

        if(!canMoveUp && !canMoveDown && !canMoveLeft && !canMoveRight)
        {
            return Vector2.zero;
        }

        // Choose a movement direction
        int randomDir;
        bool validDir = false;
        while (!validDir)
        {
            randomDir = Random.Range(0, 4);
            switch (randomDir)
            {
            case 0:
                if (canMoveUp)
                {
                    validDir = true;
                    targetDirection = Vector2.up;
                }
                break;
            case 1:
                if (canMoveDown)
                {
                    validDir = true;
                    targetDirection = Vector2.down;
                }
                break;
            case 2:
                if (canMoveLeft)
                {
                    validDir = true;
                    targetDirection = Vector2.left;
                }
                break;
            case 3:
                if (canMoveRight)
                {
                    validDir = true;
                    targetDirection = Vector2.right;
                }
                break;
            }
        }

        // Free current tile
        MapController.Instance.GetTile(Position).EntityInTile = null;

        // Occupy tile
        MapController.Instance.GetTile(Position + targetDirection).EntityInTile = this;

        return targetDirection;
    }
}