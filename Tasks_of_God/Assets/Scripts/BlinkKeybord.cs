using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BlinkKeybord : MonoBehaviour
{

    //public
    public float speed = 1.0f;

    //private
    private KeyCode key;

    void Start()
    {
        // char key_name = "a";
        // for(int i=0; i<keys.Length; i++, key_name++){
        //     keys[i] = Gameobject.Find("keybord_" + key_name);
        // }
    }

    void Update()
    {
        // if (Input.anyKeyDown) {
        //     foreach (KeyCode code in Enum.GetValues(typeof(KeyCode))) {
        //         if (Input.GetKeyDown (code)) {
        //             key = code;
        //             Debug.Log (code);
        //             break;
        //         }
        //     }
        // }
        // if(Input.GetKeyDown(key)){
        //     Image key_image = GameObject.Find("keybord_" + key.ToString());
        //     this.key_image = this.bananaObject.GetComponent<Image>();
        //     // key_image.enabled = true;
        //     Debug.Log ("Down" + key);
        // }else if(Input.GetKeyUp(key)){
        //     Image key_image = GameObject.Find("keybord_" + key.ToString());
        //     this.key_image = this.bananaObject.GetComponent<Image>();
        //     // key_image.enabled = false;
        //     Debug.Log ("Up" + key);
        // }
    }
}
