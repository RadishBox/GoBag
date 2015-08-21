using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class CivilGuyController : MonoBehaviour {
    public Image BackgroundFade;
    public RectTransform CivilGuy;
    public RectTransform CivilGuyBig;
    private RectTransform activeCivilGuy;
    public Text MessageText;

    public Ease AnimationEase;

    private CanvasGroup canvasGroup;

    public Sprite HappyCivilGuy;
    public Sprite InstructingCivilGuy;
    public Image CivilGuyImage;
	
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
        Vector2 targetPos = new Vector2(activeCivilGuy.anchoredPosition.x, -1156);
        activeCivilGuy.DOAnchorPos(targetPos, 1).SetEase(AnimationEase);
        BackgroundFade.DOFade(0, 1);
        yield return new WaitForSeconds(1);
        canvasGroup.blocksRaycasts = false;
    }

    public void ShowCivilGuyGroup(string message, bool isBig)
    {
        activeCivilGuy = (isBig ? CivilGuyBig : CivilGuy);
        SetMessage(message);

        StartCoroutine(ShowGroupRoutine());
    }

    IEnumerator ShowGroupRoutine()
    {
        canvasGroup.blocksRaycasts = true;
        Vector2 targetPos = new Vector2(activeCivilGuy.anchoredPosition.x, -352);
        activeCivilGuy.DOAnchorPos(targetPos, 1).SetEase(AnimationEase);
        BackgroundFade.DOFade(0.5f, 1);
        yield return null;
    }

    public void SetMessage(string message)
    {
        activeCivilGuy.GetComponentInChildren<Text>().text = message;
    }
}
