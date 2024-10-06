using UnityEngine;

public class Chr_ScreenManager : MonoBehaviour
{
    // Inicia en modo pantalla completa
    void Start()
    {
        Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
    }

    // Cambia a ventana sin bordes o pantalla completa según la necesidad
    public void ToggleFullScreen(bool isFullScreen)
    {
        if (isFullScreen)
        {
            // Modo pantalla completa
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            Screen.fullScreen = true;
        }
        else
        {
            // Modo ventana sin bordes
            Screen.fullScreenMode = FullScreenMode.Windowed;
            Screen.fullScreen = false;
            // Ajustamos la ventana para que sea sin bordes y ocupe la pantalla como ventana
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, false);
        }
    }
}

