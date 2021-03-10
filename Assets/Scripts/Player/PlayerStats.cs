﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    #region Variables

    // HP so the cell takes damage
    public float hp = 100f;
    public float maxHp = 100f;

    /* Energy for things such as 
     running and a special action */
    public float energy = 150f;
    public float maxEnergy = 150f;

    #endregion

    #region Inventory

    public int slotsUsed = 0;
    public int maxslots = 5;
    public Weapon[] slots = new Weapon[5];
    

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
