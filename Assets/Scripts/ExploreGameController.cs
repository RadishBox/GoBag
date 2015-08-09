using UnityEngine;
using System.Collections;

/// <summary>
/// This class controls the exploration part of the game
/// </summary>
public class ExploreGameController : MonoBehaviour {

    private int _turn = 0;
    private bool _isTurnProcessing = false;

    public GameObject Map;
    public GameObject Player;
    public Camera MainCamera;

	// Use this for initialization
	void Start () {
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

        // Place events

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
        //while (true)
        //{
        _isTurnProcessing = true;
            print("Turn started");
            Turn++;
            // Process Player turn
            PlayerEntity player = Player.GetComponent<PlayerEntity>();
            player.PlayTurn();

            while (player.InTurn)
                yield return null;

            print("Turn ended");
            _isTurnProcessing = false;
            // Process Enemy turn
        //}
    }


    #region Properties
    public int Turn
    {
        get { return _turn; }
        set { _turn = value; }
    }

    #endregion
}
