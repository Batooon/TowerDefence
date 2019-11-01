using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static void ActivateLevel(int index)
    {
        SceneManager.LoadScene(index);
    }
}
