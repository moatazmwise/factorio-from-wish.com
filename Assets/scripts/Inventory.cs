using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public int inventorySize = 50;
    public Item[] inventory;

    public Inventory(int inventorySize)
    {
        this.inventorySize = inventorySize;
        inventory = new Item[this.inventorySize];
    }

    private void Update()
    {
        
    }

    public int Insert(Item item)
    {
        int initialStack = item.stack;
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] is not null && inventory[i].id == item.id)
            {
                inventory[i].AddToStack(item);
            }
            if (item.stack == 0) return initialStack;
        }
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] is null)
            {
                inventory[i] = item;
                return initialStack;
            }
        }
        return initialStack - item.stack;
    }

    public int Remove(int id, int amount)
    {
        int extracted = 0;
        for (int i = 0; i < inventory.Length; i++)
        {
            extracted += RemoveAt(i, amount - extracted);
            if (extracted == amount) return extracted;
        }
        return extracted;
    }

    public int RemoveAt(int index, int amount)
    {
        if (inventory[index] is null) return 0;
        int removed = inventory[index].RemoveFromStack(amount);
        if (inventory[index].stack == 0) inventory[index] = null;
        return removed;
    }

    public int ExtractFromItem(Item item, int amount)
    {
        int extracted = Remove(item.id, amount);
        item.AddToStack(extracted);
        return extracted;
    }
    public Item ExtractItem(int index)
    {
        if (inventory[index] is null) return null;
        Item extracted = inventory[index];
        inventory[index] = null;
        return extracted;
    }

    
}
