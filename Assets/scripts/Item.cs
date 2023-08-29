using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Item
{
    public int id = 0;
    public int stackSize = 50;
    public int stack = 0;
    public bool wearable = false;
    public bool interactable = false;

    public int AddToStack(Item item)
    {
        if (item is not null)
        {
            if (item.id == this.id)
            {
                if (this.stack + item.stack <= this.stackSize)
                {
                    int transfer = AddToStack(item.stack);
                    item.stack -= transfer;
                    return transfer;
                }
            }
        }
        return 0;
    }

    public int AddToStack(int amount)
    {
        int availableStack = this.stackSize - this.stack;
        int transfer = Mathf.Min(availableStack, amount);
        this.stack += transfer;
        return transfer;
    }

    public int RemoveFromStack(int amount)
    {
        int transfer = Mathf.Min(amount, this.stack);
        this.stack -= transfer;
        return transfer;
    }

    public Item Split()
    {
        int amount = this.stack / 2;
        if (amount > 0)
        {
            this.stack -= amount;
            return new Item() { id = this.id, stack = amount, stackSize = this.stackSize, wearable = this.wearable, interactable = this.interactable };
        }
        return new Item() { stack = 0 };
    }
}
