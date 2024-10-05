using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Chr_LocalizableText : MonoBehaviour
{
    [Header ("Español - ES")]
    [TextArea(7,5)]
    public string ES_Text;
    [Header ("Ingles - EN")]
    [TextArea(7,5)]
    public string EN_Text;

    private TextMeshProUGUI Texto;
    [SerializeField] chr_UI_Manager UI;
    // Start is called before the first frame update
    void Start()
    {
        Texto = GetComponent<TextMeshProUGUI>();
        UI = GameObject.Find("GameManager").GetComponent<chr_UI_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(UI.idioma){
            case Traduccion.Español:
                Texto.text = ES_Text;
                break;
            case Traduccion.Ingles:
                Texto.text = EN_Text;
                break;
        }
    }
}
