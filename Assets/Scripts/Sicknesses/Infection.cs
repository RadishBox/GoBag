using UnityEngine;
using System.Collections;

public class Infection : Sickness {

	public Infection(string name, Color color) : base(name, color)
	{
	}

	/// <summary>Activated each turn.</summary>
	public override void ActivateEffect(MapEntity entity)
	{
		// When walked on, reduce that entity life
        if (entity.GetType() == typeof(PlayerEntity))
        {
            
            (entity as PlayerEntity).AlterBar(-5, PlayerBars.Health);            
        }		
	}	
}
