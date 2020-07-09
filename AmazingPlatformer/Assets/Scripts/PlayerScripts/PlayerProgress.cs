using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class PlayerProgress : MonoBehaviour
{
    private Text coinCountText;
    private AudioSource coinSound;

    private int coinCount = 0;

    private void Awake()
    {
        coinSound = GetComponent<AudioSource>();
    }

    void Start()
    {
        coinCountText = GameObject.Find("CoinCountText").GetComponent<Text>();
    }
    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == MyTags.COIN_TAG)
        {
            target.gameObject.SetActive(false);

            IncrementCoinCount();

            coinSound.Play();
        }
    }

    public void IncrementCoinCount()
    {
        coinCount++;
        coinCountText.text = coinCount.ToString();
    }
}
