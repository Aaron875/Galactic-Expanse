using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [SerializeField] private List<Building> buildings; //All the bases
    private GameManager gameManager;
    private SquadManager squadManager;

    private int randomNum; //Used for holding randoms
    private float timer; //Used for incrementing unit counts
    //int attackCounter; - Variable just for testing, basically can be ignored

    // Start is called before the first frame update
    void Start()
    {
        buildings = GetComponent<BuildingMgr>().Buildings;
        squadManager = GetComponent<SquadManager>();
        timer = 0;

        randomNum = 10000; //This is just a holder value, makes sure nothing runs frame 1
        //attackCounter = 0;
    }

    public void UpdateAI()
    {
        for (int i = 0; i < buildings.Count; i++) //Loops through all buildings
        {

            if (buildings[i].Alignment == "E" && buildings[i].NumUnits >= 15) //If a building is an enemy building and has 15 or more units
            {

                timer += Time.deltaTime;

                //Change number to change how quickly buildings gain units
                if (timer >= 2)
                {
                    randomNum = Random.Range(0, 600); //600 is just randomly there, can be changed
                    if (randomNum < buildings[i].NumUnits || buildings[i].NumUnits >= 50) //If the number is less than the unit count it attacks, more aggressive the more units it has
                    {

                        int target;
                        target = (int)Random.Range(0, buildings.Count); //This is for the target of the attack

                        //This is code for if it will target a weak planet. The %3 != 0 is basically to nerf it so it only has a 2/3 chance of going after a weak planet
                        if(randomNum % 3 != 0)
                        {
                            for (int j = 0; j < buildings.Count; j++)
                            {
                                if (buildings[j].Alignment != "E" && buildings[j].NumUnits <= buildings[i].NumUnits / 2) //If it can claim a new planet it does and then it stops the for loop
                                {
                                    target = j;
                                    j += 9000;
                                }
                            }
                        }

                        //Debug.Log(target);

                        while (target == i || (buildings[target].Alignment == "E" && buildings[target].NumUnits + (buildings[i].NumUnits / 2) >= 45)) //This ensures that it will not target itself or allies that have too high of a unit count
                        {
                            target = (int)Random.Range(0, buildings.Count);
                            //Debug.Log(target);
                        }

                        //This makes sure that the unit numbers will not be weird if it has an odd number
                        if (buildings[i].NumUnits % 2 == 0)
                        {
                            buildings[i].NumUnits = buildings[i].NumUnits / 2;
                            squadManager.CreateSquad(buildings[i].NumUnits, buildings[i], buildings[target]);
                        }
                        else
                        {
                            buildings[i].NumUnits = buildings[i].NumUnits / 2;
                            squadManager.CreateSquad(buildings[i].NumUnits, buildings[i], buildings[target]);
                            buildings[i].NumUnits++;
                        }

                    }
                    timer = 0;
                }
            }

    // Update is called once per frame
    void Update()
    {
                /*randomNum = Random.Range(0, 10000);
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

                }*/
            }
        }
    }
}
