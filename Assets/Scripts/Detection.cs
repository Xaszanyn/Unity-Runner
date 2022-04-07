using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Detection : MonoBehaviour
{
    [SerializeField] Transform collects;

    [SerializeField] CollectionManager collectionManager;

    [SerializeField] ParticleSystem particle;

    bool collect;
    bool hitRecent;


    void OnTriggerEnter(Collider other)
    {
        if (!hitRecent && other.gameObject.CompareTag("Obstacle")) CrackTheTop();
        else if (collect && other.gameObject.CompareTag("Plate")) AddPlate();
        else if (other.gameObject.CompareTag("Paint")) Paint();

        void AddPlate()
        {
            Transform t = other.transform;
            Plate p = t.GetComponent<Plate>();

            if (p.collected) return;
            else
            {
                p.Add(collects, collectionManager.Count);
                collectionManager.Increment(t);

                StartCoroutine(Particle());
            }

            IEnumerator Particle()
            {
                yield return new WaitForSeconds(.75F);
                particle.transform.localPosition = new Vector3(0, 1 - .4F + .12F * (collectionManager.Count - 1), 0);
                particle.Play();
            }
        }

        void CrackTheTop()
        {
            if (collectionManager.Count == 0) return;
            else
            {
                Transform t = collectionManager.GetTop();

                t.GetComponent<Plate>().Crack();
                collectionManager.Decrement(t);

                StartCoroutine(Hit());
            }

            IEnumerator Hit()
            {
                hitRecent = true;
                yield return new WaitForSeconds(.5F);
                hitRecent = false;
            }
        }
    }

    void Paint()
    {
        for (int i = 0; i < collectionManager.Count; i++)
        {
            Transform t = collectionManager.Get(i);

            
        }
    }

    public void StartCollect()
    {
        collect = true;
    }

    public void StopCollect()
    {
        collect = false;
    }
}