using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    private static GameAssets GameAssets = GameObject.Find("GameAssets").GetComponent<GameAssets>();
    private static Dictionary<Sound, float> soundTimerDictionary;
    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;
    
    public enum Sound
    {
        PlayerMove,
        PlayerDie,
        EnemyDie,
        GameOver,
        SpawnEnemy,
        buttonClick,
    }

    public static void Initialize()
    {
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.PlayerMove] = 0f;
    }
    public static void PlaySound(Sound sound, Vector3 position)//for 3D Audio based on player position
    {
        if (CanPlaySound(sound))
        {
            GameObject soundGameObject = new GameObject("3DSound");
            soundGameObject.transform.position = position;
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.clip = GetAudioClip(sound);
            audioSource.Play();

            Object.Destroy(soundGameObject, audioSource.clip.length);
        }
    }
    public static void PlaySound(Sound sound)
    {
        if (CanPlaySound(sound))
        {
            if (oneShotGameObject == null)
            {
                oneShotGameObject = new GameObject("OneShotSound");
                oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
            }
            oneShotAudioSource.PlayOneShot(GetAudioClip(sound));
        }
    }
    private static bool CanPlaySound(Sound sound)
    {
        switch(sound)
        {
            default:
                return true;
            case Sound.PlayerMove:
                if(soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float playerMoveTimerMax = .05f;
                    if (lastTimePlayed + playerMoveTimerMax < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    } 
                }
                else
                {
                    return false;
                }
                //break;
        }
    }
    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach(GameAssets.SoundAudioClip soundAudioClip in GameAssets.soundAudioClipArray)
        {
            if(soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound " + sound + " not found!");
        return null;
    }
}
