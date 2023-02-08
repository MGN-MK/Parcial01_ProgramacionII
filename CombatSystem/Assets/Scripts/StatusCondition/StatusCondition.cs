using UnityEngine;
using System.Collections.Generic;

public abstract class StatusCondition : MonoBehaviour
{
    [Header("Base Status Condition")]
    public GameObject effectPrfb;
    public float animationDuration;

    public string receptionMessage;
    public string applyMessage;
    public string expireMessage;

    public int turnDuration;

    public bool hasExpired { get { return turnDuration <= 0; } }

    protected Queue<string> messages;
    protected Character target;

    public void Awake()
    {
        messages = new Queue<string>();
    }

    public void SetReceiver(Character recv)
    {
        this.target = recv;
    }

    private void Animate()
    {
        var go = Instantiate(effectPrfb, target.transform.position, Quaternion.identity);
        Destroy(go, animationDuration);
    }

    public void Apply()
    {
        if (target == null)
        {
            throw new System.InvalidOperationException("StatusCondition needs a target");
        }

        if (OnApply())
        {
            Animate();
        }

        turnDuration--;

        if (hasExpired)
        {
            messages.Enqueue(expireMessage.Replace("{target}", target.nameID));
        }
    }

    public string GetNextMessage()
    {
        if (messages.Count != 0)
            return messages.Dequeue();
        else
            return null;
    }

    public string GetReceptionMessage()
    {
        return receptionMessage.Replace("{target}", this.target.nameID);
    }

    public abstract bool OnApply();
    public abstract bool BlocksTurn();
}