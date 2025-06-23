using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop_Speed : MonoBehaviour
{
    public Player player;
    public TextMeshProUGUI CheckText;

   public void CheckCoinAddSpeed()
    {
        if (player.Coin >= 30 && player.moveSpeed <= 4.5)
        {
            player.moveSpeed += 0.5f;
            player.Coin -= 30;
            CheckText.text = "+ Speed";
            GameDataManager.Instance.playerData.moveSpeed = player.moveSpeed;
            GameDataManager.Instance.SaveData();
        }
        else if (player.Coin < 30 && player.moveSpeed <= 4.5)
        {
            CheckText.text = "돈이 모자라요.";
        }
        else if (player.moveSpeed >= 5 && player.Coin >= 30)
        {
            CheckText.text = "이미 최대속도입니다.";
        }
    }
    public void CheckCoinAddHP()
    {

        if (player.Coin >= 50 && player.Hp <= 5f)
        {
            player.Hp += 1.0f;
            player.Coin -= 50;
            CheckText.text = "+ Hp";
            GameDataManager.Instance.playerData.Hp = player.Hp;
            GameDataManager.Instance.SaveData();
        }
        else if (player.Coin < 50 && player.Hp <= 5f)
        {
            CheckText.text = "돈이 모자라요.";
        }
        else if (player.Hp >= 5f && player.Coin >= 50)
        {
            CheckText.text = "이미 최대 체력입니다.";
        }
    }
}
