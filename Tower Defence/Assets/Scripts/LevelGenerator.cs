using System;
using UnityEngine;

[ExecuteInEditMode]
public class LevelGenerator : MonoBehaviour
{
    public Texture2D map;

    public ColorToPrefab[] colorMappings;

    void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        for(int x = 0; x < map.width; x++)
        {
            for(int y = 0; y < map.height; y++)
            {
                GenerateTile(x, y);
            }
        }
    }

    public virtual void GenerateTile(int x, int y)
    {
        Color pixelColor = map.GetPixel(x, y);

        if (pixelColor.a == 0)
            return;

        foreach(ColorToPrefab colorMapping in colorMappings)
        {
            if (colorMapping.color.Equals(pixelColor))
            {
                Vector3 position = new Vector3(x - 4, colorMapping.offsetPositionY, y - 4);

                Instantiate(colorMapping.prefab, position, Quaternion.identity, transform);
            }
        }
    }
}

[System.Serializable]
public class ColorToPrefab
{
    public Color color;
    public GameObject prefab;
    public float offsetPositionY;
}
