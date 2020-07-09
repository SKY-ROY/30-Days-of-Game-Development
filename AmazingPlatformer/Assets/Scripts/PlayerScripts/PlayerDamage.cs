using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour
{
    private Text LifeCountText;

    private int lifeCount;
    private bool canDamage;

    private void Awake()
    {
        LifeCountText = GameObject.Find("LifeCountText").GetComponent<Text>();
        lifeCount = 3;
        LifeCountText.text = lifeCount.ToString();

        canDamage = true;
    }

    private void Start()
    {
        Time.timeScale = 1f;
    }

    public void DealDamage()
    {
        if(canDamage)
        {
            lifeCount--;

            if (lifeCount >= 0)
            {
                LifeCountText.text = lifeCount.ToString();
            }

            if (lifeCount == 0)
            {
                //Restart Game
                Time.timeScale = 0f;
                StartCoroutine(RestartGame());
            }

            canDamage = false;

            StartCoroutine(WaitForDamage());
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.gameObject.tag == MyTags.WATER_TAG)
        {
            StartCoroutine(RestartGame());
        }
    }

    IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(2f);

        canDamage = true;
    }
    
    IEnumerator RestartGame()
    {
        yield return new WaitForSecondsRealtime(2f);
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
