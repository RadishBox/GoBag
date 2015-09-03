using UnityEngine;
using System.Collections;

public class Tennis : Item {

    protected override void Start()
    {
        base.Start();
    }

    public override void Use(MapEntity entity)
    {
        if (entity.GetType() == typeof(PlayerEntity))
        {   
            (entity as PlayerEntity).RunningShoes.enabled = true; 
            (entity as PlayerEntity).MovementNumber = 2;
        }

        // Destroy GameObject
    	Destroy(this.gameObject);
    }
}
