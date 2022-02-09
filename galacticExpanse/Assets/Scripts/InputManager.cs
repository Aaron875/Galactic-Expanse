using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    [SerializeField] GameObject base1;
    [SerializeField] GameObject base2;
    [SerializeField] ScriptableObject squad;

    Vector3 base1Pos;

    private void Start()
    {
         base1Pos = base1.transform.position;

    }
    private void Update()
    {

        // For Click and Drag function keep as GetMouseButton, if wanting to change to only click, revert to GetMouseButtonDown
        // 0 = Left Click
        // 1 = Right Click
        // 2 = Middle Click
        if (Input.GetMouseButton(0))
        {
            // Currently how this works is any object this script is attached to becomes an object movable by the mouse.
            // If we want this script to be universal (preferred) raycasting will need to be performed
            // grabs current mouse position as soon as the object is clicked on
            Vector3 mouse = Input.mousePosition;
            base1.transform.position = mouse; // Sets the clicked object's position  to the mouse's position
            //Debug.Log(base1.gameObject); // for testing
        }

        HitDetection();
    }

    private void HitDetection() 
    {
        // Top-right
        if (!Input.GetMouseButton(0) &&
            base1.transform.position.x + 5 <= base2.transform.position.x + 5 &&
            base1.transform.position.y + 5 <= base2.transform.position.y + 5)
        {
            Debug.Log("Top-Right Hit Detected");
            base1.transform.position = base1Pos;

        }


        // Top-left
        if (!Input.GetMouseButton(0) &&
            base1.transform.position.x - 5 >= base2.transform.position.x - 5 &&
            base1.transform.position.y + 5 <= base2.transform.position.y + 5)
        {
            Debug.Log("Top-left Hit Detected");
            base1.transform.position = base1Pos;

        }

        // Bottom-left
        if (!Input.GetMouseButton(0) &&
            base1.transform.position.x - 5 >= base2.transform.position.x - 5 &&
            base1.transform.position.y - 5 >= base2.transform.position.y - 5)
        {
            Debug.Log("Bottom-left Hit Detected");
            base1.transform.position = base1Pos;

        }


        // Bottom-right
        if (!Input.GetMouseButton(0) &&
            base1.transform.position.x + 5 <= base2.transform.position.x + 5 &&
            base1.transform.position.y - 5 >= base2.transform.position.y - 5)
        {
            Debug.Log("Bottom-right Hit Detected");
            base1.transform.position = base1Pos;

        }
    }

}
