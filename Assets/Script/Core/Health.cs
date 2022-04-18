using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    //  STATE
    bool isDizzying = false;

    //  REF
    CharacterControl control;

    private void Awake()
    {
        control = GetComponent<CharacterControl>();
    }

    private void OnEnable()
    {
        control.onHit += Dizzy;
    }

    private void OnDisable()
    {
        control.onHit -= Dizzy;
    }

    public void Dizzy()
    {
        print("Now I am very dizzy!");
    }
}
