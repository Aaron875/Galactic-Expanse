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
    [SerializeField] GameObject selectedObject2; // this is the building to be returned as the target
    bool isDragging;
    Vector3 pos;
    Vector3 originPos;

    // Line Renderer stuff
    private LineRenderer lr;
    private Transform[] points;
    [SerializeField]List<GameObject> attackingPlanets;

    void Start()
    {
        //squadManager = GetComponent<SquadManager>();
        //gameManager = GetComponent<GameManager>();
        isDragging = false;
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
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
            if(hit.collider != null)
            {
                Debug.Log(hit.collider.gameObject.name);
                selectedObject = hit.collider.gameObject;
                selectedObject.GetComponent<CircleCollider2D>().enabled = false;
                originPos = selectedObject.transform.position;
                isDragging = true;

/*                if(selectedObject.tag == "Carrier")
                {
                    startLocation = selectedObject.GetComponent<Squad>();
                }
                else
                {
                    startLocation = selectedObject.GetComponent<Building>();

                }*/

                lr.SetPosition(0, originPos);
            }
        }

        if (isDragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D select = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, draggableMask);
            if (select.collider != null)
            {
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
            pos = mousePos();
            lr.SetPosition(1, pos);
        }

        if (Input.GetMouseButtonUp(0))
        {
            lr.SetPosition(0, new Vector3(0, 0, 0));
            lr.SetPosition(1, new Vector3(0,0,0));
            isDragging = false;
            if (selectedObject != null)
            {
                selectedObject.transform.position = originPos;
                selectedObject.GetComponent<CircleCollider2D>().enabled = true;
                if(selectedObject != null && targetLocation != null)
                {
                    if (attackingPlanets != null)
                    {
                        for (int i = 0; i < attackingPlanets.Count; i++)
                        {
                            if(attackingPlanets[i].tag == "Carrier")
                            {
                                Attack(attackingPlanets[i].GetComponent<Squad>(), targetLocation);
                            }
                            else
                            {
                                Attack(attackingPlanets[i].GetComponent<Building>(), targetLocation);
                            }
                        }
                    }

                    if(selectedObject.gameObject.tag == "Carrier")
                    {
                        Attack(selectedObject.GetComponent<Squad>(), targetLocation);
                    }
                    else
                    {
                        Attack(selectedObject.GetComponent<Building>(), targetLocation);
                    }

                    selectedObject = null;
                    targetLocation = null;
                }
            }
            attackingPlanets.Clear();
        }

        HitDetection();
    }

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

    private void Attack(Squad _carrier, Building _targetLocation)
    {
        Debug.Log("Attacking from carrier...");
        if(_carrier.NumUnits % 2 == 0)
        {
            _carrier.NumUnits = _carrier.NumUnits / 2;
            squadManager.CreateSquad(_carrier.NumUnits, _carrier, _targetLocation);
        }
        else
        {
            _carrier.NumUnits = _carrier.NumUnits / 2;
            squadManager.CreateSquad(_carrier.NumUnits, _carrier, _targetLocation);
            _carrier.NumUnits++;
        }
    }

    public void SetUpLine(Transform[] points)
    {
        lr.positionCount = points.Length;
        this.points = points;
    }

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
