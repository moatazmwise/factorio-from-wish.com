using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public enum direction
{
    up,
    down,
    left,
    right
}

public class Placable : MonoBehaviour
{
    public int id = 0;
    public direction direction = direction.up;
    public GameObject placedObj = null;
    public bool solid = true;
    public SpriteRenderer renderer;
    public string address = "Assets/textures/blocks/";
    private AsyncOperationHandle<Sprite> handle;

    void FixedUpdate()
    {
        Action();
    }

    public virtual void Action() 
    {  
        
    }

    public void spawn()
    {
        address += id.ToString() + ".png";
        handle = Addressables.LoadAssetAsync<Sprite>(address);
        handle.Completed += (AsyncOperationHandle<Sprite> operation) =>
        {
            if (operation.Status == AsyncOperationStatus.Succeeded)
            {
                renderer.sprite = operation.Result;
            }
            else
            {
                Debug.LogError($"Asset for {address} failed to load.");
            }
        };
    }

    private void OnDestroy()
    {
        Addressables.Release(handle);
    }
}
