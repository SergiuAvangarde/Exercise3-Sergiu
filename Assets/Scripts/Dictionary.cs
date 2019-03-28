using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;

public class Dictionary : MonoBehaviour
{
    public static Dictionary Instance = null;
    public SortedDictionary<string, string> Words = new SortedDictionary<string, string>();
    public List<GameObject> WordsPool { get; set; } = new List<GameObject>();
    public GameObject EditOrRemovePopUp;
    public GameObject EditWordPanel;

    [SerializeField]
    private GameObject wordPrefab;
    [SerializeField]
    private GameObject wordsListContent;
    [SerializeField]
    private Button sortAZ;
    [SerializeField]
    private Button sortZA;
    [SerializeField]
    private ScrollRect scroll;
    [SerializeField]
    private AddWords addedWord;

    private RectTransform transformCache;
    private ToggleGroup toggleGroupCache;
    private WordClassList wordsClassListObj = new WordClassList();
    private string jsonPath;
    private float addedWordPosition;
    private int wordPoolLength = 0;

    /// <summary>
    /// Before initialization create a singleton of this script and cache the required components
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        transformCache = wordsListContent.GetComponent<RectTransform>();
        toggleGroupCache = wordsListContent.GetComponent<ToggleGroup>();
    }

    /// <summary>
    /// On initialization the program searches for a json file with the dictionary information and prints every word to UI
    /// </summary>
    private void Start()
    {
        jsonPath = Application.dataPath + "/Dictionary.json";
        Debug.Log(jsonPath);

        if (File.Exists(jsonPath))
        {
            LoadData();
        }

        if (wordPoolLength < Words.Count)
        {
            wordPoolLength = Words.Count;
        }

        for (int i = 0; i < wordPoolLength; i++)
        {
            InstantiateWordObj();
        }

        RefreshWords();
    }

    /// <summary>
    /// Instantiate a new object in the object pool for words
    /// </summary>
    public void InstantiateWordObj()
    {
        var WordObj = Instantiate(wordPrefab, transformCache);
        WordObj.SetActive(false);
        WordObj.GetComponent<Toggle>().group = toggleGroupCache;
        WordsPool.Add(WordObj);
    }

    /// <summary>
    /// Search the dictionary and add the words with the definitions to the UI list
    /// </summary>
    public void RefreshWords()
    {
        IEnumerable<KeyValuePair<string, string>> wordsDictionary = Words;
        if (sortAZ.IsActive())
        {
            wordsDictionary = Words;
        }
        else if (sortZA.IsActive())
        {
            wordsDictionary = Words.Reverse();
        }
        wordsClassListObj.WordsClass = new List<WordClass>();

        foreach (var wordObj in WordsPool)
        {
            wordObj.SetActive(false);
        }

        foreach (var word in wordsDictionary)
        {
            foreach (var wordObj in WordsPool)
            {
                if (!wordObj.activeInHierarchy)
                {
                    wordObj.SetActive(true);
                    var WordObjScript = wordObj.GetComponent<WordDefinition>();
                    WordObjScript.Word = word.Key;
                    WordObjScript.Definition = word.Value;
                    WordObjScript.UpdateText();
                    if (addedWord.AddedWord == word.Key)
                    {
                        wordObj.GetComponent<Toggle>().isOn = true;
                    }

                    var wordClassObj = new WordClass();
                    wordClassObj.Word = word.Key;
                    wordClassObj.Definition = word.Value;
                    wordsClassListObj.WordsClass.Add(wordClassObj);
                    break;
                }
            }
        }
        StartCoroutine(WaitToUpdate());
        SaveData();
    }

    /// <summary>
    /// Save the dictionary information to a json file
    /// </summary>
    private void SaveData()
    {
        string contents = JsonUtility.ToJson(wordsClassListObj, true);
        File.WriteAllText(jsonPath, contents);
    }

    /// <summary>
    /// Load the dictionary information from the json file
    /// </summary>
    private void LoadData()
    {
        string contents = File.ReadAllText(jsonPath);
        WordClassList TempWordsList = new WordClassList();
        TempWordsList = JsonUtility.FromJson<WordClassList>(contents);

        foreach (var wordObj in TempWordsList.WordsClass)
        {
            if (!Words.ContainsKey(wordObj.Word))
            {
                Words.Add(wordObj.Word, wordObj.Definition);
            }
        }
    }

    /// <summary>
    /// Move the scroll position to the selected word position
    /// </summary>
    /// <returns>wait for the list with words to be updated</returns>
    public IEnumerator WaitToUpdate()
    {
        yield return new WaitForSeconds(0.1f);
        addedWordPosition = 0;

        foreach (var toggle in toggleGroupCache.ActiveToggles())
        {
            addedWordPosition = Mathf.Abs(toggle.transform.localPosition.y);
        }

        float normalizedPosition = addedWordPosition / transformCache.rect.height;
        scroll.verticalNormalizedPosition = 1 - normalizedPosition;
    }
}
