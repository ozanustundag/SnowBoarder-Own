using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlScript : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    AudioSource audioSource;
    GameControlScript gameControlScript;

    [Header("Player Control Variables")]
    [SerializeField] float forceAmount = 10f;
    [SerializeField] float torqueAmount = 10f;

    [Header("Particle Effects")]
    [SerializeField] ParticleSystem trailEffect;
    [SerializeField] ParticleSystem headBumpEffect;
    [SerializeField] ParticleSystem finishLineEffect;

    [Header("Sound Effects")]
    [SerializeField] AudioClip headBumpAudio;
    [SerializeField] AudioClip finishLineAudio;
    [SerializeField] AudioClip coinEffect;

    bool onGround;
    public bool IsGameOver { get; set; }
    public int  Score { get; set; }


    private void Awake()
    {
        gameControlScript = FindObjectOfType<GameControlScript>().GetComponent<GameControlScript>();
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        AddForce();
        AddTorque();
    }
    void AddForce()
    {
        if (Input.GetKey(KeyCode.UpArrow) && onGround && !IsGameOver)
        {
            rigidbody2D.AddForce(new Vector2(forceAmount, 0));
        }      
    }
    void AddTorque()
    {
        if (Input.GetKey(KeyCode.RightArrow) && !IsGameOver)
        {
            rigidbody2D.AddTorque(-torqueAmount);
        }
        else if (Input.GetKey(KeyCode.LeftArrow)&& !IsGameOver)
        {
            rigidbody2D.AddTorque(torqueAmount);
        }
    }
    void GameOver()
    {
        rigidbody2D.velocity = (new Vector2(0, 0));       
        FindObjectOfType<SurfaceEffector2D>().GetComponent<SurfaceEffector2D>().enabled = false;    
        gameControlScript.EndGameScreen();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Ground"&&!IsGameOver)
        {
            onGround = true;
            trailEffect.gameObject.SetActive(true);
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Ground")
        {
            onGround = false;
            trailEffect.gameObject.SetActive(false);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Ground"&&!IsGameOver)
        {         
            headBumpEffect.gameObject.SetActive(true);
            audioSource.clip = headBumpAudio;
            audioSource.Play();
            IsGameOver = true;
            Invoke("GameOver", 2);
        }
        else if (collision.tag=="FinishLine"&&!IsGameOver)
        {
            audioSource.clip = finishLineAudio;          
            finishLineEffect.gameObject.SetActive(true);
            audioSource.Play();
            IsGameOver = true;
            Invoke("GameOver", 2);
        }
        else if (collision.tag=="Gold"&&!IsGameOver)
        {
            Score++;
            audioSource.clip = coinEffect;
            audioSource.Play();
            Destroy(collision.gameObject);

        }
    }
}
