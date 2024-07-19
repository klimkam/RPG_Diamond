using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RowManager : MonoBehaviour
{
    public Button addButton;
    public Button subButton;
    public TextMeshProUGUI text;
    public Stats.Stat stat;

    // Start is called before the first frame update
    void Start()
    {
        addButton.onClick.AddListener(AddButtonClicked);
        subButton.onClick.AddListener(SubButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        text.text = Stats.GetFullName(stat) + " : " + Stats.statsValue[(int)stat];
    }

    void AddButtonClicked()
    {
        bool increaseSuccessful = Stats.TryIncreaseStat(stat);
    }

    void SubButtonClicked()
    {
        bool increaseSuccessful = Stats.TryDecreaseStat(stat);
    }
}
