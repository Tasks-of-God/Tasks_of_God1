using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneControler : MonoBehaviour
{
    string gameSceneName = "Miyu";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickStartButton(){
        SceneManager.LoadScene(gameSceneName);
    }
}
