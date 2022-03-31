using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    [SerializeField] private int numUnits;
    [SerializeField] private string alignment;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private string type;
    [SerializeField] private Text unitText;
    [SerializeField] private int attackability;
    [SerializeField] private List<float> distances;
    private string displayUnits;

    public int NumUnits
    {
        get { return numUnits; }
        set { numUnits = value; }
    }

    public string Alignment
    {
        get { return alignment; }
        set { alignment = value; }
    }

    public string Type
    {
        get { return type; }
    }

    public SpriteRenderer SpriteRenderer
    {
        get { return spriteRenderer; }
    }

    public string DisplayUnits
    {
        get { return displayUnits; }
    }

    public int Attackability
    {
        get { return attackability; }
        set { attackability = value; }
    }

    public List<float> Distances
    {
        get { return distances; }
    }

    public virtual void Start()
    {
        displayUnits = numUnits.ToString();
        unitText.text = displayUnits;
    }

    public virtual void Update()
    {
        displayUnits = numUnits.ToString();
        unitText.text = displayUnits;

        attackability = 50 - numUnits;

        if(type != "Normal")
        {
            attackability += 30;
        }

        if(alignment == "N")
        {
            attackability += 10;
        }
        else if(alignment == "E")
        {
            attackability -= 10;

            if (numUnits >= 30)
            {
                attackability -= 400;
            }
        }
    }


    /// <summary>
    /// allows the squads to do damage to each building
    /// depending on the units alignment the squad will either reinforce or damage a givin building
    /// if a buildings unit count is reduced to zero by its opponents squad the buildings alignment will flip to be the opponents
    /// </summary>
    /// <param name="squadUnits"></param>
    /// <param name="squadAlignment"></param>
    public void damageBuilding (int squadUnits, string squadAlignment )
    {
        switch((alignment, squadAlignment))
        {
            case ("P", "P"):
                if (numUnits + squadUnits <= 50)
                {
                    numUnits += squadUnits;
                }
                else
                {
                    numUnits = 50;
                }
                displayUnits = numUnits.ToString();
                break;

            case ("P", "E"):
                if (numUnits - squadUnits <= 0)
                {

                    numUnits -= squadUnits;
                    numUnits = Mathf.Abs(numUnits);
                    alignment = "E";
                }
                else
                {
                    numUnits -= squadUnits;
                }
                displayUnits = numUnits.ToString();
                break;

            case ("E", "E"):
                if (numUnits + squadUnits <= 50)
                {
                    numUnits += squadUnits;
                }
                else
                {
                    numUnits = 50;
                }
                displayUnits = numUnits.ToString();
                break;

            case ("E", "P"):
                if (numUnits - squadUnits <= 0)
                {

                    numUnits -= squadUnits;
                    numUnits = Mathf.Abs(numUnits);
                    alignment = "P";
                }
                else
                {
                    numUnits -= squadUnits;
                }
                displayUnits = numUnits.ToString();
                break;

            case ("N", "P"):
                if (numUnits - squadUnits <= 0)
                {

                    numUnits -= squadUnits;
                    numUnits = Mathf.Abs(numUnits);
                    alignment = "P";
                }
                else
                {
                    numUnits -= squadUnits;
                }
                displayUnits = numUnits.ToString();
                break;

            case ("N", "E"):
                if (numUnits - squadUnits <= 0)
                {

                    numUnits -= squadUnits;
                    numUnits = Mathf.Abs(numUnits);
                    alignment = "E";
                }
                else
                {
                    numUnits -= squadUnits;
                }
                displayUnits = numUnits.ToString();
                break;
        }
    }
}
