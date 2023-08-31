using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform goToPos;//����ȥ��������
    private Transform playerPos;//������Ϸ��ɫ������
 
    void Start()
    {
        playerPos = GameObject.FindWithTag("Player").transform;//��ȡ��Ϸ��ɫ������
    }

    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            playerPos.transform.position = goToPos.transform.position;
        }
    }
}