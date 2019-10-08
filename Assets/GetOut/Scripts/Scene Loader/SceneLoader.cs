using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    private bool loadScene = false;

    [SerializeField] private int _scene;
    [SerializeField] private TextMeshProUGUI _loadingText;

    [SerializeField] private UnityEvent UnityAction;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartLoading()
    {
        // ...set the loadScene boolean to true to prevent loading a new scene more than once...
        loadScene = true;

        // ...change the instruction text to read "Loading..."
        _loadingText.text = "Loading...";

        // ...and start a coroutine that will load the desired scene.
        StartCoroutine(LoadNewScene());
    }

    // Update is called once per frame
    void Update() {

        // If the new scene has started loading...
        if (loadScene == true) {

            // ...then pulse the transparency of the loading text to let the player know that the computer is still working.
            _loadingText.color = new Color(_loadingText.color.r, _loadingText.color.g, _loadingText.color.b, Mathf.PingPong(Time.time, 1));

        }

    }
    
    IEnumerator LoadNewScene() {

        UnityAction.Invoke();

        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
        AsyncOperation async = Application.LoadLevelAsync(_scene);

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!async.isDone) {
            yield return null;
        }

    }
}
