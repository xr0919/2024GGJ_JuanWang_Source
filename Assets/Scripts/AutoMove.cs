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
        // ������ʱ���ɳ�ʼĿ��λ��
        GenerateNewTargetPosition();

    }

    private void Update()
    {
        // ���㳯Ŀ��λ���ƶ��ķ���
        Vector3 direction = (targetPosition - transform.position).normalized;

        // �����µ������λ��
        Vector3 newPosition = transform.position + direction * moveSpeed * Time.deltaTime;

        // ���������λ��
        transform.position = newPosition;

        // ����Ƿ�ӽ�Ŀ��λ�ã�������������µ�Ŀ��λ��
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            GenerateNewTargetPosition();
        }

    }


    private void GenerateNewTargetPosition()
    {
        // �����µ�Ŀ��λ��
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
