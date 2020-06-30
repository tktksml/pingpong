using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BallSettingsWindow : MonoBehaviour
{
    public TextMeshProUGUI currentSpeedText;
    public TextMeshProUGUI currentScaleText;
    public void OnEnable()
    {
        currentScaleText.text = SaveController.BallSettings.Value.scale.ToString();
        currentSpeedText.text = SaveController.BallSettings.Value.speed.ToString();
    }

    public void SetBallSpeed(string value)
    {
        if (value.Length > 0)
        {
            SaveController.BallSettings.Value.speed = float.Parse(value);
        }
    }
    public void SetBallScale(string value)
    {
        if (value.Length > 0)
        {
            SaveController.BallSettings.Value.scale = int.Parse(value);
        }
    }
    public void SetBallColor(Color color)
    {
        SaveController.BallSettings.Value.color = color;
    }


    public void OnDisable()
    {
        SaveController.Save();
    }
}
