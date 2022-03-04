using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Overlay : MonoBehaviour
{
    private Button startButton;
    private Button backButton;
    [SerializeField] private string overlayName;

    void Start()
    {
        startButton = GameObject.Find("StartButton").GetComponent<Button>();
        backButton = GameObject.Find("BackButton").GetComponent<Button>();
        startButton.onClick.AddListener(StartButtonClicked);
        backButton.onClick.AddListener(BackButtonClicked);
    }

    void StartButtonClicked()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    void BackButtonClicked()
    {
        GameObject overlay = GameObject.Find(overlayName);
        Destroy(overlay);
        
    }
}
