using UnityEngine;
using System.Collections;

public class Cold : Sickness {

	public Cold(string name, Color color) : base(name, color)
	{
	}

	/// <summary>Activated each turn.</summary>
	public override void ActivateEffect(MapEntity entity)
	{
		// When walked on, reduce that entity life
        if (entity.GetType() == typeof(PlayerEntity))
        {
            
            (entity as PlayerEntity).AlterBar(-3, PlayerBars.Health);            
        }		
	}	
}
