using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Keeps information of the different levels in the game
/// </summary>
public class LevelLibrary : MonoBehaviour {

	private static LevelLibrary _instance;

	public Level[] Levels;

    void Awake()
    {
        _instance = this;
    }

    public static LevelLibrary Instance
    {
        get 
        {
            return _instance;
        }
        set { _instance = value; }
    }

    /// <summary>
    /// Returns levels from the given scenario
    /// </summary>
    public List<Level> GetLevels(ScenarioLibrary.ScenarioType scenario)
    {
    	List<Level> levels = new List<Level>();
    	foreach (Level level in Levels) 
    	{
    		if(level.scenario == scenario)
    		{    			
    			levels.Add(level);
    		}
    	}

    	return levels;
    }

    /// <summary>
    /// Gets the level based on its id
    /// </summary>
    public Level GetLevel(int levelId)
    {
    	foreach (Level level in Levels) 
    	{
    		if(level.Id == levelId)
    		{
    			return level;
    		}
    	}

    	return Levels[0]; // level not found, return the first level
    }

}
