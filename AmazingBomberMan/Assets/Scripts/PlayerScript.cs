using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed = 10f;
    
    private float minX = -2.55f;
    private float maxX = 2.55f;

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float h = Input.GetAxis("Horizontal");
        Vector2 currentPosition = transform.position;//current position of gameObject
        
        if(h>0)//moving towards right
        {
            currentPosition.x += speed * Time.deltaTime;
        }
        else if(h<0)//moving towards left side
        {
            currentPosition.x -= speed * Time.deltaTime;
        }

        if(currentPosition.x < minX)
        {
            currentPosition.x = minX;
        }

        if(currentPosition.x > maxX)
        {
            currentPosition.x = maxX;
        }

        transform.position = currentPosition;//updating gameObject's new position
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Bomb")
        {
            Time.timeScale = 0f;
        }
    }
}
