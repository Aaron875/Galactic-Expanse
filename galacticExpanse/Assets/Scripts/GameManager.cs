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

    [SerializeField] private float basicUnitProdTimer = 2;
    [SerializeField] private int basicUnitProdRate = 1;

    [SerializeField] private int currentTimeMultiplier = 1;
    [SerializeField] private int previousTimeMultiplier = 1;

    // Managers
    private EnemyManager enemyManager;
    private SquadManager squadManager;
    private BuildingMgr buildingManager;

    // Tutorial Objects
    [SerializeField] private GameObject tutorialStep1;
    [SerializeField] private GameObject tutorialStep2;

    [SerializeField] private Button replayButton;
    [SerializeField] private Text replayButtonText;
    //private Text replayButtonText;


    public int CurrentTimeMultiplier
    {
        get { return currentTimeMultiplier; }
        
        set 
        { 
            previousTimeMultiplier = currentTimeMultiplier;
            currentTimeMultiplier = value; 
        }
    }

    public int PreviousTimeMultiplier
    {
        get { return previousTimeMultiplier; }
    }

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

        //Sets distances for planets
        for(int i = 0; i < buildings.Count; i++)
        {
            for (int j = 0; j < buildings.Count; j++)
            {
                buildings[i].Distances.Add(Vector3.Distance(buildings[i].transform.position, buildings[j].transform.position));
            }
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
            if ((timer * currentTimeMultiplier) >= basicUnitProdTimer)
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
            if(buildings[i].Alignment != "N" && buildings[i].NumUnits < 50 && buildings[i].Type != "Shield" && buildings[i].Type != "Turret")
            {
                // tutorial only
                if (SceneManager.GetActiveScene().name == "Tutorial" &&
                    buildings[i].Alignment == "E" &&
                    tutorialStep1.activeInHierarchy) continue;

                buildings[i].NumUnits += basicUnitProdRate;
            }
            
            if (buildings[i].NumUnits > 50)
            {
                buildings[i].NumUnits = 50;
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
            //This if statement locks and unlocks the levels for selection on the galaxy map
            //It adds and removes from the public strict list in the GalaxyMapManager Script
            if (isPaused == false)
            {
                Scene scene = SceneManager.GetActiveScene();
                switch (scene.name)
                {
                    case ("Level_1"):
                        GalaxyMapManager.lvlNames.Remove("Level_1");
                        GalaxyMapManager.lvlNames.Add("Level_2");
                        GalaxyMapManager.lvlNames.Add("Level_3");
                        break;

                    case ("Level_2"):
                        for (int i = 0; i < GalaxyMapManager.lvlNames.Count; i++)
                        {
                            if (GalaxyMapManager.lvlNames[i] == "Level_3")
                            {
                                GalaxyMapManager.lvlNames.Add("Level_4");
                            }
                        }
                        GalaxyMapManager.lvlNames.Remove("Level_2");
                        break;

                    case ("Level_3"):
                        foreach (string l in GalaxyMapManager.lvlNames)
                        {
                            if (l == "Level_2")
                            {
                                GalaxyMapManager.lvlNames.Add("Level_4");
                            }
                        }
                        GalaxyMapManager.lvlNames.Remove("Level_3");
                        break;

                    case ("Level_4"):
                        GalaxyMapManager.lvlNames.Remove("Level_4");
                        GalaxyMapManager.lvlNames.Add("Level_5");
                        GalaxyMapManager.lvlNames.Add("Level_6");
                        break;

                    case ("Level_5"):
                        foreach (string l in GalaxyMapManager.lvlNames)
                        {
                            if (l == "Level_6")
                            {
                                GalaxyMapManager.lvlNames.Add("Level_7");
                            }
                        }
                        GalaxyMapManager.lvlNames.Add("Level_8");
                        GalaxyMapManager.lvlNames.Remove("Level_5");
                        break;

                    case ("Level_6"):
                        foreach (string l in GalaxyMapManager.lvlNames)
                        {
                            if (l == "Level_5")
                            {
                                GalaxyMapManager.lvlNames.Add("Level_7");
                            }
                        }
                        GalaxyMapManager.lvlNames.Add("Level_9");
                        GalaxyMapManager.lvlNames.Remove("Level_6");
                        break;

                    case ("Level_7"):
                        foreach (string l in GalaxyMapManager.lvlNames)
                        {
                            if (l == "Level_5")
                            {
                                GalaxyMapManager.lvlNames.Add("Level_8");
                            }
                            else if (l == "Level_6")
                            {
                                GalaxyMapManager.lvlNames.Add("Level_9");
                            }
                        }
                        GalaxyMapManager.lvlNames.Remove("Level_7");
                        break;

                    case ("Level_8"):
                        foreach (string l in GalaxyMapManager.lvlNames)
                        {
                            if (l == "Level_9")
                            {
                                GalaxyMapManager.lvlNames.Add("Level_10");
                            }
                        }
                        GalaxyMapManager.lvlNames.Remove("Level_8");
                        break;

                    case ("Level_9"):
                        foreach (string l in GalaxyMapManager.lvlNames)
                        {
                            if (l == "Level_8")
                            {
                                GalaxyMapManager.lvlNames.Add("Level_10");
                            }
                            else if (l == "Level_10")
                            {
                                GalaxyMapManager.lvlNames.Add("Level_11");
                            }
                        }
                        GalaxyMapManager.lvlNames.Remove("Level_9");
                        break;

                    case ("Level_10"):
                        foreach (string l in GalaxyMapManager.lvlNames)
                        {
                            if (l == "Level_9")
                            {
                                GalaxyMapManager.lvlNames.Add("Level_11");
                            }
                        }
                        GalaxyMapManager.lvlNames.Remove("Level_10");
                        break;

                    case ("Level_11"):
                        GalaxyMapManager.lvlNames.Remove("Level_11");
                        break;
                }
            }

            isPaused = true;
            

            if (isTutorialActive)
            {
                replayButtonText.text = "Victory!\n" + "To the galaxy map!";
                //replayButton.onClick.AddListener(delegate { SceneManager.LoadScene("Galaxy Map"); });
                replayButton.gameObject.SetActive(true);

            }
            else
            {
                isPaused = true;
                replayButtonText.text = "Victory!\n" + "Back to galaxy map!";
                replayButton.gameObject.SetActive(true);
            }
        }
        else if(enemyBuildings == buildings.Count - neutralBuildings)
        {
            if (isTutorialActive)
            {
                replayButtonText.text = "Defeat!\n" + "Retry tutorial?";
                replayButton.onClick.AddListener(ReloadScene);
                replayButton.gameObject.SetActive(true);
            }
            else
            {
                isPaused = true;
                replayButtonText.text = "Defeat!\n" + "Back to galaxy map!";
                replayButton.gameObject.SetActive(true);
            }
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
