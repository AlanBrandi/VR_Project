﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectableManager : MonoBehaviour
{
   //Instance
   public static CollectableManager Instance;
   
   [Header("Collectable Configuration")] 
   
   [HideInInspector] public AudioSource colletableAudio;
   
   public int collectableAmount;
   private int currentCollectables; 
   
   [Header("Collectable Data")]
   [SerializeField] private CollectableSO collectableSo;
   [SerializeField] private ScoreData scoreData;
   
   [Header("Canvas")]
   [SerializeField] private TMP_Text colletableTXT;

   
   private void Awake()
   {
      if (!Instance)
      {
         Instance = this;
      }
      else
      {
         Destroy(this.gameObject);
      }

      colletableAudio = GetComponent<AudioSource>();
      currentCollectables = collectableSo.currentCollectable;
      colletableTXT.text = currentCollectables.ToString();
   }

   public void AddCollectable()
   {
      currentCollectables += collectableAmount;
      collectableSo.currentCollectable = currentCollectables;
      scoreData.pointsCollected = collectableSo.currentCollectable;
      colletableTXT.text = currentCollectables.ToString();
   }

   public void ResetCollectable()
   {
      collectableSo.currentCollectable = 0;
      colletableTXT.text = currentCollectables.ToString();
   }
}
