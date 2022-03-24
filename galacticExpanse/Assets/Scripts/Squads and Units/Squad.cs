using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Squad : MonoBehaviour
{
    #region Variables

    [SerializeField] private int numUnits;
    [SerializeField] private float speed;
    [SerializeField] private Building targetLocation; // change to a base
    [SerializeField] private string team;

    //all variables used here are for the text boxes above the squad
    private Text textPrefab;
    private Canvas renderCanvas;
    private string displayUnits;
    private Text tempTextBox;

    #endregion

    #region Properties

    public Text TempTextBox
    {
        get { return tempTextBox; }
    }

    public string Team
    {
        get { return team; }
        set { team = value; }
    }

    public int NumUnits
    {
        get { return numUnits; }
        set { numUnits = value; }
    }

    public Building TargetLocation
    {
        set { targetLocation = value; }
        get { return targetLocation; }
    }

    #endregion

    private void Start()
    {
        //set the prefab as an object from the scene and then instantiate the object
        textPrefab = GameObject.Find("Squad Text Prefab").GetComponent<Text>();
        tempTextBox = Instantiate(textPrefab, new Vector3(transform.position.x, transform.position.y, 0f), Quaternion.identity) as Text;

        //find the canvas in the scene and then set it as the parent to the text
        renderCanvas = FindObjectOfType<Canvas>();
        tempTextBox.transform.SetParent(renderCanvas.transform, false);

        //Set the text box's text element to the current numUnits
        displayUnits = numUnits.ToString();
        tempTextBox.text = displayUnits;
    }

    public void UpdateSquad(int _currentTimeMultiplier)
    {
        //Debug.Log("Updating position...");
  
        tempTextBox.rectTransform.position = new Vector3(transform.position.x, transform.position.y, 0.0f);
        //Debug.Log(tempTextBox);
        
        transform.position = Vector2.MoveTowards(transform.position, targetLocation.gameObject.transform.position, Time.deltaTime * speed * _currentTimeMultiplier);
        
    }
}
