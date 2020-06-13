using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoaderScript : MonoBehaviour
{
    public string nextScene;
    public Animator transition;
    public float transitionTime = 1f;

    // Update is called once per frame
    void Update()
    {
        // enable this part to test scene transitions or just call LoadNextLevel() method anywhere when you want to transition scene.
        /*       
        if(Input.GetMouseButtonDown(0))
        {
            LoadNextLevel(nextScene);
        }
        */ 
    }
    
    public void LoadNextLevel(string nextS)
    {
        StartCoroutine(LoadLevel(nextS));
    }

    IEnumerator LoadLevel(string sceneName)
    {
        //Play animation
        transition.SetTrigger("Start");

        //Wait
        yield return new WaitForSeconds(transitionTime);

        //Load Scene
        SceneManager.LoadScene(sceneName);
    }
}
