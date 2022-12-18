using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraFollower : MonoBehaviour
{
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public float smoothSpeed = 0.125f;
    public Vector3 locationOffset;
    public Vector3 rotationOffset;

    void FixedUpdate()
    {
        if (target == null)
        {
            GameObject[] results = GameObject.FindGameObjectsWithTag("PlayerInstance");
            for (int i = 0; i < results.Length; i++)
            {
                PhotonView view = results[i].GetComponent<PhotonView>();
                if (view != null)
                {
                    if (view.IsMine)
                    {
                        target = results[i];
                        break;
                    }
                }
            }
            return;

        }

        Vector3 desiredPosition = target.transform.position + target.transform.rotation * locationOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        smoothedPosition.y = 20;
        transform.position = smoothedPosition;

        Quaternion desiredrotation = target.transform.rotation * Quaternion.Euler(rotationOffset);
        Quaternion smoothedrotation = Quaternion.Lerp(transform.rotation, desiredrotation, smoothSpeed);
        transform.rotation = smoothedrotation;
    }
}
