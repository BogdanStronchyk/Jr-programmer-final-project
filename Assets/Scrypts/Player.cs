using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // INCAPSULATION EXAMPLE
    // look direction variables
    private float m_horizontal;
    public float horizontal
    {
        get { return m_horizontal; }
        set { m_horizontal = value; }
    }

    // Vertical constricrion of camera rotation
    private float m_vertical;
    public float vertical
    {
        get { return m_vertical; }
        set
        {
            if (value <= 90f && value >= -90f)
            {
                m_vertical = value;
            }

        }

    }

    // Player health incapsulation and clamping between 100 and 0
    private int m_health = 100;
    public int health 
    {
        get { return m_health; }
        set
        {   
            if (value < 0)
            {
                return;
            }
            m_health = value;
        }
    }
    private int m_gunIndex;

    public int Score { get; set; }

    public static Player Instance { get; private set; }
    public List<Gun> firearms = new List<Gun>(2);
    public Gun firearm;

    public int gunIndex
    {
        get { return m_gunIndex; }
        set
        {
            if (value > firearms.Count - 1)
            {
                m_gunIndex = 0;
            }
            else if (value < 0)
            {
                m_gunIndex = firearms.Count - 1;
            }
            else
            {
                m_gunIndex = value;
            }
        }

    }

    private float Sensitivity = 1;
    private float movementSpeed = 5;

    

    private Camera Camera;
    private GameObject focalPoint;
    private GameObject Gun;
    public bool isAlive = true;
    
    void Awake()
    {
        Instance = this;
        focalPoint = GameObject.Find("FocalPoint");
        Gun = GameObject.Find("GunMount");
        gunIndex = Random.Range(0, firearms.Count);
        InitialFirearm();
        Camera = FindObjectOfType<Camera>();
    }

    private void Move()
    {
        Vector3 directionForward = Vector3.forward;
        directionForward.y = 0;

        Vector3 directionRightLeft = Vector3.right;
        directionRightLeft.y = 0;

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(directionForward * Time.deltaTime * movementSpeed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(-directionForward * Time.deltaTime * movementSpeed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-directionRightLeft * Time.deltaTime * movementSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(directionRightLeft * Time.deltaTime * movementSpeed);
        }

    }

    public void GetScore(int score)
    {
        Score += score;
    }

    private void InitialFirearm()
    {
        firearms.Add(GameObject.Find("Shotgun").GetComponent<Gun>());
        firearms.Add(GameObject.Find("Handgun").GetComponent<Gun>());
        firearms.Add(GameObject.Find("Assalut rifle").GetComponent<Gun>());
        foreach (Gun firearm in firearms)
        {
            firearm.gameObject.SetActive(false);
        }
        firearm = firearms[gunIndex];
        firearm.gameObject.SetActive(true);
    }

    private void ChangeFirearm()
    {
        firearm.gameObject.SetActive(false);
        Debug.Log(gunIndex);
        firearm = firearms[gunIndex];
        firearm.gameObject.SetActive(true);
    }

    private void Aiming()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Camera.transform.localPosition = new Vector3(0.52f, 0.2f, -3.5f);
            Camera.fieldOfView = 30;
            Gun.transform.localEulerAngles = focalPoint.transform.localEulerAngles;


            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                firearm.Fire();
            }

            if (Input.GetKeyUp(KeyCode.Mouse0) && firearm.isAutomatic)
            {
                firearm.HoldFire();
            }

        }
        else
        {
            Camera.transform.localPosition = new Vector3(3f, 0.2f, -3.5f);
            Camera.fieldOfView = 60;
            Gun.transform.localEulerAngles = new Vector3(90f, 0, 0f);
            firearm.HoldFire();
        }

        if (Input.GetKey(KeyCode.R))
        {
            firearm.Reload();
        }

        if (Input.mouseScrollDelta.magnitude > 0)
        {
            gunIndex ++;
            ChangeFirearm();
        }
        else if (Input.mouseScrollDelta.magnitude < 0)
        {
            gunIndex --;
            ChangeFirearm();
        }

    }


    public void LookAround()
    {
        vertical += Input.GetAxis("Mouse Y");
        horizontal += Input.GetAxis("Mouse X");

        Vector3 direction = new Vector3(-vertical, 0f, 0f);
        Vector3 playerRotation = new Vector3(0f, Input.GetAxis("Mouse X"), 0f);

        transform.Rotate(playerRotation * Sensitivity);
        focalPoint.transform.localEulerAngles = direction;
    }

    public void HealthCheck()
    {
        if (m_health == 0)
        {
            Death();
        }
    }

    public void Death()
    {
        isAlive = false;
    }

    void Update()
    {
        HealthCheck();
        if (isAlive)
        {
            // ABSTRACTION EXAMPLE
            LookAround();
            Move();
            Aiming();
        }
    }
}
