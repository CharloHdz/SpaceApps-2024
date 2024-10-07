using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class chr_GameManager : MonoBehaviour
{

    [Header("Niveles de juego")]
    [SerializeField] private GameObject FerLevel;
    [SerializeField] private GameObject QuiqueLevel;
    public static chr_GameManager instance;
    void Awake()
    {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this);
        }else{
            Destroy(this);
        }
    }
    public GameState gameState;
    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.MainMenu;
    }

    // Update is called once per frame
    void Update()
    {
        switch(gameState){
            case GameState.MainMenu:
                // Lógica del menú principal
                break;
            case GameState.Game:
                // Lógica del juego
                if(Input.GetKeyDown(KeyCode.Escape)){
                    chr_UI_Manager.instance.Pause();
                    gameState = GameState.Pause;
                }
                break;
            case GameState.Pause:
                // Lógica de pausa
                if(Input.GetKeyDown(KeyCode.Escape)){
                    chr_UI_Manager.instance.ResumeGame();
                    gameState = GameState.Game;
                }
                break;
            case GameState.GameOver:
                // Lógica de fin de juego
                break;
            case GameState.Exit:
                // Lógica de salida
                break;
        }
    }

    public void CargarJuego1(){
        FerLevel.SetActive(true);
        QuiqueLevel.SetActive(false);
    }

    public void CargarJuego2(){
        FerLevel.SetActive(false);
        QuiqueLevel.SetActive(true);
    }

    public void RegresarMenu(){
        SceneManager.LoadScene("chr_EscenaFinal");
    }
}

public enum GameState{
    MainMenu,
    Game,
    Pause,
    GameOver,
    Exit
}
