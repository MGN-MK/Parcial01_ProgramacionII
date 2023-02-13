using UnityEngine;

/// <summary>
/// Apply Status Condition Skill
/// </summary>
public class ApplySCSkill : Skill
{
    private StatusCondition condition;

    protected override void OnRun(Character target)
    {
        if (condition == null)
        {
            condition = GetComponentInChildren<StatusCondition>();

            if (condition.gameObject == gameObject)
            {
                throw new System.InvalidOperationException(
                    "The StatusCondition should be a child of the skill object because it needs to be cloned"
                );
            }
        }

        if (target.GetCurrentCondition())
        {
            messages.Enqueue("Falló!");
            return;
        }

        // Clonamos la status condition
        GameObject go = Instantiate(condition.gameObject);
        go.transform.SetParent(target.transform);

        // Asignamos el cambio de estado al receptor
        StatusCondition clonedCondition = go.GetComponent<StatusCondition>();
        clonedCondition.SetReceiver(target);
        target.condition = clonedCondition;

        this.messages.Enqueue(clonedCondition.GetReceptionMessage());
    }
}