using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public float rotSpeed = 1f;
    float angle = 0f;

    public Weapon weaponGiven;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        angle += rotSpeed;
        if(angle >= 360f){
            angle = 0f;
        }
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }

    public void SelfDestruct()
    {
        Destroy(gameObject);
    }
}
