using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChanger : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Game"); // This will change once galaxy map is implemented
    }

    public void StartTutorial()
    {
        SceneManager.LoadScene("Tutorial"); // This will change once galaxy map is implemented
    }
}
