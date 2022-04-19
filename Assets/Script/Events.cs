using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Events : MonoBehaviour
{

    public void ReplayGame()
    {
        SceneManager.LoadScene("TestMap");
    }


    public void QuitGame()
    {
        NetworkManager.instance.LoadLevel("WelcomeScene");
        //SceneManager.LoadScene("WelcomeScene");
    }
}
