using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour {

    [Header("Responsavel pelo canvas")]
    public GameObject Painel; // pega o canvas inteiro
    public Transform barLoading; // pega a barra que terá o progresso

    public float porcentagemBar; // armazena o percentual da barra

    [Header("Pega o nome da cena")]
    public string cenaLoadingNome;

    [Header("Da auto Loading na cena")]
    public bool autoLoading;

    private Scene cena;

	// Use this for initialization
	void Start () {

        cena = SceneManager.GetActiveScene();

        if(cena.name != "Fisrt Cena Loading" && cena.name != "Escolha Personagem" && cena.name != "MainMenu") {
            SaveObject.Save();

        }

        Painel.SetActive(false); // desativa o painel

        // verifica se a cena tem o autoLoading, se tive, ela carrega direto
        if(autoLoading) {
            carregarLoading(cenaLoadingNome);
        }
	}
	
    // pega o nome da cena usando o parametro "nomeCena", e atualiza o nome da variavel que ira servi para mudar o nome da cena
	public void carregarLoading(string nomeCena) {
        cenaLoadingNome = nomeCena;
        Painel.SetActive(true);
        StartCoroutine("loadingCena");
        //Painel.SetActive(true);
    }

    // faz o loading e a barra atualiza
    IEnumerator loadingCena() {
        barLoading.localScale = new Vector3(porcentagemBar, 1, 1);

        // serve para carregar a cena que está como parametro, sincronizado com a cena atual
        AsyncOperation async = SceneManager.LoadSceneAsync(cenaLoadingNome);

        // enquanto o async não estiver concluido, ele vai ficar repetindo o is done retorna tru se terminou e false caso não tenha terminado
        while (!async.isDone) {
            porcentagemBar = async.progress; // a porcentagem bar pega o valor do async que retorna valores entre 0 e 1
            barLoading.localScale = new Vector3(porcentagemBar, 1, 1); // atualiza a barra de progresso
            yield return null; // não possuio um retorno
        }
    }

}
