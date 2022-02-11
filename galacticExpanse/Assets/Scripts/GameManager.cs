using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Building> buildings; //All the bases
    public GameObject pStart; //Player Start
    public GameObject eStart; //Enemy Start
    private float timer; //Used for incrementing unit counts

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        buildings = GetComponent<BuildingMgr>().Buildings;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= 4)
        {
            UpdateBases();
            timer = 0;
        }
    }

    void UpdateBases()
    {
        for(int i = 0; i < buildings.Count; i++)
        {
            if(buildings[i].Alignment != "N" && buildings[i].NumUnits < 50)
            {
                buildings[i].NumUnits += 1;
            }
        }
    }
}
