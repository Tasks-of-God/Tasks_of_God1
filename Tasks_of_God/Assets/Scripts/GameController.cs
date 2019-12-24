using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update

    InputField inputField;
    Text text;

    void Start()
    {
        inputField = GameObject.Find("InputField").GetComponent<InputField>();
        text = GameObject.Find("Message").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void InputText()
    {

        text.text = inputField.text;

    }

}
