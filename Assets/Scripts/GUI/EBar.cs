using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBar : MonoBehaviour
{
    public PlayerStats pStats;

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(pStats.energy / pStats.maxEnergy, 1f, 1f);
    }
}
