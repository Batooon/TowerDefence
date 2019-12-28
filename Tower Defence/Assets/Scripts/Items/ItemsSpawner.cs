using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Developer: Antoshka

public class ItemsSpawner : MonoBehaviour
{
    public GameObject loot;

    public void InitDrop(EnemyDieEvent enemyDie)
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(enemyDie.position);
        Drop(pos);
    }

    public void Drop(Vector3 position)
    {
        GameObject instantiatedLoot = Instantiate(loot, position, Quaternion.identity);
        instantiatedLoot.transform.SetParent(transform);
        instantiatedLoot.transform.localScale = Vector3.one;
    }
}
