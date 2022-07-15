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

    [SerializeField]
    private int projectileUpgradeCnt, slowUpgradeCnt, AdUpgradeCnt, AsUpgradeCnt = 0;

    private int[] upgradeCnt = { 0, 0, 0, 0 };

    [SerializeField]
    private Button[] buttons;

    private void Update()
    {

    }

    public void Resume()
    {
        upgradeMenuUI.SetActive(false);
        buttons[0].onClick.RemoveAllListeners();
        buttons[1].onClick.RemoveAllListeners();
        buttons[2].onClick.RemoveAllListeners();

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
        List<int> listnumbers = new List<int>();
        int number;
        for (int i = 0; i < buttons.Length; i++)
        {
            do
            {
                number = Random.Range(0, 4);
            } while (listnumbers.Contains(number) || CheckCurrentCount(number) >= 5);
            listnumbers.Add(number);
        }
        
        buttons[0].GetComponentInChildren<TextMeshProUGUI>().text = IndexToString(listnumbers[0]);
        buttons[0].onClick.AddListener(() => AddBuff(listnumbers[0]));
        buttons[0].onClick.AddListener(() => IncrementUpgrade(listnumbers[0]));
        buttons[0].onClick.AddListener(() => Resume());

        buttons[1].GetComponentInChildren<TextMeshProUGUI>().text = IndexToString(listnumbers[1]);
        buttons[1].onClick.AddListener(() => AddBuff(listnumbers[1]));
        buttons[1].onClick.AddListener(() => IncrementUpgrade(listnumbers[1]));
        buttons[1].onClick.AddListener(() => Resume());

        buttons[2].GetComponentInChildren<TextMeshProUGUI>().text = IndexToString(listnumbers[2]);
        buttons[2].onClick.AddListener(() => AddBuff(listnumbers[2]));
        buttons[2].onClick.AddListener(() => IncrementUpgrade(listnumbers[2]));
        buttons[2].onClick.AddListener(() => Resume());

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

    private void IncrementUpgrade(int i)
    {
        upgradeCnt[i]++;

        switch (i)
        {
            case 0:
                projectileUpgradeCnt++;
                return;
            case 1:
                slowUpgradeCnt++;
                return;
            case 2:
                AdUpgradeCnt++;
                return;
            case 3:
                AsUpgradeCnt++;
                return;
            default:
                return;
        }
    }

    private int CheckCurrentCount(int i)
    {
        switch (i)
        {
            case 0:
                return projectileUpgradeCnt;
            case 1:
                return slowUpgradeCnt;
            case 2:
                return AdUpgradeCnt;
            case 3:
                return AsUpgradeCnt;
            default:
                return AsUpgradeCnt;
        }
    }
}