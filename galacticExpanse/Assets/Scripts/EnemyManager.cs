using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [SerializeField] private List<Building> buildings; //All the bases
    private GameManager gameManager;
    private SquadManager squadManager;
    int randomNum;
    //int attackCounter; - Variable just for testing, basically can be ignored

    // Start is called before the first frame update
    void Start()
    {
        buildings = GetComponent<BuildingMgr>().Buildings;
        squadManager = GetComponent<SquadManager>();

    randomNum = 10000;
        //attackCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < buildings.Count; i++)
        {


            if (buildings[i].Alignment == "E" && buildings[i].NumUnits >= 15)
            {
                randomNum = Random.Range(0, 10000);
                if (randomNum * 10 < buildings[i].NumUnits) // The *20 is the nerf the enemy. The higher the number the less active it will be.
                {

                    int target = (int)Random.Range(0, buildings.Count); //This is for the target of the attack
                    //Debug.Log(target);

                    while (target == i) //This ensures that it will not target itself
                    {
                        target = (int)Random.Range(0, buildings.Count);
                        //Debug.Log(target);
                    }

                    //This makes sure that the unit numbers will not be weird if it has an odd number
                    if (buildings[i].NumUnits % 2 == 0)
                    {
                        buildings[i].NumUnits = buildings[i].NumUnits / 2;
                        squadManager.CreateSquad(1, 0, buildings[i].NumUnits, buildings[i].transform.position, buildings[target]);
                    }
                    else
                    {
                        buildings[i].NumUnits = buildings[i].NumUnits / 2;
                        squadManager.CreateSquad(1, 0, buildings[i].NumUnits, buildings[i].transform.position, buildings[target]);
                        buildings[i].NumUnits++;
                    }

                }
            }
        }
    }
}
