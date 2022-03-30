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
                    randomNum = Random.Range(0, 550); //600 is just randomly there, can be changed
                    if (randomNum < buildings[i].NumUnits || buildings[i].NumUnits >= 50) //If the number is less than the unit count it attacks, more aggressive the more units it has
                    {

                        int currentTarget = 0;
                        float targetScore = -1000;
                        int potentialTarget;
                        float potentialScore;
                        float distance;

                        for (int j = 0; j < buildings.Count; j++)
                        {
                            potentialTarget = j;

                            //If target is self then it will never attack itself
                            if (j == i)
                            {
                                potentialScore = -1000;
                            }
                            else
                            {
                                distance = Vector3.Distance(buildings[i].transform.position, buildings[j].transform.position);
                                potentialScore = 50 - (buildings[j].NumUnits);

                                //Takes planet regening troops into account
                                if(buildings[j].Alignment != "N" && potentialScore != 0)
                                {

                                    //If interceptor time it takes to get somewhere is halved so their score is as well
                                    if (buildings[i].Type == "Interceptor")
                                    {
                                        potentialScore -= 2 * (distance / 60);
                                    }

                                    potentialScore -= 2 * (distance / 30);
                                }

                                //Can kill so drastically increases score
                                if (potentialScore >= buildings[i].NumUnits / 2 && buildings[j].Alignment != "E")
                                {
                                    potentialScore += 500;
                                }

                                //Farther planets have lower score
                                //Distance is bigger than I anticipated, divided by 10 to reduce that
                                potentialScore -= (distance / 10);

                                //Higher target score if neutral, lower if enemy
                                if (buildings[j].Alignment == "E")
                                {
                                    potentialScore -= 10;

                                    //If enemy and max units, it should never send units there
                                    if(buildings[j].NumUnits == 50)
                                    {
                                        potentialScore -= 400;
                                    }
                                }else if (buildings[j].Alignment == "N")
                                {
                                    potentialScore += 10;
                                }

                                //Higher if special building
                                if(buildings[j].Type != "Normal")
                                {
                                    potentialScore += 30;
                                }

                                //Just some randomness so the AI isn't 100% predictable
                                potentialScore += Random.Range(0, 75);

                            }


                            if (potentialScore > targetScore)
                            {
                                currentTarget = potentialTarget;
                                targetScore = potentialScore;
                            }

                        }


                        Attack(i, currentTarget);

                    }
                    timer = 0;
                }
            }
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

    void Attack(int attacker, int target)
    {
        if (buildings[attacker].NumUnits % 2 == 0)
        {
            buildings[attacker].NumUnits = buildings[attacker].NumUnits / 2;
            squadManager.CreateSquad(buildings[attacker].NumUnits, buildings[attacker], buildings[target]);
        }
        else
        {
            buildings[attacker].NumUnits = buildings[attacker].NumUnits / 2;
            squadManager.CreateSquad(buildings[attacker].NumUnits, buildings[attacker], buildings[target]);
            buildings[attacker].NumUnits++;
        }
    }

}
