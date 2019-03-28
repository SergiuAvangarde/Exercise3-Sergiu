using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SearchWords : MonoBehaviour
{
    public InputField SearchField { get; set; }

    [SerializeField]
    private Text warningText;

    private bool allInactive;

    void Start()
    {
        SearchField = GetComponent<InputField>();
    }

    /// <summary>
    /// from the string in the input field searches for every word wich contains similar string
    /// if the word is not found the program prints a message
    /// </summary>
    public void SearchText()
    {
        if (!string.IsNullOrEmpty(SearchField.text))
        {
            foreach (var word in Dictionary.Instance.WordsPool)
            {
                string wordText = word.GetComponent<WordDefinition>().Word.ToLower().Trim();
                string searchtext = SearchField.text.ToLower().Trim();
                if (!wordText.Contains(searchtext))
                {
                    word.SetActive(false);
                }
                else
                {
                    word.SetActive(true);
                }
            }
        }
        else
        {
            foreach (var word in Dictionary.Instance.WordsPool)
            {
                word.SetActive(true);
            }
        }

        allInactive = !Dictionary.Instance.WordsPool.Any(obj => obj.activeInHierarchy == true);
        if (allInactive)
        {
            warningText.text = "This word is not found in the dictionary, press Add Word to add it to the Dictionary!";
        }
        else
        {
            warningText.text = " ";
        }
    }
}
