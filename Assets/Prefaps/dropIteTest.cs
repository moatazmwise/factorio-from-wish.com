using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropIteTest : MonoBehaviour
{
    Inventory internalInventory;
    public GameObject droppedItemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        internalInventory = new Inventory(1);
        Item item = new Item();
        item.id = 1;
        item.stack = 5;
        internalInventory.inventory[0] = item;

        Item extracted = internalInventory.ExtractItem(0);
        GameObject dropedItemObject = Instantiate(droppedItemPrefab);
        DroppedItem droppedItem = dropedItemObject.GetComponent<DroppedItem>();
        droppedItem.spawn(extracted);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
