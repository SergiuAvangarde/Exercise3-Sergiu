using System.Collections.Generic;
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
