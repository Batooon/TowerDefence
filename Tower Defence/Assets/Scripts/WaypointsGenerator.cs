using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointsGenerator : LevelGenerator
{
    private List<WaypointBase> waypoints = new List<WaypointBase>();
    public override void GenerateTile(int x, int y)
    {
        Color pixelColor = map.GetPixel(x, y);

        if (pixelColor.a == 0)
            return;

        foreach (ColorToPrefab colorMapping in colorMappings)
        {
            if (colorMapping.color.Equals(pixelColor))
            {
                Vector3 position = new Vector3(x - 4, colorMapping.offsetPositionY, y - 4);

                Instantiate(colorMapping.prefab, position, Quaternion.identity, transform);
                waypoints.Add(colorMapping.prefab.GetComponent<WaypointBase>());
            }
        }
    }
}
