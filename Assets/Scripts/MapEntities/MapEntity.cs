using UnityEngine;
using System.Collections;

/// <summary>
/// Class for entities that interact within the map, this includes: player, npc's and incidents 
/// </summary>
public enum State { InTurn };
public enum TurnStatus { WaitingBagUse, WaitingMove, Moving, Idle };
public abstract class MapEntity : MonoBehaviour {
    private Vector2 _position;
    private State _state;
    private bool _inTurn;
    public ScenarioLibrary.ScenarioType Scenario;


    public TurnStatus turnStatus;
    /// <summary>
    /// Position in map the entity is in
    /// </summary>
    public Vector2 Position
    {
        get { return _position; }
        set { _position = value; }
    }

    public void PlayTurn()
    {
        StartCoroutine(PlayTurnRoutine());
    }

    /// <summary>
    /// Function activated upon this entity's turn
    /// </summary>
    protected abstract IEnumerator PlayTurnRoutine();

    public State State
    {
        get { return _state; }
        set { _state = value; }
    }

    public bool InTurn
    {
        get { return _inTurn; }
        set { _inTurn = value; }
    }
}

//This is a basic interface with a single required
//method.
public interface IMovable
{
    void Move(Vector2 movement);
}
