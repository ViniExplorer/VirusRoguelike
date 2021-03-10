using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerControl : MonoBehaviour
{
    #region Components and Main Variables
    
    public Rigidbody2D rb;

    //////// KEY VARIABLES /////////
    public float speed = 5f;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Making the player move according to whether they are moving or not
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed * Time.deltaTime,
                                    Input.GetAxis("Vertical") * speed * Time.deltaTime);
    }
}
