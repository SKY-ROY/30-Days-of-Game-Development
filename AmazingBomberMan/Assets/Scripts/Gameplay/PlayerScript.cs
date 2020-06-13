using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed = 10f;
    public GameObject gameOverPanel;
    public LevelLoaderScript levelLoader;
    
    private float minX = -2.55f;
    private float maxX = 2.55f;
    private GameObject[] enemies;

    private void Awake()
    {
        SoundManager.Initialize();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            SoundManager.PlaySound(SoundManager.Sound.PlayerMove);
        }
        MovePlayer();
    }

    private void MovePlayer()
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
            SoundManager.PlaySound(SoundManager.Sound.PlayerDie);
            GameOver();
        }
    }
    
    public void GameOver()
    {
        Time.timeScale = 0f;
        SoundManager.PlaySound(SoundManager.Sound.GameOver);
        
        GameObject[] enemiesArray = GameObject.FindGameObjectsWithTag("Bomb");
        enemies = enemiesArray;

        GameObject.Find("Spawner").GetComponent<SpawnerScript>().StopSpawn();

        gameOverPanel.SetActive(true);
    }

    public void Restartgame()
    {
        Time.timeScale = 1.0f;

        SoundManager.PlaySound(SoundManager.Sound.buttonClick);
        
        //Destroy all enemies in the array
        foreach (GameObject temp in enemies)
            Destroy(temp);

        levelLoader.LoadNextLevel("GameScene");
    }
}
