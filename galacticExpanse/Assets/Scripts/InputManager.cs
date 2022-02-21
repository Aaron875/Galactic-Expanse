using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    [SerializeField] private SquadManager squadManager;
    private Building startLocation;
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

    void Start()
    {
        //squadManager = GetComponent<SquadManager>();
        isDragging = false;
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
    }
    void Update()
    {
        // Called when left mouse click is held down
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, draggableMask);
            if(hit.collider != null)
            {
                //Debug.Log(hit.collider.gameObject.name);
                selectedObject = hit.collider.gameObject;
                selectedObject.GetComponent<CircleCollider2D>().enabled = false;
                originPos = selectedObject.transform.position;
                isDragging = true;
                startLocation = selectedObject.GetComponent<Building>();

                lr.SetPosition(0, originPos);
            }
        }

        if (isDragging)
        {
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
                if(startLocation != null && targetLocation != null)
                {
                    Attack(startLocation, targetLocation);
                    startLocation = null;
                    targetLocation = null;
                }
            }
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
    }

    private void Attack(Building _startLocation, Building _targetLocation)
    {
        //This makes sure that the unit numbers will not be weird if it has an odd number
        if (_startLocation.NumUnits % 2 == 0)
        {
            _startLocation.NumUnits = _startLocation.NumUnits / 2;
            squadManager.CreateSquad(0, 0, _startLocation.NumUnits, _startLocation.transform.position, _targetLocation);
        }
        else
        {
            _startLocation.NumUnits = _startLocation.NumUnits / 2;
            squadManager.CreateSquad(0, 0, _startLocation.NumUnits, _startLocation.transform.position, _targetLocation);
            _startLocation.NumUnits++;
        }
    }

    public void SetUpLine(Transform[] points)
    {
        lr.positionCount = points.Length;
        this.points = points;
    }
}
