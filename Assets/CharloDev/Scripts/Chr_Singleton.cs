using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chr_Singleton : MonoBehaviour
{
    public static Chr_Singleton instance;
    void Awake()
    {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this);
        }else{
            Destroy(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
