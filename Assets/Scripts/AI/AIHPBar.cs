using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHPBar : MonoBehaviour
{
    public AI entity;
    float initScaleX, initScaleY;

    // Start is called before the first frame update
    void Start()
    {
        initScaleX = transform.localScale.x;
        initScaleY = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (entity == null){
            Destroy(transform.parent.gameObject);
        }
        float newScale = initScaleX * (entity.hp / entity.maxHP);
        transform.localScale = new Vector3(newScale, initScaleY, 1f);
    }
}
