using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class Grid : MonoBehaviour
{
    public int size = 1000;
    public Ground[,] ground;
    public GameObject[,] placedBlocks;
    public GameObject block;
    public GameObject dirt;
    void Start()
    {
        ground = new Ground[size,size];
        placedBlocks = new GameObject[size,size];

        for (int i = 0; i < size; i++)
        {
            for(int j = 0; j < size; j++)
            {
                PlaceGround(i, j);
                placedBlocks[i, j] = null; 
            }
        }
    }
    
    public void Place(float x, float y, Item item)
    {
        if (Mathf.RoundToInt(x) >= size || Mathf.RoundToInt(x) < 0 || Mathf.RoundToInt(y) >= size || Mathf.RoundToInt(y) < 0)
        {
            Debug.Log("Out of bounds : " + "x = " + Mathf.RoundToInt(x) +",  y = " + Mathf.RoundToInt(y) + ",  size = " + size);
            return;
        }
        if (placedBlocks[Mathf.RoundToInt(x), Mathf.RoundToInt(y)] is null)
        {
            Debug.Log("placing " + (int)x + " , " + (int)y);
            placedBlocks[Mathf.RoundToInt(x), Mathf.RoundToInt(y)] = Instantiate(block, new Vector3(Mathf.RoundToInt(x), Mathf.RoundToInt(y), -1), new Quaternion());
            placedBlocks[Mathf.RoundToInt(x), Mathf.RoundToInt(y)].GetComponent<Placable>().item = item;
            placedBlocks[Mathf.RoundToInt(x), Mathf.RoundToInt(y)].GetComponent<Placable>().spawn();
        }
        
    }

    public void PlaceGround(float x, float y)
    {
        if (Mathf.RoundToInt(x) >= size || Mathf.RoundToInt(x) < 0 || Mathf.RoundToInt(y) >= size || Mathf.RoundToInt(y) < 0)
        {
            Debug.Log("Out of bounds : " + "x = " + Mathf.RoundToInt(x) + ",  y = " + Mathf.RoundToInt(y) + ",  size = " + size);
            return;
        }
        if (ground[Mathf.RoundToInt(x), Mathf.RoundToInt(y)] is null)
        {
            Debug.Log("placing " + (int)x + " , " + (int)y);
            ground[Mathf.RoundToInt(x), Mathf.RoundToInt(y)] = new Ground();
            ground[Mathf.RoundToInt(x), Mathf.RoundToInt(y)].placedGround = Instantiate(dirt, new Vector3(Mathf.RoundToInt(x), Mathf.RoundToInt(y), 0), new Quaternion(),this.transform);
        }

    }

    public void Remove(float x, float y)
    {
        if (Mathf.RoundToInt(x) >= size || Mathf.RoundToInt(x) < 0 || Mathf.RoundToInt(y) >= size || Mathf.RoundToInt(y) < 0)
        {
            Debug.Log("Out of bounds : " + "x = " + Mathf.RoundToInt(x) + ",  y = " + Mathf.RoundToInt(y) + ",  size = " + size);
            return;
        }
        if (placedBlocks[Mathf.RoundToInt(x), Mathf.RoundToInt(y)] is not null)
        {
            Debug.Log("removing " + (int)x + " , " + (int)y);
            GameObject.Destroy(placedBlocks[Mathf.RoundToInt(x), Mathf.RoundToInt(y)]);
            placedBlocks[Mathf.RoundToInt(x), Mathf.RoundToInt(y)] = null;
        }
    }

    public void RotateBlock(float x, float y)
    {
        if (placedBlocks[Mathf.RoundToInt(x), Mathf.RoundToInt(y)] is not null)
        {
            Debug.Log("rotating " + (int)x + " , " + (int)y);
            if (placedBlocks[Mathf.RoundToInt(x), Mathf.RoundToInt(y)].GetComponent<Placable>().direction == direction.left)
            {
                placedBlocks[Mathf.RoundToInt(x), Mathf.RoundToInt(y)].GetComponent<Placable>().direction = 0;
            }
            else placedBlocks[Mathf.RoundToInt(x), Mathf.RoundToInt(y)].GetComponent<Placable>().direction += 1;
            placedBlocks[Mathf.RoundToInt(x), Mathf.RoundToInt(y)].transform.Rotate(0, 0, -90);
        }
    }
}
