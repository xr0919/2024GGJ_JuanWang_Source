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
        //���� W ��
        if (Input.GetKey(KeyCode.W) && pc.isTriggered)
        {
            isFilling = true;
            slider.gameObject.SetActive(true);
            pc.isAct = true;

            isEnemyFilling = true;
        }

        // �ɿ� W ��
        if (Input.GetKeyUp(KeyCode.W) || !pc.isTriggered)
        {
            isFilling = false;
            slider.gameObject.SetActive(false);
            pc.isAct = false;

            isEnemyFilling = false;
        }

        // ���������䣬�������� Slider ��ֵ
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
        // ���� Slider ��ֵ����������ٶȺ�ʱ��
        slider.value += fillSpeed * Time.deltaTime;

        //result
        if (slider.value == 1)
        {
            print("success");
            SuccessPanel.SetActive(true);
            Time.timeScale = 0;
        }
        // �ڴ˴���������߼������� Slider ��ֵ�ں��ʵķ�Χ��
        // ���磬����ʹ�� Mathf.Clamp ���������� 0 �� 1 ֮��

        // ʾ���������� 0 �� 1 ֮��
        slider.value = Mathf.Clamp01(slider.value);
    }
    void EnemyFillSlider()
    {
        // ���� Slider ��ֵ����������ٶȺ�ʱ��
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
        // �ڴ˴���������߼������� Slider ��ֵ�ں��ʵķ�Χ��
        // ���磬����ʹ�� Mathf.Clamp ���������� 0 �� 1 ֮��

        // ʾ���������� 0 �� 1 ֮��
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
        // ������ͣ״̬������Ϸʱ������
        Time.timeScale = isPaused ? 0 : 1;

    }
}
