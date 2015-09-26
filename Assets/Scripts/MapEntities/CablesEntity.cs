
using UnityEngine;
using System.Collections;

/// <summary>
/// Describes the trash behaviour inside the map
/// </summary>
public class CablesEntity : MapEntity
{
    public float DeathProbability = 1.0f;

    public Vector2 spawnFrequencyLimits;

    // Game Over properties
    public Sprite DeathSprite;
    public string DeathText;

    // Audio related to effect
    public AudioClip DeathFX;

    public Tip tip;

    /// <summary>
    /// Function activated upon this entity's turn
    /// </summary>
    protected override IEnumerator PlayTurnRoutine()
    {
        InTurn = true;

        // Effects
        yield return null;
        // Check for effects movement and ambient

        InTurn = false;
    }

    /// <summary>
    /// Activated when other entity walks onto this entity
    /// </summary>
    public override void ActivateEffect(MapEntity otherEntity)
    {
        // When walked on, reduce that entity life
        if (otherEntity.GetType() == typeof(PlayerEntity))
        {            
            // Probability check
            float prob = Random.Range(0.0f, 1.0f);

            if(prob <= DeathProbability)
            {
                // Play audio effect
                AudioManager.Instance.Play(AudioManager.AudioType.FX, DeathFX);

                if(!(otherEntity as PlayerEntity).Tips.Exists(x => (x.Id == tip.Id)))
                {
                    (otherEntity as PlayerEntity).Tips.Add(tip);
                }

                // Kill player
                (otherEntity as PlayerEntity).Kill(DeathSprite, DeathText);	
            }
        }
    }
}
