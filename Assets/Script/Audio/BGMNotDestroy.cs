using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMNotDestroy : MonoBehaviour
{
    private void Awake()
    {
        AudioSource bgm = GameObject.FindGameObjectWithTag("WelcomeBGM").GetComponent<AudioSource>();

        if(!bgm.isPlaying)
        {
            bgm.Play();
        }
        
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("WelcomeBGM");

        if(musicObj.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

}
