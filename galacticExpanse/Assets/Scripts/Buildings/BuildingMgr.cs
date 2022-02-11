using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMgr : MonoBehaviour
{

    [SerializeField] private Sprite playerSprite;
    [SerializeField] private Sprite enemySprite;
    [SerializeField] private List<Building> buildings;
    [SerializeField] private List<GameObject> buildingsForLayer;

    public List<Building> Buildings
    {
        get { return buildings; }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < buildings.Count; i++)
        {
            if (buildings[i].SpriteRenderer.sprite != playerSprite && 
                buildings[i].Alignment == "P")
            {
                buildings[i].SpriteRenderer.sprite = playerSprite;

                buildingsForLayer[i].layer = 6;
                buildings[i].SpriteRenderer.sortingOrder = 1;
            }
            else if (buildings[i].SpriteRenderer.sprite != enemySprite &&
                    buildings[i].Alignment == "E")
            {
                buildings[i].SpriteRenderer.sprite = enemySprite;
                if (buildingsForLayer[i].layer == 6)
                {
                    buildingsForLayer[i].layer = 7;
                    buildings[i].SpriteRenderer.sortingOrder = 0;
                }
            }
        }
    }
}
