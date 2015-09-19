using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GameSave {

    public static GameSave Instance;

    public List<int> UnlockedLevels;
    public List<int> ClearedLevels;

    public GameSave()
    {
    }
}
