using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Describes the player behaviour inside the map
/// </summary>
public enum PlayerBars { Health, Water, Energy }
public class PlayerEntity : MapEntity, IMovable {

    private int _healthPoints = 20;
    private int _waterPoints = 30;
    private int _energyPoints = 30;

    private List<Sickness> _sicknesses;

    public GameObject UpButton;
    public GameObject DownButton;
    public GameObject LeftButton;
    public GameObject RightButton;

    void Start()
    {
        Sicknesses = new List<Sickness>();
    }

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

        // Check sicknesses effects
        foreach (Sickness sickness in Sicknesses)
        {
            sickness.ActivateEffect(this);
        }
        
        // Check for effects movement and ambient

        InTurn = false;
    }

    /// <summary>
    /// Activated when other entity walks onto this entity
    /// </summary>
    public override void ActivateEffect(MapEntity otherEntity)
    {
        // When other entity walks onto the player, activate that entity's effect on the player
        otherEntity.ActivateEffect(this);
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

    public void AlterBar(int value, PlayerBars bar)
    {
        switch (bar)
        {
            case PlayerBars.Health:
                Health = Health + value;
                ExploreGUI.Instance.AlterBar(value, PlayerBars.Health);
                break;
            case PlayerBars.Water:
                Water = Water + value;
                ExploreGUI.Instance.AlterBar(value, PlayerBars.Water);
                break;
            case PlayerBars.Energy:
                Energy = Energy + value;
                ExploreGUI.Instance.AlterBar(value, PlayerBars.Energy);
                break;
        }
    }

    public void AddSickness(Sickness sickness)
    {
        // Add sickness to the player
        Sicknesses.Add(sickness);

        // Add sickness to the gui
        ExploreGUI.Instance.AddSickness(sickness);
    }

    public List<Sickness> Sicknesses
    {
        get { return _sicknesses; }
        set { _sicknesses = value; }
    }

    public void Move(Vector2 movement)
    {
        // Free current tile
        MapController.Instance.GetTile(Position).EntityInTile = null;

        // Update position
        Position += movement;

        // Reduce energy bar
        AlterBar(-1, PlayerBars.Energy);

        // Reduce water bar
        AlterBar(-1, PlayerBars.Water);

        // Check if tile had entity
        if(MapController.Instance.GetTile(Position).Occupied)
        {
            // Activate entity effect
            MapController.Instance.GetTile(Position).EntityInTile.ActivateEffect(this);
        }

        // Occupy tile
        MapController.Instance.GetTile(Position).EntityInTile = this;
    }

    public void UpdateMoveButtons()
    {
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
