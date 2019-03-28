using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditWords : MonoBehaviour
{
    public WordDefinition EditWordInput { get; set; }

    [SerializeField]
    private Text wordInput;
    [SerializeField]
    private InputField descriptionInput;
    [SerializeField]
    private Text warningText;

    /// <summary>
    /// change a words descriprion with the one from the input field
    /// </summary>
    public void OnEditWordPress()
    {
        warningText.text = "";
        if (!string.IsNullOrEmpty(descriptionInput.text))
        {
            Dictionary.Instance.Words[EditWordInput.Word] = descriptionInput.text;
            Dictionary.Instance.RefreshWords();
            gameObject.SetActive(false);
        }
        else
        {
            warningText.text = "You need to write a description for this to work!";
        }
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
}
