﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UnlockingSystem : MonoBehaviour
{
    public GameManager gameManager;
    public Research research;
    public GameObject[] upgradeObjects;
    public GameObject[] planetsPanelsObjects;
    public Button[] planetsUnlockBtnObj;
    public Button nextButton;
    public GameObject[] gateUnlockButtonObject;
    public Text[] unlockText;
    public Text[] planetPriceText;
    public Text[] planetRequirementResearch;

    [System.NonSerialized]
    public bool[] animationUnlockConfirm;
    [System.NonSerialized]
    public double[] unlockCost;
    public double[] planetCost;
    private string[] researchNames;
    public int researchID = 1;
    private int[] researchTextGreenAtID = { 0, 4, 9, 14 };

    public bool[] planetCanBeUnlocked;

    private void Start()
    {
        researchNames = new string[gameManager.Research1Level.Length];
        researchNames[0] = "Oxygen Recycle";
        researchNames[1] = "Ion Engines";
        researchNames[2] = "Water Filter";
        researchNames[3] = "Plant Space Growing";
        researchNames[4] = "Low Gravity Lander";
        researchNames[5] = "Ion Engines II";
        researchNames[6] = "Space 3D Printer";
        researchNames[7] = "Space Drones";
        researchNames[8] = "Low Gravity Lander";
        researchNames[9] = "Ion Engines III";
        researchNames[10]= "Space 3D Printer";
        researchNames[11] = "Space Drones";
        researchNames[12] = "Low Gravity Lander";
        researchNames[13] = "Ion Engines IV";
        researchNames[14] = "Space 3D Printer";
        researchNames[15] = "Space Drones";

        planetCanBeUnlocked = new bool[4];

        for (int id = 0; id < planetsPanelsObjects.Length; id++)
        {
            planetCanBeUnlocked[id] = false;
            planetRequirementResearch[id].text = researchNames[researchID];
            researchID = researchID + 4;
        }
        planetCanBeUnlocked[0] = true;

    }

    void Update()
    {
        StageUnlockTextControl();
        PlanetStatusCheck();
    }

    public void StageUnlockTextControl()
    {
        for (int id = 0; id < upgradeObjects.Length; id++)
        {
            unlockText[id].text = GameManager.ExponentLetterSystem(unlockCost[id], "F2");

            if (gameManager.mainCurrency >= unlockCost[id])
            {
                unlockText[id].color = Color.green;
            }
            else
                unlockText[id].color = Color.red;
        }
    }

    public void PlanetStatusCheck()
    {
        for (int id = 0; id < planetsPanelsObjects.Length; id++)
        {
            if (!gameManager.planetUnlocked[id])
            {
                planetPriceText[id].enabled = true;
                planetRequirementResearch[id].enabled = true;
                planetPriceText[id].text = GameManager.ExponentLetterSystem(planetCost[id], "F2");
            }
            else
            {
                planetPriceText[id].enabled = false;
                planetRequirementResearch[id].enabled = false;
            }
                

            if (gameManager.mainCurrency >= planetCost[id])
            {
                planetPriceText[id].color = Color.green;
            } else
                planetPriceText[id].color = Color.red;


        }


        for (int researchNr = 0; researchNr < planetsUnlockBtnObj.Length; researchNr++)
        {
            if (gameManager.researchUnlocked[researchTextGreenAtID[researchNr]])
            {
                planetRequirementResearch[researchNr].color = Color.green;
            }
            else
                planetRequirementResearch[researchNr].color = Color.red;
        }

        
    }

    public void UnlockingStages(int id)
    {

        if ((gameManager.mainCurrency >= unlockCost[id]) && (gameManager.upgradesActivated[id] == false))
        {
            gameManager.mainCurrency -= unlockCost[id];
            upgradeObjects[id].SetActive(true);
            gameManager.upgradesActivated[id] = true;
            gateUnlockButtonObject[id].GetComponent<Button>().interactable = false;
            animationUnlockConfirm[id] = true;
            gameManager.StageLevel[id + 1] += 1;

            if (!gameManager.researchCanBeDone[0])
            {
                gameManager.researchCanBeDone[0] = true;
            }

        }
        else
            upgradeObjects[id].SetActive(false);

    }

    public void LoadUnlocksStatus()
    {
        for (int id = 0; id < upgradeObjects.Length; id++)
        {

            if (gameManager.upgradesActivated[id] == true)
            {
                upgradeObjects[id].SetActive(true);
                gateUnlockButtonObject[id].SetActive(false);
            }
            else if (gameManager.upgradesActivated[id] == false)
            {

                upgradeObjects[id].SetActive(false);
                gateUnlockButtonObject[id].SetActive(true);
                gateUnlockButtonObject[id].GetComponent<Button>().interactable = true;
            }
        }
    }

    public void ResearchUnlocking(int id)
    {
        if(gameManager.researchCanBeDone.Length - 1 > id)
        {
            gameManager.researchCanBeDone[id+1] = true;
        }
    }

    public void PlanetsUnlockCheck()
    {
        for (int id = 0; id < planetsPanelsObjects.Length; id++)
        {

            if (gameManager.planetUnlocked[id] == true)
            {
                planetsPanelsObjects[id].SetActive(true);
            }
            else if (!gameManager.planetUnlocked[id])
            {

                planetsPanelsObjects[id].SetActive(false);
            }
        }
    }

    public void PlanetsUnlocking(int id)
    {
        if (gameManager.researchUnlocked[researchTextGreenAtID[id]] == true  && gameManager.mainCurrency >= planetCost[id] && gameManager.planetUnlocked[id] == false)
        {
            gameManager.mainCurrency -= planetCost[id];
            planetsPanelsObjects[id].SetActive(true);
            planetsUnlockBtnObj[id].interactable = true;
            gameManager.planetUnlocked[id] = true;
        }
    }

}
