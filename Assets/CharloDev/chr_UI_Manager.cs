using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class chr_UI_Manager : MonoBehaviour
{
    [Header ("Paneles")]
    [SerializeField] private GameObject MenuPanel;
    [SerializeField] private GameObject SettingsPanel;
    [SerializeField] private GameObject GamePanel;
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject GameoverPanel;
    [SerializeField] private GameObject ExitPanel;


    [Header ("Traductor")]
    public Traduccion idioma; 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void HideAllPanels(){
        MenuPanel.SetActive(false);
        SettingsPanel.SetActive(false);
        GamePanel.SetActive(false);
        PausePanel.SetActive(false);
        GameoverPanel.SetActive(false);
        ExitPanel.SetActive(false);
    }

    public void StartGame(){
        StartCoroutine(LoadGame());

        //Animator
        
    }

    public IEnumerator LoadGame(){
        yield return new WaitForSeconds(1.5f);
        HideAllPanels();
        GamePanel.SetActive(true);
    }

    public void Settings(){

    }

    public void Exit(){

    }

    public void Pause(){

    }

    public void Gameover(){

    }

    public void ExitGame(){

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
