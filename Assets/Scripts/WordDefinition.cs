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

    [HideInInspector]
    public bool MouseOvertext = false;

    [SerializeField]
    private TextMeshProUGUI wordField;
    [SerializeField]
    private TextMeshProUGUI definitionField;

    private Toggle toggleButton;
    private Image Image;
    private RectTransform popUpCachedTransform;
    private RectTransform wordPopUpCachedTransform;
    private RectTransform definitionCachedTransform;
    private EditWords popUpCachedScript;
    private SelectWord wordPopUpCachedScript;
    private WordDefinition wordDefinitionCache;
    private int selectedWordIndex = -1;

    /// <summary>
    /// initialize the required components
    /// </summary>
    private void Awake()
    {
        Image = GetComponent<Image>();
        toggleButton = GetComponent<Toggle>();
        definitionCachedTransform = gameObject.GetComponent<RectTransform>();
        popUpCachedTransform = Dictionary.Instance.EditOrRemovePopUp.GetComponent<RectTransform>();
        wordPopUpCachedTransform = Dictionary.Instance.WordPopUp.GetComponent<RectTransform>();
        popUpCachedScript = Dictionary.Instance.EditOrRemovePopUp.GetComponent<EditWords>();
        wordPopUpCachedScript = Dictionary.Instance.WordPopUp.GetComponent<SelectWord>();
        wordDefinitionCache = GetComponent<WordDefinition>();
    }

    /// <summary>
    /// check if the mouse pointer is over a word from an active toggle
    /// if yes, the word is highlighted, saved into a variable and a pop-up activates below
    /// </summary>
    private void FixedUpdate()
    {
        int wordIndex = TMP_TextUtilities.FindIntersectingWord(definitionField, Input.mousePosition, null);

        // Clear previous word selection.
        if (selectedWordIndex != -1 && (wordIndex == -1 || wordIndex != selectedWordIndex))
        {
            TMP_WordInfo wInfo = definitionField.textInfo.wordInfo[selectedWordIndex];

            // Iterate through each of the characters of the word.
            for (int i = 0; i < wInfo.characterCount; i++)
            {
                int characterIndex = wInfo.firstCharacterIndex + i;

                // Get the index of the material / sub text object used by this character.
                int meshIndex = definitionField.textInfo.characterInfo[characterIndex].materialReferenceIndex;

                // Get the index of the first vertex of this character.
                int vertexIndex = definitionField.textInfo.characterInfo[characterIndex].vertexIndex;

                // Get a reference to the vertex color
                Color32[] vertexColors = definitionField.textInfo.meshInfo[meshIndex].colors32;

                Color32 c = vertexColors[vertexIndex + 0].Tint(1.33333f);

                vertexColors[vertexIndex + 0] = c;
                vertexColors[vertexIndex + 1] = c;
                vertexColors[vertexIndex + 2] = c;
                vertexColors[vertexIndex + 3] = c;
            }

            // Update Geometry
            definitionField.UpdateVertexData(TMP_VertexDataUpdateFlags.All);

            selectedWordIndex = -1;
            SelectedDefinitionWord = "";
            Dictionary.Instance.WordPopUp.SetActive(false);
        }

        // if the mouse hovers over a word
        if (MouseOvertext)
        {
            // if the toggle of the word is active
            if (toggleButton.isOn)
            {
                Dictionary.Instance.EditOrRemovePopUp.SetActive(true);
                popUpCachedTransform.anchoredPosition = new Vector3(Input.mousePosition.x, definitionCachedTransform.position.y + definitionCachedTransform.rect.height / 2, Input.mousePosition.z);
                wordPopUpCachedTransform.anchoredPosition = Input.mousePosition;
                popUpCachedScript.EditWordInput = wordDefinitionCache;
                wordPopUpCachedScript.EditWordInput = wordDefinitionCache;
            }


            // Word Selection Handling
            if (wordIndex != -1 && wordIndex != selectedWordIndex && !(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
            {
                selectedWordIndex = wordIndex;

                TMP_WordInfo wInfo = definitionField.textInfo.wordInfo[wordIndex];

                // Iterate through each of the characters of the word.
                for (int i = 0; i < wInfo.characterCount; i++)
                {
                    int characterIndex = wInfo.firstCharacterIndex + i;

                    // Get the index of the material / sub text object used by this character.
                    int meshIndex = definitionField.textInfo.characterInfo[characterIndex].materialReferenceIndex;

                    int vertexIndex = definitionField.textInfo.characterInfo[characterIndex].vertexIndex;

                    // Get a reference to the vertex color
                    Color32[] vertexColors = definitionField.textInfo.meshInfo[meshIndex].colors32;

                    Color32 c = vertexColors[vertexIndex + 0].Tint(0.75f);

                    vertexColors[vertexIndex + 0] = c;
                    vertexColors[vertexIndex + 1] = c;
                    vertexColors[vertexIndex + 2] = c;
                    vertexColors[vertexIndex + 3] = c;
                }

                // Update Geometry
                definitionField.UpdateVertexData(TMP_VertexDataUpdateFlags.All);

                SelectedDefinitionWord = wInfo.GetWord();
                Dictionary.Instance.WordPopUp.SetActive(true);
            }
        }
    }

    /// <summary>
    /// shows this word with it's definition to the UI
    /// </summary>
    public void UpdateText()
    {
        //textMeshField.text = Word + " = " + Definition;
        wordField.text = Word;
        definitionField.text = "= " + Definition;
    }

    /// <summary>
    /// these two functions make the same thing, I used them both because only using one of them did not work in some cases
    /// if the toggle button is on the position for the PopUp with the Edit or Remove buttons changes acording to mouse position
    /// if you click a highlighted word it searches for it in the dictionary and if it's now found you can add it
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (toggleButton.isOn)
        {
            Image.enabled = true;
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
            Image.enabled = false;
            MouseOvertext = false;
            Dictionary.Instance.EditOrRemovePopUp.SetActive(false);
        }

        if (Dictionary.Instance.SelectedWord == SelectedDefinitionWord && Dictionary.Instance.IsInDictionary)
        {
            Dictionary.Instance.ActiveSelectedWord();
        }

        if (Dictionary.Instance.SelectedWord == SelectedDefinitionWord && Dictionary.Instance.AddToDictionary)
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
