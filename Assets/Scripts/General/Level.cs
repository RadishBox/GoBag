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

    public ScenarioLibrary.ScenarioType scenario;

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
}
