using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICarroCorrida : MonoBehaviour {

    public int pontosCorrida, pontosPartida, pontosAcumulados;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ganhaPontos(int pontos) {
        pontosCorrida = pontos;
    }
}
