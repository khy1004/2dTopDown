using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour
{
    public AudioClip clickSound;

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void playclicksound()
    {
        audioSource.PlayOneShot(clickSound);

    }
    public void Letsgo()
    {
        //UnityEngine.SceneManagement.SceneManager.LoadScene("Leve_1");
        GameDataManager.Instance.GameStart();
    }

}