using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{
    public Grid grid;
    public int placedBlockId = 1;
    public float speed = 1;
    public float camSize = 1;
    public float camSizeMin = 1;
    public float mouseWheelSensitivity = 10;
    public float camSizeMax = 15;
    private Vector3 prevMousePos;
    

    private void Start()
    {
        prevMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            {
                grid.Place(mousePos.x, mousePos.y, placedBlockId);
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            {
                grid.Remove(mousePos.x, mousePos.y);
            }
        }

        Camera.main.transform.position = new Vector3(
            Camera.main.transform.position.x + Input.GetAxis("Horizontal") * speed,
            Camera.main.transform.position.y + Input.GetAxis("Vertical") * speed,
            Camera.main.transform.position.z
            );
        camSize -= Input.GetAxis("Mouse ScrollWheel") * mouseWheelSensitivity;
        camSize = Mathf.Clamp(camSize, camSizeMin, camSizeMax);
        Camera.main.orthographicSize = camSize;

        if (Input.GetMouseButton(2))
        {
            Camera.main.transform.position += prevMousePos - mousePos;
        }
        prevMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
