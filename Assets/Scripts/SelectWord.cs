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
    private GameObject wordInDictionary = null;
    [SerializeField]
    private GameObject addWordDictionary = null;

    private void FixedUpdate()
    {
        if (Dictionary.Instance.Words.ContainsKey(EditWordInput.SelectedDefinitionWord))
        {
            wordInDictionary.SetActive(true);
            addWordDictionary.SetActive(false);
            Dictionary.Instance.SelectedWord = EditWordInput.SelectedDefinitionWord;
            Dictionary.Instance.IsInDictionary = true;
            Dictionary.Instance.AddToDictionary = false;
        }
        else
        {
            wordInDictionary.SetActive(false);
            addWordDictionary.SetActive(true);
            Dictionary.Instance.SelectedWord = EditWordInput.SelectedDefinitionWord;
            Dictionary.Instance.IsInDictionary = false;
            Dictionary.Instance.AddToDictionary = true;
        }
    }
}
