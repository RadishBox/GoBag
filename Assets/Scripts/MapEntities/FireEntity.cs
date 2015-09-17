using UnityEngine;
using System.Collections;

/// <summary>
/// Describes the trash behaviour inside the map
/// </summary>
public class FireEntity : MapEntity
{
	public GameObject[] SmokePrefab;

	private int turnCounter = 0;
	public int TurnsToSmoke = 3;
	public float SmokeProb = 0.3f;
	private bool smokeSpawned = false;

	// Game Over properties
    public Sprite DeathSprite;
    public string DeathText;

    // Audio related to effect
    public AudioClip DeathFX;

    public Tip tip;

    /// <summary>
    /// Function activated upon this entity's turn
    /// </summary>
    protected override IEnumerator PlayTurnRoutine()
    {
        InTurn = true;

        // Effects
        CheckSmokeSpawn();
        yield return null;
        // Check for effects movement and ambient

        InTurn = false;
    }

    /// <summary>
    /// Activated when other entity walks onto this entity
    /// </summary>
    public override void ActivateEffect(MapEntity otherEntity)
    {
        // When walked on, reduce that entity life
        if (otherEntity.GetType() == typeof(PlayerEntity))
        {    
        	// Play audio effect
            AudioManager.Instance.Play(AudioManager.AudioType.FX, DeathFX);

            // Kill player
            (otherEntity as PlayerEntity).Kill(DeathSprite, DeathText);	
            if(!(otherEntity as PlayerEntity).Tips.Exists(x => (x.Id == tip.Id)))
            {
                (otherEntity as PlayerEntity).Tips.Add(tip);
            }
        }
    }

    /// <summary>
    /// Spawns smoke around this fire
    /// </summary>
    private void CheckSmokeSpawn()
    {
    	// Check if spawns smoke or not
    	if(!smokeSpawned && turnCounter >= TurnsToSmoke)
    	{
			// Probability] check
	        float prob = Random.Range(0.0f, 1.0f);

	        if(prob <= SmokeProb)
	        {
	        	// Spawn smoke around
        		MapTile tile;

		        // Up
		        tile = MapController.Instance.GetTile(Position + Vector2.up);
		        if(tile != null) // tile not found
		        {
		        	SpawnSmoke(SmokePrefab[Random.Range(0,2)], Position + Vector2.up);		        	
		        }

		        // Down
		        tile = MapController.Instance.GetTile(Position + Vector2.down);
		        if (tile != null) // tile not found
		        {
		        	SpawnSmoke(SmokePrefab[Random.Range(0,2)], Position + Vector2.down);
		        }

		        // Left
		        tile = MapController.Instance.GetTile(Position + Vector2.left);
		        if (tile != null) // tile not found
		        {
		        	SpawnSmoke(SmokePrefab[Random.Range(0,2)], Position + Vector2.left);
		        }

		        // Right
		        tile = MapController.Instance.GetTile(Position + Vector2.right);
		        if (tile != null) // tile not found
		        {
		        	SpawnSmoke(SmokePrefab[Random.Range(0,2)], Position + Vector2.right);
		        }

		        smokeSpawned = true;
	        }
    	} 
    	turnCounter++;   	
    }

    private void SpawnSmoke(GameObject smokePrefab, Vector2 position)
    {
    	GameObject smoke = Instantiate(smokePrefab);
        smoke.transform.SetParent(ExploreGameController.Instance.Map.transform);
        smoke.transform.localPosition = position;
        smoke.GetComponent<MapEntity>().Position = position;

        //ExploreGameController.Instance.Entities.Add(smoke.GetComponent<MapEntity>());

        // Occupy tile
        MapController.Instance.GetTile(position).EntityInTile = smoke.GetComponent<MapEntity>();
    }
}
