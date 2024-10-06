using System.Runtime.Serialization;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class chr_UI_Manager : MonoBehaviour
{
    [Header ("Paneles")]
    [SerializeField] private GameObject MenuPanel;
    [SerializeField] private GameObject SettingsPanel;
    [SerializeField] private GameObject GamePanel;
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject GameoverPanel;
    [SerializeField] private GameObject ExitPanel;

    [Header ("Elementos de UI")]
    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Slider SFXSlider;
    [SerializeField] private TextMeshProUGUI ResoText;

    [Header ("Diccionarios")]
    public Traduccion idioma; 
    public Reoslution resolucion;

    [Header ("Scripts")]
    [SerializeField] private Chr_AnimManager AnimManager;

    [Header ("Singleton")]
    public static chr_UI_Manager instance;

    private void Awake() {
        if(instance == null){
            instance = this;
        }else{
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SplashScreen());
        HideAllPanels();

        //Cargar los valores
        MusicSlider.value = PlayerPrefs.GetFloat("Music", 0.5f);
        SFXSlider.value = PlayerPrefs.GetFloat("SFX", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        switch(resolucion){
            case Reoslution.r720x480:
                Screen.SetResolution(1080, 720, true);
                ResoText.text = "1080x720";
                break;
            case Reoslution.r1080x720:
                Screen.SetResolution(1920, 1080, true);
                ResoText.text = "1920x1080";
                break;
            case Reoslution.r1920x1080:
                Screen.SetResolution(720, 480, true);
                ResoText.text = "720x480";
                break;
        }

        if(Input.GetKeyDown(KeyCode.T)){
            Gameover();
        }
    }

    void HideAllPanels(){
        MenuPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        GamePanel.SetActive(false);
        PausePanel.SetActive(false);
        GameoverPanel.SetActive(false);
        ExitPanel.SetActive(false);
    }

    IEnumerator SplashScreen(){
        yield return new WaitForSeconds(0.2f);
        HideAllPanels();
        SettingsPanel.SetActive(true);
        MenuPanel.SetActive(true);
        AnimManager.AnimMenuPanelOpen();
        AnimManager.AnimSettingsPanelOpen();
    }

    public void StartGame(){
        StartCoroutine(LoadGame());
        AnimManager.AnimMenuPanelClose();
        AnimManager.AnimSettingsPanelClose();
    }

    public IEnumerator LoadGame(){
        yield return new WaitForSeconds(1f);
        HideAllPanels();
        GamePanel.SetActive(true);
    }

    public void Settings(){
        SettingsPanel.SetActive(true);
    }

    public void Exit(){
        Application.Quit();
    }

    public void Pause(){
        HideAllPanels();
        PausePanel.SetActive(true);
        AnimManager.AnimSettingsPanelOpened();
    }

    public void ResumeGame(){
        HideAllPanels();
        GamePanel.SetActive(true);
    }

    public void Gameover(){
        HideAllPanels();
        GameoverPanel.SetActive(true);
    }

    public void TakeScreenshot(){
        ScreenCapture.CaptureScreenshot("YourPlanetUwU.png");
    }

    public void ReturnToMenu(){
        StartCoroutine(SplashScreen());
    }

    public void ExitGame(){
        ExitPanel.SetActive(true);
    }

    public void ValueChangeCheck(){
        Debug.Log("Music: " + MusicSlider.value);
        Debug.Log("SFX: " + SFXSlider.value);

        //Guardar los valores
        PlayerPrefs.SetFloat("Music", MusicSlider.value);
        PlayerPrefs.SetFloat("SFX", SFXSlider.value);
        PlayerPrefs.Save();
    }

    public void CambiarIdioma(){
        if(idioma == Traduccion.Español){
            idioma = Traduccion.Ingles;
        }else{
            idioma = Traduccion.Español;
        }
    }
}

public enum Traduccion
{
    Español,
    Ingles
}

public enum Reoslution
{
    r720x480,
    r1080x720,
    r1920x1080
}  

