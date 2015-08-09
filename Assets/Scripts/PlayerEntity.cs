using UnityEngine;
using System.Collections;

/// <summary>
/// Describes the player behaviour inside the map
/// </summary>
public enum PlayerBars { Health, Water, Energy }
public class PlayerEntity : MapEntity, IMovable {

    private int _healthPoints = 20;
    private int _waterPoints = 30;
    private int _energyPoints = 30;

    public GameObject UpButton;
    public GameObject DownButton;
    public GameObject LeftButton;
    public GameObject RightButton;

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
        while(turnStatus == TurnStatus.WaitingMove)
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

    public int Health
    {
        get { return _healthPoints; }
        set { _healthPoints = value; }
    }

    public int Water
    {
        get { return _waterPoints; }
        set { _waterPoints = value; }
    }

    public int Energy
    {
        get { return _energyPoints; }
        set { _energyPoints = value; }
    }

    public void Move(Vector2 movement)
    {
        // Update position
        Position += movement;

        // Reduce energy bar
        Energy--;
        ExploreGUI.Instance.AlterHealthbar(-1, PlayerBars.Energy);
        turnStatus = TurnStatus.Moving;
    }

    public void UpdateMoveButtons()
    {
        print(Position);
        MapTile tile;

        // Up
        tile = MapController.Instance.GetTile(Position + Vector2.up);
        if(tile == null || !tile.Passable) // tile not found
        {
            UpButton.SetActive(false);
        }
        else
        {
            UpButton.SetActive(true);
        }

        // Down
        tile = MapController.Instance.GetTile(Position + Vector2.down);
        if (tile == null || !tile.Passable) // tile not found
        {
            DownButton.SetActive(false);
        }
        else
        {
            DownButton.SetActive(true);
        }

        // Left
        tile = MapController.Instance.GetTile(Position + Vector2.left);
        if (tile == null || !tile.Passable) // tile not found
        {
            LeftButton.SetActive(false);
        }
        else
        {
            LeftButton.SetActive(true);
        }

        // Right
        tile = MapController.Instance.GetTile(Position + Vector2.right);
        if (tile == null || !tile.Passable) // tile not found
        {
            RightButton.SetActive(false);
        }
        else
        {
            RightButton.SetActive(true);
        }
    }
}
