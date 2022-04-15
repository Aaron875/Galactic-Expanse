using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalaxyMapManager : MonoBehaviour
{
    [SerializeField] private List<GalaxyMap> allGalaxys;
    [SerializeField] private GameObject selector; //This is the poi prefab
    public static List<string> lvlNames = new List<string>(); //The list that will hold the names of the galaxys to be selectable
    private Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = FindObjectOfType<Canvas>();

        //loops through both lists to find what is selectable/open and what is not/closed
        for (int i = 0; i < allGalaxys.Count; i++)
        {
            for (int j = 0; j < lvlNames.Count; j++)
            {
                if (lvlNames[j] == allGalaxys[i].LvlName)
                {
                    //create the poi and add it to the canvas
                    var poi = Instantiate(selector, allGalaxys[i].transform.position, Quaternion.identity);
                    poi.transform.SetParent(canvas.transform);
                    allGalaxys[i].OpenClose = true;
                    break;
                }
                else
                {
                    allGalaxys[i].OpenClose = false;
                }
            }
        }
    }
}
