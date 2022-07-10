using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FinalHour : Ability
{
    [SerializeField]
    private float bonusAd = 55;
    private float parentBaseAD;

    [SerializeField]
    private float tumbleCDR = (float)0.5;

    [SerializeField]
    private Tumble tumble;

    private float tumbleBaseCooldown;

    public override void Activate(GameObject parent)
    {
        tumbleBaseCooldown = tumble.cooldownTime;
        parentBaseAD = parent.GetComponent<Vayne_Shooting>().damage;
        parent.GetComponent<Vayne_Shooting>().damage = parentBaseAD + bonusAd;
        tumble.cooldownTime = (1 - tumbleCDR) * tumbleBaseCooldown;
    }

    public override void BeginCooldown(GameObject parent)
    {
        parent.GetComponent<Vayne_Shooting>().damage = parentBaseAD;
        tumble.cooldownTime = tumbleBaseCooldown;
    }
}
