using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EditWords : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public WordDefinition EditWordInput { get; set; }

    [SerializeField]
    private InputField wordInput;
    [SerializeField]
    private InputField descriptionInput;
    [SerializeField]
    private Text warningText;

    [SerializeField]
    private GameObject wordText;
    [SerializeField]
    private GameObject wordInDictionary;
    [SerializeField]
    private GameObject addWordDictionary;

    private void FixedUpdate()
    {
        if (string.IsNullOrEmpty(EditWordInput.SelectedDefinitionWord))
        {
            wordText.SetActive(true);
            wordInDictionary.SetActive(false);
            addWordDictionary.SetActive(false);
        }
        else
        {
            string definition;
            if (Dictionary.Instance.Words.TryGetValue(EditWordInput.SelectedDefinitionWord, out definition))
            {
                wordText.SetActive(false);
                wordInDictionary.SetActive(true);
                Dictionary.Instance.SelectedWord = EditWordInput.SelectedDefinitionWord;
                addWordDictionary.SetActive(false);
            }
            else
            {
                wordText.SetActive(false);
                wordInDictionary.SetActive(false);
                addWordDictionary.SetActive(true);
            }
        }
    }

    /// <summary>
    /// change a words descriprion with the one from the input field
    /// </summary>
    public void OnEditWordPress()
    {
        warningText.text = "";
        if (!string.IsNullOrEmpty(descriptionInput.text))
        {
            if (wordInput.text == EditWordInput.Word)
            {
                Dictionary.Instance.SelectedWord = wordInput.text;
                Dictionary.Instance.Words[EditWordInput.Word] = descriptionInput.text;
                Dictionary.Instance.RefreshWords();
                gameObject.SetActive(false);
            }
            else
            {
                Dictionary.Instance.SelectedWord = wordInput.text;
                Dictionary.Instance.Words.Remove(EditWordInput.Word);
                Dictionary.Instance.Words.Add(wordInput.text, descriptionInput.text);
                Dictionary.Instance.RefreshWords();
                gameObject.SetActive(false);
            }
        }
        else
        {
            warningText.text = "You need to write a description for this to work!";
        }
    }

    public void GoToWord()
    {
        foreach (var word in Dictionary.Instance.WordsPool)
        {
            if (word.GetComponent<WordDefinition>().Word == Dictionary.Instance.SelectedWord)
            {
                word.GetComponent<Toggle>().isOn = true;
                StartCoroutine(Dictionary.Instance.WaitToUpdate());
            }
        }
        gameObject.SetActive(false);
    }

    /// <summary>
    /// open the edit word panel and set the input word and description acording to the word selected
    /// </summary>
    public void OpenEditPanel()
    {
        Dictionary.Instance.EditWordPanel.SetActive(true);
        wordInput.text = EditWordInput.Word;
        descriptionInput.text = EditWordInput.Definition;
    }

    /// <summary>
    /// delete a word from the dictionary and refresh the list
    /// </summary>
    public void OnRemovePress()
    {
        Dictionary.Instance.Words.Remove(EditWordInput.Word);
        Dictionary.Instance.RefreshWords();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        EditWordInput.MouseOvertext = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.SetActive(false);
    }
}
