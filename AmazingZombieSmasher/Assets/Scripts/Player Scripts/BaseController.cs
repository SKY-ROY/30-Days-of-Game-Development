using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    private bool isSlow;
    private AudioSource soundManager;

    public Vector3 speed;
    public float xSpeed = 8f, zSpeed = 15f, accelerated = 15f, decelerated = 10f;
    public float lowSoundPitch, normalSoundPitch, highSoundPitch;
    public AudioClip engineOnSound, engineOffSound;

    protected float rotationSpeed = 10f, maxAngle = 10f;

    private void Awake()
    {
        soundManager = GetComponent<AudioSource>();
        speed = new Vector3(0f, 0f, zSpeed);
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
