using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
   public float moveSpeed = 2f;
    public int score;
    public int Coin;

    public Slider HpSlide;
    public float MaxHp = 5;
    public float Hp;
   
   /* public float invincible = 3.0f;
    public bool islnvincible = false;*/

    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI CoinText;

    public GameObject GameOverPanel;
    public GameObject ShopPanel;

    [SerializeField] Sprite spriteUp;
    [SerializeField] Sprite spriteDown;
    [SerializeField] Sprite spriteLeft;
    [SerializeField] Sprite spriteRight;

    Rigidbody2D rb;
    SpriteRenderer sR;

    Vector2 input;
    Vector2 velocity;

    private void Start()
    {
        Hp = GameDataManager.Instance.playerData.Hp;
        if (Hp == 0)
            Hp = 2;
        Coin = GameDataManager.Instance.playerData.Coin;
        score = GameDataManager.Instance.playerData.score;
        
        moveSpeed = GameDataManager.Instance.playerData.moveSpeed;
        if (moveSpeed == 0)
            moveSpeed = 2;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sR = GetComponent<SpriteRenderer>();
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
            else
            {
                if (input.y > 0)
                    sR.sprite = spriteUp;
                else
                    sR.sprite = spriteDown;
            }
        }
        /*if(islnvincible == true)
        {
            yield return new WaitForSeconds(invincible);
            isInvincible false;
        }*/
        if (Hp <= 0)
        {
            Time.timeScale = 0;
            GameOverPanel.SetActive(true);
        }

        

        UpdateHPUI();

        ScoreText.text = "Score : " + score.ToString();
        CoinText.text = "Coin : " + Coin.ToString();
    }

   

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            Destroy(collision.gameObject);

            ItemObject item = collision.GetComponent<ItemObject>();
            
           score += collision.GetComponent<ItemObject>().GetPoint();
           

            Coin += collision.GetComponent<ItemObject>().GetCoin();


            GameDataManager.Instance.playerData.score = score;
            
            GameDataManager.Instance.playerData.Coin = Coin;
           GameDataManager.Instance.playerData.collectedItms.Add(item.GetName());

           GameDataManager.Instance.SaveData();
        }
        if (collision.CompareTag("Enemy"))
        {
            DecreaseHP(1.0f);
            //islnvincible = true;
           
        }
        if(collision.CompareTag("Finish"))
        {
            collision.GetComponent<LevelObject>().MoveToNextLeve();
        }
        if (collision.CompareTag(("Shop")))
        {
            Time.timeScale = 0;
            ShopPanel.SetActive(true);
        }
    }

    public void DecreaseHP(float bar)     
    {
        Hp -= bar;
        Hp = Mathf.Clamp(Hp, 0f, MaxHp);
        UpdateHPUI();
    }

    void UpdateHPUI()
    {
        if (HpSlide != null)
            HpSlide.value = Hp;
    }
}