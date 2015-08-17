using UnityEngine;
using System.Collections;

public class Injury : Sickness {

	private int _turn = 0;
	private int _turnsToInfection;

	public Injury(string name, Color color, int turnsToInfection) : base(name, color)
	{
		Turn = 0;
		TurnsToInfection = turnsToInfection;
	}

	/// <summary>Activated each turn.</summary>
	public override void ActivateEffect(MapEntity entity)
	{
		Turn++;
		// When walked on, reduce that entity life
        if (entity.GetType() == typeof(PlayerEntity))
        {
            
            (entity as PlayerEntity).AlterBar(-1, PlayerBars.Health);
            
            // Convert to Infection
            if(Turn >= TurnsToInfection)
            {
            	(entity as PlayerEntity).EvolveSickness(this, SicknessLibrary.Instance.GetSickness(SicknessType.Infection));
            }
        }		
	}	

	public int Turn
	{
		get { return _turn; }
		set { _turn = value; }
	}

	public int TurnsToInfection
	{
		get { return _turnsToInfection; }
		set { _turnsToInfection = value; }
	}
}
