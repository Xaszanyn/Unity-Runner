using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hand : MonoBehaviour
{
    [SerializeField] GameManager gm;

    [SerializeField] Transform hand;

    float start = 5F;

    [SerializeField] GameObject broken;
    [SerializeField] MeshRenderer mr;

    public void Down(int delay = 0)
    {
        hand.DOLocalMoveY(2, 5).SetEase(Ease.Linear).SetTarget(hand).SetSpeedBased(true).SetDelay(delay);
    }

    void Hit()
    {
        DOTween.Kill(hand);

        hand.DOLocalMoveY(start, 3).SetSpeedBased(true)
            .OnComplete(() => Down());

        start -= .12F;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Plate"))
        {
            other.GetComponent<MeshRenderer>().enabled = false; // clear this for plate class
            other.GetComponent<BoxCollider>().enabled = false;
            other.transform.GetChild(0).gameObject.SetActive(true);

            Hit();
        }
        else if (other.gameObject.CompareTag("Hand"))
        {
            DOTween.Kill(hand);

            mr.enabled = false;
            broken.SetActive(true);

            gm.Success();
        }
    }
}
