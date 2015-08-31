using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class ExploreGUI : MonoBehaviour
{

    private static ExploreGUI _instance;

    public Image HealthBar;
    public Image WaterBar;
    public Image EnergyBar;

    public GameObject SicknessListParent;
    public GameObject SicknessPrefab;

    public CivilGuyController civilGuyController;

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
        string barTweenId = "";

        Image targetBar = null;

        switch (bar)
        {
        case PlayerBars.Health:
            targetBar = HealthBar;
            baseBarDelta = 30;
            barTweenId = "Health";
            break;
        case PlayerBars.Water:
            targetBar = WaterBar;
            baseBarDelta = 30;
            barTweenId = "Water";
            break;
        case PlayerBars.Energy:
            targetBar = EnergyBar;
            baseBarDelta = 30;
            barTweenId = "Energy";
            break;
        }

        barLayout = targetBar.GetComponent<LayoutElement>();

        baseBarDelta = 210 / baseBarDelta;

        result = baseBarDelta * value;
        //barLayout.preferredWidth = barLayout.preferredWidth + result;
        DOTween.Complete("BarSize"+barTweenId);
        // Animation        
        targetBar.DOFade(0.2f, 0.3f).SetLoops(-1, LoopType.Yoyo).SetId("BarColor");
        barLayout.DOPreferredSize(new Vector2(barLayout.preferredWidth + result, barLayout.preferredHeight), 1.0f).SetId("BarSize"+barTweenId).OnComplete(()=>CompletedBarAnimation(targetBar));

    }

    private void CompletedBarAnimation(Image Bar)
    {
        DOTween.Kill("BarColor");
        Bar.DOFade(1f, 0.5f);

    }

    public void AddSickness(Sickness sickness)
    {
        GameObject sicknessGO;
        sicknessGO = Instantiate(SicknessPrefab);
        sicknessGO.transform.SetParent(SicknessListParent.transform, false);
        sicknessGO.GetComponent<Text>().color = sickness.Color;
        sicknessGO.GetComponent<Text>().text = sickness.Name;
        sicknessGO.name = sickness.Name;
    }

    public void AlterSickness(Sickness sickness, Sickness newSickness)
    {
        GameObject sicknessGO;
        if (SicknessListParent.transform.Find(sickness.Name))
        {
            sicknessGO = SicknessListParent.transform.Find(sickness.Name).gameObject;
            sicknessGO.GetComponent<Text>().color = newSickness.Color;
            sicknessGO.GetComponent<Text>().text = newSickness.Name;
            sicknessGO.name = newSickness.Name;
        }
    }

    public void StartGameOverSequence(Sprite gameOverSprite, string message)
    {
        civilGuyController.StartGameOverSequence(gameOverSprite, message);
    }
}
