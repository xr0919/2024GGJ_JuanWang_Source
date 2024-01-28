using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove_GamePlay : MonoBehaviour
{
    public bool isOnFirstF = true;

    public float moveSpeed = 1f;
    public float minX = -7.2f;
    public float maxX = 7.2f;

    private Vector3 targetPosition;

    //EnemyLogic
    public Transform Player;
    private PlayerControl pc;
    public Transform enemyUI;
    public GameObject UIpanel;
    private UIControl uic;

    private void Start()
    {
        // ������ʱ���ɳ�ʼĿ��λ��
        GenerateNewTargetPosition();

        //EnemyLogic
        if (Player != null)
        {
            pc = Player.transform.GetComponent<PlayerControl>();
        }
        if (enemyUI != null)
        {
            enemyUI.gameObject.SetActive(false);
        }
        if(UIpanel != null)
        {
            uic = UIpanel.GetComponent<UIControl>();
        }
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
        //���δ������
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f && !pc.isAct)
        {
            GenerateNewTargetPosition();
        }
        //��ұ�����
        if (Vector3.Distance(transform.position, targetPosition) < 3f && pc.isAct && pc.isFirstF == this.isOnFirstF)
        {
            MoveToPlayerPosition();
            uic.enemyFillSpeed = 0.35f;
        }else if (Vector3.Distance(transform.position, targetPosition) >= 3f)
        {
            uic.enemyFillSpeed = 0.02f;
        }

        //EnemyLogic
        if (pc.isAct)
        {
            enemyUI.gameObject.SetActive(true);
            MoveToPlayerPosition();
        }
        else
        {
            enemyUI.gameObject.SetActive(false);
        }
    }

    private void MoveToPlayerPosition()
    {
        // ����ҷ����ƶ�
        targetPosition = new Vector3(Player.transform.position.x, transform.position.y, transform.position.z);
        if (targetPosition.x < transform.position.x)
        {
            transform.localScale = new Vector3(-0.2f, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(0.2f, transform.localScale.y, transform.localScale.z);
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
