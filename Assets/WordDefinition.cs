using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WordDefinition : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
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

    /// <summary>
    /// shows this word with it's definition to the UI
    /// </summary>
    public void UpdateText()
    {
        textField.text = Word + " = " + Definition;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (toggleButton.isOn)
        {
            Dictionary.Instance.EditWordPanel.SetActive(false);
            Dictionary.Instance.EditOrRemovePopUp.SetActive(true);
            Dictionary.Instance.EditOrRemovePopUp.GetComponent<RectTransform>().anchoredPosition = new Vector3(Input.mousePosition.x, gameObject.transform.position.y + 80, Input.mousePosition.z);
            Dictionary.Instance.EditOrRemovePopUp.GetComponent<EditWords>().editWordInput = GetComponent<WordDefinition>();
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
            Dictionary.Instance.EditWordPanel.SetActive(false);
            Dictionary.Instance.EditOrRemovePopUp.SetActive(true);
            Dictionary.Instance.EditOrRemovePopUp.GetComponent<RectTransform>().anchoredPosition = new Vector3(Input.mousePosition.x, gameObject.transform.position.y + 80, Input.mousePosition.z);
            Dictionary.Instance.EditOrRemovePopUp.GetComponent<EditWords>().editWordInput = GetComponent<WordDefinition>();
        }
        else
        {
            Dictionary.Instance.EditOrRemovePopUp.SetActive(false);
        }
    }
}
