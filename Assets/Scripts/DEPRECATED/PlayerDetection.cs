using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerDetection : MonoBehaviour
{
    [SerializeField] PlayerMovement pMovement;

    [SerializeField] Transform player;

    bool press;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle")) pMovement.CrackTheTop();
        else if (!press && other.gameObject.CompareTag("Plate")) AddPlate();
        else if (other.gameObject.CompareTag("Finish")) pMovement.Finish();

        void AddPlate()
        {
            other.transform.SetParent(player);
            
            other.transform.DOLocalMove(new Vector3(0, .98F + (player.childCount - 1) * .12F, 0), .5F).SetTarget(other.transform);
        }
    }

    public void Press()
    {
        press = true;
    }
}