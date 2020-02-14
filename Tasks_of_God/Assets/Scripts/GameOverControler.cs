using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameOverControler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

        public void ClickBuckTitleButton(){
        SceneManager.LoadScene("StartScene");
        Debug.Log("click!");
    }
}
