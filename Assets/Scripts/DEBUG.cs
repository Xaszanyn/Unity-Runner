using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DEBUG : MonoBehaviour
{
    void Start()
    {
        transform.DOLocalRotate(new Vector3(0, 360, 0), 1, RotateMode.FastBeyond360);
    }
}
