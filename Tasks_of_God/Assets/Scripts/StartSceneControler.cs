using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneControler : MonoBehaviour
{
    string gameSceneName = "MainScene";
    bool gameStart = false;
    private float step_time;    // 経過時間カウント用
    public AudioClip startSound;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        step_time = 0.0f;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameStart){
            // 経過時間をカウント
            step_time += Time.deltaTime;
 
            // 3秒後に画面遷移（scene2へ移動）
            if (step_time >= 1.5f)
            {
                SceneManager.LoadScene(gameSceneName);
            }
        }

    }

    public void ClickStartButton(){
        gameStart = true;
        audioSource.PlayOneShot(startSound);
    }
}
