using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SearchWords : MonoBehaviour
{
    public InputField field { get; set; }
    public bool AllInactive { get; set; }

    [SerializeField]
    private Dictionary wordsList;
    [SerializeField]
    private Text WarningText;

    private List<GameObject> ActiveWords;

    void Start()
    {
        field = GetComponent<InputField>();
    }

    /// <summary>
    /// from the string in the input field searches for every word wich contains similar string
    /// if the word is not found the program prints a message
    /// </summary>
    public void SearchText()
    {
        if (!string.IsNullOrEmpty(field.text))
        {
            foreach (var word in wordsList.WordsPool)
            {
                if (!word.GetComponent<WordDefinition>().Word.ToLower().Trim().Contains(field.text.ToLower().Trim()))
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
            foreach (var word in wordsList.WordsPool)
            {
                word.SetActive(true);
            }
        }

        AllInactive = !wordsList.WordsPool.Any(obj => obj.activeInHierarchy == true);
        if (AllInactive)
        {
            WarningText.text = "This word is not found in the dictionary, press Add Word Button to add it to the Dictionary!";
        }
        else
        {
            WarningText.text = " ";
        }
    }
}
