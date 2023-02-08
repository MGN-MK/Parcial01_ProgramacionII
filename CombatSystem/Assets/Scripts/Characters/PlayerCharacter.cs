using UnityEngine;

public class PlayerCharacter : Character
{
    [Header("UI")]
    public PlayerSkillPanel skillPanel;
    public EnemiesPanel enemiesPanel;

    private Skill skillToBeExecuted;

    void Awake()
    {
        stats = new Stats(21, 60, 50, 45, 20, 20);
    }

    public override void InitTurn()
    {
        skillPanel.ShowForPlayer(this);

        for (int i = 0; i < skills.Length; i++)
        {
            this.skillPanel.ConfigureButton(i, this.skills[i].skillName);
        }
    }

    public void ExecuteSkill(int index)
    {
        skillToBeExecuted = this.skills[index];
        skillToBeExecuted.SetEmitter(this);

        if (skillToBeExecuted.needsManualTargeting)
        {
            Character[] targets = GetSkillTargets(skillToBeExecuted);
            enemiesPanel.Show(this, targets);
        }
        else
        {
            AutoConfigureSkillTargeting(skillToBeExecuted);

            combatManager.OnFighterSkill(skillToBeExecuted);
            skillPanel.Hide();
        }
    }

    public void SetTargetAndAttack(Character enemyFigther)
    {
        skillToBeExecuted.AddReceiver(enemyFigther);

        combatManager.OnFighterSkill(skillToBeExecuted);

        skillPanel.Hide();
        enemiesPanel.Hide();
    }
}
