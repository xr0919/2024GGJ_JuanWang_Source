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

    //ͼƬ����
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

        // ������ʱ���ɳ�ʼĿ��λ��
        GenerateNewTargetPosition();

        //
        // ���û���ֶ�ָ�� Text ��������Ի�ȡ���ظýű��� GameObject �ϵ� Text ���
       /* if (textComponent == null)
        {
            textComponent = GetComponent<Text>();
        }*/
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

        /*
        // ��ȡ��ǰ��ɫ
        Color currentColor = textComponent.color;

        // ���ݵ�����ݼ��ķ���������͸����
        float newAlpha = increasing ? currentColor.a + speed * Time.deltaTime : currentColor.a - speed * Time.deltaTime;

        // �л������͵ݼ��ķ���
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

        // �����µ���ɫ
        textComponent.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);
        */

        //ͼƬ����
        float newAlpha = increasing ? image.color.a + fadeSpeed * Time.deltaTime : image.color.a - fadeSpeed * Time.deltaTime;

        // �л������͵ݼ��ķ���
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

        // �����µ���ɫ������ԭ����ɫ�� RGB �ɷ֣�ֻ�޸�͸����
        image.color = new Color(image.color.r, image.color.g, image.color.b, newAlpha);
    }

    private void GenerateNewTargetPosition()
    {
        // �����µ�Ŀ��λ��
        targetPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), transform.position.z);
    }
}
