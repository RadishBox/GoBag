using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Contains all different sickness status in the game
/// </summary>
public enum SicknessType { Injury, Infection, Dirty, Stomachache, Cold, Cough };
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
        Sicknesses.Add(new Injury("Herida", GeneralGUI.Instance.HealthColor, new Vector2(3, 5)));
        Sicknesses.Add(new Infection("Infección", GeneralGUI.Instance.HealthColor));
        Sicknesses.Add(new Dirty("Sucio", GeneralGUI.Instance.HealthColor, new Vector2(3, 5)));
        Sicknesses.Add(new Stomachache("Dolor de estómago", GeneralGUI.Instance.HealthColor));

    }

    public static SicknessLibrary Instance
    {
        get { return _instance; }
        set { _instance = value; }
    }
}
