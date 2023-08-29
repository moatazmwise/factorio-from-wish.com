using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class DroppedItem : MonoBehaviour
{
    public SpriteRenderer renderer;
    public Item item;
    public GameObject placedObj = null;
    public string address = "Assets/textures/dropped items/";
    private AsyncOperationHandle<Sprite> handle;

    public void spawn(Item item)
    {
        this.item = item;
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

    private void OnDestroy()
    {
        Addressables.Release(handle);
    }

}
