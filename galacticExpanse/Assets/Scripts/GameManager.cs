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
    [SerializeField] private bool isPaused = false;

    // Managers
    private EnemyManager enemyManager;
    private BuildingMgr buildingManager;

    // Tutorial Objects
    [SerializeField] private GameObject tutorialStep1;
    [SerializeField] private GameObject tutorialStep2;

    // Start is called before the first frame update
    void Start()
    {
        enemyManager = GetComponent<EnemyManager>();
        buildings = GetComponent<BuildingMgr>().Buildings;
        Debug.Log(buildings.Count);

        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            isPaused = true;
            isTutorialActive = true;
            tutorialStep1.SetActive(true);
            tutorialStep2.SetActive(false);
        }

        timer = 0;
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
            if (buildings[2].Alignment == "P" && isPaused)
            {
                tutorialStep1.SetActive(false);
                tutorialStep2.SetActive(true);
                isPaused = false;

                buildings[1].NumUnits = 40;
                buildings[2].NumUnits = 40;
            }

            if(tutorialStep2.activeInHierarchy && buildings[0].Alignment == "P")
            {
                tutorialStep2.SetActive(false);
            }

            CheckWinState();
        }
    }

    void UpdateBases()
    {
        for(int i = 0; i < buildings.Count; i++)
        {
            if(buildings[i].Alignment != "N" && buildings[i].NumUnits < 50)
            {
                // tutorial only
                if (SceneManager.GetActiveScene().name == "Tutorial" &&
                    buildings[i].Alignment == "E") continue;

                buildings[i].NumUnits += 1;
            }
        }
    }

    void CheckWinState()
    {
        foreach(Building building in buildings)
        {
            if(building.Alignment == "E")
            {
                return;
            }
        }

        // Set the victory state
        SceneManager.LoadScene("Game");
    }
}
