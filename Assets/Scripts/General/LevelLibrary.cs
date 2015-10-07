using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Keeps information of the different levels in the game
/// </summary>
public class LevelLibrary : MonoBehaviour
{

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
            if (level.scenario == scenario)
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
            if (level.Id == levelId)
            {
                return level;
            }
        }

        return Levels[0]; // level not found, return the first level
    }

    public Dictionary<int, Level> GetLevelsDictionary()
    {
        Dictionary<int, Level> result = new Dictionary<int, Level>();
        foreach (Level level in Levels)
        {
            result.Add(level.Id, level);
        }

        return result;
    }

    public Dictionary<int, int> GetLevelsIndexDictionary()
    {
        Dictionary<int, int> result = new Dictionary<int, int>();
        for(int i = 0; i < Levels.Length; i++)
        {
            result.Add(Levels[i].Id, i);
        }

        return result;
    }

    public void UpdateLevel(Level level)
    {
        int targetIndex = 0;
        for (int i = 0; i < Levels.Length; i++)
        {
            if (Levels[i].Id == level.Id)
            {
                targetIndex = i;
                break;
            }
        }

        Levels[targetIndex] = level;
    }

    public void LoadLibrary()
    {
        List<int> idList = GameSave.Instance.UnlockedLevels;        
        Dictionary<int, int> levelsIndexDict = GetLevelsIndexDictionary();

        // UnlockLevels
        for (int i = 0; i < idList.Count; i++)
        {
            if(levelsIndexDict.ContainsKey(idList[i]))
                Levels[levelsIndexDict[idList[i]]].IsLocked = false; 
        }

        // MarkClearedLevels
        idList = GameSave.Instance.ClearedLevels;
        for (int i = 0; i < idList.Count; i++)
        {
            if(levelsIndexDict.ContainsKey(idList[i]))
                Levels[levelsIndexDict[idList[i]]].IsClear = true; 
        }
    }

    public List<int> GetUnlockedLevelIds()
    {
        List<int> idList = new List<int>();

        for (int i = 0; i < Levels.Length; i++)
        {
            if (!Levels[i].IsLocked)
            {
                idList.Add(Levels[i].Id);
            }
        }

        return idList;
    }

    public List<int> GetClearedLevelIds()
    {
        List<int> idList = new List<int>();

        for (int i = 0; i < Levels.Length; i++)
        {
            if (Levels[i].IsClear)
            {
                idList.Add(Levels[i].Id);
            }
        }

        return idList;
    }

    public void ClearLevel(int levelId)
    {
        Dictionary<int, Level> LevelsDictionary = GetLevelsDictionary();
        if(LevelsDictionary.ContainsKey(levelId))
        {
            Level TargetLevel = LevelsDictionary[levelId];
            LevelsDictionary[levelId].IsClear = true;

            // Unlock levels this level unlocks
            foreach (int id in TargetLevel.LevelsToUnlock) 
            {
                UnlockLevel(id);
            }

            foreach (int id in TargetLevel.LevelsToClear) 
            {
                ClearLevel(id);
            }
        }
    }

    public void UnlockLevel(int levelId)
    {
        Dictionary<int, Level> LevelsDictionary = GetLevelsDictionary();
        if(LevelsDictionary.ContainsKey(levelId))
        {
            Level TargetLevel = LevelsDictionary[levelId];
            LevelsDictionary[levelId].IsLocked = false;
        }
    }
}
