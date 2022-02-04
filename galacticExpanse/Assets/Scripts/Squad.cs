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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateSquad()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetLocation, Time.deltaTime * speed);
    }
}
