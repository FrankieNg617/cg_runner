//***
//'Sound' is a customized class for instantiating the sounds


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Sound 
{
  public string name;
  public AudioClip clip;
  public float volume;
  public bool loop;
  public AudioSource source;
}
