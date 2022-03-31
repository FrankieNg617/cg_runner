using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    public static bool isGameStarted;
    public GameObject startingText;

    void Start()
    {
        isGameStarted = false;
    }

    
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            isGameStarted = true;
            Destroy(startingText);
        }
    }
}
