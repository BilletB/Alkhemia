﻿using UnityEngine;
using System.Collections;

public class particule_script : MonoBehaviour
{


    public string LayerName = "Element";

    public void Start()
    {
        this.gameObject.GetComponent<Renderer>().sortingLayerName = LayerName;
        this.gameObject.GetComponent<Renderer>().sortingOrder = 1;
        
    }


}