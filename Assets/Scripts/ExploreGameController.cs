using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This class controls the exploration part of the game
/// </summary>
public class ExploreGameController : MonoBehaviour
{


    private static ExploreGameController _instance;

    private int _turn = 0;
    private bool _isTurnProcessing = false;

    public GameObject Map;
    public GameObject Player;
    public Camera MainCamera;

    private List<ScenarioEffect> scenarioEffects;
    private List<MapEntity> entities;

    // GUI prep
    public GameObject StatusGroup;

    void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start ()
    {
        Initialize();
    }

    void Initialize()
    {
        // BuildMap
        Map = Instantiate(Map);

        // Place Player
        Player = Instantiate(Player);
        Player.transform.SetParent(Map.transform);
        Player.GetComponent<PlayerEntity>().Position = Player.transform.localPosition;

        // Set camera target
        MainCamera.GetComponent<CameraController>().Target = Player.transform;

        // Place objective

        // Place entities
        entities = new List<MapEntity>();
        PlaceEntities();

        // Set scenario effects
        scenarioEffects = new List<ScenarioEffect>();
        PrepareScenarioEffects();

        StartCoroutine(AdvanceTurn());
    }

    /// <summary>
    /* General turn
     *  1. Player uses up to 1 item
     *  2. Check used item effects
     *  3. Player moves
     *  4. Check winning condition
     *  5. Player movement effects
     *  6. Scenario ambient effects
     *  7. Other movable entities move
     *  8. Other entity effects
     *  9. Status bars get modified
     */
    /// </summary>
    private IEnumerator AdvanceTurn()
    {
        while (true)
        {
            while (_isTurnProcessing)
            {
                yield return null;
            }

            if (!_isTurnProcessing)
            {
                StartCoroutine(ProcessTurn());
            }
        }
    }

    // Turn loop for all entities
    private IEnumerator ProcessTurn()
    {
        _isTurnProcessing = true;
        print("Turn started");
        Turn++;
        // Process Player turn
        PlayerEntity player = Player.GetComponent<PlayerEntity>();
        player.PlayTurn();

        while (player.InTurn)
            yield return null;

        // Process other entities' turn
        foreach (MapEntity entity in entities)
        {
            entity.PlayTurn();

            while (entity.InTurn)
                yield return null;
        }

        print("Turn ended");
        _isTurnProcessing = false;
    }

    /// <summary>Places entities in the map.</summary>
    private void PlaceEntities()
    {
        // Get entities for this scenario
        GameObject[] gameEntities = MapEntityLibrary.Instance.Entities;

        foreach (GameObject entity in gameEntities)
        {
            ScenarioLibrary.ScenarioType scenarioType = entity.GetComponent<MapEntity>().Scenario;
            // Check if it belongs to that scenario
            if ((scenarioType == ScenarioLibrary.ScenarioType.All) || (scenarioType == GameConfiguration.Instance.Level.scenario))
            {
                // Decide number of said entity
                int numberToSpawn = Random.Range(4, 5);

                for (int i = 0; i < numberToSpawn; i++)
                {
                    MapTile targetTile = Map.GetComponent<MapController>().GetRandomTile();

                    GameObject entityGO = Instantiate(entity);
                    entityGO.transform.SetParent(Map.transform);
                    entityGO.transform.localPosition = targetTile.transform.localPosition;
                    entityGO.GetComponent<MapEntity>().Position = entityGO.transform.localPosition;

                    targetTile.EntityInTile = entityGO.GetComponent<MapEntity>();
                    entities.Add(entityGO.GetComponent<MapEntity>());

                }
            }
        }
    }

    private void PrepareScenarioEffects()
    {
        // Get scenario effect if any
        if (GameConfiguration.Instance.Level.CurrentScenario.ScenarioEffect)
        {
            GameObject ScenarioEffect = Instantiate(GameConfiguration.Instance.Level.CurrentScenario.ScenarioEffect);
            scenarioEffects.Add(ScenarioEffect.GetComponent<ScenarioEffect>());
        }

        // Get scenario decoration tiles
    }


    #region Properties
    public int Turn
    {
        get { return _turn; }
        set { _turn = value; }
    }

    public static ExploreGameController Instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

    public List<ScenarioEffect> ScenarioEffects
    {
        get { return scenarioEffects; }
    }

    #endregion
}
