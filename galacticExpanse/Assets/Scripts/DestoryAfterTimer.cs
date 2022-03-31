using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryAfterTimer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroySelf", 1);
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}
