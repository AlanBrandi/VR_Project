using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventarioController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeIten(GameObject iten)
    {
        Debug.LogWarning("Item =" + iten.GetComponent<Iten>().id);
    }
}
