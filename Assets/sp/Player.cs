using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class Player : MonoBehaviour
{
    float moveSpeed = 2f;
    public float score;

    public TextMeshProUGUI ScoreText;
    public GameObject GameOverPanel;

    [SerializeField] Sprite spriteUp;
    [SerializeField] Sprite spriteDown;
    [SerializeField] Sprite spriteLeft;
    [SerializeField] Sprite spriteRight;

    Rigidbody2D rb;
    SpriteRenderer sR;

    Vector2 input;
    Vector2 velocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sR = GetComponent<SpriteRenderer>();
        rb.bodyType = RigidbodyType2D.Kinematic;

  
    }

    private void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        velocity = input.normalized * moveSpeed;

        if(input.sqrMagnitude > .01f)
        {
            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            {
                if (input.x > 0)
                    sR.sprite = spriteRight;
                else if (input.x < 0)
                    sR.sprite = spriteLeft;
            }
        }
        else
        {
            if (input.y > 0)
                sR.sprite = spriteUp;
            else
                sR.sprite = spriteDown;
        }

        ScoreText.text = "Score" + score.ToString();
    }

   

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            ItemObject item = collision.GetComponent<ItemObject>();
            
            score += collision.GetComponent<ItemObject>().GetPoint();

            GameDataManager.Instance.playerData.collectedItms.Add(item.GetName());

            Destroy(collision.gameObject);

            GameDataManager.Instance.SaveData(GameDataManager.Instance.playerData);
        }
        if (collision.CompareTag("Enemy"))
        {
            //SceneManager.LoadScene("Leve_1");
            GameOverPanel.SetActive(true);
            
        }
    }

}