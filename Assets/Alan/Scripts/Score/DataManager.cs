using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [SerializeField] private ScoreData scoreData;
    [SerializeField] private CollectableSO collectable;

    private void Awake()
    {
        collectable.currentCollectable = 0;
        scoreData.ResetData();
    }
}
