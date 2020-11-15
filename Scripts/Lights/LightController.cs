using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LightTypes;

public class LightController : MonoBehaviour
{
    private float ModeTimer;
    public GameObject FireFlyParent;
    public GameObject SpotlightParent;

    private enum lightMode { Firefly, Spotlight, Off };
    private lightMode currentMode;
    private lightMode previousMode;

    private void Start()
    {
        currentMode = lightMode.Off;
        previousMode = lightMode.Off;
        ModeTimer = 10f;
    }

    void Update()
    {
        if (currentMode == lightMode.Spotlight)
        {
            if (previousMode != currentMode)
            {
                FireFlyParent.SetActive(false);
                SpotlightParent.SetActive(true);
                ModeTimer = Random.Range(3f, 7f);
                previousMode = currentMode;
            }
        }
        else if (currentMode == lightMode.Firefly)
        {
            if (previousMode != currentMode)
            {
                SpotlightParent.SetActive(false);
                FireFlyParent.SetActive(true);
                ModeTimer = Random.Range(10f, 15f);
                previousMode = currentMode;
            }
        }
        else
        {
            if (previousMode != currentMode)
            {
                SpotlightParent.SetActive(false);
                FireFlyParent.SetActive(false);
                ModeTimer = Random.Range(0f, 3f);
                previousMode = currentMode;
            }
        }

        ModeTimer -= Time.deltaTime;

        if (ModeTimer <= 0f)
        {
            modeTimerEnded();
        }
    }

    private void modeTimerEnded()
    {
        float randRoll = Random.Range(0f, 1f);

        switch (currentMode)
        {
            case lightMode.Off:
                if (randRoll < 0.5) currentMode = lightMode.Firefly;
                else currentMode = lightMode.Spotlight;
                break;
            case lightMode.Firefly:
                if (randRoll < 0.3) currentMode = lightMode.Off;
                else currentMode = lightMode.Spotlight;
                break;
            case lightMode.Spotlight:
                if (randRoll < 0.3) currentMode = lightMode.Off;
                else currentMode = lightMode.Firefly;
                break;
            default:
                currentMode = lightMode.Off;
                break;
        }
    }
}