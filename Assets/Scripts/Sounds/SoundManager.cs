using System;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    [SerializeField] private AudioClipsRefsSO audioClipsRefsSO;
    public static SoundManager instance { get; private set; }

    private void Awake() {
        instance = this;
    }

    private void Start() {
        DeliveryManager.instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickSomething += Player_OnPickSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, EventArgs e)
    {
        Transform transform = (sender as TrashCounter).transform;
        PlaySound(audioClipsRefsSO.trash, transform.position);
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender, EventArgs e)
    {
        Transform transform = (sender as BaseCounter).transform;
        PlaySound(audioClipsRefsSO.objectDrop, transform.position);
    }

    private void Player_OnPickSomething(object sender, EventArgs e)
    {
        PlaySound(audioClipsRefsSO.objectPickup, Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, EventArgs e) {
        Transform transform = (sender as CuttingCounter).transform;
        PlaySound(audioClipsRefsSO.chop, transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e) {
        Transform transform = DeliveryCounter.instance.transform;
        PlaySound(audioClipsRefsSO.deliveryFail, transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e) {
        Transform transform = DeliveryCounter.instance.transform;
        PlaySound(audioClipsRefsSO.deliverySuccess, transform.position);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f) {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f) {
        PlaySound(audioClipArray[UnityEngine.Random.Range(0, audioClipArray.Length)], position, volume);
    }

    public void PlayFootstepSound(Vector3 position, float volume = 1f) {
        PlaySound(audioClipsRefsSO.footstep, position, volume);
    }

   

}
