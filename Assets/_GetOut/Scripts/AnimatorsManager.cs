using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorsManager : MonoBehaviour
{
    [SerializeField]
    private Animator _busAnimator;
    [SerializeField]
    private Animator _playerAnimator;
    [SerializeField]
    private Animator _platformAnimator;
    [SerializeField]
    private Animator _cameraAnimator;



    private void Awake() {

        GameLoop.onPhaseStart += SetPhaseAnimations;

    }


    private void SetPhaseAnimations(GameLoop.GamePhase phase) {
        switch (phase) {
            case GameLoop.GamePhase.None:
                break;
            case GameLoop.GamePhase.Waiting:
                _busAnimator.SetTrigger("arrive");
                break;
            case GameLoop.GamePhase.Entering:
                _playerAnimator.SetTrigger("enter");
                _busAnimator.SetBool("playerInside",true);
                _cameraAnimator.SetBool("onBus",true);
                break;
            case GameLoop.GamePhase.Playing:
                //movimento?
                break;
            case GameLoop.GamePhase.Leaving:
                _busAnimator.SetTrigger("leave");
                _busAnimator.SetBool("playerInside", false);
                //_busAnimator.SetTrigger("leave");
                _cameraAnimator.SetBool("onBus", false);
                break;
        }
    }
    private void OnDestroy() {

        GameLoop.onPhaseStart -= SetPhaseAnimations;
    }
}
