using UnityEngine;
using System.Collections;

public class Beans : Item {

    protected override void Start()
    {
        base.Start();
    }

    public override void Use(MapEntity entity)
    {
        throw new System.NotImplementedException();
    }
}
