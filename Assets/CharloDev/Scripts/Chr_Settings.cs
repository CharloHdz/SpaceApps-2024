using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Chr_Settings : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject SettingsContainer;
    public TextMeshProUGUI WindowedText;

    void Start()
    {
        SettingsContainer.SetActive(false);
        Screen.fullScreen = false;
        Screen.SetResolution(1280, 720, false);
        WindowedText.text = "7";
    }

    public void Windowed(){
        if(Screen.fullScreen){
            Screen.fullScreen = false;
            WindowedText.gameObject.SetActive(false);
        } else {
            Screen.fullScreen = true;
            WindowedText.gameObject.SetActive(true);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SettingsContainer.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SettingsContainer.SetActive(false);
    }
}

