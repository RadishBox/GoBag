using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Contains all different scenarios in the game
/// </summary>
public class ScenarioLibrary : MonoBehaviour {
    public enum ScenarioType { All, Hurricane, Fire };
    public Scenario[] Scenarios;

    private static ScenarioLibrary _instance;
    public GameObject SicknessPrefab;

    public static ScenarioLibrary Instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

    void Awake()
    {
        Instance = this;
    }

    public List<ScenarioEffect> GetScenarioEffects(ScenarioType scenario)
    {
        return null;
    }

    public Scenario GetScenario(ScenarioType type)
    {
        return Scenarios[(int)type];
    }
	
}
