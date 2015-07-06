using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class CivilGuyController : MonoBehaviour {
    public Image BackgroundFade;
    public RectTransform CivilGuy;
    public Text MessageText;

    public Ease AnimationEase;

    private CanvasGroup canvasGroup;
	
    // Use this for initialization
    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void HideCivilGuyGroup()
    {
        StartCoroutine(HideGroupRoutine());
    }

    IEnumerator HideGroupRoutine()
    {
        Vector2 targetPos = new Vector2(CivilGuy.anchoredPosition.x, -1156);
        CivilGuy.DOAnchorPos(targetPos, 1).SetEase(AnimationEase);
        BackgroundFade.DOFade(0, 1);
        yield return new WaitForSeconds(1);
        canvasGroup.blocksRaycasts = false;
    }

    public void ShowCivilGuyGroup()
    {
        StartCoroutine(ShowGroupRoutine());
    }

    IEnumerator ShowGroupRoutine()
    {
        canvasGroup.blocksRaycasts = true;
        Vector2 targetPos = new Vector2(CivilGuy.anchoredPosition.x, -352);
        CivilGuy.DOAnchorPos(targetPos, 1).SetEase(AnimationEase);
        BackgroundFade.DOFade(0.5f, 1);
        yield return null;
    }

    public void SetMessage(string message)
    {
        MessageText.text = message;
    }
}
