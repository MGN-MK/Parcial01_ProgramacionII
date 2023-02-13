using UnityEngine;
using System.Collections.Generic;

public abstract class Skill : MonoBehaviour
{
    [Header("Base Skill")]
    public string skillName;
    public float animationDuration;

    public SkillTargeting targeting;

    public GameObject effectPrfb;

    protected Character sender;
    protected List<Character> target;

    protected Queue<string> messages;

    public bool needsManualTargeting
    {
        get
        {
            switch (targeting)
            {
                case SkillTargeting.SINGLE_ALLY:
                case SkillTargeting.SINGLE_OPPONENT:
                    return true;

                default:
                    return false;
            }
        }
    }

    void Awake()
    {
        messages = new Queue<string>();
        target = new List<Character>();
    }

    private void Animate(Character target)
    {
        var go = Instantiate(effectPrfb, target.transform.position, Quaternion.identity);
        Destroy(go, animationDuration);
    }

    public void Run()
    {
        foreach (var target in target)
        {
            Animate(target);
            this.OnRun(target);
        }

        this.target.Clear();
    }

    public void SetEmitter(Character _sender)
    {
        this.sender = _sender;
    }

    public void AddReceiver(Character _targelt)
    {
        target.Add(_targelt);
    }

    public string GetNextMessage()
    {
        if (messages.Count != 0)
            return messages.Dequeue();
        else
            return null;
    }

    protected abstract void OnRun(Character receiver);
}