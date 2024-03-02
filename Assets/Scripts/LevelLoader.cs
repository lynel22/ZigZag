using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public static class LevelLoader 
{
    public static string nextLevel;

    

    public static void LoadNextLevel(string nivel){
        nextLevel = nivel;
        SceneManager.LoadScene("CargaNivel");
    }
}
