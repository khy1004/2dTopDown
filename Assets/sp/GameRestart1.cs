using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRestart1 : MonoBehaviour
{
    // Start is called before the first frame update
    public void RestartLevel_1()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Leve_1");
    }
}
