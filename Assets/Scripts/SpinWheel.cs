using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpinWheel : MonoBehaviour
{
    public bool spinStarted = false;
    public bool activeSpinTab = false;
    public GameObject wheel;
    public GameObject spinWheelMenu;
    public GameObject spinArea;
    public float[] sectorAngles;
    public float spinStartAngle = 0;
    public float spinEndAngle;
    public Text awardDisplay;
    public GameManager gameManager;
    public float currentLerpRotation;
    public float maxLerpRotationTime;


    void Update()
    {
        HideIfClickedOutside(spinArea);
        if (!spinStarted)
            return;

        maxLerpRotationTime = 4f;
        currentLerpRotation += Time.deltaTime;
        if (currentLerpRotation > maxLerpRotationTime || wheel.transform.eulerAngles.z == spinEndAngle)
        {
            currentLerpRotation = maxLerpRotationTime;
            spinStarted = false;
            spinStartAngle = spinEndAngle % 360;


            GiveAwardByAngle();
            StartCoroutine("HideAwardDisplay");
        }

        float spinTime = currentLerpRotation / maxLerpRotationTime;

        spinTime = spinTime * spinTime * spinTime * (spinTime * (6f * spinTime - 15f) + 10f);

        float angle = Mathf.Lerp(spinStartAngle, spinEndAngle, spinTime);
        wheel.transform.eulerAngles = new Vector3(0, 0, angle);

    }

    public void SpinWheelButton(){

        currentLerpRotation = 0f;

        sectorAngles = new float[] { 30, 60, 90, 120, 150, 180, 210, 240, 270, 300, 330, 360 };

        int fullspins = 5;

        float randomAngle = sectorAngles[Random.Range(0,sectorAngles.Length)];
        spinEndAngle = -(fullspins * 360 + randomAngle);
        Debug.Log(spinEndAngle + "endangle");
        spinStarted = true;
    }

    private void GiveAwardByAngle()
    {

        switch ((int)spinStartAngle)
        {
            case 0:
                Rewards(100000);
                break;
            case -330:
                Rewards(2000);
                break;
            case -300:
                Rewards(1000);
                break;
            case -270:
                Rewards(5000);
                break;
            case -240:
                Rewards(300);
                break;
            case -210:
                Rewards(10000);
                break;
            case -180:
                Rewards(900);
                break;
            case -150:
                Rewards(200);
                break;
            case -120:
                Rewards(100);
                break;
            case -90:
                Rewards(700);
                break;
            case -60:
                Rewards(500);
                break;
            case -30:
                Rewards(100);
                break;
            default:
                Rewards(3000);
                break;
        }
    }

    private void Rewards(int award)
    {
        gameManager.mainCurrency += award;
        awardDisplay.text = award.ToString("F2");
        awardDisplay.gameObject.SetActive(true);
    }

    private IEnumerator HideAwardDisplay()
    {
        yield return new WaitForSeconds(1f);
        awardDisplay.gameObject.SetActive(false);
    }

    public void SpinWheelMenu()
    {

        if (!activeSpinTab) {
            spinWheelMenu.gameObject.SetActive(true);
            activeSpinTab = true;
        }
        //else
        //{
        //    spinWheelMenu.gameObject.SetActive(false);
        //    activeSpinTab = false;
        //}
    }

    private void HideIfClickedOutside(GameObject panel)
    {
        if (Input.GetMouseButton(0) && panel.activeSelf && !RectTransformUtility.RectangleContainsScreenPoint(panel.GetComponent<RectTransform>(),
            Input.mousePosition, Camera.main))
        {
            spinWheelMenu.SetActive(false);
            activeSpinTab = false;
        }
    }

}
