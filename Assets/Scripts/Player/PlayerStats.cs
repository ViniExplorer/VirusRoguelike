using System.Collections;
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

    public float energyRefillPoints = 10f;
    public float eRefInterval = 1f;
    public float hpRefillPoints = 0.2f;
    public float hpRefInterval = 0.1f;

    bool alive = true;

    public MainPlayerControl controls;

    #endregion

    #region Inventory

    public int selSlot = 0;

    public int slotsUsed = 0;

    public Weapon[] slots = new Weapon[5];
    public Sprite[] slotPics = new Sprite[5];
    

    #endregion

    IEnumerator EnergyRefill(){
        while(alive)
        {
            if(energy < maxEnergy){
                if(!controls.speeding){
                    if(energy + energyRefillPoints > maxEnergy){
                        energy = maxEnergy;
                    }
                    else{
                        energy += energyRefillPoints;
                    }
                }
            }

            yield return new WaitForSeconds(eRefInterval);
        }
    }
    IEnumerator HPRefill(){
        while(alive)
        {
            if(hp < maxHp){
                if(hp + hpRefillPoints > maxHp){
                    hp = maxHp;
                }
                else{
                    hp += hpRefillPoints;
                }
            }

            yield return new WaitForSeconds(hpRefInterval);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnergyRefill());
        StartCoroutine(HPRefill());
    }
}
