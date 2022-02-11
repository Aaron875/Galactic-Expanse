using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [SerializeField] private List<Building> buildings; //All the bases
    private GameManager gameManager;
    float randomNum;
    int attackCounter;

    // Start is called before the first frame update
    void Start()
    {
        buildings = GetComponent<BuildingMgr>().Buildings;
        randomNum = 10000;
        attackCounter = 0;
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
                    Debug.Log(i + " Attack Number " + attackCounter);
                    attackCounter++;
                }
            }
        }
    }
}
