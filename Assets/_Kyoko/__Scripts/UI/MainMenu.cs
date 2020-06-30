using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI logText = null;
    [SerializeField] private GameObject cotent = null;

    public static MainMenu self = null;

    private void Awake()
    {
        self = this;
    }


    public static void Log(string text)
    {
        self.logText.text = text;
        ToggleLog(true);
    }


    public static void ToggleLog(bool state)
    {
        self.logText.gameObject.SetActive(state);
    }


    public static void OnRoomReady()
    {
        ToggleLog(false);
    }


    public static void OnLeftRoom()
    {
        self.cotent.SetActive(true);
        ToggleLog(false);
    }


    public void OnOpponentSearching()
    {
        ToggleLog(false);
        cotent.SetActive(false);
    }
}
