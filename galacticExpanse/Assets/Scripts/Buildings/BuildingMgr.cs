using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMgr : MonoBehaviour
{
    [SerializeField] private Sprite normalPlayerSprite;
    [SerializeField] private Sprite normalEnemySprite;
    [SerializeField] private Sprite interceptorPlayerSprite;
    [SerializeField] private Sprite interceptorEnemySprite;
    [SerializeField] private Sprite shieldPlayerSprite;
    [SerializeField] private Sprite shieldEnemySprite;
    [SerializeField] private List<Building> buildings;
    [SerializeField] private List<GameObject> buildingsForLayer;

    public List<Building> Buildings
    {
        get { return buildings; }
    }
    // Update is called once per frame
    void Update()
    {
        //when the alignment of a building changes the buildings sprite will change to who ever now controls it
        for (int i = 0; i < buildings.Count; i++)
        {
            if (buildings[i].SpriteRenderer.sprite != normalPlayerSprite && 
                buildings[i].Alignment == "P" && buildings[i].Type == "Normal")
            {
                buildings[i].SpriteRenderer.sprite = normalPlayerSprite;

                buildingsForLayer[i].layer = 6;
                buildings[i].SpriteRenderer.sortingOrder = 1;
            }
            else if (buildings[i].SpriteRenderer.sprite != normalEnemySprite &&
                    buildings[i].Alignment == "E" && buildings[i].Type == "Normal")
            {
                buildings[i].SpriteRenderer.sprite = normalEnemySprite;
                if (buildingsForLayer[i].layer == 6)
                {
                    buildingsForLayer[i].layer = 7;
                    buildings[i].SpriteRenderer.sortingOrder = 0;
                }
            }
            // Interceptor Planets
            else if (buildings[i].SpriteRenderer.sprite != interceptorPlayerSprite &&
                    buildings[i].Alignment == "P" && buildings[i].Type == "Interceptor")
            {
                buildings[i].SpriteRenderer.sprite = interceptorPlayerSprite;

                buildingsForLayer[i].layer = 6;
                buildings[i].SpriteRenderer.sortingOrder = 1;
            }
            else if (buildings[i].SpriteRenderer.sprite != interceptorEnemySprite &&
                    buildings[i].Alignment == "E" && buildings[i].Type == "Interceptor")
            {
                buildings[i].SpriteRenderer.sprite = interceptorEnemySprite;
                if (buildingsForLayer[i].layer == 6)
                {
                    buildingsForLayer[i].layer = 7;
                    buildings[i].SpriteRenderer.sortingOrder = 0;
                }
            }
            // Turret Planets
            else if (buildings[i].SpriteRenderer.sprite != normalPlayerSprite &&
                buildings[i].Alignment == "P" && buildings[i].Type == "Turret")
            {
                buildings[i].SpriteRenderer.sprite = normalPlayerSprite; // Change if we get a special planet sprite

                buildingsForLayer[i].layer = 6;
                buildings[i].SpriteRenderer.sortingOrder = 1;
            }
            else if (buildings[i].SpriteRenderer.sprite != normalEnemySprite &&
                    buildings[i].Alignment == "E" && buildings[i].Type == "Turret")
            {
                buildings[i].SpriteRenderer.sprite = normalEnemySprite;
                if (buildingsForLayer[i].layer == 6)
                {
                    buildingsForLayer[i].layer = 7;
                    buildings[i].SpriteRenderer.sortingOrder = 0;
                }
            }
            else if (buildings[i].SpriteRenderer.sprite != shieldPlayerSprite &&
                    buildings[i].Alignment == "P" && buildings[i].Type == "Shield")
            {
                buildings[i].SpriteRenderer.sprite = shieldPlayerSprite;

                buildingsForLayer[i].layer = 6;
                buildings[i].SpriteRenderer.sortingOrder = 1;
            }
            else if (buildings[i].SpriteRenderer.sprite != shieldEnemySprite &&
                    buildings[i].Alignment == "E" && buildings[i].Type == "Shield")
            {
                buildings[i].SpriteRenderer.sprite = shieldEnemySprite;
                if (buildingsForLayer[i].layer == 6)
                {
                    buildingsForLayer[i].layer = 7;
                    buildings[i].SpriteRenderer.sortingOrder = 0;
                }
            }
        }
    }
}
