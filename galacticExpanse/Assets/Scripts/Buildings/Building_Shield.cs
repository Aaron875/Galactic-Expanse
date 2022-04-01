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

    void ChangeShield()
    {
        switch (this.Alignment, targetPlayerScript.Shielded, targetEnemyScript.Shielded)
        {
            //case ("P",false,true):
                //Instantiate(playerShield, targetPlayer.transform.position, Quaternion.identity);
                //break;
            case ("P", false, false || true):
                Instantiate(playerShield, targetPlayer.transform.position, Quaternion.identity);
                targetPlayerScript.Shielded = true;
                targetEnemyScript.Shielded = false;
                break;
            case ("E", false, false):
                Instantiate(enemyShield, targetEnemy.transform.position, Quaternion.identity);
                targetPlayerScript.Shielded = false;
                targetEnemyScript.Shielded = true;
                break;
            //case ("E", false, false):
                //Instantiate(enemyShield, targetEnemy.transform.position, Quaternion.identity);
                //break;
        }
    }
}
