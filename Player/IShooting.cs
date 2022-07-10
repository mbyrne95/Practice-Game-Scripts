using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShooting
{
    public bool slowDebuff { get; set; }

    public float fireRate { get; set; }
    public float damage { get; set; }

    public int numProjectiles { get; set; }

    public void Shoot();

}
