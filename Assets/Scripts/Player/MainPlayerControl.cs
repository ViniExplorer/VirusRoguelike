using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerControl : MonoBehaviour
{
    #region Components and Main Variables
    
    public Rigidbody2D rb;
    public PlayerStats stats;
    public SlotManager slotManager;

    //////// KEY VARIABLES /////////
    public float speed = 5f;

    // Multiplier for when the player is sprinting
    public float speedingMod = 1.5f;

    public float speedECost = 1f;

    // if the player is running
    public bool speeding = false;

    // Attacking variables
    bool isAttacking = false;
    public float attackLength = .2f;
    float currentDamage;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (stats.selSlot + (int)Input.mouseScrollDelta.y < 0){
            stats.selSlot = 4;
        }
        else if (stats.selSlot + (int)Input.mouseScrollDelta.y > 4){
            stats.selSlot = 0;
        }
        else {
            stats.selSlot += (int)Input.mouseScrollDelta.y;
        }

        if (!isAttacking)
        {
            rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed * Time.deltaTime,
                                        Input.GetAxis("Vertical") * speed * Time.deltaTime);
        }
        
        // Checking for sprinting
        if(Input.GetKey(KeyCode.LeftShift)){
            if(stats.energy > 0f){
                if(!speeding){
                    speed *= speedingMod;
                    speeding = true;
                }
                stats.energy -= speedECost * Time.fixedDeltaTime;
            }
        }
        else{
            if(speeding){
                speed /= speedingMod;
                speeding = false;
            }
        }
        
        // Checking if energy is depleted
        if(speeding && stats.energy < 0f){
            speed /= speedingMod;
            speeding = false;
        }

        // Attacking mechanics
        if (Input.GetMouseButtonDown(0))
        {
            Weapon selWeapon = stats.slots[stats.selSlot];
            if (stats.energy - selWeapon.energyPointsNeeded > 0f)
            {
                if(selWeapon.type == Weapon.WeaponType.Melee)
                {
                    isAttacking = true;
                    Vector3 lookDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                    currentDamage = selWeapon.damage;
                    selWeapon.Attack(lookDir, rb);
                    StartCoroutine(AttackingCoolDown());
                    stats.energy -= selWeapon.energyPointsNeeded;

                }
            }
        }
    }

    IEnumerator AttackingCoolDown()
    {
        yield return new WaitForSeconds(attackLength);
        isAttacking = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "EvilBoi" && isAttacking)
        {
            collision.gameObject.GetComponent<AI>().hp -= currentDamage;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Item")
        {
            if(stats.slotsUsed < 5)
            {
                stats.slots[stats.slotsUsed] = collision.GetComponent<Item>().weaponGiven;
                slotManager.AddItem(stats.slotsUsed, stats.slots[stats.slotsUsed].icon);
                stats.slotsUsed++;
                collision.GetComponent<Item>().SelfDestruct();
            }
        }
    }
}
