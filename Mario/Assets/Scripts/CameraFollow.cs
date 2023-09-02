using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float speed;
    public float followDelay = 1.0f; // 延迟时间，可以根据需要调整

    private bool shouldFollow = false;
    private float delayTimer = 0f;

    private void LateUpdate()
    {
        if (transform.position != target.position)
        {
            // 如果玩家移动了并且不在跟随状态
            if (!shouldFollow)
            {
                delayTimer += Time.deltaTime; // 开始计时

                // 如果计时器超过了指定的延迟时间
                if (delayTimer >= followDelay)
                {
                    shouldFollow = true; 
                    delayTimer = 0f; // 
                }
            }

            
            if (shouldFollow)
            {
                Vector3 targetPos = target.position;
                transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);
            }
        }
        else
        {
            shouldFollow = false; // 如果相机到达目标位置，停止跟随
            delayTimer = 0f; // 重置计时器
        }
    }
}
