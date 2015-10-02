using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Fire : ScenarioEffect {
	public GameObject FirePrefab;

	private int turnCount = 0;
	public int startOnTurnNumber = 6;
	private int rowToMoveTo = 0;

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

        if(turnCount >= startOnTurnNumber && (turnCount % 2 == 0))
        {
        	SpawnFire(rowToMoveTo, entity);        	
        	rowToMoveTo++;
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
		PlayerEntity player = entity as PlayerEntity;
		if(player != null && player.Position.y == mapRow)
		{
			// Kill player
            player.Kill(DeathSprite, DeathText);	
            if(!player.Tips.Exists(x => (x.Id == tip.Id)))
	        {
            	player.Tips.Add(tip);
        	}
		}
		
	}

}
