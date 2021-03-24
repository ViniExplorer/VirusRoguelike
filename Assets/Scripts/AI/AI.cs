using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    /*
     * The Fighter AI has three phases:
     * - Looking for a new enemy;
     * - Going to the enemy;
     * - attacking the enemy;
     * 
     * In the search phase, the AI looks
     * for the nearest enemy, and while the 
     * virus is attacking  or going to an enemy, 
     * if the enemy goes to a certain distance, 
     * the virus abandons that enemy and looks for 
     * a closer one.
     * 
     * In the moving phase, the AI constantly
     * check the target's position and moves towards
     * it.
     * 
     * In the attack phase, the AI keeps attacking
     * the target until it's dead or runs away.
     */

    // STATS
    public float hp;
    public float maxHP;

    // Components for collision and movement
    public Collider2D col;
    public Rigidbody2D rb;
    public float speed;

    // Enemy info
    public string targetEnemyTag;
    GameObject targetEnemy;

    // Attack variables
    public float dashForce;
    bool isAttacking;    
    public float attackDelay;

    // Enemy detecting variables
    public float minDisFromEnemy;
    public float maxDisFromEnemy;

    // Weapon variables
    public Weapon weapon;
    public float weaponDmg;

    public float secsTillAtkReset = 2f;

    bool isMoving = false;
    bool furtherThanMaxDis;

    IEnumerator MoveToTarget()
    {
        print("MOVING...");
        isMoving = true;

        float distance = Vector2.Distance(transform.position, targetEnemy.transform.position);

        while (distance > minDisFromEnemy)
        {
            Vector2 newPosition = Vector2.MoveTowards(rb.position, targetEnemy.transform.position, speed * Time.deltaTime);
            rb.MovePosition(newPosition);

            distance = Vector2.Distance(transform.position, targetEnemy.transform.position);

            yield return null;
        }
        try
        {
            StopCoroutine(Attack());
        } finally {
            furtherThanMaxDis = false;
            StartCoroutine(Attack());
        }

        yield return null;
    }

    IEnumerator Attack()
    {
        StopCoroutine(MoveToTarget());
        isMoving = false;
        while (targetEnemy != null)
        {
            print("ATTACKING!!!!");

            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;

            Vector3 initPos = transform.position;
            MeleeAttack();
            
            while (isAttacking)
            {
                yield return null;
            }

            print("GOING BACK TO INITIAL POSITION...");

            ///// RESETING ROTATION AND PREVENTING ANY FURTHER ROTATIONS /////
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            transform.rotation = Quaternion.identity;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;

            ///// MOVING BACK TO INITIAL POSITION /////
            
            float distance = Vector2.Distance(transform.position, initPos);

            while (distance > 0.1f)
            {
                Vector2 newPosition = Vector2.MoveTowards(rb.position, initPos, speed * Time.deltaTime);
                rb.MovePosition(newPosition);

                distance = Vector2.Distance(transform.position, initPos);

                yield return null;
            }

            yield return new WaitForSeconds(attackDelay);
        }
    }

    void MeleeAttack()
    {
        Vector3 lookDir = targetEnemy.transform.position - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        rb.constraints = new RigidbodyConstraints2D();

        rb.rotation = angle; // Making the circle face the enemy

        isAttacking = true;
        rb.AddForce(lookDir * dashForce, ForceMode2D.Impulse);

        StartCoroutine(CheckForNoCollision());
    }

    IEnumerator CheckForNoCollision()
    {
        print("No collision?");
        yield return new WaitForSeconds(secsTillAtkReset);

        if (isAttacking)
        {
            isAttacking = false;
            print("looks like no collision. Resetting :(");
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == targetEnemyTag)
        {
            if (isAttacking)
            {
                try
                {
                    collision.gameObject.GetComponent<AI>().hp -= weapon.damage;
                    isAttacking = false;
                }
                catch
                {
                    collision.gameObject.GetComponent<PlayerStats>().hp -= weapon.damage;
                    isAttacking = false;
                }
            }
        }
    }

    private void Start()
    {
        weapon = new Weapon(weaponDmg);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttacking){
            
            if (targetEnemy == null)
            {
                print("FINDING ENEMY");
                StopAllCoroutines();
                GameObject[] enemies = GameObject.FindGameObjectsWithTag(targetEnemyTag);
                float minDist = Mathf.Infinity;
                GameObject bestTarget = null;
                foreach (GameObject t in enemies)
                {
                    float dist = Vector3.Distance(t.transform.position, transform.position);
                    if (dist < minDist)
                    {
                        bestTarget = t;
                        minDist = dist;
                    }
                }
                if(minDist > maxDisFromEnemy){
                    furtherThanMaxDis = true;
                } else {
                    furtherThanMaxDis = false;
                }
                targetEnemy = bestTarget;
                print("FOUND ENEMY. MOVING TO TARGET.");
                StartCoroutine(MoveToTarget());
            }
            else
            {
                if (Vector2.Distance(targetEnemy.transform.position, transform.position) > maxDisFromEnemy && !furtherThanMaxDis)
                {
                    print("TARGET TOO FAR, FIND A NEW ENEMY");
                    targetEnemy = null;
                }
                else if (Vector2.Distance(targetEnemy.transform.position, transform.position) > minDisFromEnemy){
                    if(!isMoving){
                        rb.velocity = Vector2.zero;
                        rb.angularVelocity = 0f;
                        transform.rotation = Quaternion.identity;
                        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                        StopAllCoroutines();
                        StartCoroutine(MoveToTarget());
                    }
                }
            }

        }
    }
}
