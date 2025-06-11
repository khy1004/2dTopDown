using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] ItemSO data;
    [SerializeField]

    public int GetPoint()
    {
        return data.point;
    }
    public string GetName()
    {
        return data.itemname;
    }

}   
