using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building_Turret : Building
{
    [SerializeField] private SquadManager squadManager;
    [SerializeField] private GameManager gameManager;

    [SerializeField] private GameObject turretBase;
    [SerializeField] private GameObject turretBarrel;
    [SerializeField] private GameObject projectileGO;
    [SerializeField] private int range = 150;
    [SerializeField] private float firerate = 2.5f;
    [SerializeField] private float lastFired = 0;

    [SerializeField] private List<Projectile> firedProjectiles;
    [SerializeField] private int currentTimeManipulation;

    public int CurrentTimeManipulation
    {
        set { currentTimeManipulation = value; }
    }


    // Start is called before the first frame update
    public override void Start()
    {
        squadManager = GameObject.Find("GameManager").GetComponent<SquadManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        firedProjectiles = new List<Projectile>();

        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        currentTimeManipulation = gameManager.CurrentTimeMultiplier;

        // turret code
        CheckForTargets();

        CleanupProjectiles();

        foreach(Projectile projectile in firedProjectiles)
        {
            projectile.UpdateProjectile(currentTimeManipulation);
        }

        base.Update();
    }

    private void CleanupProjectiles()
    {
        List<Projectile> projectilesToRemove = new List<Projectile>();

        for (int i = 0; i < firedProjectiles.Count; i++)
        {
            if (firedProjectiles[i] == null)
            {
                projectilesToRemove.Add(firedProjectiles[i]);
            }
        }

        foreach (Projectile projectile in projectilesToRemove)
        {
            firedProjectiles.Remove(projectile);
        }
    }

    private void Fire(Squad _target)
    {
        turretBase.transform.up = (_target.transform.position - transform.position);

        if(Time.time >= lastFired + firerate)
        {
            Debug.Log("Firing...");
            GameObject firedProjectile = Instantiate(projectileGO, transform.position, Quaternion.identity);

            Projectile firedProjectileScript = firedProjectile.GetComponent<Projectile>();
            firedProjectileScript.Target = _target;
            firedProjectiles.Add(firedProjectileScript);

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