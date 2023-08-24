using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int id = 0;
    public int stackSize = 50;
    public int stack = 0;
    public bool wearable = false;
    public bool interactable = false;

    public bool AddToStack(Item item)
    {
        if (item is not null)
        {
            if (item.id == this.id)
            {
                if (this.stack + item.stack <= this.stackSize)
                {
                    this.stack += item.stack;
                    return true;
                }
            }
        }
        return false;
    }

    public bool RemoveFromStack(int delta)
    {
        if (this.stack - delta >= 0)
        {
            this.stack -= delta;
            return true;
        }
        return false;
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
