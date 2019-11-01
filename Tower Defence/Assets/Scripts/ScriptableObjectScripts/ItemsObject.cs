using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="New Item",menuName ="New item")]
public class ItemsObject : ScriptableObject
{
    public Sprite sprite;

    public int number;
}
