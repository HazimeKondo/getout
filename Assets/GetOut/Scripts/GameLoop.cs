using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLoop : MonoBehaviour {
    //    Criar um script usando Co Routines da Unity que lide com o GameLoop do jogo.

    //Este Gameloop tem pelo menos 4 estágios:
    //Esperando Onibus // Move onibus > abaixa rampa
    //Efeito de Entrada // Entra onibus + tira teto + troca camera
    //Contando para sair do Onibus
    //Efeito de Saída // Sai onibus, coloca teto + troca camera

    //O jogo acontece durante o "Contando para sair do Onibus". 
    //Nessa etapa um temporizador é usado.Se o jogador nunca chegar 
    //a sair do onibus dentro desse tempo cria-se a situação de gameover

    public enum GamePhase {
        None = 0,
        Waiting = 1,
        Entering = 2,
        Playing = 3,
        Leaving = 4
    }
    private GamePhase _currentPhase;
    public GamePhase currentPhase {
        get {
            return _currentPhase;
        }
        private set {

            //if (_currentPhase == value) {
            //    return;
            //}
            _currentPhase = value;
            onPhaseStart(_currentPhase);
            StopAllCoroutines();
            switch (value) {

                case GamePhase.Waiting:
                    StartCoroutine(WaitingRoutine());
                    break;
                case GamePhase.Entering:
                    StartCoroutine(EnteringRoutine());
                    break;
                case GamePhase.Playing:
                    StartCoroutine(PlayingRoutine());
                    break;
                case GamePhase.Leaving:
                    StartCoroutine(LeavingRoutine());
                    break;
                case GamePhase.None:
                    ResetLoop();
                    break;
            }

        }

    }

    [SerializeField]
    private float _waitTime;
    [SerializeField]
    private float _enterTime;
    [SerializeField]
    private float _stageTime;
    [SerializeField]
    private float _leaveTime;
    [SerializeField]
    private Slider _slider;
    [SerializeField]
    private Transform _dropOffPosition;
    [SerializeField]
    private Transform _playStartPosition;
    [SerializeField]
    private float _playerSpeed;
    private GameObject _player;
    /// <summary>
    /// onStageOver(sucess)
    /// </summary>
    public static Action<bool> onStageOver;
    public static Action<GamePhase> onPhaseStart;
    public static GameLoop instance;

    private void Awake() {
        if (!instance) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
        _player = GameObject.FindGameObjectWithTag("Player");
        _currentPhase = GamePhase.None;
        Invoke("StartStage", .5f);
    }

    public void StartStage() {
        LevelManager.Instance.PrepareLevel();
        currentPhase = GamePhase.Waiting;
    }
    public void CompleteStage() {
        LevelManager.Instance.WinLevel();
        currentPhase = GamePhase.Leaving;
    }

    private IEnumerator WaitingRoutine() {
        //desativar/ ativar coisas
        for (float f = 0; f < _waitTime; f += Time.deltaTime) {

            //esperando
            yield return null;
        }
        //ativar/ reativar coisas
        currentPhase = GamePhase.Entering;
    }
    private IEnumerator EnteringRoutine() {

        // Entra onibus + tira teto + troca camera > levanta rampa
        //Vector3 p0 = _player.transform.position;
        Vector3 p = new Vector3(_playStartPosition.position.x, _player.transform.position.y, _playStartPosition.position.z);
        for (float f = 0; f < _enterTime; f += Time.deltaTime) {

            //_player.transform.position = Vector3.Lerp(p0, p, f / _enterTime);
            _player.transform.position = Vector3.MoveTowards(_player.transform.position, p, _playerSpeed * Time.deltaTime);
            yield return null;
        }
        _player.transform.position = p;
        currentPhase = GamePhase.Playing;
    }
    private IEnumerator PlayingRoutine() {
        //"fazer basicao: -Player pode sair a qualquer momento por enquanto"

        //desativar/ ativar coisas

        for (float f = 0; f < _stageTime; f += Time.deltaTime) {


            //sensacao onibus se movendo
            //jogando
            _slider.value = f / _stageTime;
            yield return null;
        }
        _slider.value = 1f;
        //ativar/ reativar coisas
        //game over
        onStageOver(false);
        currentPhase = GamePhase.None;
    }
    private IEnumerator LeavingRoutine() {
        //Vector3 p0 = _player.transform.position;
        Vector3 p = new Vector3(_dropOffPosition.position.x, _player.transform.position.y, _dropOffPosition.position.z);
        //desativar/ ativar coisas
        for (float f = 0; f < _leaveTime; f += Time.deltaTime) {

            //_player.transform.position = Vector3.Lerp(p0, p, f / _leaveTime);
            _player.transform.position = Vector3.MoveTowards(_player.transform.position, p, _playerSpeed * Time.deltaTime);

            //esperando
            yield return null;
        }
        _player.transform.position = p;

        //ativar/ reativar coisas
        //fim de jogo/ proximo nivel/ recomecar fase
        onStageOver(true);
        currentPhase = GamePhase.None;
    }


    private void ResetLoop() {
        //tirar movimentacao?, etc
    }
}
