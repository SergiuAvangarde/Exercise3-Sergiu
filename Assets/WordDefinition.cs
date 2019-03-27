using System.Collections;
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

    private RectTransform PopUpCachedTransform;
    private EditWords PopUpCachedScript;

    private void Awake()
    {
        textField = GetComponent<Text>();
        toggleButton = GetComponent<Toggle>();
        PopUpCachedTransform = Dictionary.Instance.EditOrRemovePopUp.GetComponent<RectTransform>();
        PopUpCachedScript = Dictionary.Instance.EditOrRemovePopUp.GetComponent<EditWords>();
    }

    /// <summary>
    /// shows this word with it's definition to the UI
    /// </summary>
    public void UpdateText()
    {
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
            PopUpCachedTransform.anchoredPosition = new Vector3(Input.mousePosition.x, gameObject.transform.position.y + 80, Input.mousePosition.z);
            PopUpCachedScript.EditWordInput = GetComponent<WordDefinition>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (toggleButton.isOn)
        {
            Dictionary.Instance.EditOrRemovePopUp.SetActive(true);
            PopUpCachedTransform.anchoredPosition = new Vector3(Input.mousePosition.x, gameObject.transform.position.y + 80, Input.mousePosition.z);
            PopUpCachedScript.EditWordInput = GetComponent<WordDefinition>();
        }
        else
        {
            Dictionary.Instance.EditOrRemovePopUp.SetActive(false);
        }
    }
}
