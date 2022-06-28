using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimWeapon : MonoBehaviour
{

    private Transform aimTransform;

    private void Awake()
    {
        aimTransform = transform.Find("Aim");
    }

    public static Vector3 GetMouseWorldPosition(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 vec = worldCamera.ScreenToWorldPoint(screenPosition);
        vec.z = 0f;
        return vec;

    }

    private void Update()
    {
        HandleAiming();
    }

    private void HandleAiming()
    {
        Vector3 mousePosition = GetMouseWorldPosition(Input.mousePosition, Camera.main);
        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
    }
}
