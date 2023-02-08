using UnityEngine;
using System.Collections.Generic;

public abstract class Character : MonoBehaviour
{
    public Team team;

    public string nameID;
    public StatusPanel statusPanel;

    public CombatManager combatManager;

    public List<StatusMod> statusMods;

    protected Stats stats;

    protected Skill[] skills;

    public StatusCondition condition;

    public bool isAlive
    {
        get => stats.hp > 0;
    }

    protected virtual void Start()
    {
        statusPanel.SetStats(nameID, stats);
        skills = GetComponentsInChildren<Skill>();

        statusMods = new List<StatusMod>();
    }

    protected void AutoConfigureSkillTargeting(Skill skill)
    {
        skill.SetEmitter(this);

        switch (skill.targeting)
        {
            case SkillTargeting.AUTO:
                skill.AddReceiver(this);
                break;
            case SkillTargeting.ALL_ALLIES:
                Character[] allies = combatManager.GetAllyTeam();
                foreach (var target in allies)
                {
                    skill.AddReceiver(target);
                }

                break;
            case SkillTargeting.ALL_OPPONENTS:
                Character[] enemies = combatManager.GetOpposingTeam();
                foreach (var target in enemies)
                {
                    skill.AddReceiver(target);
                }
                break;

            case SkillTargeting.SINGLE_ALLY:
            case SkillTargeting.SINGLE_OPPONENT:
                throw new System.InvalidOperationException("Unimplemented! This skill needs manual targeting.");
        }
    }

    protected Character[] GetSkillTargets(Skill skill)
    {
        switch (skill.targeting)
        {
            case SkillTargeting.AUTO:
            case SkillTargeting.ALL_ALLIES:
            case SkillTargeting.ALL_OPPONENTS:
                throw new System.InvalidOperationException("Unimplemented! This skill doesn't need manual targeting.");

            case SkillTargeting.SINGLE_ALLY:
                return combatManager.GetAllyTeam();
            case SkillTargeting.SINGLE_OPPONENT:
                return combatManager.GetOpposingTeam();
        }

        // Esto no debería ejecutarse nunca pero hay que ponerlo para hacer al compilador feliz.
        throw new System.InvalidOperationException("Fighter::GetSkillTargets. Unreachable!");
    }

    protected void Die()
    {
        statusPanel.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public void ModifyHealth(float amount)
    {
        stats.hp = Mathf.Clamp(stats.hp + amount, 0f, stats.maxHP);

        stats.hp = Mathf.Round(stats.hp);
        statusPanel.SetHealth(stats.hp, stats.maxHP);

        if (isAlive == false)
        {
            Invoke("Die", 0.75f);
        }
    }

    public Stats GetCurrentStats()
    {
        Stats modedStats = stats;

        foreach (var mod in statusMods)
        {
            modedStats = mod.Apply(modedStats);
        }

        return modedStats;
    }

    public StatusCondition GetCurrentCondition()
    {
        if (condition != null && condition.hasExpired)
        {
            Destroy(condition.gameObject);
            condition = null;
        }

        return condition;
    }

    public abstract void InitTurn();
}
