using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ExploreGUI : MonoBehaviour {

    private static ExploreGUI _instance;

    public Image HealthBar;
    public Image WaterBar;
    public Image EnergyBar;

    public GameObject SicknessListParent;
    public GameObject SicknessPrefab;

    public Color HealthColor;
    public Color WaterColor;
    public Color EnergyColor;


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

    public void AlterBar(int value, PlayerBars bar)
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

    public void AddSickness(Sickness sickness)
    {
        GameObject sicknessGO;
        sicknessGO = Instantiate(SicknessPrefab);
        sicknessGO.transform.SetParent(SicknessListParent.transform,false);
        sicknessGO.GetComponent<Text>().color = sickness.Color;
        sicknessGO.GetComponent<Text>().text = sickness.Name;
        sicknessGO.name = sickness.Name;
    }

    public void AlterSickness(Sickness sickness, Sickness newSickness)
    {
        GameObject sicknessGO;
        if(SicknessListParent.transform.Find(sickness.Name))
        {
            sicknessGO = SicknessListParent.transform.Find(sickness.Name).gameObject;
            sicknessGO.GetComponent<Text>().color = newSickness.Color;
            sicknessGO.GetComponent<Text>().text = newSickness.Name;
            sicknessGO.name = newSickness.Name;
        }
    }
}
