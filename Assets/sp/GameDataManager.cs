using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements.Experimental;


[Serializable]

public class PlayerData
{
    public List<string> collectedItms = new List<string>();

    public int stage = 1;
    public int Coin;
    public int score;
    public float moveSpeed = 2;
    public float Hp;
}

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance;

    public PlayerData playerData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveData()
    {
        string filepath = Application.persistentDataPath +"/player_data.json";
        string json = JsonUtility.ToJson(playerData, true);
        System.IO.File.WriteAllText(filepath, json);
        Debug.Log("게임 데이터 로드됨: " + json);
    }
    public PlayerData LoadData()
    {
        string filePath = Application.persistentDataPath + "/player_data.json";
        if (System.IO.File.Exists(filePath))
        {
            string json = System.IO.File.ReadAllText(filePath);
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);
            Debug.Log("게임 데이터 로드됨: " + json);
            return playerData;
        }
        else 
        {
            Debug.LogWarning("저장된 게임 데이터가 없습니다.");
            return new PlayerData();
        
        }
    }
    public void GameStart()
    {
        playerData = LoadData();
        if (playerData == null)
        {
            playerData = new PlayerData();
            SceneManager.LoadScene("Leve_1");
        }
        else
        {
            SceneManager.LoadScene("Leve_" + playerData.stage);
        }
    }

    public void PlayerDead()
    {
        PlayerData _playerData = LoadData();
        if (_playerData != null)
        {
            _playerData.stage = 1;

            foreach (string item in _playerData.collectedItms.ToList())
            {
                if (UnityEngine.Random.Range(0, 1) == 0)
                {
                    _playerData.collectedItms.Remove(item);
                }
            }
            playerData = _playerData;
            SaveData();
        }
        SceneManager.LoadScene("GameOver");
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
