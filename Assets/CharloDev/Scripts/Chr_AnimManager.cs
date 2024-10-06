using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chr_AnimManager : MonoBehaviour
{
    [Header ("Animaciones")]
    public Animator MenuPanelAnim;
    public Animator PausePanelAnim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MenuPanelOpen(){
        MenuPanelAnim.SetTrigger("Open");
    }

    public void MenuPanelClose(){
        MenuPanelAnim.SetTrigger("Close");
    }
}
