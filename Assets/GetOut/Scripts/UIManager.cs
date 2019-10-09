using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

    [SerializeField]
    private GameObject _winUI;
    [SerializeField]
    private GameObject _lossUI;
    [SerializeField]
    private float _stageOverDelay = 0;

    private void Awake() {
        GameLoop.onStageOver += ShowStageUICaller;
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Space) && (_winUI.gameObject.activeInHierarchy || _lossUI.gameObject.activeInHierarchy))
            Retry();
    }

    private void ShowStageUICaller(bool sucess) {
        StartCoroutine(ShowStageUI(sucess));
    }
    private IEnumerator ShowStageUI(bool sucess) {

        yield return new WaitForSeconds(_stageOverDelay);

        if (sucess) {
            _winUI.SetActive(true);
        } else {
            _lossUI.SetActive(true);
        }

    }

    public void NextStage(int id = -1) {
        if (id == -1) {
            id = SceneManager.GetActiveScene().buildIndex + 1;
        }
        SceneManager.LoadScene(id);
    }

    public void Retry() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDestroy() {

        GameLoop.onStageOver -= ShowStageUICaller;
    }
}
