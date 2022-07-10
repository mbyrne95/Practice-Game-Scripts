using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDebuff : Debuff
{
    public float slowAmount = (float)0.5;

    public SlowDebuff(IEnemy enemy) : base(enemy)
    {

    }

    // Update is called once per frame
    public override void Update()
    {
        float currentMS = enemy.moveSpeed;
        enemy.moveSpeedActive = currentMS * (1 - slowAmount);
    }
}