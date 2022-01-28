using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameControlScript : MonoBehaviour
{
    [Header("Texts")]
    [SerializeField] TextMeshProUGUI skorText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI endGameText;

    PlayerControlScript playerControlScript;
    GameObject endGameCanvas;

    float timer;
    

    void Awake()
    {
        endGameCanvas = GameObject.FindGameObjectWithTag("EndGameCanvas");
        playerControlScript = FindObjectOfType<PlayerControlScript>().GetComponent<PlayerControlScript>();
    }
     void Start()
    {
        endGameCanvas.gameObject.SetActive(false);
    }
    void Update()
    {
        DisplayBackground();
    }
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void EndGameScreen()
    {
        GameObject.FindGameObjectWithTag("BackGroundCanvas").gameObject.SetActive(false);
        endGameCanvas.gameObject.SetActive(true);

        timerText.enabled = false;
        endGameText.text = "Skor: " + playerControlScript.Score+"\n"+
                         "Time: " +(int)timer;
        GetComponent<AudioSource>().Stop();
       
    }
    void DisplayBackground()
    {
        timer += Time.deltaTime;
        timerText.text = "Time: " + (int)timer;
        skorText.text = "Score: " + playerControlScript.Score;
    }

    
}
