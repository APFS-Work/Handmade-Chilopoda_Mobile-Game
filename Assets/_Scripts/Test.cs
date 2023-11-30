using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    Collider collide;

    // Start is called before the first frame update
    void Start()
    {
        collide = gameObject.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != collide)
        { 
            Debug.Log("Trigger     ");
        }
    }
}
