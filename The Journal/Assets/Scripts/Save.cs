using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Save : MonoBehaviour
{
    public GameObject controllers;
    public Button house;
    public Button clinic;
    private int childCount;
    private bool houseEnable = false;
    private bool clinicEnable = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        childCount = controllers.transform.childCount;
        if(childCount == 2)
        {
            if (!houseEnable)
            {
                house.gameObject.SetActive(true);
                houseEnable = true;
            }

        }
        else if(childCount == 1)
        {
            if (!clinicEnable)
            {
                clinic.gameObject.SetActive(true);
                clinicEnable = true;
            }
        }
         if(childCount == 0)
        {
            SceneManager.LoadScene("End");
        }
    }
}
