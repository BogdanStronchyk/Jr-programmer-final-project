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

    private CharacterController Controller;
    // Start is called before the first frame update
    void Start()
    {
        Controller = GetComponent<CharacterController>();
    }

    public void Move()
    {
        Vector3 directionForward = transform.forward;
        directionForward.y = 0;

        Vector3 directionRightLeft = transform.right;
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
        Vector3 direction = new Vector3(-vertical, horizontal, 0f);
        transform.localEulerAngles = direction * Sensitivity;
    }


    // Update is called once per frame
    void Update()
    {
        LookAround();
        Move();
    }
}
