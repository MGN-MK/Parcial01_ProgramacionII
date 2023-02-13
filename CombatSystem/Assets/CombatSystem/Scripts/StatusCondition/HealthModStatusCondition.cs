using UnityEngine;

public class HealthModStatusCondition : StatusCondition
{
    [Header("Health mod")]
    public float percentage;

    public override bool OnApply()
    {
        Stats tStats = target.GetCurrentStats();

        target.ModifyHealth(tStats.maxHP * this.percentage);

        messages.Enqueue(applyMessage.Replace("{target}", target.nameID));

        return true;
    }

    public override bool BlocksTurn() => false;
}
