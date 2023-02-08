using UnityEngine;
using UnityEngine.UI;

public class StatusPanel : MonoBehaviour
{
    public Text nameLabel;
    public Text levelLabel;

    public Slider healthSlider;
    public Image healthSliderBar;
    public Text healthLabel;

    public void SetStats(string name, Stats stats)
    {
        nameLabel.text = name;

        levelLabel.text ="LV. " + stats.lv;
        SetHealth(stats.hp, stats.maxHP);
    }

    public void SetHealth(float health, float maxHealth)
    {
        healthLabel.text = Mathf.RoundToInt(health) + "/" + maxHealth;
        float percentage = health / maxHealth;

        healthSlider.value = percentage;

        if (percentage < 0.33f)
        {
            healthSliderBar.color = Color.red;
        }
    }
}
