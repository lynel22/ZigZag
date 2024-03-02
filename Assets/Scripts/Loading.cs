using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        audioManager.instance.Play("Carga");

        string levelTOload = LevelLoader.nextLevel;

        StartCoroutine(this.MakeTheLoad(levelTOload));
    }

    // Update is called once per frame
    IEnumerator MakeTheLoad(string level)
    {
        yield return new WaitForSeconds(3f);

        audioManager.instance.Stop("Carga");
        AsyncOperation operation = SceneManager.LoadSceneAsync(level);

        while (!operation.isDone)
        {
            yield return null;
        }
        
    }
}
