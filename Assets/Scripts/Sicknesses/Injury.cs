using UnityEngine;
using System.Collections;

public class Injury : Sickness {

	private int _turn = 0;

	public Injury(string name, Color color) : base(name, color)
	{
	}

	public override void ActivateEffect(MapEntity entity)
	{
		// When walked on, reduce that entity life
        if (entity.GetType() == typeof(PlayerEntity))
        {
            
            (entity as PlayerEntity).AlterBar(-1, PlayerBars.Health);
            

            // Converto to Infection
        }		
	}	

	public int Turn
	{
		get { return _turn; }
		set { _turn = value; }
	}
}
