using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class WordDefinition : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public string Word;
    public string Definition;

    public string SelectedDefinitionWord;
    public bool MouseOvertext = false;

    //private Text textField;
    private TextMeshProUGUI textMeshField;
    private Toggle toggleButton;
    private RectTransform popUpCachedTransform;
    private RectTransform wordPopUpCachedTransform;
    private RectTransform definitionCachedTransform;
    private EditWords popUpCachedScript;
    private SelectWord wordPopUpCachedScript;
    private WordDefinition wordDefinitionCache;
    private int m_selectedWord = -1;

    private void Awake()
    {
        //textField = GetComponent<Text>();
        textMeshField = GetComponent<TextMeshProUGUI>();

        toggleButton = GetComponent<Toggle>();
        definitionCachedTransform = gameObject.GetComponent<RectTransform>();
        popUpCachedTransform = Dictionary.Instance.EditOrRemovePopUp.GetComponent<RectTransform>();
        wordPopUpCachedTransform = Dictionary.Instance.WordPopUp.GetComponent<RectTransform>();
        popUpCachedScript = Dictionary.Instance.EditOrRemovePopUp.GetComponent<EditWords>();
        wordPopUpCachedScript = Dictionary.Instance.WordPopUp.GetComponent<SelectWord>();
        wordDefinitionCache = GetComponent<WordDefinition>();
    }

    private void FixedUpdate()
    {
        int wordIndex = TMP_TextUtilities.FindIntersectingWord(textMeshField, Input.mousePosition, null);

        // Clear previous word selection.
        if (m_selectedWord != -1 && (wordIndex == -1 || wordIndex != m_selectedWord))
        {
            TMP_WordInfo wInfo = textMeshField.textInfo.wordInfo[m_selectedWord];

            // Iterate through each of the characters of the word.
            for (int i = 0; i < wInfo.characterCount; i++)
            {
                int characterIndex = wInfo.firstCharacterIndex + i;

                // Get the index of the material / sub text object used by this character.
                int meshIndex = textMeshField.textInfo.characterInfo[characterIndex].materialReferenceIndex;

                // Get the index of the first vertex of this character.
                int vertexIndex = textMeshField.textInfo.characterInfo[characterIndex].vertexIndex;

                // Get a reference to the vertex color
                Color32[] vertexColors = textMeshField.textInfo.meshInfo[meshIndex].colors32;

                Color32 c = vertexColors[vertexIndex + 0].Tint(1.33333f);

                vertexColors[vertexIndex + 0] = c;
                vertexColors[vertexIndex + 1] = c;
                vertexColors[vertexIndex + 2] = c;
                vertexColors[vertexIndex + 3] = c;
            }

            // Update Geometry
            textMeshField.UpdateVertexData(TMP_VertexDataUpdateFlags.All);

            m_selectedWord = -1;
            Dictionary.Instance.WordPopUp.SetActive(false);
        }

        if (MouseOvertext)
        {
            if (toggleButton.isOn)
            {
                Dictionary.Instance.EditOrRemovePopUp.SetActive(true);
                popUpCachedTransform.anchoredPosition = new Vector3(Input.mousePosition.x, definitionCachedTransform.position.y + definitionCachedTransform.rect.height / 2, Input.mousePosition.z);
                wordPopUpCachedTransform.anchoredPosition = Input.mousePosition;
                popUpCachedScript.EditWordInput = wordDefinitionCache;
                wordPopUpCachedScript.EditWordInput = wordDefinitionCache;
            }

            if (wordIndex > 0)
            {
                // Word Selection Handling
                if (wordIndex != -1 && wordIndex != m_selectedWord && !(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
                {
                    m_selectedWord = wordIndex;

                    TMP_WordInfo wInfo = textMeshField.textInfo.wordInfo[wordIndex];

                    // Iterate through each of the characters of the word.
                    for (int i = 0; i < wInfo.characterCount; i++)
                    {
                        int characterIndex = wInfo.firstCharacterIndex + i;

                        // Get the index of the material / sub text object used by this character.
                        int meshIndex = textMeshField.textInfo.characterInfo[characterIndex].materialReferenceIndex;

                        int vertexIndex = textMeshField.textInfo.characterInfo[characterIndex].vertexIndex;

                        // Get a reference to the vertex color
                        Color32[] vertexColors = textMeshField.textInfo.meshInfo[meshIndex].colors32;

                        Color32 c = vertexColors[vertexIndex + 0].Tint(0.75f);

                        vertexColors[vertexIndex + 0] = c;
                        vertexColors[vertexIndex + 1] = c;
                        vertexColors[vertexIndex + 2] = c;
                        vertexColors[vertexIndex + 3] = c;
                    }

                    // Update Geometry
                    textMeshField.UpdateVertexData(TMP_VertexDataUpdateFlags.All);

                    //Debug.Log("word at index: " + wordIndex + " is " + wInfo.GetWord());
                    SelectedDefinitionWord = wInfo.GetWord();
                    Dictionary.Instance.WordPopUp.SetActive(true);
                }
            }
        }
    }

    /// <summary>
    /// shows this word with it's definition to the UI
    /// </summary>
    public void UpdateText()
    {
        //textField.text = Word + " = " + Definition;
        textMeshField.text = Word + " = " + Definition;
    }

    /// <summary>
    /// these two functions make the same thing, I used them both because only using one of them did not work in some cases
    /// if the toggle button is on the position for the PopUp with the Edit or Remove buttons changes acording to mouse position
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (toggleButton.isOn)
        {
            MouseOvertext = true;
            Dictionary.Instance.EditWordPanel.SetActive(false);
            Dictionary.Instance.EditOrRemovePopUp.SetActive(true);
            popUpCachedTransform.anchoredPosition = new Vector3(Input.mousePosition.x, definitionCachedTransform.position.y + definitionCachedTransform.rect.height / 2, Input.mousePosition.z);
            wordPopUpCachedTransform.anchoredPosition = Input.mousePosition;
            popUpCachedScript.EditWordInput = wordDefinitionCache;
            wordPopUpCachedScript.EditWordInput = wordDefinitionCache;
        }
        else
        {
            MouseOvertext = false;
            Dictionary.Instance.EditOrRemovePopUp.SetActive(false);
        }

        if (Dictionary.Instance.SelectedWord == SelectedDefinitionWord && Dictionary.Instance.IsInDictionary)
        {
            Dictionary.Instance.ActiveSelectedWord();
        }

        if(Dictionary.Instance.SelectedWord == SelectedDefinitionWord && Dictionary.Instance.AddToDictionary)
        {
            Dictionary.Instance.AddWordPanel.SetActive(true);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (toggleButton.isOn)
        {
            MouseOvertext = true;
            Dictionary.Instance.EditOrRemovePopUp.SetActive(true);
            popUpCachedTransform.anchoredPosition = new Vector3(Input.mousePosition.x, definitionCachedTransform.position.y + definitionCachedTransform.rect.height / 2, Input.mousePosition.z);
            wordPopUpCachedTransform.anchoredPosition = Input.mousePosition;
            popUpCachedScript.EditWordInput = wordDefinitionCache;
            wordPopUpCachedScript.EditWordInput = wordDefinitionCache;
        }
        else
        {
            MouseOvertext = false;
            Dictionary.Instance.EditOrRemovePopUp.SetActive(false);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MouseOvertext = false;
    }

}
