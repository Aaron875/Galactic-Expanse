using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public LayerMask draggableMask;
    GameObject selectedObject;
    bool isDragging;
    [SerializeField] GameObject[] enemyBuildings;
    Vector3 pos;
    Vector3 originPos;

    void Start()
    {
        isDragging = false;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, draggableMask);
            if(hit.collider != null)
            {
                //Debug.Log(hit.collider.gameObject.name);
                selectedObject = hit.collider.gameObject;
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
            selectedObject.transform.position = originPos;
        }
    }

    Vector3 mousePos()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
    }


    public void HitDetection()
    {
        for(int i = 0; i < enemyBuildings.Length; i++)
        {
            if (enemyBuildings[i].transform.position == pos)
            {
                Debug.Log("hit detected");
            }
        }

    }
}
