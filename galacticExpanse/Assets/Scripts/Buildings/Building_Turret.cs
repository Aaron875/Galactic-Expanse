using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building_Turret : Building
{
    [SerializeField] private SquadManager squadManager;

    [SerializeField] private GameObject turretBase;
    [SerializeField] private GameObject turretBarrel;
    [SerializeField] private GameObject projectile;
    [SerializeField] private int range = 150;
    [SerializeField] private float firerate = 0.5f;
    [SerializeField] private float lastFired = 0;

    // Start is called before the first frame update
    public override void Start()
    {
        squadManager = GameObject.Find("GameManager").GetComponent<SquadManager>();

        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        // turret code
        CheckForTargets();

        base.Update();
    }

    private void Fire(Squad _target)
    {
        turretBase.transform.up = (_target.transform.position - transform.position);

        if(Time.time >= lastFired + firerate)
        {
            Debug.Log("Firing...");
            //Instantiate(projectile)
            lastFired = Time.time;
        }
    }

    private void CheckForTargets()
    {
        List<Squad> potentialTargets = new List<Squad>();

        if(Alignment == "P")
        {
            potentialTargets = squadManager.EnemySquads;
        }
        else if(Alignment == "E")
        {
            potentialTargets = squadManager.PlayerSquads;
        }


        Squad target = null;

        foreach(Squad squad in potentialTargets)
        {
            if(Vector2.Distance(transform.position, squad.transform.position) <= range)
            {
                if(target == null)
                {
                    target = squad;
                }
                // Compares the current targets distance to the current squad
                else if(Vector2.Distance(transform.position, squad.transform.position) < Vector2.Distance(transform.position, target.transform.position))
                {
                    target = squad;
                }
            }
        }


        if(target != null)
        {
            Fire(target);
        }
    }
}