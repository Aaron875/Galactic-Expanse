using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public LayerMask draggableMask;
    public LayerMask hittableMask;
    GameObject selectedObject;
    [SerializeField] GameObject selectedObject2; // this is the building to be returned as the target
    bool isDragging;
    Vector3 pos;
    Vector3 originPos;

    void Start()
    {
        isDragging = false;
    }
    void Update()
    {
        // Called when left mouse click is held down
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, draggableMask);
            if(hit.collider != null)
            {
                //Debug.Log(hit.collider.gameObject.name);
                selectedObject = hit.collider.gameObject;
                selectedObject.GetComponent<CircleCollider2D>().enabled = false;
                originPos = selectedObject.transform.position;
                isDragging = true;
            }
        }

        if (isDragging)
        {
            pos = mousePos();
            selectedObject.transform.position = pos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            if (selectedObject != null)
            {
                selectedObject.transform.position = originPos;
                selectedObject.GetComponent<CircleCollider2D>().enabled = true;
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

        }
    }
}
