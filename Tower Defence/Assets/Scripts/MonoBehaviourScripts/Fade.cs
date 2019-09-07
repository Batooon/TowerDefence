using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Fade : MonoBehaviour
{
    private float Lifetime;
    public float TimeToLive;
    public TextMeshProUGUI text;
    void Start()
    {
        if (text == null)
            Debug.Log("Text is equal to null!!!");
        Lifetime = 0;
    }

    void Update()
    {
        if (Lifetime >= TimeToLive)
            Destroy(gameObject);
        Lifetime += Time.deltaTime;
        FadeText();
    }
        
    void FadeText()
    {
        Color c = text.color;
        c.a = Mathf.Lerp(1, 0, Lifetime / TimeToLive);
        text.color = c;
    }
}
