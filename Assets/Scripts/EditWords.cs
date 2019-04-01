using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EditWords : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public WordDefinition EditWordInput { get; set; }

    [SerializeField]
    private InputField wordInput = null;
    [SerializeField]
    private InputField descriptionInput = null;
    [SerializeField]
    private Text warningText = null;

    /// <summary>
    /// change a words descriprion with the one from the input field
    /// </summary>
    public void OnEditWordPress()
    {
        warningText.text = "";
        if (!string.IsNullOrEmpty(descriptionInput.text))
        {
            if (wordInput.text.ToLower() == EditWordInput.Word.ToLower())
            {
                GameManager.Instance.SelectedWord = wordInput.text.ToLower();
                GameManager.Instance.Words[EditWordInput.Word] = descriptionInput.text;
                GameManager.Instance.RefreshWords();
            }
            else
            {
                GameManager.Instance.SelectedWord = wordInput.text.ToLower();
                GameManager.Instance.Words.Remove(EditWordInput.Word);
                GameManager.Instance.Words.Add(wordInput.text, descriptionInput.text);
                GameManager.Instance.RefreshWords();
            }
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
        GameManager.Instance.EditWordPanel.SetActive(true);
        wordInput.text = EditWordInput.Word;
        descriptionInput.text = EditWordInput.Definition;
    }

    /// <summary>
    /// delete a word from the dictionary and refresh the list
    /// </summary>
    public void OnRemovePress()
    {
        GameManager.Instance.Words.Remove(EditWordInput.Word);
        GameManager.Instance.RefreshWords();
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
