using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlateBreak : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform camera;
    [SerializeField] Transform collects;

    [SerializeField] Transform playerSlot;
    [SerializeField] Transform cameraSlot;

    [SerializeField] Transform hand;
    float start = 5.5F;
    float speed = 5F;

    [SerializeField] GameManager gameManager;

    [SerializeField] GameObject lastPlate;
    [SerializeField] MeshRenderer lastPlatesMeshRenderer;

    [SerializeField] InputMovement movement;

    public void BringPlates()
    {
        movement.Lock();

        collects.SetParent(player);
        player.SetParent(playerSlot);
        camera.SetParent(cameraSlot);

        collects.DOLocalMove(new Vector3(0, collects.localPosition.y, 0), .5F);
        player.DOLocalMove(Vector3.zero, .5F);
        camera.DOLocalMove(Vector3.zero, .5F);
        camera.DOLocalRotate(Vector3.zero, .5F)
            .OnComplete(() => HandDown());
    }

    void HandDown(int delay = 0)
    {
        hand.DOLocalMoveY(1, speed).SetEase(Ease.Linear).SetTarget(hand).SetSpeedBased(true).SetDelay(delay);
    }

    void HandHit()
    {
        DOTween.Kill(hand);

        hand.DOLocalMoveY(start, speed * .6F).SetSpeedBased(true)
            .OnComplete(() => HandDown());

        start -= .12F;
        speed += .25F;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Plate"))
        {
            other.GetComponent<Plate>();
            other.GetComponent<MeshRenderer>().enabled = false; // clear this for plate class
            other.GetComponent<BoxCollider>().enabled = false;
            other.transform.GetChild(0).gameObject.SetActive(true);

            HandHit();
        }
        else if (other.gameObject.CompareTag("Hand"))
        {
            DOTween.Kill(hand);

            lastPlatesMeshRenderer.enabled = false;
            lastPlate.SetActive(true);

            StartCoroutine(Delay());

            IEnumerator Delay()
            {
                yield return new WaitForSeconds(.5F);

                gameManager.Success();
            }
        }
    }
}