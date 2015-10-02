using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public enum CivilGuyState { Happy, Worried };
public class CivilGuyController : MonoBehaviour
{
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

    public Button DismissButton;

    //Game over
    public Image EndingSprite;
    public Button GameOverNextButton;

    // Tips
    public GameObject TipsGroup;
    public GameObject TipsParent;
    public GameObject TipPrefab;
    public GameObject TipElementSolutionPrefab;
    public GameObject TipElementCausePrefab;

    public GameObject RetryMenu;

    public AudioClip LevelButtonFX;

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
        canvasGroup.blocksRaycasts = false;
        BackgroundFade.DOFade(0, 1);
        yield return new WaitForSeconds(1);
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

        if (isBig)
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

    public void StartGameOverSequence(Sprite gameOverSprite, string message, CivilGuyState civilGuyState)
    {
        // Disable return button
        DismissButton.interactable = false;

        // set variables
        EndingSprite.sprite = gameOverSprite;
        StartCoroutine(GameOverSequence(message, civilGuyState));
    }

    private IEnumerator GameOverSequence(string message, CivilGuyState civilGuyState)
    {
        // Fade in ending sprite
        BackgroundFade.DOFade(0.5f, 1);
        EndingSprite.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.0f);

        // set message and show civil guy
        ShowCivilGuyGroup(message, false, civilGuyState);

        GameOverNextButton.gameObject.SetActive(true);
        GameOverNextButton.GetComponent<Image>().DOFade(0.25f, 0.5f).SetLoops(-1, LoopType.Yoyo);
        GameOverNextButton.onClick.AddListener(LoadTips);
    }

    public void LoadTips()
    {
        AudioManager.Instance.Play(AudioManager.AudioType.FX, LevelButtonFX);
        GameOverNextButton.onClick.RemoveAllListeners();
        List<Tip> Tips = PlayerEntity.Instance.Tips;
        if (Tips.Count > 0)
        {
            foreach (Tip tip in Tips)
            {
                GameObject TipGroup = Instantiate(TipPrefab);
                TipGroup.transform.SetParent(TipsParent.transform, false);

                // Cause
                GameObject TipElement = Instantiate(TipElementCausePrefab);
                TipElement.transform.SetParent(TipGroup.transform, false);
                TipElement.GetComponentsInChildren<Image>(true)[0].sprite = tip.Cause.Image;
                TipElement.GetComponentsInChildren<Text>(true)[0].text = tip.Cause.Text;
                TipElement.transform.SetAsLastSibling();

                // Solutions
                foreach (Tip.TipItem solution in tip.Solutions)
                {
                    GameObject TipSolutionElement = Instantiate(TipElementSolutionPrefab);
                    TipSolutionElement.transform.SetParent(TipGroup.transform.Find("Solutions"), false);
                    TipSolutionElement.GetComponentsInChildren<Image>(true)[0].sprite = solution.Image;
                    TipSolutionElement.GetComponentsInChildren<Text>(true)[0].text = solution.Text;
                }

                // Modify Tip height according to number of solutions
                float preferredHeight = TipGroup.GetComponent<LayoutElement>().preferredHeight;
                TipGroup.GetComponent<LayoutElement>().preferredHeight = preferredHeight * tip.Solutions.Length;
            }

            TipsGroup.SetActive(true);

            // Change button listener
            if (GameConfiguration.Instance.Level.IsLocalClear)
            {
                GameOverNextButton.onClick.AddListener(LoadTitle);
            }
            else
            {
                GameOverNextButton.onClick.AddListener(LoadMenu);
            }

        }
        else
        {
            LoadTitle();
        }
    }

    public void LoadMenu()
    {
        AudioManager.Instance.Play(AudioManager.AudioType.FX, LevelButtonFX);
        RetryMenu.SetActive(true);
    }

    public void LoadTitle()
    {
        GameController.Instance.LoadTitle();
    }
}
