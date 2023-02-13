using UnityEngine;
using System.Collections.Generic;

public class EnemiesPanel : MonoBehaviour
{
    public GameObject sampleButton;

    private PlayerCharacter targetCharacter;
    private List<Character> targets;

    private List<EnemyButtonUI> buttons;

    private float baseHeight;
    private RectTransform rectTransform;

    void Awake()
    {
        targets = new List<Character>();
        buttons = new List<EnemyButtonUI>();

        rectTransform = GetComponent<RectTransform>();
        baseHeight = rectTransform.rect.height;

        // Añadimos el botón de ejemplo como el primer botón disponible
        EnemyButtonUI btn = InsertNewButton(sampleButton, 0);
        btn.Hide();

        Hide();
    }

    public void OnTargetButtonClick(int index)
    {
        Character target = targets[index];

        targetCharacter.SetTargetAndAttack(target);
    }

    public void Show(PlayerCharacter playerFighter, Character[] targets)
    {
        gameObject.SetActive(true);

        targetCharacter = playerFighter;

        int btnIndex = 0;

        foreach (var target in targets)
        {
            EnemyButtonUI btn = ActivateNextButton(btnIndex);
            btn.SetText(target.nameID);

            this.targets.Add(target);

            btnIndex++;
        }

            rectTransform.sizeDelta = new Vector2(
            rectTransform.rect.width,
            baseHeight * targets.Length
        );
    }

    public void Hide()
    {
        gameObject.SetActive(false);

        foreach (var btn in buttons)
        {
            btn.Hide();
        }

        targets.Clear();
    }

    private EnemyButtonUI ActivateNextButton(int index)
    {
        foreach (var btn in buttons)
        {
            if (btn.index == index)
            {
                btn.Show();
                return btn;
            }
        }

        // Clonamos el botón de ejemplo
        GameObject btnGO = Instantiate(sampleButton);
        btnGO.transform.SetParent(transform);
        btnGO.transform.localScale = Vector3.one;

        // Lo añadimos como nuevo botón disponible
        EnemyButtonUI enemyBtn = InsertNewButton(btnGO, index);
        enemyBtn.Show();

        return enemyBtn;
    }

    private EnemyButtonUI InsertNewButton(GameObject btnGO, int index)
    {
        EnemyButtonUI btn = new EnemyButtonUI(btnGO, index);
        btn.button.onClick.AddListener(() => { OnTargetButtonClick(btn.index); });

        buttons.Add(btn);

        return btn;
    }
}
