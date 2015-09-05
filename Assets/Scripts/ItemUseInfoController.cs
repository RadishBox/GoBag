using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class ItemUseInfoController : MonoBehaviour {

    public Text ItemUseMessage;

    public Ease AnimationEase;
    public float AnimationDuration;

    private CanvasGroup canvasGroup;

    // Use this for initialization
    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void HideItemInfoGroup()
    {
        StartCoroutine(HideGroupRoutine());
    }

    IEnumerator HideGroupRoutine()
    {
        canvasGroup.DOFade(0, AnimationDuration);
        yield return new WaitForSeconds(AnimationDuration);
        canvasGroup.blocksRaycasts = false;
    }

    public void ShowItemInfoGroup(string message)
    {
        StartCoroutine(ShowGroupRoutine(message));
    }

    IEnumerator ShowGroupRoutine(string message)
    {
        ItemUseMessage.text = message;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.DOFade(1, AnimationDuration);
        yield return null;
    }
}
