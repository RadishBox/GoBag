using UnityEngine;
using System.Collections;

[System.Serializable]
public class Level {

    [SerializeField]
    private int _id;
    [SerializeField]
    private float _bagTime;
    public GameObject Map;
    public AudioClip BgMusic;
    public bool IsTutorial;

    public bool isLocked = false;
    [SerializeField]
    private bool _isClear = false;
    private bool _isLocalClear = false;

    public ScenarioLibrary.ScenarioType scenario;

    public int[] LevelsToUnlock;

    private Item[] _availableItems;

    public int Id
    {
        get { return _id; }
        set { _id = value; }
    }

    public float BagTime
    {
        get { return _bagTime; }
        set { _bagTime = value; }
    }

    public Item[] AvailableItems
    {
        get { return _availableItems; }
        set { _availableItems = value; }
    }

    public Scenario CurrentScenario
    {
        get { return ScenarioLibrary.Instance.GetScenario(scenario); }
    }

    public bool IsClear
    {
        get { return _isClear; }
        set { _isClear = value; }
    }

    public bool IsLocalClear
    {
        get { return _isLocalClear; }
        set { _isLocalClear = value; }
    }

    public bool IsLocked
    {
        get { return isLocked; }
        set { isLocked = value; }
    }
}
