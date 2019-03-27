using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordDefinition : MonoBehaviour
{
    public GameObject EditPanel { get; set; }
    public string Word;
    public string Definition;

    //private Transform editPanelTransformCache;
    private Text textField;
    private Toggle toggleButton;

    private void Awake()
    {
        textField = GetComponent<Text>();
        toggleButton = GetComponent<Toggle>();
    }

    //public void ToggleSelected()
    //{
    //    if (toggleButton.isOn)
    //    {
    //        EditPanel.SetActive(true);
    //    }
    //    else
    //    {
    //        EditPanel.SetActive(false);
    //    }
    //}

    /// <summary>
    /// shows this word with it's definition to the UI
    /// </summary>
    public void UpdateText()
    {
        textField.text = Word + " = " + Definition;
    }

    public void OnEditPress()
    {
        EditPanel.GetComponent<EditWords>().editWordInput = GetComponent<WordDefinition>();
        EditPanel.SetActive(true);
    }

    public void OnRemovePress()
    {
        Dictionary.Instance.words.Remove(Word);
        Dictionary.Instance.RefreshWords();
    }
}
