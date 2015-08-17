using UnityEngine;
using System.Collections;

public class Dirty : Sickness {

	private int _turn = 0;
	private Vector2 _turnsToStomachacheLimits;

	public Dirty(string name, Color color, Vector2 turnsToStomachacheLimits) : base(name, color)
	{
		Turn = 0;
		TurnsToStomachacheLimits = turnsToStomachacheLimits;
	}

	/// <summary>Activated each turn.</summary>
	public override void ActivateEffect(MapEntity entity)
	{
		// When walked on, reduce that entity life
        if (entity.GetType() == typeof(PlayerEntity))
        {
            
            (entity as PlayerEntity).AlterBar(-1, PlayerBars.Health);
            
            // Convert to Stomachache
            if(Turn >= TurnsToStomachacheLimits.y)
            {
            	EvolveSickness(entity);
            }
            else if(Turn >= TurnsToStomachacheLimits.x)
            {
            	// Check prob
            	float prob = Random.Range(0.0f, 1.0f);
            	if(prob < 0.5f)
            	{
            		EvolveSickness(entity);
            	}
            }
        }	
        
		Turn++;	
	}	

	private void EvolveSickness(MapEntity entity)
	{
		(entity as PlayerEntity).EvolveSickness(this, SicknessLibrary.Instance.GetSickness(SicknessType.Stomachache));
	}

	public int Turn
	{
		get { return _turn; }
		set { _turn = value; }
	}

	public Vector2 TurnsToStomachacheLimits
	{
		get { return _turnsToStomachacheLimits; }
		set { _turnsToStomachacheLimits = value; }
	}
}
