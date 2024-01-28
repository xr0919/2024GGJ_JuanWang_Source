using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float minX = -10f;
    public float maxX = 10f;

    private Vector3 targetPosition;

    private void Start()
    {
        // 在启动时生成初始目标位置
        GenerateNewTargetPosition();

    }

    private void Update()
    {
        // 计算朝目标位置移动的方向
        Vector3 direction = (targetPosition - transform.position).normalized;

        // 计算新的摄像机位置
        Vector3 newPosition = transform.position + direction * moveSpeed * Time.deltaTime;

        // 更新摄像机位置
        transform.position = newPosition;

        // 检查是否接近目标位置，如果是则生成新的目标位置
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            GenerateNewTargetPosition();
        }

    }


    private void GenerateNewTargetPosition()
    {
        // 生成新的目标位置
        targetPosition = new Vector3(Random.Range(minX, maxX), transform.position.y, transform.position.z);
        if (targetPosition.x < transform.position.x)
        {
            transform.localScale = new Vector3(-0.2f, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(0.2f, transform.localScale.y, transform.localScale.z);

        }
    }
}
