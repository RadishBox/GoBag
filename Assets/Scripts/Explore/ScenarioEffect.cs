using UnityEngine;
using System.Collections;


public abstract class ScenarioEffect : MonoBehaviour
{
	[SerializeField]
	private string _name;

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
