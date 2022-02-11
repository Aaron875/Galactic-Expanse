using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private int numUnits;
    [SerializeField] private string alignment;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private string type;

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

    public void damageBuilding (int squadNum, string squadAlignment )
    {
        switch((alignment, squadAlignment))
        {
            case ("P", "P"):
                if (numUnits + squadNum <= 50)
                {
                    numUnits += squadNum;
                }
                else
                {
                    numUnits = 50;
                }
                break;

            case ("P", "E"):
                if (numUnits - squadNum <= 0)
                {

                    numUnits -= squadNum;
                    Mathf.Abs(numUnits);
                    alignment = "E";
                }
                else
                {
                    numUnits -= squadNum;
                }
                break;

            case ("E", "E"):
                if (numUnits + squadNum <= 50)
                {
                    numUnits += squadNum;
                }
                else
                {
                    numUnits = 50;
                }
                break;

            case ("E", "P"):
                if (numUnits - squadNum <= 0)
                {

                    numUnits -= squadNum;
                    Mathf.Abs(numUnits);
                    alignment = "P";
                }
                else
                {
                    numUnits -= squadNum;
                }
                break;

            case ("N", "P"):
                if (numUnits - squadNum <= 0)
                {

                    numUnits -= squadNum;
                    Mathf.Abs(numUnits);
                    alignment = "P";
                }
                else
                {
                    numUnits -= squadNum;
                }
                break;

            case ("N", "E"):
                if (numUnits - squadNum <= 0)
                {

                    numUnits -= squadNum;
                    Mathf.Abs(numUnits);
                    alignment = "E";
                }
                else
                {
                    numUnits -= squadNum;
                }
                break;
        }
    }
}
