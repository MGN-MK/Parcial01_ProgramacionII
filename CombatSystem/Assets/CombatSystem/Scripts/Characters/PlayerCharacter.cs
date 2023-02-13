using UnityEngine;

public class PlayerCharacter : Character
{
    [Header("UI")]
    public PlayerSkillPanel skillPanel;
    public EnemiesPanel enemiesPanel;

    private Skill skillToBeExecuted;

    public int lv = 21;
    public float maxHP = 60;
    public float ap = 50;
    public float dp = 45;
    public float speed = 20;

    void Awake()
    {
        stats = new Stats(lv, maxHP, ap, dp, speed);
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

            combatManager.OnCharacterSkill(skillToBeExecuted);
            skillPanel.Hide();
            enemiesPanel.Hide();
        }
    }

    public void SetTargetAndAttack(Character enemyFigther)
    {
        skillToBeExecuted.AddReceiver(enemyFigther);

        combatManager.OnCharacterSkill(skillToBeExecuted);

        skillPanel.Hide();
        enemiesPanel.Hide();
    }
}
