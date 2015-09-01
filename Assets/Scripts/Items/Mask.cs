using UnityEngine;
using System.Collections;

public class Mask : Item {

    protected override void Start()
    {
        base.Start();
    }

    public override void Use(MapEntity entity)
    {
        if (entity.GetType() == typeof(PlayerEntity))
        {   
            (entity as PlayerEntity).Mask.enabled = true;  
        }

        // Destroy GameObject
    	Destroy(this.gameObject);
    }
}
