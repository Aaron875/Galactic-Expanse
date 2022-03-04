using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Building> buildings; //All the bases
    public GameObject pStart; //Player Start
    public GameObject eStart; //Enemy Start
    private float timer; //Used for incrementing unit counts
    [SerializeField] private bool isTutorialActive = false;
    [SerializeField] private bool isPaused = false;

    [SerializeField] private float basicUnitProdRate = 2;

    // Managers
    private EnemyManager enemyManager;
    private SquadManager squadManager;
    private BuildingMgr buildingManager;

    // Tutorial Objects
    [SerializeField] private GameObject tutorialStep1;
    [SerializeField] private GameObject tutorialStep2;

    [SerializeField] private GameObject replayButton;
    [SerializeField] private Text replayButtonText;
    //private Text replayButtonText;

    // Start is called before the first frame update
    void Start()
    {
        enemyManager = GetComponent<EnemyManager>();
        squadManager = GetComponent<SquadManager>();
        buildings = GetComponent<BuildingMgr>().Buildings;
        Debug.Log(buildings.Count);

        //replayButtonText = replayButton.GetComponent<Text>();

        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            isPaused = false;
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

        if(!isPaused)
        {
            //Change number to change how quickly buildings gain units
            if (timer >= basicUnitProdRate)
            {
                UpdateBases();
                timer = 0;
            }

            if(isTutorialActive && tutorialStep2.activeInHierarchy)
            {
                enemyManager.UpdateAI();
            }
            else if(!isTutorialActive)
            {
                enemyManager.UpdateAI();
            }
        }

        if(isTutorialActive)
        {
            if ((buildings[2].Alignment == "P" || buildings[3].Alignment == "P") &&
                tutorialStep1.activeInHierarchy)
            {
                tutorialStep1.SetActive(false);
                tutorialStep2.SetActive(true);

               /* buildings[1].NumUnits = 40;
                buildings[2].NumUnits = 30;
                buildings[3].NumUnits = 30;*/
            }

            if(tutorialStep2.activeInHierarchy && buildings[0].Alignment == "P")
            {
                tutorialStep2.SetActive(false);
            }
        }

        CheckWinState();
    }

    private void LateUpdate()
    {
        if(!isPaused)
        {
            squadManager.UpdateSquads();
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
                    buildings[i].Alignment == "E" &&
                    tutorialStep1.activeInHierarchy) continue;

                buildings[i].NumUnits += 1;
            }
        }
    }

    void CheckWinState()
    {
        int playerBuildings = 0;
        int enemyBuildings = 0;
        int neutralBuildings = 0;

        // Calculate how many buildings each team has
        foreach(Building building in buildings)
        {
            switch (building.Alignment)
            {
                case "P":
                    playerBuildings++;
                    break;
                case "N":
                    neutralBuildings++;
                    break;
                case "E":
                    enemyBuildings++;
                    break;
            }
        }


        // Ends the game and either exits the tutorial or shows the replay button
        if(playerBuildings == buildings.Count - neutralBuildings)
        {
            if(isTutorialActive)
                SceneManager.LoadScene("Game");
            else
            {
                isPaused = true;
                replayButtonText.text = "Victory!\n" + "Back to galaxy map!";
                replayButton.SetActive(true);
            }
        }
        else if(enemyBuildings == buildings.Count - neutralBuildings)
        {
            if (isTutorialActive)
                SceneManager.LoadScene("Tutorial");
            else
            {
                isPaused = true;
                replayButtonText.text = "Defeat!\n" + "Back to galaxy map!";
                replayButton.SetActive(true);
            }
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game"); // This will change once galaxy map is implemented
    }

    public void StartTutorial()
    {
        SceneManager.LoadScene("Tutorial"); // This will change once galaxy map is implemented
    }
}
