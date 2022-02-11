using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [SerializeField] private List<Building> buildings; //All the bases
    private GameManager gameManager;
    private SquadManager squadManager;
    float randomNum;
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
                randomNum = Random.Range(0, 10000f);
                if (randomNum * 6 < buildings[i].NumUnits)
                {
                    //Debug.Log("ATTACK " + randomNum);
                    //Debug.Log("Units " + gameManager.buildings[i].NumUnits);
                    //Debug.Log(i + " Attack Number " + attackCounter);
                    //attackCounter++;

                    int target = (int)Random.Range(0, buildings.Count);

                    while(target == 1)
                    {
                        target = (int)Random.Range(0, buildings.Count);
                    }

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
