using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogo : MonoBehaviour {

    [Header("Pega o texto do dialogo e do nome do player")]
    public TextMeshProUGUI txt_Dialogo;
    public TextMeshProUGUI txt_NomePlayer;
    [Header("Desativa a caixa de dialogos")]
    public GameObject canvasDialogo; // pega o canvas da caixa de dialogo
    private int timeDialogosPlayer, contadorDialogo, somarDialogo = 0; // tempo dos dialogos
    private string[] txt_TextoJogadores = new string[10]; // pega os textos dos jogadores


    // Use this for initialization
    void Start() {
        txt_Dialogo.SetText("");
        canvasDialogo.SetActive(false);
        if (PlayerPrefs.GetInt("Personagem") == 0) {
            txt_NomePlayer.SetText("Adan");
            if (PlayerPrefs.GetInt("Idioma") == 0) {
                txt_TextoJogadores[0] = "Acho que hoje vou coletar muitos materiais.\nVou até os Containers verificar se tem algum material lá";
                txt_TextoJogadores[1] = "Peguei alguns materiais legais.";
                txt_TextoJogadores[2] = "But I must get even more materials, I will look for";
                txt_TextoJogadores[3] = "Parece que não tem mais materiais aqui.";
                txt_TextoJogadores[4] = "Acho que é melhor eu ir até a cidade, vou até o barco.";
                txt_TextoJogadores[5] = "Finalmente estou na cidade.";
                txt_TextoJogadores[6] = "Irei fazer algumas peças na maquina de reciclar.";
                txt_TextoJogadores[7] = "Essa é a minha primeira corrida.\nEu estou um pouco inseguro.";
                txt_TextoJogadores[8] = "Bom, espero que eu consiga vencer";
            } else {
                txt_TextoJogadores[0] = "I think I'm going to collect lots of materials today.\nI'll go to the Containers to check if there is any material there";
                txt_TextoJogadores[1] = "I took some cool stuff.";
                txt_TextoJogadores[2] = "But I must get even more materials, I will look for";
                txt_TextoJogadores[3] = "Looks like there are no more materials here.";
                txt_TextoJogadores[4] = "I think I'd better go into town, I'm going to the ship..";
                txt_TextoJogadores[5] = "I'm finally in town.";
                txt_TextoJogadores[6] = "I'll make some parts in the recycling machine.";
                txt_TextoJogadores[7] = "This is my first race.\nI'm a little unsure.";
                txt_TextoJogadores[8] = "Well, I hope I can win.";
            }
        } else {
            txt_NomePlayer.SetText("Selena");
            if (PlayerPrefs.GetInt("Idioma") == 0)
            {
                txt_TextoJogadores[0] = "Certo, vamos andar pela ilha, aposto que deve ter altos materiais legais hoje. Vou construir um lindo carrinho, o campeonato que me espere.";
                txt_TextoJogadores[1] = "Huuun... Coletei alguns itens bem interessante aqui.";
                txt_TextoJogadores[2] = "Estou certa que ainda tem mais Containers com itens, então vou procurar eles, vamos nessa!";
                txt_TextoJogadores[3] = "Parece que não tem mais nenhum material nos Containers agora.";
                txt_TextoJogadores[4] = "Certo, agora vamos para a cidade, o Barco já deve está ai.";
                txt_TextoJogadores[5] = "Cheguei na cidade, agora vou até a maquina de reciclar, para criar as minhas peças.";
                txt_TextoJogadores[6] = "Ela continua linda como sempre, com aquelas cores verde e cinza.\nFico empolgada demais sempre que vejo ela.";
                txt_TextoJogadores[7] = "Vai começar, à minha primeira corrida.\nEu vou vencer, vou vencer com toda certeza.";
                txt_TextoJogadores[8] = "Estou super empolgada, que adrenalina enorme essa que eu estou sentindo.";

            }
            else
            {
                txt_TextoJogadores[0] = "Right, I go to walk island, I'm right that there have many materiais cool today. I'll go create a beautiful car, The championship that wait me.";
                txt_TextoJogadores[1] = "Huuun... I collected some very interesting items here..";
                txt_TextoJogadores[2] = "I'm sure there are still more Containers with items, so I'll go look for them, come on!";
                txt_TextoJogadores[3] = "I think that don't have materiais in Containers now.";
                txt_TextoJogadores[4] = "Right, now let's go to city, The ship must be here by now.";
                txt_TextoJogadores[5] = "I arrived in the city, now I go to the recycle machine, to create my pieces.";
                txt_TextoJogadores[6] = "She remains beautiful as ever, with those green and gray colors.\nI get too excited every time I see her.";
                txt_TextoJogadores[7] = "It's gonna start, on my first run.\nI will win, I will win with certainty.";
                txt_TextoJogadores[8] = "I'm super excited, what a huge adrenaline's I'm feeling.";
            }
        }
    }

    // serve para definir o numero de dialogos que terá
    public void numeroDialogo(int numero, int indexDialogo) {
        somarDialogo = numero;
        contadorDialogo = indexDialogo; // recebe o index do dialogo
        diago_ColetarItens(); // apos definir o numero, chama a função para aparecer os dialogos na tela
    }

    // é o primeiro dialogo do personagem apos o tutorial
    private void diago_ColetarItens() {
        canvasDialogo.SetActive(true);
        timeDialogosPlayer = 5;
        StartCoroutine("tempoDialogo");
    }

    // chama os dialogos apos um numero de tempo
    IEnumerator tempoDialogo() {
        if(somarDialogo <= contadorDialogo) {
            txt_Dialogo.SetText(txt_TextoJogadores[somarDialogo]);

        } else {

            txt_Dialogo.SetText("");
            canvasDialogo.SetActive(false);
            contadorDialogo = 0;
            somarDialogo = 0;
            StopCoroutine("tempoDialogo");
        }
        yield return new WaitForSeconds(timeDialogosPlayer);
        somarDialogo++;
        StartCoroutine("tempoDialogo");
    }
}
