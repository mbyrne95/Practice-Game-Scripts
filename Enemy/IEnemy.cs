using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    //protected void MoveCharacter(Vector2 dir);

    public float moveSpeed { get; set; }
    public float moveSpeedActive { get; set; }
    public void ContactDamage(int targetHealth);
    public void TakeDamage(float damageAmount);

    public void AddDebuff(Debuff debuff);
    //protected void HandleDebuffs();
}
