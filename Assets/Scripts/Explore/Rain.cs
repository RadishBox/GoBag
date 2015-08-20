﻿using UnityEngine;
using System.Collections;

public class Rain : ScenarioEffect {

	public float SicknessProbability = 0.5f;

	public override void ActivateEffect(MapEntity entity)
	{
        if (entity.GetType() == typeof(PlayerEntity))
        {            
        	// if player is not equipped with a raincoat
        	if((entity as PlayerEntity).raincoat == null)
        	{
        		Sickness cold = SicknessLibrary.Instance.GetSickness(SicknessType.Cold);

	            // Check that the player doesn't have that sickness
	            if(!(entity as PlayerEntity).Sicknesses.Exists(x => (x.Name == cold.Name)))
	            {
	                // Probability 50%
	                float prob = Random.Range(0.0f, 1.0f);

	                if(prob <= SicknessProbability)
	                {
	                    (entity as PlayerEntity).AddSickness(cold);
	                }
	            }	        		
        	}            
        }
	}

}