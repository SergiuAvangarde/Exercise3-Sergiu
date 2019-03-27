﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddWords : MonoBehaviour
{
    [SerializeField]
    private SearchWords searchInput;
    [SerializeField]
    private InputField wordInput;
    [SerializeField]
    private InputField descriptionInput;

    public string AddedWord;

    private void OnEnable()
    {
        if (searchInput.AllInactive)
        {
            wordInput.text = searchInput.field.text;
        }
    }

    /// <summary>
    /// Takes the input word and description and adds it to the Dictionary
    /// </summary>
    public void OnAddWordPress()
    {
        if(!string.IsNullOrEmpty(wordInput.text) && !string.IsNullOrEmpty(descriptionInput.text))
        {
            AddedWord = wordInput.text;
            Dictionary.Instance.Words.Add(wordInput.text, descriptionInput.text);
            Dictionary.Instance.InstantiateWordObj();
            Dictionary.Instance.RefreshWords();
            wordInput.text = "";
            descriptionInput.text = "";
            searchInput.field.text = "";
            gameObject.SetActive(false);
        }
        else
        {
            print("You need to write a word and a description for this to work!");
        }
    }
}
