using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // look direction variables
    private float m_horizontal;
    public float horizontal
    {
        get { return m_horizontal; }
        set { m_horizontal = value; }
    }

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

    

    [SerializeField] float Sensitivity = 1;
    [SerializeField] float movementSpeed = 5;

    private Camera Camera;
    private CharacterController Controller;
    private GameObject focalPoint;
    private GameObject Gun;
    private FireScrypt fireScrypt;
    // Start is called before the first frame update
    void Start()
    {
        Controller = GetComponent<CharacterController>();
        focalPoint = GameObject.Find("FocalPoint");
        Gun = GameObject.Find("GunMount");
        fireScrypt = GetComponent<FireScrypt>();
        Camera = FindObjectOfType<Camera>();
    }

    public void Move()
    {
        Vector3 directionForward = focalPoint.transform.forward;
        directionForward.y = 0;

        Vector3 directionRightLeft = focalPoint.transform.right;
        directionRightLeft.y = 0;
        if (Input.GetKey(KeyCode.W))
        {
            Controller.Move(directionForward * Time.deltaTime * movementSpeed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Controller.Move(-directionForward * Time.deltaTime * movementSpeed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            Controller.Move(-directionRightLeft * Time.deltaTime * movementSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Controller.Move(directionRightLeft * Time.deltaTime * movementSpeed);
        }

    }

    private void GunzUp()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Camera.transform.localPosition = new Vector3(0.52f, 0.2f, -3.5f);
            Camera.fieldOfView = 30;
            Gun.transform.localEulerAngles = focalPoint.transform.localEulerAngles;

            if (Input.GetKey(KeyCode.Mouse0))
            {
                fireScrypt.Fire();
            }
            else if (!Input.GetKey(KeyCode.Mouse0))
            {
                fireScrypt.HoldFire();
            }
        }
        else
        {
            Camera.transform.localPosition = new Vector3(3f, 0.2f, -3.5f);
            Camera.fieldOfView = 60;
            Gun.transform.localEulerAngles = new Vector3(90f, 0, 0f);
            fireScrypt.HoldFire();
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


    // Update is called once per frame
    void Update()
    {
        LookAround();
        Move();
        GunzUp();
        
    }
}
