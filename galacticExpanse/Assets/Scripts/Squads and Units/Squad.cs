using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squad : MonoBehaviour
{
    #region Variables

    [SerializeField] private int numUnits;
    [SerializeField] private float speed;
    [SerializeField] private Building targetLocation; // change to a base
    [SerializeField] private string team;

    #endregion

    #region Properties

    public string Team
    {
        get { return team; }
        set { team = value; }
    }

    public int NumUnits
    {
        get { return numUnits; }
        set { numUnits = value; }
    }

    public Building TargetLocation
    {
        set { targetLocation = value; }
        get { return targetLocation; }
    }

    #endregion

    public void UpdateSquad()
    {
        //Debug.Log("Updating position...");
        transform.position = Vector2.MoveTowards(transform.position, targetLocation.gameObject.transform.position, Time.deltaTime * speed);
    }
}
