using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class SessionController : MonoBehaviour
{
    private void Awake()
    {
        SoundManager.Initialize();
    }
    private void Update()
    {
        if(Input.GetKey(KeyCode.Alpha0))
            SoundManager.PlaySound(SoundManager.Sound.PlayerMove, transform.position);
    }
    public void Button1()
    {
        SoundManager.PlaySound(SoundManager.Sound.PlayerMove);
    }
    public void Button2()
    {
        SoundManager.PlaySound(SoundManager.Sound.EnemyDie);
    }
    public void Button3()
    {

    }
    public void Button4()
    {

    }
    public void Button5()
    {

    }
}
