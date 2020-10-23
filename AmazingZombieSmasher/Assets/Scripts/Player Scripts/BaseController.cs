using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    [Header("Movement Parameters")]
    public Vector3 speed;
    public float xSpeed = 7.5f, zSpeed = 15f, accelerated = 25f, decelerated = 5f;
    public float speedIncrementPeriod = 30f;

    [Header("On-screen buttons")]
    public GameObject shootButton;
    public GameObject upButton;
    public GameObject downButton;
    public GameObject rightButton;
    public GameObject leftButton;

    [Header("Monitoring through editor")]
    public float timePassed;

    protected float rotationSpeed = 10f, maxAngle = 10f;

    private AudioSource soundManager;
    private bool isSlow;
    private bool isFast;

    private void Awake()
    {
        soundManager = GetComponent<AudioSource>();
        speed = new Vector3(0f, 0f, zSpeed);
        timePassed = 0f;
    }

    //sideways movement towards left
    protected void MoveLeft()
    {
        speed = new Vector3(-xSpeed / 2f, 0f, speed.z);
    }
    //sideways movement towards right
    protected void MoveRight()
    {
        speed = new Vector3(xSpeed / 2f, 0f, speed.z);
    }
    //forward movement
    protected void MoveStraight()
    {
        speed = new Vector3(0f, 0f, speed.z);
    }

    protected void MoveNormal()
    {
        speed = new Vector3(speed.x, 0f, zSpeed);
    }

    protected void MoveSlow()
    {
        speed = new Vector3(speed.x, 0f, decelerated);
    }

    protected void MoveFast()
    {
        speed = new Vector3(speed.x, 0f, accelerated);
    }
}
