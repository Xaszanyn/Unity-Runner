using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PressMovement : MonoBehaviour
{
    [SerializeField] Transform press;

    [SerializeField] GameManager gm;

    public void StartPressing()
    {
        press.DOMoveY(-.22F, 4)
            .OnComplete(() => press.DOMoveY(3.5F, 1).SetDelay(.5F)
                    .OnComplete(() => gm.Success()));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Plate"))
        {
            other.GetComponent<MeshRenderer>().enabled = false;
            other.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
