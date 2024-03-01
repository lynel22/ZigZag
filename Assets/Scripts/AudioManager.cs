using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class audioManager : MonoBehaviour
{   
    public Audio[] audios;
    public static audioManager instance;
    
    void Awake()
    {
         if(instance == null)
          {
                instance = this;
          }
          else
          {
                Destroy(gameObject);
                return;
          }
          
          DontDestroyOnLoad(gameObject);
          
          foreach(Audio a in audios)
          {
                a.source = gameObject.AddComponent<AudioSource>();
                a.source.clip = a.clip;
                a.source.volume = a.volume;
                a.source.loop = a.loop;
          }    
    }

    public void Play(string name)
    {
        Audio a = System.Array.Find(audios, audio => audio.name == name);
        if(a == null)
        {
            Debug.LogWarning("Audio: " + name + " not found!");
            return;
        }
        a.source.Play();
    }

    public void Stop(string name)
    {
        Audio a = System.Array.Find(audios, audio => audio.name == name);
        if(a == null)
        {
            Debug.LogWarning("Audio: " + name + " not found!");
            return;
        }
        a.source.Stop();
    }
}
