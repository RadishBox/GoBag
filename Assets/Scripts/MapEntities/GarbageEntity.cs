using UnityEngine;
using System.Collections;

/// <summary>
/// Describes the trash behaviour inside the map
/// </summary>
public class GarbageEntity : MapEntity
{
    public float DirtyProbability = 1.0f;

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
            Sickness injury = SicknessLibrary.Instance.GetSickness(SicknessType.Injury);

            // Check that the player doesn't have that sickness
            if(!(otherEntity as PlayerEntity).Sicknesses.Exists(x => ((x.Name == "Sucio") || (x.Name == "Dolor de estómago"))))
            {
                // Probability 60%
                float prob = Random.Range(0.0f, 1.0f);

                if(prob <= DirtyProbability)
                {
                    (otherEntity as PlayerEntity).AddSickness(injury);
                }
            }
        }
    }
}
