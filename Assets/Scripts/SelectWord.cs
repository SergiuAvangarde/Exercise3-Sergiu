using UnityEngine;
using UnityEngine.UI;

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
        if (GameManager.Instance.Words.ContainsKey(EditWordInput.SelectedDefinitionWord))
        {
            wordInDictionary.text = "This word is in the dictionary, Click to go to word";
            GameManager.Instance.SelectedWord = EditWordInput.SelectedDefinitionWord;
            GameManager.Instance.IsInDictionary = true;
            GameManager.Instance.AddToDictionary = false;
        }
        else
        {
            wordInDictionary.text = "Click to add this word to dictionary.";
            GameManager.Instance.SelectedWord = EditWordInput.SelectedDefinitionWord;
            GameManager.Instance.IsInDictionary = false;
            GameManager.Instance.AddToDictionary = true;
        }
    }

    /// <summary>
    /// reset the variables
    /// </summary>
    private void OnDisable()
    {
        wordInDictionary.text = "";
        GameManager.Instance.SelectedWord = "";
        GameManager.Instance.IsInDictionary = false;
        GameManager.Instance.AddToDictionary = false;
    }
}
