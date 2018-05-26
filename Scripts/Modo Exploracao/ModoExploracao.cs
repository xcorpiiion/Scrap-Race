using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ModoExploracao : MonoBehaviour {

    public float tempo = 0;
    public TextMeshProUGUI txt_Time;
    private TutorialGeral tutorialGeral;
    private GameController gameController;
    public GameObject[] containers;
    public int numeroContainerAtivo = 12;
    private Dialogo dialogo;

    // Use this for initialization
    void Start () {
        dialogo = FindObjectOfType(typeof(Dialogo)) as Dialogo;
        gameController = FindObjectOfType(typeof(GameController)) as GameController;
        tutorialGeral = FindObjectOfType(typeof(TutorialGeral)) as TutorialGeral;

        txt_Time.SetText(tempo.ToString());
        StartCoroutine("tempoModoExploracao");
    }

    IEnumerator tempoModoExploracao () {
        yield return new WaitForSeconds(1);
        tempo--;
        txt_Time.SetText(tempo.ToString());
        if(tempo <= 0) {
            for (int i = 0; i < containers.Length; i++)
            {
                if (!containers[i].activeInHierarchy)
                {
                    numeroContainerAtivo--;
                }


            }
            StopCoroutine("tempoModoExploracao");
            if (!tutorialGeral.fezTutorialTempo)
            {
                tutorialGeral.canvasTutorial.SetActive(true);
                tutorialGeral.canvasPlayer.SetActive(false);
                tutorialGeral.contadorTexto = 6;
                tutorialGeral.txt_tutorialExplicacao.SetText(tutorialGeral.txt_explicacaoExploracao[5]);
                gameController.changeState(GameState.INTERACAO);
            }
        } else {
            StartCoroutine("tempoModoExploracao");
        }
    }

    public void dialogoContainer() {
        if (numeroContainerAtivo <= 0)
        {

            dialogo.numeroDialogo(3, 4);
        }
    }
    

}
