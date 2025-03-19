using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public TMP_Text waterText;
    private float gameTime;
    // Start is called before the first frame update
    void Start()
    {
        gameTime = 0;
        StartCoroutine(IncreaseWater());
        UpdateWaterUI();
    }

    private void UpdateWaterUI()
    {
        if (waterText != null)
        {
            waterText.text = $"Water:{curWater}";
        }
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
                UpdateWaterUI();
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
            UpdateWaterUI();
            if(selectedCard.isMagicCard == false)
            {
                GameObject ob = Instantiate(selectedCard.unitPrefab, pos, Quaternion.identity);
                Unit unit = ob.GetComponent<Unit>();
                if (unit != null)
                {
                    unit.faction = Faction.Player;
                }
                else
                {
                    Tower tower = ob.GetComponent<Tower>();
                    tower.faction = Faction.Player;
                }
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
    public void PlayEnemyCard(int cardIndex, Vector3 pos, Transform fireTransform)
    {
        Debug.Log("PlayCard called");
        if (cardIndex < 0 || cardIndex >= deck.Count)
            return;

        Card selectedCard = deck[cardIndex];

        //if (curWater >= selectedCard.cost)
        {
            //curWater -= selectedCard.cost;
            if (selectedCard.isMagicCard == false)
            {
                GameObject ob = Instantiate(selectedCard.unitPrefab, pos, Quaternion.identity);
                Unit unit = ob.GetComponent<Unit>();
                if (unit != null)
                {
                    unit.faction = Faction.Computer;
                }
                else
                {
                    Tower tower = ob.GetComponent<Tower>();
                    tower.faction = Faction.Computer;
                }
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
