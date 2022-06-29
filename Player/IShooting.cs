using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IShooting
{
    public float fireRate { get; set; }
    public float damage { get; set; }
    public void Shoot();

}
