using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New LootBox",menuName ="New LootBox")]
public class LootBoxObject : ScriptableObject
{
    public ItemsObject[] Items;
}
