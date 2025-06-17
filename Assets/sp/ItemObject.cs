using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField] ItemSO data;

    public int GetPoint()
    {
        return data.point;
    }
    public string GetName()
    {
        return data.itemname;
    }
    public int GetCoin()
    {
        return data.coin;
    }

}   
