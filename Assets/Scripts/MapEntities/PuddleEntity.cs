using UnityEngine;
using System.Collections;

/// <summary>
/// Describes the trash behaviour inside the map
/// </summary>
public class PuddleEntity : MapEntity
{
    public float ColdProbability = 0.5f;
    public Sprite[] puddleSprites;
    public SpriteRenderer spriteRenderer;

    public Tip tip;

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
            // if player is not equipped with boots
            if(!(otherEntity as PlayerEntity).Boots.enabled)
            {
                Sickness cold = SicknessLibrary.Instance.GetSickness(SicknessType.Cold);

                // Check that the player doesn't have that sickness
                if(!(otherEntity as PlayerEntity).Sicknesses.Exists(x => (x.Name == cold.Name)))
                {
                    // Probability check
                    float prob = Random.Range(0.0f, 1.0f);

                    if(prob <= ColdProbability)
                    {
                        (otherEntity as PlayerEntity).AddSickness(cold);
                        if(!(otherEntity as PlayerEntity).Tips.Exists(x => (x.Id == tip.Id)))
                        {
                            (otherEntity as PlayerEntity).Tips.Add(tip);
                        }
                    }
                }
            }
        }
    }

    private void SetRandomSprite()
    {
        spriteRenderer.sprite = puddleSprites[Random.Range(0, puddleSprites.Length)];
    }
}
