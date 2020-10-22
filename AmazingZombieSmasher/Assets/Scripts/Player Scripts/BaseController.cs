using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    [Header("Movement Parameters")]
    public Vector3 speed;
    public float xSpeed = 7.5f, zSpeed = 15f, accelerated = 25f, decelerated = 5f;
    public float incrementPeriod = 30f;

    [Header("Movement based Sound Parameters")]
    public float lowSoundPitch;
    public float normalSoundPitch; 
    public float highSoundPitch;
    public AudioClip engineOnSound; 
    public AudioClip engineOffSound;

    [Header("On-screen buttons")]
    public GameObject buttonShoot;
    public GameObject buttonDown;
    public GameObject buttonRight;
    public GameObject buttonLeft;
    public GameObject buttonUp;

    [Header("Monitoring through editor")]
    public float timePassed;

    protected float rotationSpeed = 10f, maxAngle = 10f;

    private AudioSource soundManager;
    private bool isSlow;

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
        if(isSlow)
        {
            isSlow = false;

            soundManager.Stop();
            soundManager.clip = engineOnSound;
            soundManager.volume = 0.3f;
            soundManager.Play();
        }
        speed = new Vector3(speed.x, 0f, zSpeed);
    }

    protected void MoveSlow()
    {
        if(!isSlow)
        {
            isSlow = true;

            soundManager.Stop();
            soundManager.clip = engineOffSound;
            soundManager.volume = 0.5f;
            soundManager.Play();
        }

        speed = new Vector3(speed.x, 0f, decelerated);
    }

    protected void MoveFast()
    {
        soundManager.Stop();
        soundManager.clip = engineOnSound;
        soundManager.volume = 0.3f;
        soundManager.Play();

        speed = new Vector3(speed.x, 0f, accelerated);
    }
}
