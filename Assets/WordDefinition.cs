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

    /// <summary>
    /// shows this word with it's definition to the UI
    /// </summary>
    public void UpdateText()
    {
        TextField.text = Word + " = " + Definition;
    }
}
