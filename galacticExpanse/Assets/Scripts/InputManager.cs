using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    [SerializeField] private SquadManager squadManager;
    [SerializeField] private GameManager gameManager;
    //private Building startLocation;
    private Building targetLocation;

    public LayerMask draggableMask;
    public LayerMask hittableMask;
    [SerializeField] GameObject selectedObject;
    [SerializeField] GameObject selectedObject2;
    bool isDragging;
    Vector3 pos;
    Vector3 originPos;

    // Line Renderer stuff
    private LineRenderer lr;
    private Transform[] points;
    [SerializeField]List<GameObject> attackingPlanets;

    void Start()
    {
        isDragging = false;
    }
    void Update()
    {
        UpdateTimeMultiplier();


        // Called when left mouse click is held down
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, draggableMask);

            // if the user clicks and a valid planet is clicked on
            if(hit.collider != null)
            {
                // saves the clicked planet and it's position as the origin for attacks
                selectedObject = hit.collider.gameObject;
                originPos = selectedObject.transform.position;
                isDragging = true;
            }
        }

        // if the user is dragging...
        if (isDragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D select = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, draggableMask);
            if (select.collider != null)
            {
                // if the current planet has not been added to the attacking planets list, do it
                if (attackingPlanets.Count < 1)
                {
                    attackingPlanets.Add(select.collider.gameObject);
                }
                else if (!attackingPlanets.Contains(select.collider.gameObject)) // MUST HAVE THIS ELSE IF OTHERWISE IT BREAKS
                {
                    for (int i = 0; i < attackingPlanets.Count; i++)
                    {
                        if (select.collider.gameObject != attackingPlanets[i].gameObject)
                        {
                            i++;
                            attackingPlanets.Add(select.collider.gameObject);
                            return;
                        }
                    }
                }
            }

            // Loops through list of attacking planets, and if the planet changes 
            for (int i = 0; i < attackingPlanets.Count; i++)
            {
                if (attackingPlanets[i].GetComponent<Building>().Alignment != "P")
                {
                    attackingPlanets[i].GetComponent<LineRenderer>().enabled = false;
                    attackingPlanets.RemoveAt(i);
                }
            }

            // saves the position of the mouse for line renderer purposes
            pos = mousePos();

            // loop for drawing lines from each attacking planet to the mouse cursor
            for (int i = 0; i < attackingPlanets.Count; i++)
            {
                attackingPlanets[i].GetComponent<LineRenderer>().enabled = true;
                lr = attackingPlanets[i].GetComponent<LineRenderer>();
                lr.SetPosition(0, attackingPlanets[i].transform.position);
                lr.SetPosition(1, pos);
            }
        }

        // When the mouse button is released, send attacks
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            if (selectedObject2 != null)
            {
                if(selectedObject2 != null && targetLocation != null)
                {
                    if (attackingPlanets != null)
                    {
                        // Loop for sending attacks from each planet accordingly
                        for (int i = 0; i < attackingPlanets.Count; i++)
                        {
                            Attack(attackingPlanets[i].GetComponent<Building>(), targetLocation);
                        }
                    }

                    selectedObject = null;
                    targetLocation = null;
                }
            }

            // This loop is necessary so the lines for targeting don't stay on the screen in between sent attacks
            for (int i = 0; i < attackingPlanets.Count; i++)
            {
                attackingPlanets[i].GetComponent<LineRenderer>().enabled = false;
            }

            // clears the attacking planets list
            attackingPlanets.Clear();
        }

        HitDetection();
    }

    /// <summary>
    /// Grabs mouse position on the screen, and converts its coordinates for use elsewhere
    /// </summary>
    /// <returns></returns>
    Vector3 mousePos()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
    }

    /// <summary>
    /// Detects a hit using raycasts
    /// </summary>
    public void HitDetection()
    {
        // Raycast for hit detection of target building
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, hittableMask);
        if (hit.collider != null)
        {
            //Debug.Log(hit.collider.gameObject.name);
            selectedObject2 = hit.collider.gameObject;
            targetLocation = selectedObject2.GetComponent<Building>();
        }

        // hit detection for friendly planet (quick and dirty reinforcement of player planets)
        hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, draggableMask);
        if (hit.collider != null)
        {
            //Debug.Log(hit.collider.gameObject.name);
            selectedObject2 = hit.collider.gameObject;
            targetLocation = selectedObject2.GetComponent<Building>();
        }
    }

    private void Attack(Building _startLocation, Building _targetLocation)
    {
        // public void CreateSquad(int _numUnits, Building _startLocation, Building _targetLocation)
        Debug.Log(targetLocation);
        //This makes sure that the unit numbers will not be weird if it has an odd number
        if (_startLocation.NumUnits % 2 == 0)
        {
            _startLocation.NumUnits = _startLocation.NumUnits / 2;
            squadManager.CreateSquad(_startLocation.NumUnits, _startLocation, _targetLocation);
        }
        else
        {
            _startLocation.NumUnits = _startLocation.NumUnits / 2;
            squadManager.CreateSquad(_startLocation.NumUnits, _startLocation, _targetLocation);
            _startLocation.NumUnits++;
        }
    }

    // WHOEVER MADE THIS I COMMENTED IT OUT FOR NOW, IN CASE IT IS NECESSARY LATER
    //private void Attack(Squad _carrier, Building _targetLocation)
    //{
    //    Debug.Log("Attacking from carrier...");
    //    if(_carrier.NumUnits % 2 == 0)
    //    {
    //        _carrier.NumUnits = _carrier.NumUnits / 2;
    //        squadManager.CreateSquad(_carrier.NumUnits, _carrier, _targetLocation);
    //    }
    //    else
    //    {
    //        _carrier.NumUnits = _carrier.NumUnits / 2;
    //        squadManager.CreateSquad(_carrier.NumUnits, _carrier, _targetLocation);
    //        _carrier.NumUnits++;
    //    }
    //}

    /// <summary>
    /// Sets position count for lines drawn 
    /// </summary>
    /// <param name="points"></param>
    public void SetUpLine(Transform[] points)
    {
        lr.positionCount = points.Length;
        this.points = points;
    }

    /// <summary>
    /// Handles changing the speed of the game
    /// </summary>
    private void UpdateTimeMultiplier()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(gameManager.CurrentTimeMultiplier != 0)
            {
                gameManager.CurrentTimeMultiplier = 0;
            }
            else
            {
                gameManager.CurrentTimeMultiplier = gameManager.PreviousTimeMultiplier;
            }
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            gameManager.CurrentTimeMultiplier = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gameManager.CurrentTimeMultiplier = 2;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            gameManager.CurrentTimeMultiplier = 3;
        }
    }
}
