using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoundChecker : MonoBehaviour
{
    public GameObject colliderx;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (colliderx.GetComponent<Rigidbody>().transform.position.y < 0)
        {
            colliderx.GetComponent<Rigidbody>().transform.Translate(0, 20, 0);
        }
    }
}
