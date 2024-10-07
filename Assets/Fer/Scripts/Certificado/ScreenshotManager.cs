using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class ScreenshotManager : MonoBehaviour
{
    private Texture2D screenshot1;
    private Texture2D screenshot2;
    private Texture2D screenshot3;

    public CertificateGenerator certificateGenerator; 
    public Camera mainCamera; 

    // Metodo primera pantalla
    public void CaptureFirstScreenshot()
    {
        StartCoroutine(CaptureScreenshot(1));
    }

    // Metodo segunda pantalla
    public void CaptureSecondScreenshot()
    {
        StartCoroutine(CaptureScreenshot(2));
    }

    public void CaptureThirdScreenshot()
    {
        StartCoroutine(CaptureScreenshot(3));
    }

    private System.Collections.IEnumerator CaptureScreenshot(int screenshotNumber)
    {
        // Captura la pantalla
        Texture2D screenshot = ScreenCapture.CaptureScreenshotAsTexture();
        
        // Guarda la captura según el número
        if (screenshotNumber == 1)
        {
            screenshot1 = screenshot;
        }
        else if (screenshotNumber == 2)
        {
            screenshot2 = screenshot;
            certificateGenerator.SetupCertificate(screenshot1, screenshot2);
        }

        else if (screenshotNumber == 3)
        {
            screenshot3 = screenshot;
            SaveThirdScreenshotAsImage(screenshot3);
        }

        yield return new WaitForSeconds(0);
    }

    private void SaveThirdScreenshotAsImage(Texture2D screenshot)
    {
        string path = "";

        #if UNITY_STANDALONE_WIN
        path = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), "ThirdScreenshot.png");
        #elif UNITY_STANDALONE_OSX
        path = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory), "ThirdScreenshot.png");
        #endif

        // Guarda la imagen de la tercera captura
        byte[] bytes = screenshot.EncodeToPNG();
        System.IO.File.WriteAllBytes(path, bytes);

        Debug.Log("Tercera captura guardada en: " + path);
        OpenFile(path);
    }

     private void OpenFile(string path)
    {
        #if UNITY_STANDALONE_WIN
        System.Diagnostics.Process.Start("explorer.exe", path);
        #elif UNITY_STANDALONE_OSX
        System.Diagnostics.Process.Start("open", path);
        #endif
    }

}
