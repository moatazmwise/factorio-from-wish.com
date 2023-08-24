using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public int size = 1000;
    public Ground[,] ground;
    public Placable[,] placables;
    public GameObject obj;
    public GameObject dirt;
    void Start()
    {
        ground = new Ground[size,size];
        placables = new Placable[size,size];

        for (int i = 0; i < size; i++)
        {
            for(int j = 0; j < size; j++)
            {
                PlaceGround(i, j);
                placables[i, j] = null; 
            }
        }
    }
    
    public void Place(float x, float y)
    {
        if (Mathf.RoundToInt(x) >= size || Mathf.RoundToInt(x) < 0 || Mathf.RoundToInt(y) >= size || Mathf.RoundToInt(y) < 0)
        {
            Debug.Log("Out of bounds : " + "x = " + Mathf.RoundToInt(x) +",  y = " + Mathf.RoundToInt(y) + ",  size = " + size);
            return;
        }
        if (placables[Mathf.RoundToInt(x), Mathf.RoundToInt(y)] is null)
        {
            Debug.Log("placing " + (int)x + " , " + (int)y);
            placables[Mathf.RoundToInt(x), Mathf.RoundToInt(y)] = new Wall();
            placables[Mathf.RoundToInt(x), Mathf.RoundToInt(y)].placedObj = Instantiate(obj, new Vector3(Mathf.RoundToInt(x), Mathf.RoundToInt(y), -1), new Quaternion());
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
        if (placables[Mathf.RoundToInt(x), Mathf.RoundToInt(y)] is not null)
        {
            Debug.Log("removing " + (int)x + " , " + (int)y);
            GameObject.Destroy(placables[Mathf.RoundToInt(x), Mathf.RoundToInt(y)].placedObj);
            placables[Mathf.RoundToInt(x), Mathf.RoundToInt(y)] = null;
        }
    }
}
