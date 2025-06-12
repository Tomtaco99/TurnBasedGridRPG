using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Skills/new Skill")]
public class Skill : ScriptableObject
{
    public string description;

    public Cost cost;
    public CastEffect castEffect;
    public HitEffect hitEffect;
    public MoveEffect moveEffect;

    public HealEffect healEffect;

    public Effect extraEffects;


    public Target targetPolicy;

    private bool skillSelected = false;
    public bool SkillSelected { get { return skillSelected; } set { skillSelected = value; } }

    TargetGroup targets = new TargetGroup();
    public virtual void Use()
    {
        if (hitEffect != null)
            hitEffect.ApplyHit(targets);
        if (moveEffect != null)
            moveEffect.ApplyHit(targets);
        if (healEffect != null)
            healEffect.ApplyHit(targets);

    }

    public virtual Tile.TileObject[] GetTargets(Vector3 worldPosition)
    {
        //targets = new TargetGroup();
        targets = targetPolicy.GetTargets(worldPosition);
        if (targets.Tiles.Length > 0)
        {
            for (int i = 0; i < targets.Tiles.Length; i++)
            {
                if (targets.Tiles[i] != null)
                    targets.Tiles[i].TargetTile();
            }
            //Use();
            return targets.Tiles;
        }
        return targets.Tiles;
    }


}
