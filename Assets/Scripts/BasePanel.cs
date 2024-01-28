using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public abstract class BasePanel : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    //淡入淡出速度
    private float alphaSpeed = 10;
    private bool isShow;
    private UnityAction hideCallBack;

    protected virtual void Awake()
    {
        //一开始获取面板上挂载的组件 若没有 代码添加
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
    /// 主要用于 初始化 按钮事件监听等等
    /// </summary>
    public abstract void Init();

    /// <summary>
    /// 显示自己时做的事情
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
        //淡入
        if (isShow && canvasGroup.alpha != 1)
        {
            canvasGroup.alpha += alphaSpeed * Time.deltaTime;
            if (canvasGroup.alpha >= 1)
            {
                canvasGroup.alpha = 1;
            }
        }
        //淡出
        else if (!isShow)
        {
            canvasGroup.alpha -= alphaSpeed * Time.deltaTime;
            if (canvasGroup.alpha <= 0)
            {
                canvasGroup.alpha = 0;
                //让管理器 删除自己
                hideCallBack?.Invoke();
            }
        }
    }
}
