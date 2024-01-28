using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : BasePanel
{
    public Button btnResume;
    public override void Init()
    {
        if(btnResume != null)
        {
            btnResume.onClick.AddListener(() =>
            {
                Time.timeScale = 1;
                this.gameObject.SetActive(false);
                //UIMgr.Instance.HidePanel<PausePanel>();
            });
        }
    }


}
