using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    public GameObject[] obstaclePrefabs;
    public GameObject[] zombiePrefabs;
    public Transform[] lanes;

    public float min_ObstacleDelay = 10f, max_ObstacleDelay = 40f;

    private float halfGroundSize;
    private BaseController playerController;

    private void Awake()
    {
        MakeInstance();
    }

    // Start is called before the first frame update
    void Start()
    {
        halfGroundSize = GameObject.Find("GroundBlockMain").GetComponent<GroundBlock>().halfLength;
        playerController = GameObject.FindGameObjectWithTag(MyTags.PLAYER_TAG).GetComponent<BaseController>();
    }

    void MakeInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != null)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator GenerateObstacles()
    {
        float timer = Random.Range(min_ObstacleDelay, max_ObstacleDelay) / playerController.speed.z;

        yield return new WaitForSeconds(timer);

        CreateObstacles(playerController.gameObject.transform.position.z + halfGroundSize);

        StartCoroutine("GenerateObstacles");
    }

    void CreateObstacles(float zPos)
    {
        int r = Random.Range(0, 10);

        if(0 <= r && r < 7)
        {
            int obstacleLane = Random.Range(0, lanes.Length);

            //Add Obstacle

            int zombieLane = 0;

            if(obstacleLane == 0)
            {
                zombieLane = (Random.Range(0, 2) == 1) ? 1 : 2;
            }
            else if(obstacleLane == 1)
            {
                zombieLane = (Random.Range(0, 2) == 1) ? 0 : 2;
            }
            else if(obstacleLane == 2)
            {
                zombieLane = (Random.Range(0, 2) == 1) ? 1 : 0;
            }
        }
    }
}
