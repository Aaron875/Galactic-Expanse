using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMgr : MonoBehaviour
{

    [SerializeField] private Sprite playerSprite;
    [SerializeField] private Sprite enemySprite;
    [SerializeField] private List<Building> buildings;

    public List<Building> Buildings
    {
        get { return buildings; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
            }
            else if (buildings[i].SpriteRenderer.sprite != enemySprite &&
                    buildings[i].Alignment == "E")
            {
                buildings[i].SpriteRenderer.sprite = enemySprite;
            }
        }
    }
}
