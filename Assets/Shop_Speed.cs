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
        if (player.Coin >= 30 && player.moveSpeed <= 9)
        {
            player.moveSpeed += 5;
            player.Coin -= 30;
            CheckText.text = "+ Speed";
        }
        else if (player.Coin < 30 && player.moveSpeed <= 9)
        {
            CheckText.text = "돈이 모자라요. ㅠㅠ";
        }
        else if (player.moveSpeed >= 10 && player.Coin >= 30)
        {
            CheckText.text = "이미 속도가 빨라요.";
        }
    }
    public void CheckCoinAddHP()
    {

        if (player.Coin >= 50 && player.Hp <= 5)
        {
            player.Hp += 1;
            player.Coin -= 50;
            CheckText.text = "+ Hp";
        }
        else if (player.Coin < 50 && player.Hp <= 5)
        {
            CheckText.text = "돈이 모자라요. ㅠㅠ";
        }
        else if (player.Hp >= 5 && player.Coin >= 50)
        {
            CheckText.text = "이미 최대 체력입니다.";
        }
    }
}
