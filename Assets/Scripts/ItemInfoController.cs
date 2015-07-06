using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class ItemInfoController : MonoBehaviour {

    public Text ItemName;
    public Text ItemDescription;
    public Image ItemImage;

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

    public void ShowItemInfoGroup(Item item)
    {
        StartCoroutine(ShowGroupRoutine(item));
    }

    IEnumerator ShowGroupRoutine(Item item)
    {
        ItemName.text = item.Name;
        ItemDescription.text = item.Description;
        ItemImage.sprite = item.ItemSprite;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.DOFade(1, AnimationDuration);
        yield return null;
    }
}
