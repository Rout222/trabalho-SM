using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovimento : MonoBehaviour {

    public GameObject Mario;
    public GameObject PosicaoMinimo;
    public GameObject PosicaoMaximo;

    // Update is called once per frame
    void Update () {
        Vector3 pos = transform.position;
        Vector3 minimo = PosicaoMinimo.transform.position;
        Vector3 maximo = PosicaoMaximo.transform.position;

        pos.x = Mathf.Clamp(Mario.transform.position.x, minimo.x + Camera.main.orthographicSize * Camera.main.aspect, maximo.x - Camera.main.orthographicSize * Camera.main.aspect);
        pos.y = Mathf.Clamp(Mario.transform.position.y, minimo.y + Camera.main.orthographicSize, maximo.y - Camera.main.orthographicSize);

        transform.position = pos;
    }
}
