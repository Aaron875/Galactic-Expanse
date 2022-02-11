using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    private GameManager gameManager;
    float randomNum;
    int attackCounter;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GetComponent<GameManager>();
        randomNum = 10000;
        attackCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < gameManager.buildings.Count; i++)
        {


            if (gameManager.buildings[i].Alignment == "E" && gameManager.buildings[i].NumUnits >= 15)
            {
                randomNum = Random.Range(0, 10000f);
                if (randomNum * 6 < gameManager.buildings[i].NumUnits)
                {
                    //Debug.Log("ATTACK " + randomNum);
                    //Debug.Log("Units " + gameManager.buildings[i].NumUnits);
                    Debug.Log("Attack Number " + attackCounter);
                    attackCounter++;
                }
            }
        }
    }
}
