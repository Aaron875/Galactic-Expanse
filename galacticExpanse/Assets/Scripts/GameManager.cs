using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Building> buildings; //All the bases
    public GameObject pStart; //Player Start
    public GameObject eStart; //Enemy Start
    private float timer; //Used for incrementing unit counts
    [SerializeField] private bool isTutorialActive = false;
    [SerializeField] private bool isPaused = true;

    // Managers
    private EnemyManager enemyManager;
    private BuildingMgr buildingManager;

    // Tutorial Objects
    [SerializeField] private GameObject tutorialStep1;
    [SerializeField] private GameObject tutorialStep2;

    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "Tutorial")
        {
            isTutorialActive = true;
/*            tutorialStep1.SetActive(true);
            tutorialStep2.SetActive(false);*/
        }

        enemyManager = GetComponent<EnemyManager>();

        timer = 0;
        buildings = GetComponent<BuildingMgr>().Buildings;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        //Change number to change how quickly buildings gain units
        if(timer >= 2)
        {
            UpdateBases();
            timer = 0;
        }

        if(!isPaused)
        {
            enemyManager.UpdateAI();
        }

        if(isTutorialActive)
        {
/*            if (buildingManager.Buildings[2].Alignment == "P")
            {
                tutorialStep1.SetActive(false);
            }*/
        }
    }

    void UpdateBases()
    {
        for(int i = 0; i < buildings.Count; i++)
        {
            if(buildings[i].Alignment != "N" && buildings[i].NumUnits < 50)
            {
                buildings[i].NumUnits += 1;
            }
        }
    }
}
