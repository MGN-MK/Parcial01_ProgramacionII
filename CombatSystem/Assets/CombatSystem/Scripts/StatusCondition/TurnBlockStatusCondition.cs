using UnityEngine;

public class TurnBlockStatusCondition : StatusCondition
{
    [Range(0f, 1f)]
    public float blockChance;

    private bool blocked;

    public override bool OnApply()
    {
        blocked = false;

        float dice = Random.Range(0f, 1f);

        if (dice <= this.blockChance)
        {
            blocked = true;
            messages.Enqueue(applyMessage.Replace("{target}", target.nameID));

            return true;
        }

        return false;
    }

    public override bool BlocksTurn() => blocked;
}
