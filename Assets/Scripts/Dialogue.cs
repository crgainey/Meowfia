using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public Text text;
    public string[] sentences;
    //int index;


    public void NextSentence()
    {
        text.text = "";
    }

}
