﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    public PlayerStats pStats;

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(pStats.hp / pStats.maxHp, 1f, 1f);        
    }
}
