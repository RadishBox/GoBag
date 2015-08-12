using UnityEngine;
using System.Collections;

/// <summary>
/// Contains all different sickness status in the game
/// </summary>
public class SicknessLibrary : MonoBehaviour {
    public enum SicknessType { Injury, Infection, Dirty, StomachAche, Cold, Cough };
    public Sickness[] Sicknesses;

    private SicknessLibrary _instance;


    void Awake()
    {
        Instance = this;
    }

    public SicknessLibrary Instance
    {
        get { return _instance; }
        set { _instance = value; }
    }
}
