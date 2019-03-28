# Exercise3-Sergiu, Dictionary

This app is an english dictionary with a list of words and definitions for each of the word.
The list is saved into a json file at runtime, and at startup the program loads every word and definition from the json file.
The user can search for a word in the dinctionary, if it is not found you can add any new word to the dictionary, you can edit the existing ones, or remove any word from the dictionary.

## Script Logic

To save the Dictionary to the json file, I created a Class with two strings word, wich is the key of the dictionary and definition, wich is the value, and for every word in the dictionary I created a list of objects from that class. I serialized every object and then I serialized the list and added it to the json file. For loading the data i created a new temporary list of objects wich then i compare with the existing list, and add them together, this is done because at start-up the program may have some words in the dictionary and I want to keep them if they are not found in the list from the json file.

At startup I instantiate some gameobjects wich contain all of the information for every word, into an object pool.
I used SortedDictionary because it automatically sorts every new entry alphabetically, and everytime I add a new word I refresh every word from the pool with the new values. this seamed simpler to me than searching for the position on the list where the word should be put. Also for sorting the words from Z to A I used the Reverse() funtion on the sorted dictionary.

To add a new word on the dictionary you can press the "Add Word" button on the top right of the menu, a new panel will open with two input fields for the new word and it's definition. When you are done editing you can press the "Add This Word" button to confirm. The program checks if you are trying to add a null string for the word or the description and prints a message, also if you are trying to add a word that is already in the dictionary would redirect you to that word to edit or remove it.
To edit or remove a word, you can click it on the list and a popUp menu will appear on the click position with the edit or remove options.
If you click on the edit button a panel similar with the one for adding a new word will open, but you can only change it's definition there.
