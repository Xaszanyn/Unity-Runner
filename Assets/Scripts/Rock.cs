using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] GameObject[] versions;

    void Awake()
    {
        int s = Random.Range(0, versions.Length);
        Transform selected = null;

        for (int i = 0; i < versions.Length; i++)
        {
            if (i == s)
            {
                selected = versions[i].transform;
                versions[i].SetActive(true);
            }
            else versions[i].SetActive(false);
        }

        selected.rotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
    }
}
