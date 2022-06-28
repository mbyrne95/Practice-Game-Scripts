using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject upgradeMenuUI;

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
        upgradeMenuUI.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
    }


}
