using UnityEngine;
using System.Collections;

public class Injury : Sickness {

	private int _turn = 0;
	private Vector2 _turnsToInfectionLimits;
	private int _turnsToHeal = 2; 

	public Injury(string name, Color color, Vector2 turnsToInfectionLimits) : base(name, color)
	{
		Turn = 0;
		TurnsToInfectionLimits = turnsToInfectionLimits;
	}

	/// <summary>Activated each turn.</summary>
	public override void ActivateEffect(MapEntity entity)
	{
		// When walked on, reduce that entity life
        if (entity.GetType() == typeof(PlayerEntity))
        {
            
            (entity as PlayerEntity).AlterBar(-1, PlayerBars.Health);
            if(Turn >= _turnsToHeal)
            {
            	(entity as PlayerEntity).CureSickness(this);
            }
        }	
        
		Turn++;	
	}	

	private void EvolveSickness(MapEntity entity)
	{
		(entity as PlayerEntity).EvolveSickness(this, SicknessLibrary.Instance.GetSickness(SicknessType.Infection));
	}

	public int Turn
	{
		get { return _turn; }
		set { _turn = value; }
	}

	public Vector2 TurnsToInfectionLimits
	{
		get { return _turnsToInfectionLimits; }
		set { _turnsToInfectionLimits = value; }
	}
}
