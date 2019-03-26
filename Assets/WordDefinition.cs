using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordDefinition : MonoBehaviour
{
    public string Word;
    public string Definition;

    private Text TextField;

    private void Awake()
    {
        TextField = GetComponent<Text>();
    }

    public void UpdateText()
    {
        TextField.text = Word + " = " + Definition;
    }
}
