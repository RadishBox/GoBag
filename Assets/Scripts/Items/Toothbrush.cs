using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Toothbrush : Item {

	public BarValue[] BarEffects;

    protected override void Start()
    {
        base.Start();
    }

    public override void Use(MapEntity entity)
    {
    	StartCoroutine(AnimationCoroutine());

        if (entity.GetType() == typeof(PlayerEntity))
        {   
            foreach (BarValue barValue in BarEffects)
            {                
                (entity as PlayerEntity).AlterBar(barValue.EffectValue, barValue.TargetBar);  
            }          
        }
    }

    private IEnumerator AnimationCoroutine()
    {
    	// Spawn animation
    	GameObject Animation = Instantiate(AnimationObject);
    	Animation.transform.SetParent(transform.root, false);

    	Animation.GetComponent<Animator>().SetInteger("type", AnimationType);

    	// Turn on object
    	Animation.GetComponent<Image>().color = Color.white;

    	// Play FX
    	AudioManager.Instance.Play(AudioManager.AudioType.FX, UseFX);
    	AudioManager.Instance.Fade(AudioManager.AudioType.FX, 0, 2.5f);

    	yield return new WaitForSeconds(2.5f);

    	print(Animation.name);

    	// Despawn Animation
    	Destroy(Animation.gameObject);

    	// Destroy GameObject
    	Destroy(this.gameObject);
    }
}
