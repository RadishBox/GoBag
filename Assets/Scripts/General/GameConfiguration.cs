using UnityEngine;
using System.Collections;

/// <summary>
/// Keeps information on the configuration of the game, from the title screen to the game screen
/// </summary>
public class GameConfiguration : MonoBehaviour
{
	private static GameConfiguration _instance;
    void Awake()
    {
        _instance = this;
    }

    public static GameConfiguration Instance
    {
        get 
        {
            return _instance;
        }
        set { _instance = value; }
    }

	private Level _level;

	public Level Level
	{
		get { return _level; }
		set { _level = value; }
	}

}
