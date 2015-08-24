using UnityEngine;
using System.Collections;

/// <summary>
/// Describes the trash behaviour inside the map
/// </summary>
public class DeepPuddleEntity : MapEntity
{
    public float DeathProbability = 1.0f;
    public Sprite[] puddleSprites;
    public SpriteRenderer spriteRenderer;

    public Vector2 spawnFrequencyLimits;

    // Game Over properties
    public Sprite DeathSprite;
    public string DeathText;

    void Start()
    {
        SetRandomSprite();
    }

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
                // Kill player
<<<<<<< HEAD
                
=======
                (otherEntity as PlayerEntity).Kill(DeathSprite, DeathText);
>>>>>>> origin/master
            }
        }
    }

    private void SetRandomSprite()
    {
        spriteRenderer.sprite = puddleSprites[Random.Range(0, puddleSprites.Length)];
    }
}
