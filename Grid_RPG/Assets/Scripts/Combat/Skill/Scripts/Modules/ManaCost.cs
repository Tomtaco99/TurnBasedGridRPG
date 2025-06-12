using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Mana/Mana cost")]

public class ManaCost : Cost
{

    public int manaDrain;

    public override bool TryApplyCost(Stats stats)
    {
        if (stats.currentMana < manaDrain) return false;
        stats.currentMana -= manaDrain;
        return true;
    }

    public override bool ValidateCost(Stats stats)
    {
        return stats.currentMana > manaDrain;
    }
}
