using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Card
{
    public int cost;
    public string name;
    public string description;
    public GameObject unitPrefab;
    public bool isMagicCard;
}
public class CardManager : MonoBehaviour
{
    public List<Card> deck;
    //public Transform point;

    public int curWater = 5;
    private int maxWater = 10;
    public float waterspeed = 1f;
    private float gameTime;
    // Start is called before the first frame update
    void Start()
    {
        gameTime = 0;
        StartCoroutine(IncreaseWater());
    }

    IEnumerator IncreaseWater()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f / waterspeed);
            gameTime += Time.deltaTime;
            if (gameTime >= 60f)
            {
                waterspeed *= 2;
            }
            if (curWater < maxWater)
            {
                curWater++;
            }
        }
    }

    public void PlayCard(int cardIndex,Vector3 pos,Transform fireTransform)
    {
        Debug.Log("PlayCard called");
        if (cardIndex < 0 || cardIndex >= deck.Count)
            return;

        Card selectedCard = deck[cardIndex];

        if (curWater >= selectedCard.cost)
        {
            curWater -= selectedCard.cost;
            if(selectedCard.isMagicCard == false)
            {
                 Instantiate(selectedCard.unitPrefab, pos, Quaternion.identity);
                
            }
            else
            {
                GameObject magicEffect = Instantiate(selectedCard.unitPrefab, fireTransform.position, Quaternion.identity);
                MagicEffect magicEffectScript = magicEffect.GetComponent<MagicEffect>();
                if (magicEffectScript != null)
                {
                    magicEffectScript.targetPosition = pos;
                }
            }
        }
    }

    public int GetCurWater()
    {
        return curWater;
    }

    
}
