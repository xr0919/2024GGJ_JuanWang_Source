using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenControl : MonoBehaviour
{
    public Button btnStart;

    public float moveSpeed = 1f;
    public float minX = -5f;
    public float maxX = 5f;
    public float minY = -2f;
    public float maxY = 2f;

    private Vector3 targetPosition;

    //public Text textComponent;
    //public float speed = 1f;
    private bool increasing = true;

    //图片渐变
    public Image image;
    public float fadeSpeed = 0.5f;

    //private bool increasing = true;

    private void Start()
    {
        if(btnStart != null)
        {
            btnStart.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(1);
            });
        }

        // 在启动时生成初始目标位置
        GenerateNewTargetPosition();

        //
        // 如果没有手动指定 Text 组件，则尝试获取挂载该脚本的 GameObject 上的 Text 组件
       /* if (textComponent == null)
        {
            textComponent = GetComponent<Text>();
        }*/
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

        /*
        // 获取当前颜色
        Color currentColor = textComponent.color;

        // 根据递增或递减的方向来更新透明度
        float newAlpha = increasing ? currentColor.a + speed * Time.deltaTime : currentColor.a - speed * Time.deltaTime;

        // 切换递增和递减的方向
        if (newAlpha >= 1f)
        {
            newAlpha = 1f;
            increasing = false;
        }
        else if (newAlpha <= 0.2f)
        {
            newAlpha = 0.2f;
            increasing = true;
        }

        // 设置新的颜色
        textComponent.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
        */

        //图片渐变
        float newAlpha = increasing ? image.color.a + fadeSpeed * Time.deltaTime : image.color.a - fadeSpeed * Time.deltaTime;

        // 切换递增和递减的方向
        if (newAlpha >= 1f)
        {
            newAlpha = 1f;
            increasing = false;
        }
        else if (newAlpha <= 0f)
        {
            newAlpha = 0f;
            increasing = true;
        }

        // 设置新的颜色，保持原有颜色的 RGB 成分，只修改透明度
        image.color = new Color(image.color.r, image.color.g, image.color.b, newAlpha);
    }

    private void GenerateNewTargetPosition()
    {
        // 生成新的目标位置
        targetPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), transform.position.z);
    }
}
