using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    private bool isPaused = false;
    public Button btnPause;
    public GameObject pausePanel;

    //Slider
    public Transform Player;
    private PlayerControl pc;
    public bool isTriggered;

    public Slider slider;
    public float fillSpeed = 0.3f;
    private bool isFilling = false;

    public Slider sliderEnemy;
    //public Transform slideIMG;
    public Image sImg;
    public Sprite sprite_angry;
    public float enemyFillSpeed = 0.02f;
    private bool isEnemyFilling = false;

    public GameObject SuccessPanel;
    public GameObject FailPanel;

    public Button[] btnRestart;


    void Start()
    {
        Time.timeScale = 1;

        if (btnPause != null)
        {
            btnPause.onClick.AddListener(PauseGame);
        }
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
        if (SuccessPanel != null && FailPanel != null)
        {
            SuccessPanel.SetActive(false);
            FailPanel.SetActive(false);
        }

        for (int i = 0; i < btnRestart.Length; i++)
        {
            btnRestart[i].onClick.AddListener(() =>
            {
                SceneManager.LoadScene(1);
            });
        }


        //Slider
        pc = Player.transform.GetComponent<PlayerControl>();
        isTriggered = pc.isTriggered;
        slider.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }

        //Slider
        //按下 W 键
        if (Input.GetKey(KeyCode.W) && pc.isTriggered)
        {
            isFilling = true;
            slider.gameObject.SetActive(true);
            pc.isAct = true;

            isEnemyFilling = true;
        }

        // 松开 W 键
        if (Input.GetKeyUp(KeyCode.W) || !pc.isTriggered)
        {
            isFilling = false;
            slider.gameObject.SetActive(false);
            pc.isAct = false;

            isEnemyFilling = false;
        }

        // 如果正在填充，则逐渐增加 Slider 的值
        if (isFilling)
        {
            FillSlider();
        }
        if (isEnemyFilling)
        {
            EnemyFillSlider();
        }
    }
    //Slider
    void FillSlider()
    {
        // 增加 Slider 的值，考虑填充速度和时间
        slider.value += fillSpeed * Time.deltaTime;

        //result
        if (slider.value == 1)
        {
            print("success");
            SuccessPanel.SetActive(true);
            Time.timeScale = 0;
        }
        // 在此处可以添加逻辑以限制 Slider 的值在合适的范围内
        // 比如，可以使用 Mathf.Clamp 方法限制在 0 到 1 之间

        // 示例：限制在 0 到 1 之间
        slider.value = Mathf.Clamp01(slider.value);
    }
    void EnemyFillSlider()
    {
        // 增加 Slider 的值，考虑填充速度和时间
        sliderEnemy.value += enemyFillSpeed * Time.deltaTime;
        if (sliderEnemy.value > 0.5)
        {
            sImg.sprite = sprite_angry;
        }

        //result
        if (sliderEnemy.value == 1)
        {
            //print("fail");
            FailPanel.SetActive(true);
            Time.timeScale = 0;
        }
        // 在此处可以添加逻辑以限制 Slider 的值在合适的范围内
        // 比如，可以使用 Mathf.Clamp 方法限制在 0 到 1 之间

        // 示例：限制在 0 到 1 之间
        slider.value = Mathf.Clamp01(slider.value);
    }

    public void PauseGame()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            pausePanel.SetActive(true);
            //UIMgr.Instance.ShowPanel<PausePanel>();
        }
        else
        {
            pausePanel.SetActive(false);
            //UIMgr.Instance.HidePanel<PausePanel>();
        }
        // 根据暂停状态设置游戏时间缩放
        Time.timeScale = isPaused ? 0 : 1;

    }
}
