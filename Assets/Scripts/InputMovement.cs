using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMovement : MonoBehaviour
{
    [SerializeField] Transform player;

    [SerializeField] float edges;
    [SerializeField] float sensitivity;

    bool moveLock;

    bool dragging;
    float position;
    float lastPosition;

    void Update()
    {
        if (!moveLock) Drag();
    }

    void Drag()
    {
        if (Input.GetMouseButtonDown(0) && !dragging)
        {
            dragging = true;
            position = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
            lastPosition = player.localPosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            float difference = Input.mousePosition.x - position;

            player.localPosition = new Vector3(Mathf.Clamp(lastPosition + (difference / sensitivity), -edges, edges), player.localPosition.y, player.localPosition.z);
        }
    }

    public void Lock()
    {
        moveLock = true;
    }

    public void Unlock()
    {
        moveLock = false;
    }
}
