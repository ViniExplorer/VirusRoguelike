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

        // Finding the location of the mouse for the cell to face
        Vector3 lookDir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        
        // Calculating the angle
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        
        // Setting the rotation for the cell to face the mouse
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
