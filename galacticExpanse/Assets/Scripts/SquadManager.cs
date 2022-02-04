using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadManager : MonoBehaviour
{
    #region Variables

    [SerializeField] private List<GameObject> playerSquads; // change to squad prefabs
    [SerializeField] private List<GameObject> enemySquads;

    #endregion

    #region Scripts

    public void CreateSquad(int _unitType, int _numUnits, Vector2 _startLocation, Vector2 _endLocation)
    {

    }

    #endregion
}
