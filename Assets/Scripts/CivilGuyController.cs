using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public enum CivilGuyState { Happy, Worried };
public class CivilGuyController : MonoBehaviour {
    public Image BackgroundFade;
    public RectTransform CivilGuy;
    public RectTransform CivilGuyBig;
    private RectTransform activeCivilGuy;
    public Text MessageText;

    public Ease AnimationEase;

    private CanvasGroup canvasGroup;

    private CivilGuyState state = CivilGuyState.Happy;
    public Sprite HappyCivilGuy;
    public Sprite InstructingCivilGuy;
    public Image CivilGuyImage;

    //Game over 
    public Image EndingSprite;
	
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

    public void ShowCivilGuyGroup(string message, bool isBig, CivilGuyState state)
    {
        activeCivilGuy = (isBig ? CivilGuyBig : CivilGuy);
        SetMessage(message);

        switch (state) 
        {
            case CivilGuyState.Happy:
                activeCivilGuy.GetComponent<Image>().sprite = HappyCivilGuy;
                break;
            case CivilGuyState.Worried:
                activeCivilGuy.GetComponent<Image>().sprite = InstructingCivilGuy;
                break;
        }

        StartCoroutine(ShowGroupRoutine(isBig));
    }

    IEnumerator ShowGroupRoutine(bool isBig)
    {
        canvasGroup.blocksRaycasts = true;
        Vector2 targetPos;

        if(isBig)
        {            
            targetPos = new Vector2(activeCivilGuy.anchoredPosition.x, -352);
        }
        else
        {
            targetPos = new Vector2(activeCivilGuy.anchoredPosition.x, 241);
        }
        activeCivilGuy.DOAnchorPos(targetPos, 1).SetEase(AnimationEase);
        BackgroundFade.DOFade(0.5f, 1);
        yield return null;
    }

    public void SetMessage(string message)
    {
        activeCivilGuy.transform.Find("Message").GetComponentInChildren<Text>().text = message;
    }

    public void StartGameOverSequence(Sprite gameOverSprite, string message)
    {
        // set variables
        EndingSprite.sprite = gameOverSprite;

        StartCoroutine(GameOverSequence(message));
    }

    private IEnumerator GameOverSequence(string message)
    {
        // Fade in ending sprite
        EndingSprite.gameObject.SetActive(true);

        // set message and show civil guy
        ShowCivilGuyGroup(message, false, CivilGuyState.Worried);
        yield return null;

    }
}
