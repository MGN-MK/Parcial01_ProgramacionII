using UnityEngine;
using UnityEngine.UI;

public class LogPanel : MonoBehaviour
{
    protected static LogPanel current;

    public Text logLabel;

    void Awake()
    {
        current = this;
    }

    public static void Log(string message)
    {
        current.logLabel.text = message;
    }
}
