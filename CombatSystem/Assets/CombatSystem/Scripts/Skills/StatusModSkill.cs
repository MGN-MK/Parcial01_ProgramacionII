using UnityEngine;

public class StatusModSkill : Skill
{
    [Header("Status mod skill")]
    public string message;

    protected StatusMod mod;

    protected override void OnRun(Character target)
    {
        if (mod == null)
        {
            mod = GetComponent<StatusMod>();
        }


        messages.Enqueue(message.Replace("{target}", target.nameID));

        target.statusMods.Add(mod);
    }
}
