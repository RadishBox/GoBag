using UnityEngine;
using System.Collections;

public class DryFruit : Item {

    protected override void Start()
    {
        base.Start();
    }

    public override void Use(MapEntity entity)
    {
        if (entity.GetType() == typeof(PlayerEntity))
        {   
            (entity as PlayerEntity).AlterBar(3, PlayerBars.Energy);            
        }
    }
}
