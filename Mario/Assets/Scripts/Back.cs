using UnityEngine;
using System.Collections;
using System.IO;


public class Back : MonoBehaviour
{
    public Transform obj;
    public GameObject tiga;

    public AudioSource backAudio;

    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            obj.transform.position = tiga.transform.position;
        }
        if (Input.GetKey(KeyCode.W))
        {
            tiga.transform.position = obj.transform.position;
            backAudio.Play();
        }
    }

}