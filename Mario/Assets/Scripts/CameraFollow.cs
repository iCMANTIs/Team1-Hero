using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void LateUpdate()
    {
        if(transform.position != target.position)
        {
            Vector3 targetPos = target.position;
            transform.position = Vector3.Lerp(transform.position, targetPos, smoothing); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
