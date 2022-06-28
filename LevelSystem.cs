using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{

    public int level;
    public float currentXP;
    public float requiredXP;

    [Header("Multipliers")]
    [Range(1, 300)]
    public float additionMultiplier = 300;
    [Range(2, 4)]
    public float powerMultiplier = 2;
    [Range(7, 14)]
    public float divisionMultiplier = 7;

    //private float totalXP = 0;
    private float lerpTimer;
    private float delayTimer;

    [Header("UI")]
    public Image frontXpBar;
    public Image backXpBar;



    // Start is called before the first frame update
    void Start()
    {
        frontXpBar.fillAmount = currentXP / requiredXP;
        backXpBar.fillAmount = currentXP / requiredXP;
        requiredXP = CalculateRequiredXP();
        //totalXP = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateXpUI();

        if (Input.GetKeyDown(KeyCode.E))
        {
            GainExperienceFlatRate(20);
        }

        if (currentXP > requiredXP)
        {
            LevelUp();
        }
    }

    public void UpdateXpUI()
    {
        float xpFraction = currentXP / requiredXP;
        float FXP = frontXpBar.fillAmount;

        if(FXP < xpFraction)
        {
            delayTimer += Time.deltaTime;
            backXpBar.fillAmount = xpFraction;

            if(delayTimer > 1)
            {
                lerpTimer += Time.deltaTime;
                float percentComplete = lerpTimer / 1;
                frontXpBar.fillAmount = Mathf.Lerp(FXP, backXpBar.fillAmount, percentComplete);
            }
        }
    }

    public void GainExperienceFlatRate(float xpGained)
    {
        currentXP += xpGained;
        lerpTimer = 0;
        delayTimer = 0;
    }

    //do level up shit here
    public void LevelUp()
    {
        //totalXP += currentXP;
        currentXP = Mathf.RoundToInt(currentXP - requiredXP);
        frontXpBar.fillAmount = 0; 
        backXpBar.fillAmount = 0;

        level++;

        requiredXP = CalculateRequiredXP();
    }


    //calculate required XP - this follows runescapes level system
    private int CalculateRequiredXP()
    {
        int solveForRequiredXP = 0;
        for (int levelCycle = 1; levelCycle <= level; levelCycle++)
        {
            solveForRequiredXP += (int)Mathf.Floor(levelCycle + additionMultiplier * Mathf.Pow(powerMultiplier, levelCycle / divisionMultiplier));
        }
        return solveForRequiredXP / 4;
    }
}
