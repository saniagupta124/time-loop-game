using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StudyByPlayer : MonoBehaviour
{
    [SerializeField] GameObject canvasText;
    Text theText;

    void Start()
    {
        theText = canvasText.GetComponent<Text>();
        RecieveText("");
    }


    public void RecieveText(string _text) 
    {
        theText.text = _text;
    }
}
