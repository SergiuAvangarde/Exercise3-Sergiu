using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditWords : MonoBehaviour
{
    public WordDefinition editWordInput { get; set; }

    [SerializeField]
    private Text WordInput;
    [SerializeField]
    private InputField descriptionInput;

    private void OnEnable()
    {
        WordInput.text = editWordInput.Word;
        descriptionInput.text = editWordInput.Definition;
    }

    public void OnEditWordPress()
    {
        if (!string.IsNullOrEmpty(descriptionInput.text))
        {
            Dictionary.Instance.words[editWordInput.Word] = descriptionInput.text;
            Dictionary.Instance.RefreshWords();
            gameObject.SetActive(false);
        }
        else
        {
            print("You need to write description for this to work!");
        }
    }
}
