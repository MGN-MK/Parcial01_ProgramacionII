using UnityEngine;
using System.Collections;

public class EnemyCharacter : Character
{
    void Awake()
    {
        stats = new Stats(20, 50, 40, 30, 60, 15);
    }

    public override void InitTurn()
    {
        StartCoroutine(IA());
    }

    IEnumerator IA()
    {
        yield return new WaitForSeconds(1f);

        Skill skill = skills[Random.Range(0, skills.Length)];
        skill.SetEmitter(this);

        if (skill.needsManualTargeting)
        {
            Character[] targets = GetSkillTargets(skill);

            Character target = targets[Random.Range(0, targets.Length)];

            skill.AddReceiver(target);
        }
        else
        {
            AutoConfigureSkillTargeting(skill);
        }

        combatManager.OnFighterSkill(skill);
    }
}
