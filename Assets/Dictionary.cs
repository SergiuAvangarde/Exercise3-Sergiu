using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;

public class Dictionary : MonoBehaviour
{
    public static Dictionary Instance = null;
    public SortedDictionary<string, string> words = new SortedDictionary<string, string>();
    public List<GameObject> WordsPool { get; set; } = new List<GameObject>();

    [SerializeField]
    private GameObject WordPrefab;
    [SerializeField]
    private GameObject WordsListContent;
    [SerializeField]
    private GameObject editPanel;

    private RectTransform transformCache;
    private ToggleGroup toggleGroupCache;

    private WordClassList wordsClassListObj = new WordClassList();
    private string JsonPath;
    private int wordPoolLength = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        transformCache = WordsListContent.GetComponent<RectTransform>();
        toggleGroupCache = WordsListContent.GetComponent<ToggleGroup>();
    }

    private void Start()
    {
        JsonPath = Application.dataPath + "/Dictionary.json";
        Debug.Log(JsonPath);

        if (File.Exists(JsonPath))
        {
            LoadData();
        }

        if (wordPoolLength < words.Count)
        {
            wordPoolLength = words.Count;
        }

        for (int i = 0; i < wordPoolLength; i++)
        {
            InstantiateWordObj();
        }

        RefreshWords();

        //foreach (var word in words.Reverse())
        //{
        //    print("Z-A: word: " + word.Key + " is: " + word.Value);
        //}
    }

    public void InstantiateWordObj()
    {
        var WordObj = Instantiate(WordPrefab, transformCache);
        WordObj.SetActive(false);
        WordObj.GetComponent<Toggle>().group = toggleGroupCache;
        WordsPool.Add(WordObj);
    }

    /// <summary>
    /// searches the dictionary and adds the words with the definitions to the UI list
    /// </summary>
    public void RefreshWords()
    {
        //if(sortAZ){
        SortedDictionary<string, string> wordsDictionary = new SortedDictionary<string, string>();
        wordsDictionary = words;
        //}
        //else if(sortZA)
        //{
        //var wordsDictionary = words.Reverse();
        //}
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
                    WordObjScript.EditPanel = editPanel;
                    WordObjScript.UpdateText();

                    var wordClassObj = new WordClass();
                    wordClassObj.Word = word.Key;
                    wordClassObj.Definition = word.Value;

                    wordsClassListObj.WordsClass.Add(wordClassObj);
                    break;
                }
            }
        }
        SaveData();
    }

    /// <summary>
    /// Save the dictionary information to a json file
    /// </summary>
    private void SaveData()
    {
        string contents = JsonUtility.ToJson(wordsClassListObj, true);
        File.WriteAllText(JsonPath, contents);
    }

    /// <summary>
    /// load the dictionary information from the json file
    /// </summary>
    private void LoadData()
    {
        string contents = File.ReadAllText(JsonPath);
        WordClassList TempWordsList = new WordClassList();
        TempWordsList = JsonUtility.FromJson<WordClassList>(contents);

        foreach (var wordObj in TempWordsList.WordsClass)
        {
            if (!words.ContainsKey(wordObj.Word))
            {
                words.Add(wordObj.Word, wordObj.Definition);
            }
        }
    }
}
