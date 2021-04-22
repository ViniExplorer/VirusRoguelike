using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerControl : MonoBehaviour
{
    #region Components and Main Variables
    
    public Rigidbody2D rb;
    public PlayerStats stats;

    //////// KEY VARIABLES /////////
    public float speed = 5f;

    // Multiplier for when the player is sprinting
    public float speedingMod = 1.5f;

    public float speedECost = 1f;

    // if the player is running
    public bool speeding = false;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){

        }
        
        if (stats.selSlot + (int)Input.mouseScrollDelta.y < 0){
            stats.selSlot = 4;
        }
        else if (stats.selSlot + (int)Input.mouseScrollDelta.y > 4){
            stats.selSlot = 0;
        }
        else {
            stats.selSlot += (int)Input.mouseScrollDelta.y;
        }

        // Making the player move according to whether they are moving or not
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed * Time.deltaTime,
                                    Input.GetAxis("Vertical") * speed * Time.deltaTime);
        
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

        if(speeding && stats.energy < 0f){
            speed /= speedingMod;
            speeding = false;
        }
    }
}
