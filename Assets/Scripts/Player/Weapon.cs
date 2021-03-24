using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    public enum WeaponType
    {
        Shooting,
        Melee
    };

    public WeaponType type;

    public Sprite icon;

    public float damage;

    public float energyPointsNeeded;

    public GameObject bulletPrefab;

    public void Shoot(Vector2 startPos, float rotation, float bulletForce)
    {
        if (type == WeaponType.Shooting)
        {
            GameObject bullet = Object.Instantiate(bulletPrefab, startPos, Quaternion.Euler(new Vector3(0f, 0f, rotation)));
            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * bulletForce, ForceMode2D.Impulse);
        }
    }

    public Weapon(float dmg, float ePN, Sprite weaponIcon)
    {
        type = WeaponType.Melee;
        damage = dmg;
        energyPointsNeeded = ePN;
        icon = weaponIcon;
    }
    public Weapon(float dmg, float ePN, Sprite weaponIcon, GameObject bullet)
    {
        type = WeaponType.Shooting;
        damage = dmg;
        energyPointsNeeded = ePN;
        icon = weaponIcon;
        bulletPrefab = bullet;
    }
    public Weapon(float dmg, GameObject bullet)
    {
        type = WeaponType.Shooting;
        damage = dmg;
        bulletPrefab = bullet;
    }
    public Weapon(float dmg)
    {
        type = WeaponType.Melee;
        damage = dmg;
    }
}
