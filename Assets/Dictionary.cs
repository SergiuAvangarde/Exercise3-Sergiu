using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;

public class Dictionary : MonoBehaviour
{
    public SortedDictionary<string, string> words = new SortedDictionary<string, string>();
    public List<GameObject> WordsPool { get; set; } = new List<GameObject>();

    [SerializeField]
    private GameObject WordPrefab;
    [SerializeField]
    private RectTransform WordsList;

    private WordClassList wordsClassListObj = new WordClassList();
    private string JsonPath;
    private int wordPoolLength = 0;

    private void Start()
    {
        wordsClassListObj.WordsClass = new List<WordClass>();
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
            var WordObj = Instantiate(WordPrefab, WordsList);
            WordObj.SetActive(false);
            WordsPool.Add(WordObj);
        }

        RefreshWords();

        //foreach (var word in words.Reverse())
        //{
        //    print("Z-A: word: " + word.Key + " is: " + word.Value);
        //}
        SaveData();
    }

    public void RefreshWords()
    {
        foreach (var wordObj in WordsPool)
        {
            wordObj.SetActive(false);
        }

        foreach (var word in words)
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

                    var wordClassObj = new WordClass();
                    wordClassObj.Word = word.Key;
                    wordClassObj.Definition = word.Value;

                    wordsClassListObj.WordsClass.Add(wordClassObj);
                    break;
                }
            }
        }
    }

    private void SaveData()
    {
        string contents = JsonUtility.ToJson(wordsClassListObj, true);
        File.WriteAllText(JsonPath, contents);
    }

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
