using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Describes the player behaviour inside the map
/// </summary>
public enum PlayerBars { Health, Water, Energy }
public class PlayerEntity : MapEntity, IMovable {

    private int _healthPoints = 30;
    private int _waterPoints = 30;
    private int _energyPoints = 30;

    private List<Sickness> _sicknesses;

    public GameObject UpButton;
    public GameObject DownButton;
    public GameObject LeftButton;
    public GameObject RightButton;

    // Equippable slots
    public Raincoat raincoat;

    // Death by natural causes (any of the bars)
    public Sprite NaturalDeathSprite;
    public string HealthDeathText;
    public string WaterDeathText;
    public string EnergyDeathText;
    private string NaturalDeathText;

    void Awake()
    {        
        DisableMoveButtons();
    }


    void Start()
    {
        Sicknesses = new List<Sickness>();
        turnStatus = TurnStatus.WaitingMove;
    }

    /// <summary>
    /// Function activated upon this entity's turn
    /// </summary>
    protected override IEnumerator PlayTurnRoutine()
    {
        InTurn = true;
        //turnStatus = TurnStatus.Idle;

        // Item usage
        //while (turnStatus == TurnStatus.Idle)
        //{
        //   yield return null;
        //}

        // Move
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
        UpdateMoveButtons();

        // Check winning condition
        if(CheckVictory())
        {
            // Win

        }
        else
        {   
            // Check if GameOver
            if(CheckGameOver())
            {
                // Die
                Kill(NaturalDeathSprite, NaturalDeathText);
            }
            else
            {   
                // Check scenario effects
                for(int i = 0; i < ExploreGameController.Instance.ScenarioEffects.Count; i++)
                {
                    ExploreGameController.Instance.ScenarioEffects[i].ActivateEffect(this);
                }

                // Check sicknesses effects
                for(int i = 0; i < Sicknesses.Count; i++)
                {
                    Sicknesses[i].ActivateEffect(this);
                }
                
                // Check for effects movement and ambient

                InTurn = false;
            }
        }        
    }

    /// <summary>
    /// Activated when other entity walks onto this entity
    /// </summary>
    public override void ActivateEffect(MapEntity otherEntity)
    {
        // When other entity walks onto the player, activate that entity's effect on the player
        otherEntity.ActivateEffect(this);
    }

    /// <summary>Checks winning condition (On finish tile).</summary>
    public bool CheckVictory()
    {
        bool victory = false;
        return victory;
    }

    /// <summary>Checks game over condition (Any bar equals 0).</summary>
    public bool CheckGameOver()
    {
        bool gameOver = false;

        if(Health <= 0)
        {
            gameOver = true;
            NaturalDeathText = HealthDeathText;
        }
        if(Water <= 0)
        {
            gameOver = true;
            NaturalDeathText = WaterDeathText;
        }
        if(Energy <= 0)
        {
            gameOver = true;
            NaturalDeathText = EnergyDeathText;
        }        

        return gameOver;
    }

    /// <summary>
    /// Kills player
    /// </summary>
    public void Kill(Sprite deathSprite, string deathText)
    {
        DisableMoveButtons();
        ExploreGUI.Instance.StartGameOverSequence(deathSprite, deathText);
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

    /// <summary>Evolves given sickness for a new one.</summary>
    public void EvolveSickness(Sickness sickness, Sickness newSickness)
    {
        if(Sicknesses.Exists(x => x.Name == sickness.Name))
        {
            Sicknesses.Remove(Sicknesses.Find(x => x.Name == sickness.Name));
            Sicknesses.Add(newSickness);
            ExploreGUI.Instance.AlterSickness(sickness, newSickness);
        }
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

    /// <summary>Updates movement buttons according to tile availability.</summary>
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

    /// <summary>Disables all movebuttons.</summary>
    public void DisableMoveButtons()
    {
        UpButton.SetActive(false);
        DownButton.SetActive(false);
        LeftButton.SetActive(false);
        RightButton.SetActive(false);
    }
}
