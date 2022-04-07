using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Hammer : MonoBehaviour
{
    [SerializeField] bool direction;

    void Awake()
    {
        Spin(Random.Range(0F, 2F));
    }

    void Spin(float delay = 0)
    {
        transform.DOLocalRotate(new Vector3(0, 0, 90 * (direction ? -1 : 1)), .5F).SetDelay(delay)
            .OnComplete(() => transform.DOLocalRotate(Vector3.zero, 1)
                .OnComplete(() => Spin()));
                
    }
}