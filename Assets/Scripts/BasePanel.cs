using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public abstract class BasePanel : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    //���뵭���ٶ�
    private float alphaSpeed = 10;
    private bool isShow;
    private UnityAction hideCallBack;

    protected virtual void Awake()
    {
        //һ��ʼ��ȡ����Ϲ��ص���� ��û�� �������
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = this.AddComponent<CanvasGroup>();
        }

    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Init();
    }
    /// <summary>
    /// ��Ҫ���� ��ʼ�� ��ť�¼������ȵ�
    /// </summary>
    public abstract void Init();

    /// <summary>
    /// ��ʾ�Լ�ʱ��������
    /// </summary>
    public virtual void ShowMe()
    {
        isShow = true;
        canvasGroup.alpha = 0;
    }
    public virtual void HideMe(UnityAction callback)
    {
        isShow = false;
        canvasGroup.alpha = 1;
        hideCallBack = callback;
    }
    // Update is called once per frame
    void Update()
    {
        //����
        if (isShow && canvasGroup.alpha != 1)
        {
            canvasGroup.alpha += alphaSpeed * Time.deltaTime;
            if (canvasGroup.alpha >= 1)
            {
                canvasGroup.alpha = 1;
            }
        }
        //����
        else if (!isShow)
        {
            canvasGroup.alpha -= alphaSpeed * Time.deltaTime;
            if (canvasGroup.alpha <= 0)
            {
                canvasGroup.alpha = 0;
                //�ù����� ɾ���Լ�
                hideCallBack?.Invoke();
            }
        }
    }
}
