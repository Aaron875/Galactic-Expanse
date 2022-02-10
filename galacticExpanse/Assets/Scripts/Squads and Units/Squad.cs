using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squad : MonoBehaviour
{
    #region Variables

    [SerializeField] private int numUnits;
    [SerializeField] private float speed;
    [SerializeField] private Vector2 targetLocation; // change to a base

    #endregion

    #region Properties

    public int NumUnits
    {
        get { return numUnits; }
        set { numUnits = value; }
    }

    public Vector2 TargetLocation
    {
        set { targetLocation = value; }
        get { return targetLocation; }
    }

    #endregion

    public void UpdateSquad()
    {
        Debug.Log("Updating position...");
        transform.position = Vector2.MoveTowards(transform.position, targetLocation, Time.deltaTime * speed);
    }
}
