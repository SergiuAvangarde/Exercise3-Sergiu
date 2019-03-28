﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WordDefinition : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    public string Word;
    public string Definition;

    private Text textField;
    private Toggle toggleButton;
    private RectTransform popUpCachedTransform;
    private RectTransform WordCachedTransform;
    private EditWords popUpCachedScript;
    private WordDefinition wordDefinitionCache;
    private string[] definitionWords;

    private void Awake()
    {
        textField = GetComponent<Text>();
        toggleButton = GetComponent<Toggle>();
        WordCachedTransform = gameObject.GetComponent<RectTransform>();
        popUpCachedTransform = Dictionary.Instance.EditOrRemovePopUp.GetComponent<RectTransform>();
        popUpCachedScript = Dictionary.Instance.EditOrRemovePopUp.GetComponent<EditWords>();
        wordDefinitionCache = GetComponent<WordDefinition>();
    }

    /// <summary>
    /// shows this word with it's definition to the UI
    /// </summary>
    public void UpdateText()
    {
        definitionWords = Definition.Trim().Split(' ', '.', ',', ';', '/', '(', ')', '[', ']');
        textField.text = Word + " = " + Definition;
    }

    /// <summary>
    /// these two functions make the same thing, I used them both because only using one of them did not work in some cases
    /// if the toggle button is on the position for the PopUp with the Edit or Remove buttons changes acording to mouse position
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (toggleButton.isOn)
        {
            Dictionary.Instance.EditWordPanel.SetActive(false);
            Dictionary.Instance.EditOrRemovePopUp.SetActive(true);
            popUpCachedTransform.anchoredPosition = new Vector3(Input.mousePosition.x, WordCachedTransform.position.y + WordCachedTransform.rect.height/2, Input.mousePosition.z);
            popUpCachedScript.EditWordInput = wordDefinitionCache;
        }
        else
        {
            Dictionary.Instance.EditOrRemovePopUp.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (toggleButton.isOn)
        {
            Dictionary.Instance.EditOrRemovePopUp.SetActive(true);
            popUpCachedTransform.anchoredPosition = new Vector3(Input.mousePosition.x, WordCachedTransform.position.y + WordCachedTransform.rect.height/2, Input.mousePosition.z);
            popUpCachedScript.EditWordInput = wordDefinitionCache;
        }
        else
        {
            Dictionary.Instance.EditOrRemovePopUp.SetActive(false);
        }
    }
}
