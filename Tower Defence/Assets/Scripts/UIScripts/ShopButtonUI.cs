using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ShopButtonUI : MonoBehaviour
{
    [SerializeField]
    private Image image;

    private Sprite selectedSprite;
    private Sprite deselectedSprite;
    public GameObject turretPrefab { get; private set; }

    public void Init(TurretObject t)
    {
        turretPrefab = t.TurretPrefab;
        selectedSprite = t.selectedTurretUI;
        deselectedSprite = t.deselectedTurretUI;

        Shop shop = transform.parent.GetComponent<Shop>();

        Button turretButton = GetComponent<Button>();
        turretButton.onClick.AddListener(() => shop.TurretPressed(turretPrefab));
    }

    public void Select()
    {
        image.sprite = selectedSprite;
    }

    public void Deselect()
    {
        image.sprite = deselectedSprite;
    }
}
