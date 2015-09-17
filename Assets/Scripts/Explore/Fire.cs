using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Fire : ScenarioEffect {
	public GameObject FirePrefab;

	private int turnCount = 0;
	public int startOnTurnNumber = 6;

	// Audio related to effect
    public AudioClip FireAmbience;

    // Game Over properties
    public Sprite DeathSprite;
    public string DeathText;

    // Audio related to effect
    public AudioClip DeathFX;
    public Tip tip;


    public void Awake()
    {
    	// Play audio effect
        AudioManager.Instance.Play(AudioManager.AudioType.Ambience, FireAmbience);
    }


	public override void ActivateEffect(MapEntity entity)
	{
        turnCount++;

        if(turnCount >= startOnTurnNumber)
        {
        	SpawnFire(turnCount - startOnTurnNumber, entity);
        }
	}

	private void SpawnFire(int mapRow, MapEntity entity)
	{
		List<MapTile> Tiles = MapController.Instance.GetRow(mapRow);

		foreach (MapTile tile in Tiles) 
		{
			GameObject Fire = Instantiate(FirePrefab);
			Fire.transform.SetParent(ExploreGameController.Instance.Map.transform, false);
			Fire.transform.localPosition = tile.transform.localPosition;
			Fire.GetComponentInChildren<SpriteRenderer>().sortingOrder = 0 - mapRow;
			tile.EntityInTile = Fire.GetComponent<MapEntity>();
		}

		// Check if player was not consumed by the flames of Hell
		if((entity as PlayerEntity).Position.y == mapRow)
		{
			// Kill player
            (entity as PlayerEntity).Kill(DeathSprite, DeathText);	
            if(!(entity as PlayerEntity).Tips.Exists(x => (x.Id == tip.Id)))
	        {
            	(entity as PlayerEntity).Tips.Add(tip);
        	}
		}
		
	}

}
