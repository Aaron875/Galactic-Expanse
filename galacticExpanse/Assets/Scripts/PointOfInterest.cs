using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointOfInterest : MonoBehaviour
{
    private Quaternion rotation;
    private Vector2 scale;
    private bool shrinking;

    [SerializeField] float rotationSpeed = 10;
    [SerializeField] float shrinkingSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        rotation = transform.rotation;
        scale = transform.localScale;
        shrinking = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime), Space.Self);
        
        if(shrinking)
        {
            transform.localScale -= new Vector3(shrinkingSpeed * Time.deltaTime, shrinkingSpeed * Time.deltaTime);
            if (transform.localScale.x <= 0.75f) shrinking = false;
        }
        else
        {
            transform.localScale += new Vector3(shrinkingSpeed * Time.deltaTime, shrinkingSpeed * Time.deltaTime);
            if (transform.localScale.x >= 1f) shrinking = true;
        }
    }

    private void FixedUpdate()
    {
    }
}
