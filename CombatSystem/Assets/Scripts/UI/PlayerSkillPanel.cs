using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillPanel : MonoBehaviour
{
    public GameObject[] skillButtons;
    public Text[] skillButtonLabels;

    private PlayerCharacter targetChr;

    void Awake()
    {
        Hide();
    }

    public void ConfigureButton(int index, string skillName)
    {
        skillButtons[index].SetActive(true);
        skillButtonLabels[index].text = skillName;
    }

    public void OnSkillButtonClick(int index)
    {
        targetChr.ExecuteSkill(index);
    }

    public void ShowForPlayer(PlayerCharacter newTarget)
    {
        gameObject.SetActive(true);

        targetChr = newTarget;
    }

    public void Hide()
    {
        gameObject.SetActive(false);

        foreach (var btn in skillButtons)
        {
            btn.SetActive(false);
        }
    }


}
