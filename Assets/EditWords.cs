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

    public void OnEditWordPress()
    {
        if (!string.IsNullOrEmpty(descriptionInput.text))
        {
            Dictionary.Instance.Words[EditWordInput.Word] = descriptionInput.text;
            Dictionary.Instance.RefreshWords();
            gameObject.SetActive(false);
        }
        else
        {
            print("You need to write a description for this to work!");
        }
    }

    public void OpenEditPanel()
    {
        Dictionary.Instance.EditWordPanel.SetActive(true);
        wordInput.text = EditWordInput.Word;
        descriptionInput.text = EditWordInput.Definition;
    }

    public void OnRemovePress()
    {
        Dictionary.Instance.Words.Remove(EditWordInput.Word);
        Dictionary.Instance.RefreshWords();
    }
}
