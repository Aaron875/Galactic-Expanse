using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building_Shield : Building
{
    [SerializeField] private GameObject playerShield;
    [SerializeField] private GameObject enemyShield;
    [SerializeField] private GameObject targetPlayer;
    [SerializeField] private GameObject targetEnemy;
    [SerializeField] private Building targetPlayerScript;
    [SerializeField] private Building targetEnemyScript;

    // Start is called before the first frame update
    public override void Start()
    {
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
        switch (this.Alignment, targetPlayerScript.Shielded, targetEnemyScript.Shielded)
        {
            case ("P",false,true):
                Instantiate(playerShield, targetPlayer.transform.position, Quaternion.identity);
                targetPlayerScript.Shielded = true;
                targetEnemyScript.Shielded = false;
                if (GameObject.Find("Shield_For_Enemy(Clone)"))
                {
                    Destroy(GameObject.Find("Shield_For_Enemy(Clone)"));
                }
                break;
            case ("P", false, false):
                Instantiate(playerShield, targetPlayer.transform.position, Quaternion.identity);
                targetPlayerScript.Shielded = true;
                targetEnemyScript.Shielded = false;
                if (GameObject.Find("Shield_For_Enemy(Clone)"))
                {
                    Destroy(GameObject.Find("Shield_For_Enemy(Clone)"));
                }
                break;
            case ("E", true, false):
                Instantiate(enemyShield, targetEnemy.transform.position, Quaternion.identity);
                targetPlayerScript.Shielded = false;
                targetEnemyScript.Shielded = true;
                if (GameObject.Find("Shield_For_Player(Clone)"))
                {
                    Destroy(GameObject.Find("Shield_For_Player(Clone)"));
                }
                break;
            case ("E", false, false):
                Instantiate(enemyShield, targetEnemy.transform.position, Quaternion.identity);
                targetPlayerScript.Shielded = false;
                targetEnemyScript.Shielded = true;
                if (GameObject.Find("Shield_For_Player(Clone)"))
                {
                    Destroy(GameObject.Find("Shield_For_Player(Clone)"));
                }
                break;
        }
    }
}
