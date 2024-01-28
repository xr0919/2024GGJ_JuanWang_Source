using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMgr
{
    private static UIMgr instance = new UIMgr();
    private Transform canvasTrans;
    private UIMgr()
    {
        canvasTrans = GameObject.Find("Canvas").transform;
        GameObject.DontDestroyOnLoad(canvasTrans);
    }
    public static UIMgr Instance { get { return instance; } }

    //存储面板的容器
    Dictionary<string, BasePanel> dic = new Dictionary<string, BasePanel>();


    public T ShowPanel<T>() where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (dic.ContainsKey(panelName))
        {
            return dic[panelName] as T;
        }
        //动态创建面板预设体 设置父对象
        GameObject obj = GameObject.Instantiate(Resources.Load("UI/" + panelName)) as GameObject;
        obj.transform.SetParent(canvasTrans, false);

        T panel = obj.GetComponent<T>();
        //之后隐藏也要用到他的脚本 所以先保存起来
        dic.Add(panelName, panel);
        //显示自己逻辑
        panel.ShowMe();
        return panel;
    }
    public void HidePanel<T>(bool isFade = true) where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (dic.ContainsKey(panelName))
        {
            if (isFade)
            {
                dic[panelName].HideMe(() =>
                {
                    GameObject.Destroy(dic[panelName].gameObject);
                    dic.Remove(panelName);
                });

            }
            else
            {
                GameObject.Destroy(dic[panelName].gameObject);
                dic.Remove(panelName);
            }
        }
    }
    //获得面板
    public T GetPanel<T>() where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (dic.ContainsKey(panelName))
        {
            T t = dic[panelName] as T;
            return t;
        }
        return null;
    }
}
