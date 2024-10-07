using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CertificateGenerator : MonoBehaviour
{
    public GameObject certificatePanel;
    public Image screenshotImage1;
    public Image screenshotImage2;
    public TMP_InputField playerNameInput;
    public Button saveButton;

    private void Start()
    {
        certificatePanel.SetActive(false);
        saveButton.onClick.AddListener(SaveCertificateAsImage);
    }

    public void SetupCertificate(Texture2D screenshot1, Texture2D screenshot2)
    {
        screenshotImage1.sprite = Sprite.Create(screenshot1, new Rect(0, 0, screenshot1.width, screenshot1.height), new Vector2(0.5f, 0.5f));
        screenshotImage2.sprite = Sprite.Create(screenshot2, new Rect(0, 0, screenshot2.width, screenshot2.height), new Vector2(0.5f, 0.5f));

        certificatePanel.SetActive(true);
    }

    public void SaveCertificateAsImage()
    {
        string playerName = playerNameInput.text;
    }

   
}
