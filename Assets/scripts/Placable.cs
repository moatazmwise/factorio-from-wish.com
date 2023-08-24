using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum direction
{
    up,
    down,
    left,
    right
}

public class Placable : MonoBehaviour
{
    public direction direction = direction.up;
    public GameObject placedObj = null;
    public bool solid = true;

    void FixedUpdate()
    {
        Action();
    }

    public virtual void Action() 
    {  
        
    }
}
