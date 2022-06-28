using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveName : MonoBehaviour
{
    public InputField textBox;

    public void clickSaveButton()
    {
        PlayerPrefs.SetString("name", textBox.text);
        Debug.Log("Your name is " + PlayerPrefs.GetString("name"));
    }
    // Start is called before the first frame update
   
}
