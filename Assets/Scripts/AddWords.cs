﻿using UnityEngine;
using UnityEngine.UI;

public class AddWords : MonoBehaviour
{
    [SerializeField]
    private InputField wordInput;
    [SerializeField]
    private InputField descriptionInput;
    [SerializeField]
    private Text warningText;
    [SerializeField]
    private SearchWords searchInput;

    private void OnEnable()
    {
        if (!string.IsNullOrEmpty(searchInput.SearchField.text))
        {
            wordInput.text = searchInput.SearchField.text;
        }
        if (!string.IsNullOrEmpty(GameManager.Instance.SelectedWord))
        {
            wordInput.text = GameManager.Instance.SelectedWord;
        }
    }

    /// <summary>
    /// Takes the input word and description and adds it to the Dictionary
    /// </summary>
    public void OnAddWordPress()
    {
        warningText.text = "";
        if (!string.IsNullOrEmpty(wordInput.text) && !string.IsNullOrEmpty(descriptionInput.text))
        {
            GameManager.Instance.SelectedWord = wordInput.text.ToLower();
            if (GameManager.Instance.Words.ContainsKey(GameManager.Instance.SelectedWord))
            {
                warningText.text = "This word is already in the dictionary, you can edit or remove it here.";
                GameManager.Instance.ActiveSelectedWord();
                gameObject.SetActive(false);
            }
            else
            {
                GameManager.Instance.Words.Add(GameManager.Instance.SelectedWord, descriptionInput.text);
                GameManager.Instance.InstantiateWordObj();
                GameManager.Instance.RefreshWords();
                wordInput.text = "";
                descriptionInput.text = "";
                searchInput.SearchField.text = "";
                gameObject.SetActive(false);
            }
        }
        else
        {
            warningText.text = "You need to write a word and a description for this to work!";
        }
    }
}
