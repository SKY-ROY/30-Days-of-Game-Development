using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossHealth : MonoBehaviour
{
    private Animator anim;

    [SerializeField]
    private int health = 10;
    private bool canDamage;

    public GameObject mainMenuButton;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        canDamage = true;
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(canDamage)
        {
            if(target.tag == MyTags.BULLET_TAG)
            {
                health--;
                canDamage = false;

                if (health == 0)
                {
                    GetComponent<BossScript>().DeactivateBoss();
                    //GetComponent<BoxCollider2D>().isTrigger = true;
                    anim.Play("BossDead");
                    Time.timeScale = 0f;
                    mainMenuButton.SetActive(true);

                }

                StartCoroutine(WaitForDamage());
            }
        }
    }

    IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(2f);

        canDamage = true;
    }

    public void GotoMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
