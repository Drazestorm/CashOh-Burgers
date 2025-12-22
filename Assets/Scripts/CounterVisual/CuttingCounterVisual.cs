using System;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter;
    private Animator animator;

    private const string CUT = "Cut";

    private void Awake(){
        animator = GetComponent<Animator>();

    }

    private void Start() {
        cuttingCounter.OnCut += cuttingCounter_onPlayerGrabbedObject;
    }

    private void cuttingCounter_onPlayerGrabbedObject(object sender, EventArgs e) {
        animator.SetTrigger(CUT);
    }
}
