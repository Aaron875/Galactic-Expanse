using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Base> bases; //All the bases
    public GameObject pStart; //Player Start
    public GameObject eStart; //Enemy Start
    private int updateTime; //Used for incrementing unit counts

    // Start is called before the first frame update
    void Start()
    {
        updateTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        updateTime++;

        if(updateTime >= 600)
        {
            UpdateBases();
            updateTime = 0;
        }
    }

    void UpdateBases()
    {
        for(int i = 0; i < bases.Count; i++)
        {
            if(bases[i].owner != 0)
            {
                bases[i].units++;
            }
        }
    }
}
