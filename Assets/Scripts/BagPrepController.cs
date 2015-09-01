using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class BagPrepController : MonoBehaviour
{
    public static BagPrepController Instance;

    public GameObject GridItemsParentGO;
    public GameObject SelectedItemParentGO;
    public GameObject ItemListGroupGO;
    public GameObject ItemListParentGO;

    public Item SelectedItem;

    public Item DraggedItem;

    private bool _isInDropArea = false;

    // Bag grid
    public GridController BagGrid;

    // Civil Guy Group
    public CivilGuyController civilGuyController;

    public GameObject ReadyButton;
    public GameObject DeleteArea;
    public GameObject UseArea;
    public GameObject BagGroup;

    // Level info
    private Level level;


    // Use this for initialization
    void Awake ()
    {
        Instance = this;
        Input.multiTouchEnabled = false;
    }

    void Start()
    {
        
    }

    public void Initialize()
    {
        // Obtain level
        level = GameConfiguration.Instance.Level;
        civilGuyController.ShowCivilGuyGroup("¡Empaca para un <color=\"#335A96FF\">" + level.CurrentScenario.Name + "</color>!",true,CivilGuyState.Worried);
    }

    // Update is called once per frame
    void Update ()
    {

    }

    public void ItemSelect(Item selectedItem)
    {
        this.SelectedItem = selectedItem;
        SpawnNewSelectedItem();
    }

    public void SpawnNewSelectedItem()
    {
        //DraggedItem.SetSize(false);
        //GameObject item = Instantiate(DraggedItem.gameObject);
        //item.transform.SetParent(SelectedItemParentGO.transform, false);
        //item.GetComponent<CanvasGroup>().blocksRaycasts = true;

        // Detatch selected object children
        if (SelectedItemParentGO.transform.childCount > 0)
            Destroy(SelectedItemParentGO.transform.GetChild(0).gameObject);

        if (SelectedItem.gameObject.GetComponent<Toggle>().isOn)
        {
            GameObject item = Instantiate(SelectedItem.gameObject);
            Destroy(item.GetComponent<Toggle>());
            Destroy(item.transform.FindChild("Selected").gameObject);
            //Destroy(item.GetComponent<LayoutElement>());
            item.transform.SetParent(SelectedItemParentGO.transform, false);
            item.AddComponent<DragHandler>();
            item.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }

    public void LoadExploreScene()
    {
        ItemListGroupGO.SetActive(false);
        DeleteArea.SetActive(false);
        ReadyButton.SetActive(false);
        UseArea.SetActive(true);

        BagGroup.SetActive(false);

        ExploreGameController.Instance.StatusGroup.SetActive(true);

        ExploreGameController.Instance.BeginExploration();
    }

    public void TriggerBagOpen()
    {
        if (BagGroup.activeInHierarchy)
        {
            BagGroup.SetActive(false);

            // Activate input
            GameController.Instance.Paused = false;
        }
        else
        {
            BagGroup.SetActive(true);

            // Deactivate input
            GameController.Instance.Paused = true;
        }

    }

    /* Getters and Setters */
    public bool IsInDropArea
    {
        get { return _isInDropArea; }
        set { _isInDropArea = value; }
    }

    public bool HasSelectedItem
    {
        get { return SelectedItemParentGO.transform.childCount > 0; }
    }
}
