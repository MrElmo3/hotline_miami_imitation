using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Handgun,
    Shotgun,
    Automatic,
    Melee
}

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private Transform firePivot;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float timeBetweenShots;
    [SerializeField] private float automaticFireRate;
    [SerializeField] private int dispersionAngle;
    [SerializeField] private int numBullets = 1;
    [SerializeField] private WeaponType weaponType;

    private bool shootingInDelay;
    private bool isShooting;
    private float nextTimeToShot;
    private float angleIncrement;
    public int ammo;

    public WeaponType WeaponType
    {
        get => weaponType;

        set
        {
            weaponType = value;

            if (value == WeaponType.Automatic)
                ammo = 24;
            else if (value == WeaponType.Shotgun)
                ammo = 6;
            else if (value == WeaponType.Handgun)
                ammo = 8;
        }
    }
    void Start()
    {
        angleIncrement = dispersionAngle / numBullets*1.0f;
    }

    void Update()
    {
        GetAttackInput();
        TryShoot();
    }

    private void GetAttackInput()
    {
        if (WeaponType != WeaponType.Automatic)
        {
            isShooting = Input.GetMouseButtonDown(0);

            if (Input.GetMouseButtonDown(0) && ammo > 0)
                shootingInDelay = true;

            return;
        }
        isShooting = Input.GetMouseButton(0);
    }

    private void TryShoot()
    {
        Vector3 adjustmentX;
        Vector3 adjustmentY;

        Vector3 position = firePivot.transform.position;
        Quaternion rotation = firePivot.transform.rotation;
        Vector2 scale = bulletPrefab.transform.localScale;
        float angleRad = angleIncrement * Mathf.Deg2Rad;
        float sin;
        float cos;
        Quaternion rotateAngle;

        bool canShoot = Time.time >= nextTimeToShot && ammo > 0;
        
        if (WeaponType == WeaponType.Handgun)
        {
            if ((isShooting || shootingInDelay) && canShoot)
            {
                nextTimeToShot = Time.time + timeBetweenShots;
                Instantiate(bulletPrefab, position, rotation);
                shootingInDelay = false;
                ammo--;
            }
        }
        else if (WeaponType == WeaponType.Shotgun)
        {
            if ((isShooting || shootingInDelay) && canShoot)
            {
                nextTimeToShot = Time.time + timeBetweenShots;
                for (int i = 0; i < numBullets / 2; i++)
                {
                    sin = Mathf.Sin(angleRad * (i + 1));
                    cos = Mathf.Cos(angleRad * (i + 1));
                    rotateAngle = Quaternion.Euler(0, 0, angleIncrement * (i + 1));

                    adjustmentX = firePivot.transform.right * sin  * scale.y;
                    adjustmentY = firePivot.transform.up * (scale.y - cos * scale.y);

                    Instantiate(bulletPrefab,
                        position - adjustmentY - adjustmentX,
                        rotation * rotateAngle );
                }

                Instantiate(bulletPrefab, position, rotation);

                for (int i = 0; i < numBullets / 2; i++)
                {
                    sin = Mathf.Sin(angleRad * (i + 1));
                    cos = Mathf.Cos(angleRad * (i + 1));
                    rotateAngle = Quaternion.Euler(0, 0, angleIncrement * (-i - 1));

                    adjustmentX = firePivot.transform.right * sin * scale.y;
                    adjustmentY = firePivot.transform.up * (scale.y - cos * scale.y);

                    Instantiate(bulletPrefab,
                        position - adjustmentY + adjustmentX,
                        rotation * rotateAngle);
                }
                shootingInDelay = false;
                ammo--;
            }
        }
        else if (WeaponType == WeaponType.Automatic)
        {
            if (isShooting && canShoot)
            {
                nextTimeToShot = Time.time + automaticFireRate;
                Instantiate(bulletPrefab, position, rotation);
                ammo--;
            }
        }
    }
}
