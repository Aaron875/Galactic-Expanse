using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadManager : MonoBehaviour
{
    #region Variables

    [SerializeField] private GameObject playerSquadPrefab;
    [SerializeField] private GameObject enemySquadPrefab;
    [SerializeField] private List<Squad> playerSquads;
    [SerializeField] private List<Squad> enemySquads;
    private List<Squad> squadsToRemove;
    [SerializeField] private float distanceToAttack;

    [Header("Debug Values")]
    [SerializeField] private GameObject targetTower;

    #endregion

    #region Properties

    public List<Squad> PlayerSquads
    {
        get { return playerSquads; }
    }

    public List<Squad> EnemySquads
    {
        get { return enemySquads; }
    }

    #endregion

    #region Scripts

    private void Start()
    {
        squadsToRemove = new List<Squad>();
        playerSquads = new List<Squad>();
        enemySquads = new List<Squad>();
    }

    private void LateUpdate()
    {
        // Debug spawing squads
/*        if(Input.GetMouseButtonDown(0))
        {
            CreateSquad(0, 0, 5, Camera.main.ScreenToWorldPoint(Input.mousePosition), targetTower.transform.position);
        }

        if (Input.GetMouseButtonDown(1))
        {
            CreateSquad(1, 0, 5, Camera.main.ScreenToWorldPoint(Input.mousePosition), targetTower.transform.position);
        }*/

        UpdateSquads();
    }

    private void UpdateSquads()
    {
        // Update player squads
        foreach(Squad squad in playerSquads)
        {
            squad.UpdateSquad();

            if (Vector2.Distance(squad.transform.position, squad.TargetLocation.transform.position) <= distanceToAttack)
            {
                //DamageTower(squad); // pass in the tower to attack
                squad.TargetLocation.damageBuilding(squad.NumUnits, squad.Team);
                squadsToRemove.Add(squad);
            }
        }
        if(squadsToRemove.Count > 0)
        {
            for(int i = 0; i < squadsToRemove.Count; i++)
            {
                Destroy(squadsToRemove[i].TempTextBox.gameObject);
                playerSquads.Remove(squadsToRemove[i]);
                Destroy(squadsToRemove[i].gameObject);
            }
            squadsToRemove.Clear();
        }

        // Update enemy squads
        foreach(Squad squad in enemySquads)
        {
            squad.UpdateSquad();

            if (Vector2.Distance(squad.transform.position, squad.TargetLocation.transform.position) <= distanceToAttack)
            {
                //DamageTower(squad); // pass in the tower to attack
                squad.TargetLocation.damageBuilding(squad.NumUnits, squad.Team);
                squadsToRemove.Add(squad);
            }
        }
        if (squadsToRemove.Count > 0)
        {
            for (int i = 0; i < squadsToRemove.Count; i++)
            {
                Destroy(squadsToRemove[i].TempTextBox.gameObject);
                enemySquads.Remove(squadsToRemove[i]);
                Destroy(squadsToRemove[i].gameObject);
            }
            foreach (Squad squad in squadsToRemove)
            {
                enemySquads.Remove(squad);
            }
            squadsToRemove.Clear();
        }
    }

    /// <summary>
    /// Creates a Squad of _unitType at _startLocation with target _targetLocation.
    ///     Assigns it to the corresponding team.
    /// </summary>
    /// <param name="_team"></param>
    /// <param name="_unitType"></param>
    /// <param name="_numUnits"></param>
    /// <param name="_startLocation"></param>
    /// <param name="_targetLocation"></param>
    public void CreateSquad(int _team, int _unitType, int _numUnits, Vector2 _startLocation, Building _targetLocation)
    {
        GameObject newSquad;

        // Set the prefab
        if (_team == 0)
        {
            newSquad = Instantiate(playerSquadPrefab, _startLocation, Quaternion.identity);
        }
        else
        {
            newSquad = Instantiate(enemySquadPrefab, _startLocation, Quaternion.identity);
        }

        // Set the script info
        Squad newSquadScript = newSquad.GetComponent<Squad>();

        newSquadScript.NumUnits = _numUnits;
        newSquadScript.TargetLocation = _targetLocation;
        newSquad.transform.up = _targetLocation.transform.position - newSquad.transform.position;

        // Assign the team
        if (_team == 0)
        {
            newSquadScript.Team = "P";
            playerSquads.Add(newSquadScript);
            return;
        }

        newSquadScript.Team = "E";
        enemySquads.Add(newSquadScript);
    }

    #endregion
}
