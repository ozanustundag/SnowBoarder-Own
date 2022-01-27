using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldControlScript : MonoBehaviour
{
    [Header("Coin Sprites")]
    [SerializeField] Sprite[] coinSprites;

    float timer;
    int coinSpriteNumber=0;
   
    void Update()
    {
        CoinSpriteControl();

    }
   
    void CoinSpriteControl()
    {
        timer += Time.deltaTime;
        if (timer > 0.03f)
        {
            coinSpriteNumber++;
            timer = 0;
            gameObject.GetComponent<SpriteRenderer>().sprite = coinSprites[coinSpriteNumber];
            if (coinSpriteNumber == coinSprites.Length - 1)
            {
                coinSpriteNumber = 0;
            }
        }
    }
   
}
