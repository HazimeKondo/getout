using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackGuyBehaviour : MonoBehaviour {
    [Tooltip("Total rotation duration until returning to starting rotation")]
    [SerializeField]
    private float _interval = 2f;
    [SerializeField]
    [Range(0, 360)]
    private float _angle = 180f;
    [Tooltip("time% / space. ")]
    [SerializeField]
    AnimationCurve _swingCurve;
    [SerializeField]
    private bool _isClockwise = true;
    int mult = 0;
    private float t = 0;

    private void Start() {
        StartCoroutine(SwingRoutine());
        mult = _isClockwise ? 1 : -1;
    }

    private IEnumerator SwingRoutine() {
        Vector3 rot0 = transform.rotation.eulerAngles;

        while (true) {

            t += Time.deltaTime;

            float evaluation = _swingCurve.Evaluate(t % _interval);

            transform.rotation = Quaternion.Euler(new Vector3(0, evaluation * _angle, 0) * mult + rot0);


            yield return null;

        }
    }



}
