using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Placable : MonoBehaviour
{
    public Item item;
    public Vector2 direction = Vector2.up;
    public GameObject placedObj = null;
    public bool solid = true;
    public SpriteRenderer renderer;
    public string address = "Assets/textures/blocks/";
    private AsyncOperationHandle<Sprite> handle;

    void FixedUpdate()
    {
        item.Action(this);
    }

    public void spawn()
    {
        address += item.id.ToString() + ".png";
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        item.PlacableOnTriggerStay(this, collision);
    }

    private void OnDestroy()
    {
        Addressables.Release(handle);
    }
}
