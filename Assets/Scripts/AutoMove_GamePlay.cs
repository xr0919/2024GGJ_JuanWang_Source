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
        // 在启动时生成初始目标位置
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
        // 计算朝目标位置移动的方向
        Vector3 direction = (targetPosition - transform.position).normalized;

        // 计算新的摄像机位置
        Vector3 newPosition = transform.position + direction * moveSpeed * Time.deltaTime;

        // 更新摄像机位置
        transform.position = newPosition;

        // 检查是否接近目标位置，如果是则生成新的目标位置
        //玩家未被发现
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f && !pc.isAct)
        {
            GenerateNewTargetPosition();
        }
        //玩家被发现
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
        // 向玩家方向移动
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
