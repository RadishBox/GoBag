using UnityEngine;
using System.Collections;

/// <summary>
/// Describes the sickness status that the player can be in
/// </summary>
[System.Serializable]
public class Sickness {

    [SerializeField]
    private string _name;

    public ScenarioLibrary.ScenarioType Scenario;
    public Color Color;

    /// <summary>
    /// Name of the sickness as it will appear in the game
    /// </summary>
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }
}
