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
    public GameObject NivelFer;
    public GameObject NivelQuique;
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject GameoverPanel;
    [SerializeField] private GameObject ExitPanel;

    [Header ("Elementos de UI")]
    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Slider SFXSlider;
    [SerializeField] private TextMeshProUGUI ResoText;
    [SerializeField] private TextMeshProUGUI WindowedStateText;
    [SerializeField] private Button cambiarResolucionButton;  // Añadido para vincular el botón

    [Header ("Diccionarios")]
    public Traduccion idioma; 
    public Resolution resolucion;  // Corrección de nombre "Resolution"
    public bool WindowedState;

    [Header ("Scripts")]
    [SerializeField] private Chr_AnimManager AnimManager;

    [Header ("Singleton")]
    public static chr_UI_Manager instance;

    [Header("Certificate Generator")]
    public Image screenshotImage1;
    public Image screenshotImage2;
    public TMP_InputField playerNameInput;

    [Header("Musica")]
    public AudioListener audioListener;
    public AudioClip audioMenu;
    public AudioClip audioGame;
    public AudioClip musicClip;

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

        // Cargar los valores
        MusicSlider.value = PlayerPrefs.GetFloat("Music", 0.5f);
        SFXSlider.value = PlayerPrefs.GetFloat("SFX", 0.5f);

        // Cargar la resolución inicial
        resolucion = Resolution.r720x480;

        // Asignar el botón de cambio de resolución a la función
        cambiarResolucionButton.onClick.AddListener(CambiarResolucion);

    }

    // Update se ha limpiado de redundancias
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T)){
            Gameover();
        }

        // Actualizar el volumen de la música

    }

    void HideAllPanels(){
        MenuPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        GamePanel.SetActive(false);
        PausePanel.SetActive(false);
        GameoverPanel.SetActive(false);
        ExitPanel.SetActive(false);
    }

    public void SetupCertificate1(Texture2D screenshot1)
    {
        screenshotImage1.sprite = Sprite.Create(screenshot1, new Rect(0, 0, screenshot1.width, screenshot1.height), new Vector2(0.5f, 0.5f));
    }

    public void SetupCertificate2(Texture2D screenshot2){
        screenshotImage2.sprite = Sprite.Create(screenshot2, new Rect(0, 0, screenshot2.width, screenshot2.height), new Vector2(0.5f, 0.5f));
    }

    public void SaveCertificateAsImage()
    {
        string playerName = playerNameInput.text;
    }

    public void CambiarResolucion(){
        // Cambiar la resolución cíclicamente
        resolucion = (Resolution)(((int)resolucion + 1) % System.Enum.GetValues(typeof(Resolution)).Length);

        // Actualiza la resolución según el nuevo valor de la enumeración
        switch(resolucion){
            case Resolution.r720x480:
                if (WindowedState)
                {
                    Screen.SetResolution(720, 405, true);
                }
                else
                {
                    Screen.SetResolution(720, 405, false);
                }
                ResoText.text = "720x405";
                break;
            case Resolution.r1080x720:
                if (WindowedState)
                {
                    Screen.SetResolution(1280, 720, true);
                }
                else
                {
                    Screen.SetResolution(1280, 720, false);
                }
                ResoText.text = "1280x720";
                break;
            case Resolution.r1920x1080:
                if (WindowedState)
                {
                    Screen.SetResolution(1920, 1080, true);
                }
                else
                {
                    Screen.SetResolution(1920, 1080, false);
                }
                ResoText.text = "1920x1080";
                break;
        }

        Debug.Log("Resolución cambiada a: " + ResoText.text);
    }

    public void Windowed(){
        if(WindowedState){
            WindowedState = true;
            Screen.fullScreen = true;
            WindowedStateText.gameObject.SetActive(true);
        } else {
            WindowedState = false;
            Screen.fullScreen = false;
            // Ajustamos la ventana para que sea sin bordes y ocupe la pantalla como ventana
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, false);
            WindowedStateText.gameObject.SetActive(false);
        }
    }

    IEnumerator SplashScreen(){
        yield return new WaitForSeconds(0.2f);
        HideAllPanels();
        SettingsPanel.SetActive(true);
        MenuPanel.SetActive(true);
        AnimManager.AnimMenuPanelOpen();
        AnimManager.AnimSettingsPanelOpen();
        chr_GameManager.instance.gameState = GameState.MainMenu;

        //Reproducir música de menú
    }

    public void StartGame(){
        StartCoroutine(LoadGame());
        AnimManager.AnimMenuPanelClose();
        AnimManager.AnimSettingsPanelClose();
        chr_GameManager.instance.gameState = GameState.Game;

        //Reproducir música de juego
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
        GamePanel.SetActive(true);
        PausePanel.SetActive(true);
        AnimManager.AnimSettingsPanelOpened();
        chr_GameManager.instance.gameState = GameState.Pause;
    }

    public void ResumeGame(){
        HideAllPanels();
        GamePanel.SetActive(true);
        chr_GameManager.instance.gameState = GameState.Game;
    }

    public void Gameover(){
        HideAllPanels();
        GameoverPanel.SetActive(true);
        chr_GameManager.instance.gameState = GameState.GameOver;

        SetupCertificate2(ScreenCapture.CaptureScreenshotAsTexture());
    }

    public void CancelExit(){
        switch(chr_GameManager.instance.gameState){
            case GameState.MainMenu:
                ExitPanel.SetActive(false);
                break;
            case GameState.Game:
                ExitPanel.SetActive(false);
                break;
            case GameState.Pause:
                ExitPanel.SetActive(false);
                break;
            case GameState.GameOver:
                ExitPanel.SetActive(false);
                break;
            case GameState.Exit:
                ExitPanel.SetActive(false);
                break;
        }
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

        // Guardar los valores
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

public enum Resolution // Cambio de nombre "Resolution"
{
    r720x480,
    r1080x720,
    r1920x1080
}

