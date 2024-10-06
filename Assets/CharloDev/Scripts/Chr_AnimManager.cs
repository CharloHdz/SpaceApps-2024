using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chr_AnimManager : MonoBehaviour
{
    [Header ("Animaciones")]
    public Animator MenuPanelAnim;
    public Animator PausePanelAnim;
    public Animator SettingsPanelAnim;
    public Animator GamePanelAnim;
    public Animator ExitPanelAnim;
    public Animator GameOverPanelAnim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Menu
    public void AnimMenuPanelOpen(){
        MenuPanelAnim.SetTrigger("Open");
    }

    public void AnimMenuPanelClose(){
        MenuPanelAnim.SetTrigger("Close");
    }

    //Settings
    public void AnimSettingsPanelOpen(){
        SettingsPanelAnim.SetTrigger("Open");
    }
    
    public void AnimSettingsPanelClose(){
        SettingsPanelAnim.SetTrigger("Close");
    }

    public void AnimSettingsPanelOpened(){
        SettingsPanelAnim.SetTrigger("Opened");
    }
}
