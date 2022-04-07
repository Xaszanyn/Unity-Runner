using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Plate : MonoBehaviour
{
    public bool collected;
    public bool UNCOMPLETED; // :(

    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] BoxCollider boxCollider;

    [SerializeField] Transform brokenPieces;
    [SerializeField] Transform dyedPlate;
    [SerializeField] Transform dyedBrokenPieces;

    public void Add(Transform collects, int count)
    {
        collected = true;

        transform.SetParent(collects);

        transform.DOLocalMoveZ(0, .5F).SetTarget(transform);

        transform.DOLocalMoveY(1 + .12F * (count - 1), .5F).SetTarget(transform).SetEase(Ease.OutQuart)
            .OnComplete(() => transform.DOLocalMoveY(count * .12F, .5F));
        
        transform.localRotation = Quaternion.identity;
        transform.DOLocalRotate(new Vector3(360, 0, 0), 1, RotateMode.FastBeyond360).SetEase(Ease.OutExpo);
    }

    public void Crack()
    {
        DOTween.Kill(transform);
        
        meshRenderer.enabled = false;
        boxCollider.enabled = false;

        transform.GetChild(0).gameObject.SetActive(true);

        transform.SetParent(GameManager.junk);

        StartCoroutine(Delay());

        IEnumerator Delay()
        {
            yield return new WaitForSeconds(1);
            gameObject.SetActive(false);
        }
    }

    public void Break()
    {

    }
}
