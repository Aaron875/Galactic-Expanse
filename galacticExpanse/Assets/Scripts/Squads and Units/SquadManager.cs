using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadManager : MonoBehaviour
{
    #region Variables

    [SerializeField] private GameObject squadPrefab;
    [SerializeField] private List<GameObject> playerSquads; // change to squad prefabs
    [SerializeField] private List<GameObject> enemySquads;

    #endregion

    #region Properties

    public List<GameObject> PlayerSquads
    {
        get { return playerSquads; }
    }

    public List<GameObject> EnemySquads
    {
        get { return enemySquads; }
    }

    #endregion

    #region Scripts

    public void CreateSquad(int _team, int _unitType, int _numUnits, Vector2 _startLocation, Vector2 _targetLocation)
    {
        GameObject newSquad = Instantiate(squadPrefab, _startLocation, Quaternion.identity);
        Squad newSquadScript = newSquad.GetComponent<Squad>();

        newSquadScript.NumUnits = _numUnits;
        newSquadScript.TargetLocation = _targetLocation;

        if (_team == 0)
        {
            playerSquads.Add(newSquad);
            return;
        }

        enemySquads.Add(newSquad);
    }

    #endregion
}
