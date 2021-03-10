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
}
