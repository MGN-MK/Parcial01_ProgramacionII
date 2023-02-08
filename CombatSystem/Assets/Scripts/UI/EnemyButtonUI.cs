using UnityEngine;
using UnityEngine.UI;

class EnemyButtonUI
{
    public int index { protected set; get; }
    public Button button { protected set; get; }

    private Text label;
    private GameObject btn;

    public EnemyButtonUI(GameObject btn, int index)
    {
        this.index = index;
        this.btn = btn;

        label = this.btn.GetComponentInChildren<Text>();
        button = this.btn.GetComponentInChildren<Button>();
    }

    public void Show()
    {
        btn.SetActive(true);
    }

    public void Hide()
    {
        btn.SetActive(false);
    }

    public void SetText(string text)
    {
        label.text = text;
    }
}