using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameStartsCountDownUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI countDownText;

    private void Start() {
        KitchenGameManager.instance.OnStateChanged += KitchenGameManager_OnStateChanged;
        Hide();
    }

    private void KitchenGameManager_OnStateChanged(object sender, EventArgs e) {
        if (KitchenGameManager.instance.IscCountdownToStartActive()) {
            Show();
        } else {
            Hide();
        }
    }

    private void Update() {
        countDownText.text = Math.Ceiling(KitchenGameManager.instance.GetCountDownToStartTimer()).ToString();
    }

    private void Show(){
        gameObject.SetActive(true);
    }

    private void Hide(){
        gameObject.SetActive(false);
    }
}
