using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core.Patterns.Creational;
using TMPro;
using DG.Tweening;

public class UIManager : Singleton<UIManager>
{
    public Image pogImage;
    public GameObject pogImageMovePos;
    public TextMeshProUGUI pogText;
    public int pogScore;
    // Start is called before the first frame update
    void Start()
    {
        pogScore = PlayerPrefs.GetInt("PogScore", 0);
        AssignPogValue();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void IncreasePogScore(int amount)
    {
        pogScore += amount;
        PlayerPrefs.SetInt("PogScore", pogScore);
        AssignPogValue();
    }
    public void AssignPogValue()
    {
        pogText.text = "x" + pogScore;
    }
}