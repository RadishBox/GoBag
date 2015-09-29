using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tuna : Item {

	public int EffectValue;
	public PlayerBars TargetBar;

    protected override void Start()
    {
        base.Start();
    }

    public override void Use(MapEntity entity)
    {
        hasAnimStarted = true;
    	StartCoroutine(AnimationCoroutine());

        if (entity.GetType() == typeof(PlayerEntity))
        {   
            (entity as PlayerEntity).AlterBar(EffectValue, TargetBar);            
        }
    }

    private IEnumerator AnimationCoroutine()
    {
    	// Spawn animation
    	Animation = Instantiate(AnimationObject);
    	Animation.transform.SetParent(GameObject.Find("BagGroup").transform, false);

    	Animation.GetComponent<Animator>().SetInteger("type", AnimationType);

    	// Turn on object
    	Animation.GetComponent<Image>().color = Color.white;

    	// Play FX
    	AudioManager.Instance.Play(AudioManager.AudioType.FX, UseFX);
    	AudioManager.Instance.Fade(AudioManager.AudioType.FX, 0, 2.0f);

        yield return new WaitForSeconds(2.0f);  
       
        CompleteAnimation();    
    }

    void OnDisable()
    {
        if(hasAnimStarted)
        {            
            CompleteAnimation();  
        }
    }
}
