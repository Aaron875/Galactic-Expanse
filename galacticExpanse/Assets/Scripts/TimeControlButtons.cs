using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeControlButtons : MonoBehaviour
{
    // VARIABLES    
    private GameManager gm;

    // color: 0069FF

    [SerializeField] private RectTransform pauseTransform;
    [SerializeField] private RectTransform playTransform;
    [SerializeField] private RectTransform twoTimesTransform;
    [SerializeField] private RectTransform threeTimesTransform;
    private Image pauseImage;
    private Image playImage;
    private Image twoTimesImage;
    private Image threeTimesImage;

    private List<RectTransform> buttonTransformsList; // list for easy resizing of all images
    private List<Image> buttonImages;

    private float speedPrevious;
    private float speedCurrent;



    // Start is called before the first frame update
    void Start()
    {
        // Grab images
        pauseImage = pauseTransform.GetComponent<Image>();
        playImage = playTransform.GetComponent<Image>();
        twoTimesImage = twoTimesTransform.GetComponent<Image>();
        threeTimesImage = threeTimesTransform.GetComponent<Image>();

        buttonImages = new List<Image>();
        buttonImages.Add(pauseImage);
        buttonImages.Add(playImage);
        buttonImages.Add(twoTimesImage);
        buttonImages.Add(threeTimesImage);

        // Add buttons to list
        buttonTransformsList = new List<RectTransform>();
        buttonTransformsList.Add(pauseTransform);
        buttonTransformsList.Add(playTransform);
        buttonTransformsList.Add(twoTimesTransform);
        buttonTransformsList.Add(threeTimesTransform);


        try
        {
            gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
        catch
        {
            Debug.Log("No GameManager found!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        speedCurrent = gm.CurrentTimeMultiplier;

        // Check if the speed has changed
        if(speedCurrent != speedPrevious)
        {
            UpdateTimeControlButtons();
        }
    }

    /// <summary>
    /// Resets all time control buttons. Will resize and recolor the currently
    ///     active time control button.
    /// </summary>
    private void UpdateTimeControlButtons()
    {
        Vector3 normalScale = new Vector3(1f, 1f, 1f);


        // Scale down all buttons first
        foreach (RectTransform rect in buttonTransformsList)
        {
            rect.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        }
        foreach(Image image in buttonImages)
        {
            image.color = Color.white;
        }

        // Scale up the selected button
        switch (gm.CurrentTimeMultiplier)
        {
            case 0:
                pauseTransform.localScale = normalScale;
                pauseImage.color = new Color(0, 105, 255);
                break;

            case 1:
                playTransform.localScale = normalScale;
                playImage.color = new Color(0, 105, 255);
                break;

            case 2:
                twoTimesTransform.localScale = normalScale;
                twoTimesImage.color = new Color(0, 105, 255);
                break;

            case 3:
                threeTimesTransform.localScale = normalScale;
                threeTimesImage.color = new Color(0, 105, 255);
                break;
        }

        speedPrevious = speedCurrent;
    }

    /// <summary>
    /// Used for time control button presses.
    /// </summary>
    /// <param name="_speed"></param>
    public void ChangeGameSpeed(int _speed)
    {
        gm.CurrentTimeMultiplier = _speed;
    }
}
