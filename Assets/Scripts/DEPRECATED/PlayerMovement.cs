using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    bool moveLock;

    [SerializeField] GameManager gm;
    
    [SerializeField] float sensitivity;

    [SerializeField] Transform player;

    [SerializeField] PlayerDetection pDetection;

    [SerializeField] PressMovement press;
    [SerializeField] Hand hand;

    bool dragging;
    float position;
    float lastPosition;

    [SerializeField] Camera camera;

    [SerializeField] Transform junk;

    void Awake()
    {
        transform.DOMoveZ(45, 10).SetEase(Ease.Linear).SetTarget(transform);
    }

    void Update()
    {
        if (!moveLock) DragPosition();

        if (Input.GetKeyDown(KeyCode.A)) CrackTheTop();
    }

    void DragPosition()
    {
        if (!dragging && Input.GetMouseButtonDown(0))
        {
            dragging = true;
            position = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
            lastPosition = player.position.x;
        }
        else if (Input.GetMouseButton(0))
        {
            float difference = Input.mousePosition.x - position;

            player.position = new Vector3(Mathf.Clamp(lastPosition + (difference / sensitivity), -4, 4), player.position.y, player.position.z);
        }
    }

    public void Halt()
    {
        moveLock = true;
    }

    public void FinishPress()
    {
        DOTween.Kill(transform);

        camera.transform.DOLocalMove(new Vector3(0, 3.4F, 0), 1);

        camera.transform.DORotateQuaternion(Quaternion.Euler(new Vector3(0, 0, 0)), 1);
        
        Halt();

        pDetection.Press();

        player.DOMove(new Vector3(0, 1.35F, 45), 1);

        press.StartPressing();
    }

    public void Finish()
    {
        DOTween.Kill(transform);

        camera.transform.DOLocalMove(new Vector3(0, 3.4F, 0), 1);

        camera.transform.DORotateQuaternion(Quaternion.Euler(new Vector3(0, 0, 0)), 1);

        Halt();

        pDetection.Press();

        player.DOMove(new Vector3(0, 1.35F, 45), 1);



        // press.StartPressing();
        hand.Down(1);
    }

    public void Fail()
    {
        DOTween.Kill(transform);

        for (int i = 0; i < player.childCount; i++)
        {
            if (i != 0) player.GetChild(i).GetComponent<MeshRenderer>().enabled = false;
            player.GetChild(i).GetChild(0).gameObject.SetActive(true);
        }

        Halt();

        gm.Fail();
    }

    public void CrackTheTop()
    {
        if (player.childCount < 2) return;

        Transform top = player.GetChild(player.childCount - 1);

        DOTween.Kill(top);

        top.GetComponent<MeshRenderer>().enabled = false;
        top.GetChild(0).gameObject.SetActive(true);

        top.SetParent(junk);

        Destroy(top.gameObject, 1);
    }
}