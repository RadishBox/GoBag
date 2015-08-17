using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Contains all different sickness status in the game
/// </summary>
public enum SicknessType { Injury, Infection, Dirty, StomachAche, Cold, Cough };
public class SicknessLibrary : MonoBehaviour
{    
    public List<Sickness> Sicknesses;

    private static SicknessLibrary _instance;


    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Initialize();
    }

    public Sickness GetSickness(SicknessType type)
    {
        return Sicknesses[(int)type];
    }

    private void Initialize()
    {
        Sicknesses = new List<Sickness>();
        Sicknesses.Add(new Injury("Herida", ExploreGUI.Instance.HealthColor, 3));
        Sicknesses.Add(new Infection("Infección", ExploreGUI.Instance.HealthColor));
    }

    public static SicknessLibrary Instance
    {
        get { return _instance; }
        set { _instance = value; }
    }
}
