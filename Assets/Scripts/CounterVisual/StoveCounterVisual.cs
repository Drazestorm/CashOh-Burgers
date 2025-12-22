using System;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour {

    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject stonveOnGameObject;
    [SerializeField] private GameObject particalGameObject;

    private void Start() {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArges e) {
        bool showVisual = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        stonveOnGameObject.SetActive(showVisual);
        particalGameObject.SetActive(showVisual);
    }

}
