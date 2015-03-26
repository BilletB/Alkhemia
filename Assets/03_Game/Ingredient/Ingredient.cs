﻿using UnityEngine;
using System.Collections;

public class Ingredient : MonoBehaviour 
{
    public string name;
    public Element element;
    public bool IsInInventaire;


    public Transform PlayerPosition;

    public GameObject Origin;

    public int index_inventaire;

    void OnMouseOver()
    {
        BagManager.instance.m_CanShoot = false;
        if (IsInInventaire)
        {
            
        }
    }
    void OnMouseExit()
    {
        BagManager.instance.m_CanShoot = true;
        if (IsInInventaire)
        {

        }
    }

    void OnMouseDown()
    {
        if(IsInInventaire)
        {
            Instantiate(Origin, PlayerPosition.position, this.transform.rotation);
            BagManager.instance.m_HUDInventaire.drop_inventaire(index_inventaire);

            BagManager.instance.m_CanShoot = true;
            Destroy(this.gameObject);
        }
    }


}