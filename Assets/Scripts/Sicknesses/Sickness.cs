using UnityEngine;
using System.Collections;

/// <summary>
/// Describes the sickness status that the player can be in
/// </summary>
[System.Serializable]
public abstract class Sickness
{

    [SerializeField]
    private string _name;
    public Color Color;

    public Sickness(string name, Color color)
    {
        Name = name;
        Color = color;
    }

    public abstract void ActivateEffect(MapEntity entity);

    /// <summary>
    /// Name of the sickness as it will appear in the game
    /// </summary>
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }
}
