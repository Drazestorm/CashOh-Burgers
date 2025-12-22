using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour {
    
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform CounterTopPoint;
    [SerializeField] private Transform platesVisualPrefab;

    private List<GameObject> plateVisualGameObjectList;

    private void Awake() {
        plateVisualGameObjectList = new List<GameObject>();
    }

    private void Start(){
        platesCounter.OnPlateSpawnd += PlatesCounter_OnPlateSpawnd;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateRemoved(object sender, EventArgs e) {
        GameObject plateGameObject = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
        plateVisualGameObjectList.Remove(plateGameObject);
        Destroy(plateGameObject);
    }

    private void PlatesCounter_OnPlateSpawnd(object sender, EventArgs e) {
        Transform plateVisualTransform = Instantiate(platesVisualPrefab, CounterTopPoint);

        float plateOffsetY = 0.1f;
        plateVisualTransform.localPosition = new Vector3(0f, plateOffsetY * plateVisualGameObjectList.Count, 0f);
        plateVisualGameObjectList.Add(plateVisualTransform.gameObject);

        
    }
}
