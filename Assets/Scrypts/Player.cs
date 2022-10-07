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
            if (value <= 0f && value >= -90f)
            {
                m_vertical = value;
            }
           
        }
        
    }



    [SerializeField] float Sensitivity = 1;
    [SerializeField] float movementSpeed = 5;

    private CharacterController Controller;
    private GameObject focalPoint;
    // Start is called before the first frame update
    void Start()
    {
        Controller = GetComponent<CharacterController>();
        focalPoint = GameObject.Find("FocalPoint");
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

    public void LookAround()
    {
        vertical += Input.GetAxis("Mouse Y");
        horizontal += Input.GetAxis("Mouse X");
        Vector3 direction = new Vector3(-vertical, 0f, 0f);
        Vector3 playerRotation = new Vector3(0f, Input.GetAxis("Mouse X"), 0f);
        focalPoint.transform.localEulerAngles = direction;
        transform.Rotate(playerRotation * Sensitivity);
        Debug.Log(transform.localEulerAngles);
    }


    // Update is called once per frame
    void Update()
    {
        LookAround();
        Move();
    }
}
