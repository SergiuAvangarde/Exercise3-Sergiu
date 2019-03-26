using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class WordClass
{
    public string Word;
    public string Definition;
}

[Serializable]
public class WordClassList
{
    public List<WordClass> WordsClass;
}
