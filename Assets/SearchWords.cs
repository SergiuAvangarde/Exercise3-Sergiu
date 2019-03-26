using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchWords : MonoBehaviour
{
    [SerializeField]
    private Dictionary wordsList;
    private List<GameObject> ActiveWords;
    private InputField field;

    void Start()
    {
        field = GetComponent<InputField>();
    }

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
    }
}
