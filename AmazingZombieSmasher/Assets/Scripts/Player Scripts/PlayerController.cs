using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    private Rigidbody myBody;

    void Start()
    {
        myBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        ControlMovementWithKeyboard();
    }

    private void FixedUpdate()
    {
        MoveTank();
        ChangeRotation();
    }

    void MoveTank()
    {
        myBody.MovePosition(myBody.position + speed * Time.deltaTime);
    }

    void ControlMovementWithKeyboard()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            MoveLeft();
        }
        
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }
        //increasing forward movement speed
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            MoveFast();
        }
        //reducing forward movement speed
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            MoveSlow();
        }
        
        //the player moves straight when we let go(key comes up) of the left movement key
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            MoveStraight();
        }
        //the player moves straight when we let go(key comes up) of the right movement key
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            MoveStraight();
        }
        //resetting back to normal speed when we let go of Speed Up key
        if(Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
        {
            MoveNormal();
        }
        //resetting back to normal speed when we let go of Speed Down key
        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            MoveNormal();
        }
    }

    void ChangeRotation()
    {
        if(speed.x > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, maxAngle, 0f), Time.deltaTime * rotationSpeed);
        }
        else if (speed.x < 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, -maxAngle, 0f), Time.deltaTime * rotationSpeed);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, 0f), Time.deltaTime * rotationSpeed);
        }
    }
}
