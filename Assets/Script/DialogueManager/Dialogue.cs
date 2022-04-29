using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    /// <summary>
    /// Permet de recuperer le nom du PNJ
    /// De cree du texte sur plusieurs lignes
    /// De stocker chaque dialogue dans un array
    /// </summary>
    public string name;

    [TextArea(3,10)]
    public string[] sentences;
}
