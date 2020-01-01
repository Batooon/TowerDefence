using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Developer: Antoshka

public class EntryPoint : MonoBehaviour
{
    private void Awake()
    {
        gameObject.AddComponent<Game>();
    }
}
