using UnityEngine;

public enum HealthModType
{
    STAT_BASED, FIXED, PERCENTAGE
}

public class HealthModSkill : Skill
{
    [Header("Health Mod")]
    public float amount;

    public HealthModType modType;

    [Range(0f, 1f)]
    public float critChance = 0;

    protected override void OnRun(Character target)
    {
        float amount = GetModification(target);

        float dice = Random.Range(0f, 1f);

        if (dice <= critChance)
        {
            amount *= 2f;
            messages.Enqueue("Golpe crítico!");
        }

        target.ModifyHealth(amount);
    }

    public float GetModification(Character target)
    {
        switch (modType)
        {
            case HealthModType.STAT_BASED:
                Stats senderStats = sender.GetCurrentStats();
                Stats targetStats = target.GetCurrentStats();

                // Fórmula de pokemon: https://bulbapedia.bulbagarden.net/wiki/Damage
                float rawDamage = (((2 * senderStats.lv) / 5) + 2) * this.amount * (senderStats.ap / targetStats.dp);

                return (rawDamage / 50) + 2;
            case HealthModType.FIXED:
                return amount;
            case HealthModType.PERCENTAGE:
                Stats tStats = target.GetCurrentStats();

                return tStats.maxHP * amount;
        }

        throw new System.InvalidOperationException("HealthModSkill::GetDamage. Unreachable!");
    }
}