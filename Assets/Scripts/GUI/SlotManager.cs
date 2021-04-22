using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    public PlayerStats stats;

    GameObject[] slots;

    public RectTransform selSlotSprite;

    // Start is called before the first frame update
    void Start()
    {
        slots = GameObject.FindGameObjectsWithTag("Slot");
        for(int i = 0; i < slots.Length; i++){
            slots[i].GetComponent<Image>().sprite = stats.slotPics[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        selSlotSprite.position = slots[stats.selSlot].transform.position;
    }

    public void AddItem(int index, Sprite sprite){
        slots[index].transform.GetChild(0).GetComponent<Image>().sprite = sprite;
    }
}
