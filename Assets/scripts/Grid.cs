using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Mathematics;
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

    public void Place(float x, float y, Item item, Vector2 direction = default)
    {
        if (direction == default) direction = Vector2.up;
        block.GetComponent<Placable>().direction = direction;

        Vector2Int p = new Vector2Int(Mathf.RoundToInt(x), Mathf.RoundToInt(y));

        if (p.x >= size || p.x < 0 || p.y >= size || p.y < 0)
        {
            Debug.Log("Out of bounds : " + "x = " + p.x +",  y = " + p.y + ",  size = " + size);
            return;
        }
        if (placedBlocks[p.x, p.y] is null)
        {
            Debug.Log("placing " + (int)x + " , " + (int)y);
            if (item.id == 2 || item.id == 3 || item.id == 4)
            {
                item.id = 2;
                Vector2Int leftDir = Vector2Int.RoundToInt(Vector2.Perpendicular(direction));
                Vector2Int rightDir = -leftDir;
                Vector2Int downDir = -Vector2Int.RoundToInt(direction);
                Vector2Int left = leftDir + p;
                Vector2Int right = rightDir + p;
                Vector2Int down = downDir + p;

                int leftId = placedBlocks[left.x, left.y] != null ? placedBlocks[left.x, left.y].GetComponent<Placable>().item.id : -1;
                int rightId = placedBlocks[right.x, right.y] != null ? placedBlocks[right.x, right.y].GetComponent<Placable>().item.id : -1;
                int downId = placedBlocks[down.x, down.y] != null ? placedBlocks[down.x, down.y].GetComponent<Placable>().item.id : -1;
                if ((leftId == 2 || leftId == 3 || leftId == 4) && placedBlocks[left.x, left.y].GetComponent<Placable>().direction == rightDir)
                {
                    item.id = 4;
                }
                if ((rightId == 2 || rightId == 3 || rightId == 4) && placedBlocks[right.x, right.y].GetComponent<Placable>().direction == leftDir)
                {
                    item.id = 3;
                }
                if (((leftId == 2 || leftId == 3 || leftId == 4) && placedBlocks[left.x, left.y].GetComponent<Placable>().direction == rightDir) &&
                    ((rightId == 2 || rightId == 3 || rightId == 4) && placedBlocks[right.x, right.y].GetComponent<Placable>().direction == leftDir))
                {
                    item.id = 2;
                }
                if ((downId == 2 || downId == 3 || downId == 4) && placedBlocks[down.x, down.y].GetComponent<Placable>().direction == direction)
                {
                    item.id = 2;
                }
            }
            placedBlocks[p.x, p.y] = Instantiate(block, new Vector3(p.x, p.y, -1), new Quaternion());
            placedBlocks[p.x, p.y].GetComponent<Placable>().item = item;
            placedBlocks[p.x, p.y].GetComponent<Placable>().spawn();
            placedBlocks[p.x, p.y].transform.Rotate(0, 0, Vector2.SignedAngle(Vector2.up,direction));
            print(direction);
        }
        
    }

    public void PlaceGround(float x, float y)
    {
        Vector2Int p = new Vector2Int(Mathf.RoundToInt(x), Mathf.RoundToInt(y));

        if (p.x >= size || p.x < 0 || p.y >= size || p.y < 0)
        {
            Debug.Log("Out of bounds : " + "x = " + p.x + ",  y = " + p.y + ",  size = " + size);
            return;
        }
        if (ground[p.x, p.y] is null)
        {
            Debug.Log("placing " + (int)x + " , " + (int)y);
            ground[p.x, p.y] = new Ground();
            ground[p.x, p.y].placedGround = Instantiate(dirt, new Vector3(p.x, p.y, 0), new Quaternion(),this.transform);
        }

    }

    public void Remove(float x, float y)
    {
        Vector2Int p = new Vector2Int(Mathf.RoundToInt(x), Mathf.RoundToInt(y));

        if (p.x >= size || p.x < 0 || p.y >= size || p.y < 0)
        {
            Debug.Log("Out of bounds : " + "x = " + p.x + ",  y = " + p.y + ",  size = " + size);
            return;
        }
        if (placedBlocks[p.x, p.y] is not null)
        {
            Debug.Log("removing " + (int)x + " , " + (int)y);
            GameObject.Destroy(placedBlocks[p.x, p.y]);
            placedBlocks[p.x, p.y] = null;
        }
    }

    public void RotateBlock(float x, float y)
    {
        Vector2Int p = new Vector2Int(Mathf.RoundToInt(x), Mathf.RoundToInt(y));

        if (placedBlocks[p.x, p.y] is not null)
        {
            Debug.Log("rotating " + (int)x + " , " + (int)y);
            Vector2 dir = placedBlocks[p.x, p.y].GetComponent<Placable>().direction;
            Vector2 newDir = new Vector2 (
                Mathf.RoundToInt(dir.x * Mathf.Cos(-Mathf.PI/2) - dir.y * Mathf.Sin(-Mathf.PI / 2)),
                Mathf.RoundToInt(dir.x * Mathf.Sin(-Mathf.PI / 2) + dir.y * Mathf.Cos(-Mathf.PI / 2)));
            Item item = placedBlocks[p.x, p.y].GetComponent<Placable>().item;
            Remove(p.x, p.y);
            Place(x, y, item, newDir);
        }
    }
}
