using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building_Shield : Building
{
    [SerializeField] private GameObject playerShield;
    [SerializeField] private GameObject enemyShield;
    private GameObject targetPlayer;
    private GameObject targetEnemy;
    [SerializeField] private Building targetPlayerScript;
    [SerializeField] private Building targetEnemyScript;
    [SerializeField] private List<Building> allShieldPlanets;

    // Start is called before the first frame update
    public override void Start()
    {
        //there should always be a player and enemy start in the scene
        targetPlayer = GameObject.Find("Player_Start");
        targetEnemy = GameObject.Find("Enemy_Start");
        ChangeShield();
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        ChangeShield();
        base.Update();
    }

    /// <summary>
    /// Changes who has a shield depending on who currently owns the planet
    /// </summary>
    void ChangeShield()
    {

        int counter = 0;

        switch (this.Alignment)
        {
            case ("P"):
                //we only make a player shield if the player currently holds its start planet
                if (targetPlayerScript.Alignment == "P")
                {
                    if (!GameObject.Find("Shield_For_Player(Clone)"))
                    {
                        Instantiate(playerShield, targetPlayer.transform.position, Quaternion.identity);
                    }
                    targetPlayerScript.Shielded = true;
                }

                for (int i = 0; i < allShieldPlanets.Count; i++)
                {
                    counter++;

                    //if there is an enemy planet still then we do nothing
                    if (allShieldPlanets[i].Alignment == "E")
                    {
                        return;
                    }
                    //if there are no enemy shield planets then we destroy the shield we left and make the enemy not shielded
                    else if (counter == allShieldPlanets.Count)
                    {
                        targetEnemyScript.Shielded = false;
                        Destroy(GameObject.Find("Shield_For_Enemy(Clone)"));
                    }
                }
                break;


            case ("E"):
                //we only make a enemy shield if the enemy currently holds its start planet
                if (targetEnemyScript.Alignment == "E")
                {
                    if (!GameObject.Find("Shield_For_Enemy(Clone)"))
                    {
                        Instantiate(enemyShield, targetEnemy.transform.position, Quaternion.identity);
                    }
                    targetEnemyScript.Shielded = true;
                }

                for (int i = 0; i < allShieldPlanets.Count; i++)
                {
                    counter++;

                    //if there is a player planet still then we do nothing
                    if (allShieldPlanets[i].Alignment == "P")
                    {
                        return;
                    }
                    //if there are no player shield planets then we destroy the shield we left and make the player not shielded
                    else if (counter == allShieldPlanets.Count)

                    {
                        targetPlayerScript.Shielded = false;
                        Destroy(GameObject.Find("Shield_For_Player(Clone)"));
                    }
                }
                break;
        }
    }
}
