using UnityEngine;
using System.Collections;

/// <summary>
/// Describes the trash behaviour inside the map
/// </summary>
public class GarbageEntity : MapEntity
{
    public float DirtyProbability = 1.0f;

    public float FireProbabilitiy = 0.25f;
    public GameObject FireEntityPrefab;
    private bool _onFire = false;

    /// <summary>
    /// Function activated upon this entity's turn
    /// </summary>
    protected override IEnumerator PlayTurnRoutine()
    {
        InTurn = true;

        // Effects
        if(!OnFire)
        {            
            FireScenarioEffect();
        }

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
            Sickness dirty = SicknessLibrary.Instance.GetSickness(SicknessType.Dirty);

            // Check that the player doesn't have that sickness
            if(!(otherEntity as PlayerEntity).Sicknesses.Exists(x => ((x.Name == "Sucio") || (x.Name == "Dolor de estómago"))))
            {
                // Probability 60%
                float prob = Random.Range(0.0f, 1.0f);

                if(prob <= DirtyProbability)
                {
                    (otherEntity as PlayerEntity).AddSickness(dirty);
                }
            }
        }
    }

    /// <summary>
    /// Activated when during fire scenario, there's a probability of garbage catching fire
    /// </summary>
    private void FireScenarioEffect()
    {
        if(GameConfiguration.Instance.Level.scenario == ScenarioLibrary.ScenarioType.Fire)
        {
            // Check probability
            float prob = Random.Range(0.0f, 1.0f);

            if(prob <= FireProbabilitiy)
            {
                // Catch fire
                GameObject fire = Instantiate(FireEntityPrefab);
                fire.transform.SetParent(ExploreGameController.Instance.Map.transform);
                fire.transform.localPosition = Position;
                fire.GetComponent<MapEntity>().Position = Position;

                ExploreGameController.Instance.Entities.Add(fire.GetComponent<MapEntity>());

                // Occupy tile
                MapController.Instance.GetTile(Position).EntityInTile = fire.GetComponent<MapEntity>();
                
                OnFire = true;
            }
        }
    }

    public bool OnFire
    {
        get { return _onFire; }
        set { _onFire = value; }
    }
}
