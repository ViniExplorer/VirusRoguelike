using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
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

    public float dashForce = 20f;
    
    public IEnumerator Dash(Vector2 dir, Rigidbody2D rb, Transform t)
    {
        var initPos = t.position;

        float distance = Vector2.Distance(t.position, dir * dashForce * 10f);

        while (distance > 0.1f)
        {
            Vector2 newPosition = Vector2.MoveTowards(rb.position, initPos, dashForce * 10f * Time.deltaTime);
            rb.MovePosition(newPosition);

            distance = Vector2.Distance(t.position, initPos); 

            yield return null;
        }
    }

    public void Shoot(Vector2 startPos, float rotation, float bulletForce)
    {
        if (type == WeaponType.Shooting)
        {
            GameObject bullet = Object.Instantiate(bulletPrefab, startPos, Quaternion.Euler(new Vector3(0f, 0f, rotation)));
            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * bulletForce, ForceMode2D.Impulse);
        }
    }
    /// <summary>
    /// Attack, with parameters for a melee attack
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="rb"></param>
    public void Attack(Vector2 dir, Rigidbody2D rb, MainPlayerControl ctrl)
    {
        if (type == WeaponType.Melee)
        {
            ctrl.StartCoroutine(Dash(dir, rb, ctrl.transform));
        }
        else if (type == WeaponType.Shooting) {
            Debug.LogError("Error: Wrong weapon or parameters for attack. Try again.");
        }
    }
    /// <summary>
    /// Attack, with paramenters for a ranged attack
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="startPos"></param>
    /// <param name="rotation"></param>
    /// <param name="bulletForce"></param>
    public void Attack(Vector2 startPos, float rotation, float bulletForce)
    {
        if (type == WeaponType.Melee)
        {
            Debug.LogError("Error: Wrong weapon or parameters for attack. Try again.");
        }
        else if (type == WeaponType.Melee)
        {
            Shoot(startPos, rotation, bulletForce);
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
