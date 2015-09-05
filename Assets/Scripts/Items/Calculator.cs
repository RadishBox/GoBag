﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Calculator : Item {

    public string UseMessage;

    protected override void Start()
    {
        base.Start();
    }

    public override void Use(MapEntity entity)
    {        
        Vector2 pos = (entity as PlayerEntity).Position;
        string vector = "\nv(x:{0},y:{1})\n";
        vector = String.Format(vector, pos.x, pos.y);
        UseMessage = String.Format(UseMessage, vector);
        StartCoroutine(AnimationCoroutine());
    }

    private IEnumerator AnimationCoroutine()
    {
        // Spawn animation
        GameObject Message = Instantiate(AnimationObject);
        Message.transform.SetParent(transform.root, false);

        // Turn on object
        Message.GetComponent<ItemUseInfoController>().ShowItemInfoGroup(UseMessage);

        // Play FX
        AudioManager.Instance.Play(AudioManager.AudioType.FX, UseFX);
        AudioManager.Instance.Fade(AudioManager.AudioType.FX, 0, 2.5f);

        //yield return new WaitForSeconds(2.5f);

        // Despawn Message
        //Destroy(Message.gameObject);

        // Destroy GameObject
        Destroy(this.gameObject);

        yield return null;
    }
}
