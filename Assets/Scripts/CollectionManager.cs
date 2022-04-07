using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    [SerializeField] Transform target;

    List<Transform> plates = new List<Transform>();

    int count = 0;
    public int Count => count;

    void Update()
    {
        for (int i = 0; i < count; i++)
        {
            // Sorry but I don't see any other way.
            Transform plate = transform.GetChild(i);

            // Slowest Interpolation .9F - Fastest Interpolation .7F
            float x = Mathf.Lerp(target.localPosition.x, plate.localPosition.x, ((i / (float)count) * .2F) + .7F);

            plate.localPosition = new Vector3(x, plate.localPosition.y, plate.localPosition.z);
        }
    }

    public void Increment(Transform plate)
    {
        plates.Add(plate);
        count++;
    }

    public void Decrement(Transform plate)
    {
        plates.Remove(plate);
        count--;
    }

    public Transform GetTop()
    {
        return plates[count - 1];
    }

    public Transform Get(int index)
    {
        return plates[index];
    }
}
