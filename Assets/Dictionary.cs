using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Dictionary : MonoBehaviour
{
    public SortedDictionary<string, string> words = new SortedDictionary<string, string>()
    {
        {"f", "definition for f"},
        {"a", "definition for a"},
        {"g", "definition for g"},
        {"h", "definition for h"},
        {"d", "definition for d"},
        {"o", "definition for o"},
        {"w", "definition for w"},
        {"b", "definition for b"}
    };

    private void Start()
    {
        foreach(var word in words)
        {
            print("word: " + word.Key + " is: " + word.Value);
        }
    }

    private void Update()
    {
        
    }
}
