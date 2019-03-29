using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SelectWord : MonoBehaviour
{
    public WordDefinition EditWordInput { get; set; }

    [SerializeField]
    private Text wordInDictionary = null;

    /// <summary>
    /// check if highlighted word is in dictionary and show the coresponding message to popup
    /// set the bool in Dictionary script to know what action is needed on click
    /// </summary>
    private void FixedUpdate()
    {
        if (Dictionary.Instance.Words.ContainsKey(EditWordInput.SelectedDefinitionWord))
        {
            wordInDictionary.text = "This word is in the dictionary, Click to go to word";
            Dictionary.Instance.SelectedWord = EditWordInput.SelectedDefinitionWord;
            Dictionary.Instance.IsInDictionary = true;
            Dictionary.Instance.AddToDictionary = false;
        }
        else
        {
            wordInDictionary.text = "Click to add this word to dictionary.";
            Dictionary.Instance.SelectedWord = EditWordInput.SelectedDefinitionWord;
            Dictionary.Instance.IsInDictionary = false;
            Dictionary.Instance.AddToDictionary = true;
        }
    }

    /// <summary>
    /// reset the variables
    /// </summary>
    private void OnDisable()
    {
        wordInDictionary.text = "";
        Dictionary.Instance.SelectedWord = "";
        Dictionary.Instance.IsInDictionary = false;
        Dictionary.Instance.AddToDictionary = false;
    }
}
