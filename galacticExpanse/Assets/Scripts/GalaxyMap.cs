using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyMap : MonoBehaviour
{

    [SerializeField] private GameObject overlayPrefab;
    [SerializeField] private bool openClose;
    [SerializeField] private string lvlName;

    public bool OpenClose
    {
        get { return openClose; }
        set { openClose = value; }
    }

    //What is the name of the scene this galaxy will link to
    public string LvlName
    {
        get { return lvlName; }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// When a galaxy is clicked then it opens its respective level overlay
    /// </summary>
    void OnMouseDown()
    {
        if (openClose)
        {
            bool noOverlay = true;

            //if there is already an overlay dont open another
            for (int i = 1; i <= 10; i++)
            {
                if (GameObject.Find("Overlay_" + i + "(Clone)"))
                {
                    noOverlay = false;
                }
            }

            //if there is no current overlay then create one
            if (noOverlay)
            {
                Instantiate(overlayPrefab, new Vector3(951.3505f, 527.9f, -1f), Quaternion.identity);
            }
        }
    }
}
