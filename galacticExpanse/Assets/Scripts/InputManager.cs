using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    [SerializeField] GameObject base1;
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
            Vector3 mouse = Input.mousePosition; // grabs current mouse position as soon as the object is clicked on
            base.transform.position = mouse; // Sets the clicked object's position  to the mouse's position
        }


    }

}
