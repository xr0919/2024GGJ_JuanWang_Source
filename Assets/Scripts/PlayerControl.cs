using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Camera mainCamera;
    private Animator ani;
    public Transform exclamationUI;

    public bool isTriggered;
    public bool isAct;
    public bool canUp;
    public bool canDown;
    public bool isFirstF = true;
    public bool isSencondF;

    void Start()
    {
        ani = GetComponent<Animator>();
        if (exclamationUI != null)
        {
            exclamationUI.gameObject.SetActive(false);

        }
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal != 0)
        {
            ani.SetBool("IsRun", true);
            transform.localScale = new Vector3(horizontal > 0 ? -0.2f : 0.2f, transform.localScale.y, transform.localScale.z);
            this.transform.Translate(Vector2.right * horizontal * Time.deltaTime);
        }
        else
        {
            ani.SetBool("IsRun", false);

        }

        if (mainCamera != null)
        {
            mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y + 2f, mainCamera.transform.position.z);

        }
        float vertical = Input.GetAxis("Vertical");
        if (vertical != 0)
        {
            ani.SetBool("IsInteractBool", true);
        }
        else
        {
            ani.SetBool("IsInteractBool", false);
        }

        //interact
        if (Input.GetKey(KeyCode.Space))
        {
            ani.SetTrigger("IsInteractTri");
        }
        //up/downstair
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (canUp)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 3.5f, transform.position.z);
                isSencondF = true;
                isFirstF = false;
            }
            if (canDown)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 3.5f, transform.position.z);
                isSencondF = false;
                isFirstF = true;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trigger"))
        {
            exclamationUI.gameObject.SetActive(true);
            isTriggered = true;
        }
        if (collision.CompareTag("upstair"))
        {
            exclamationUI.gameObject.SetActive(true);
            canUp = true;
            //transform.position = new Vector3(transform.position.x, transform.position.y + 3.5f, transform.position.z);
        }
        if (collision.CompareTag("downstair"))
        {
            exclamationUI.gameObject.SetActive(true);
            canDown = true;
            //transform.position = new Vector3(transform.position.x, transform.position.y - 3.5f, transform.position.z);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Trigger") || collision.CompareTag("upstair") || collision.CompareTag("downstair"))
        {
            exclamationUI.gameObject.SetActive(false);
            isTriggered = false;
            canUp = false;
            canDown = false;
        }
    }
}
