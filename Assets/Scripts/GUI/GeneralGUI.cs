using UnityEngine;
using System.Collections;

public class GeneralGUI : MonoBehaviour {

	private static GeneralGUI _instance;

	public Color HealthColor;
    public Color WaterColor;
    public Color EnergyColor;

    void Awake()
    {
        _instance = this;
    }

    public static GeneralGUI Instance
    {
        get 
        {
            return _instance;
        }
        set { _instance = value; }
    }
}
