using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    }

    /// <summary>
    /// Takes the input word and description and adds it to the Dictionary
    /// </summary>
    public void OnAddWordPress()
    {
        warningText.text = "";
        if (!string.IsNullOrEmpty(wordInput.text) && !string.IsNullOrEmpty(descriptionInput.text))
        {
            Dictionary.Instance.AddedWord = wordInput.text;
            if (!Dictionary.Instance.Words.ContainsKey(Dictionary.Instance.AddedWord))
            {
                Dictionary.Instance.Words.Add(Dictionary.Instance.AddedWord, descriptionInput.text);
                Dictionary.Instance.InstantiateWordObj();
                Dictionary.Instance.RefreshWords();
                wordInput.text = "";
                descriptionInput.text = "";
                searchInput.SearchField.text = "";
                gameObject.SetActive(false);
            }
            else
            {
                warningText.text = "This word is already in the dictionary, you can edit or remove it here.";
                foreach (var word in Dictionary.Instance.WordsPool)
                {
                    if(word.GetComponent<WordDefinition>().Word == Dictionary.Instance.AddedWord)
                    {
                        word.GetComponent<Toggle>().isOn = true;
                        Dictionary.Instance.UpdateLayout();
                    }
                }
                gameObject.SetActive(false);
            }
        }
        else
        {
            warningText.text = "You need to write a word and a description for this to work!";
        }
    }
}
