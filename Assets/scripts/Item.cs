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

    public virtual void Action(Placable placable)
    {

    }

    public virtual void PlacableOnTriggerStay(Placable placable, Collider2D collision)
    {

    }

    public virtual void OnPlace(Placable placable)
    {
        BoxCollider2D collider = placable.gameObject.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(1, 1);
    }
}

public class ConveyorBelt : Item
{
    public ConveyorBelt()
    {
        id = 2;
    }

    public override void Action(Placable placable)
    {
        
    }

    public override void PlacableOnTriggerStay(Placable placable, Collider2D collision)
    {
        if (collision.attachedRigidbody)
            collision.attachedRigidbody.velocity = placable.direction;
    }

    public override void OnPlace(Placable placable)
    {
        BoxCollider2D collider = placable.gameObject.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(0.5f, 1.5f);
    }
}

public class RightCurvedConveyorBelt : ConveyorBelt
{
    public RightCurvedConveyorBelt() { 
        id = 3;
    }
}

public class LeftCurvedConveyorBelt : ConveyorBelt
{
    public LeftCurvedConveyorBelt()
    {
        id = 4;
    }
}

public class Machine : Item
{
    float speed = 1;
    Inventory[] inputInventory;
    Inventory[] outputInventory;

    public Machine(int inputSize, int outputSize, int inventoryStackSize)
    {
        inputInventory = new Inventory[inputSize];
        outputInventory = new Inventory[outputSize];

        for (int i = 0; i < inputSize; i++)
        {
            inputInventory[i] = new Inventory(inventoryStackSize);
        }
        for (int i = 0;i < outputSize; i++)
        {
            outputInventory[i] = new Inventory(inventoryStackSize);
        }
    }
}

public class Inserter : Machine
{
    public Inserter(int inputSize, int outputSize, int inventoryStackSize) : base(inputSize, outputSize, inventoryStackSize)
    { }

}