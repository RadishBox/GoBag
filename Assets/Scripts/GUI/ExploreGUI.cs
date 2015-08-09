using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ExploreGUI : MonoBehaviour {

    private static ExploreGUI _instance;

    public Image HealthBar;
    public Image WaterBar;
    public Image EnergyBar;

    void Awake()
    {
        _instance = this;
    }

    public static ExploreGUI Instance
    {
        get 
        {
            return _instance;
        }
        set { _instance = value; }
    }

    public void AlterHealthbar(int value, PlayerBars bar)
    {
        LayoutElement barLayout = null;
        int baseBarDelta = 0;
        int result = 0;

        switch (bar)
        {
            case PlayerBars.Health:
                barLayout = HealthBar.GetComponent<LayoutElement>();
                baseBarDelta = 20;
                break;
            case PlayerBars.Water:
                barLayout = WaterBar.GetComponent<LayoutElement>();
                baseBarDelta = 30;
                break;
            case PlayerBars.Energy:
                barLayout = EnergyBar.GetComponent<LayoutElement>();
                baseBarDelta = 30;
                break;
        }

        baseBarDelta = 210 / baseBarDelta;

        result = baseBarDelta * value;
        barLayout.preferredWidth = barLayout.preferredWidth + result;
    }
}
