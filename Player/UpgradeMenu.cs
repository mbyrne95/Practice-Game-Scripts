using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject player;

    public PowerupEffect[] powerupEffects; 

    public GameObject upgradeMenuUI;

    private int projectileUpgradeCnt, slowUpgradeCnt, AdUpgradeCnt, AsUpgradeCnt = 0;

    [SerializeField]
    private Button[] buttons;

    private void Update()
    {

    }

    public void Resume()
    {
        upgradeMenuUI.SetActive(false);
        Time.timeScale = 1;
        GameIsPaused = false;
    }

    public void Pause()
    {
        populateButtons();
        upgradeMenuUI.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
    }

    public void populateButtons()
    {
        List<int> numbers = new List<int>();
        int number;
        for(int i = 0; i < 3; i++)
        {
            do
            {
                number = Random.Range(0, 4);
            } while (numbers.Contains(number));
            numbers.Add(number);
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            Button upgrade = buttons[i];
            upgrade.GetComponentInChildren<TextMeshProUGUI>().text = IndexToString(numbers[i]);
            upgrade.onClick.AddListener(() => AddBuff(numbers[i]));
            upgrade.onClick.AddListener(() => Resume());

        }
    }

    private string IndexToString(int i) 
    {
        switch (i)
        {
            case 0:
                return "+1 Projectile, +AS, - AD";
            case 1:
                return "Slowing Autos";
            case 2:
                return "+AD, -MS";
            case 3:
                return "+AS, -AD";
            default:
                return "+AS, -AD";

        }
    }

    private void AddBuff(int i)
    {
        powerupEffects[i].Apply(player);
    }
}
