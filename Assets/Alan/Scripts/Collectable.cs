using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Collectable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectableManager.Instance.AddCollectable();
            // Instantiate(CollectableManager.Instance.collectableFX, transform.position, Quaternion.identity); 
            CollectableManager.Instance.colletableAudio.pitch = Random.Range(.9f, 1.3f);
            CollectableManager.Instance.colletableAudio.Play();
            Destroy(gameObject);
        }
    }
}
