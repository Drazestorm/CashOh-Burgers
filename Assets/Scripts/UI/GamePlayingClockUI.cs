using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour {
    
    [SerializeField] private Image timerImage;

    private void Update(){
        timerImage.fillAmount = KitchenGameManager.instance.GetPlayingTimerNormalized();
    }

}
