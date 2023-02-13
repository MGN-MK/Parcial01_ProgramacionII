using UnityEngine;
using System.Collections;

public class EnemyCharacter : Character
{
    public int lv = 20;
    public float maxHP = 50;
    public float ap = 40;
    public float dp = 30;
    public float speed = 15;

    void Awake()
    {
        stats = new Stats(lv, maxHP, ap, dp, speed);
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

        combatManager.OnCharacterSkill(skill);
    }
}
