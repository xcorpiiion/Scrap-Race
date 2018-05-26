using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MudarIdioma : MonoBehaviour {

    public Scene cena;

    private Loading loading;

    #region menu principal
    // pega os textos do menu principal
    [Header("Pega o texto do start")]
    public TextMeshProUGUI txt_start;

    [Header("Pega o texto do menu principal")]
    public TextMeshProUGUI[] txt_MenuPrincipal;

    [Header("Pega o texto do menu principal")]
    public Text[] txt_InicioGame;

    [Header("Pega o texto de aviso")]
    public TextMeshProUGUI[] txt_Aviso;

    [Header("Pega o texto dos botoes")]
    public TextMeshProUGUI[] txt_Botoes;

    [Header("Pega o texto dos textos do config")]
    public TextMeshProUGUI[] txt_ConfigNomes;

    [Header("Pega os textos do tela cheia")]
    public TextMeshProUGUI[] txt_Tela;

    [Header("Pega os textos da linguagem")]
    public TextMeshProUGUI[] txt_Idioma;

    [Header("Pega os textos da qualidade")]
    public TextMeshProUGUI[] txt_Qualidade;

    [Header("Pega os textos dos detalhes")]
    public TextMeshProUGUI[] txt_Detalhes;

    [Header("Pega os textos do antialising")]
    public TextMeshProUGUI[] txt_AntiAlising;

    [Header("Pega os textos do aplicar")]
    public TextMeshProUGUI[] txt_Aplicar;

    [Header("Pega os textos dos creditos")]
    public TextMeshProUGUI[] txt_Creditos;
    #endregion

    #region Cidade Rica
    [Space(3)]
    [Header("-----------------------------------------------------------------------------------------------------")]
    [Header("Pega os textos da cidade Rica")]
    [Header("Pega os textos do mapa mundi")]
    public TextMeshProUGUI[] txt_MapaMundi;
    [Header("Pega os textos da maquina de reciclar")]
    public TextMeshProUGUI[] txt_maquinaRecilar;
    [Header("Pega os textos do Start")]
    public TextMeshProUGUI[] txt_Start;
    [Header("Pega os textos do tutorial")]
    public TextMeshProUGUI[] txt_TutorialCidade;

    #endregion

    #region Garagem
    [Header("-----------------------------------------------------------------------------------")]
    [Space(3)]
    [Header("Texto da garagem")]
    [Header("pega o texto do botão de sair")]
    public TextMeshProUGUI txt_Sair;
    [Header("pega o texto do status do carro")]
    public TextMeshProUGUI[] txt_Status;
    [Header("pega o texto das peças do carro")]
    public TextMeshProUGUI[] txt_Peças;
    [Header("pega o texto do aviso")]
    public TextMeshProUGUI txt_AvisoGaragem;
    #endregion

    #region Ilha
    [Header("-----------------------------------------------------------------------------------")]
    [Space(3)]
    [Header("Texto da Ilha")]
    [Header("pega o texto do tempo")]
    public TextMeshProUGUI txt_TempoExploracao;
    [Header("pega o texto da coleta de item")]
    public TextMeshProUGUI txt_ColetarItem;
    [Header("pega o texto de sair da ilha")]
    public TextMeshProUGUI txt_SairIlha;
    [Header("pega o texto do container")]
    public TextMeshProUGUI txt_Container;
    [Header("pega o texto do tutorial")]
    public TextMeshProUGUI[] txt_Tutorial;

    #endregion

    #region Corridas
    [Header("-----------------------------------------------------------------------------------")]
    [Space(3)]
    [Header("Texto das Corridas")]
    [Header("pega o texto das Posicoes")]
    public TextMeshProUGUI[] txt_posicoes;
    #endregion

    #region Feedback Corrida
    [Header("-----------------------------------------------------------------------------------")]
    [Space(3)]
    [Header("Texto dos feedback das corridas")]
    [Header("pega as imagens de feedback")]
    public GameObject img_VenceuPortugues;
    public GameObject img_VenceuIngles, img_PerdeuPortugues, img_PerdeuIngles;
    [Header("pega os personagens")]
    public GameObject[] personagemVencedor;
    public GameObject[] personagemPerdedor;
    [Header("pega o texto do botao")]
    public TextMeshProUGUI txt_Botao;
    public Animator[] animacaoPersonagens;
    #endregion

    // Use this for initialization
    void Start () {
        cena = SceneManager.GetActiveScene();
        loading = FindObjectOfType(typeof(Loading)) as Loading;
        trocaIdioma();

    }
	
    public void trocaIdioma() {
        switch (cena.name)
        {
            case "MainMenu":

                if (PlayerPrefs.GetInt("Idioma") == 0)
                {
                    txt_Creditos[0].SetText("Créditos");
                    txt_Creditos[1].SetText("Programação");
                    txt_Creditos[2].SetText("Personagens");
                    txt_Creditos[3].SetText("Modelagem Carros");
                    txt_start.SetText("START para começar");
                    txt_MenuPrincipal[0].SetText("Modo História");
                    txt_MenuPrincipal[1].SetText("Créditos");
                    txt_MenuPrincipal[2].SetText("Configurações");
                    txt_MenuPrincipal[3].SetText("Sair");
                    txt_InicioGame[0].text = "Novo Jogo";
                    txt_InicioGame[1].text = "Continuar";

                    txt_Aviso[0].SetText("Dejesa apagar seu save e começar um novo jogo ?");
                    txt_Aviso[1].SetText("Sim");
                    txt_Aviso[2].SetText("Não");
                    txt_Botoes[0].SetText("Selecionar");
                    txt_Botoes[1].SetText("Voltar");
                    txt_ConfigNomes[0].SetText("Configurações");
                    txt_ConfigNomes[1].SetText("Tela Cheia");
                    txt_ConfigNomes[2].SetText("Idioma");
                    txt_ConfigNomes[3].SetText("Resolução");
                    txt_ConfigNomes[4].SetText("Qualidade");
                    txt_ConfigNomes[5].SetText("Detalhes");
                    txt_Tela[0].SetText("Ligado");
                    txt_Tela[1].SetText("Desligado");
                    txt_Idioma[0].SetText("Português");
                    txt_Idioma[1].SetText("Ingles");
                    txt_Qualidade[0].SetText("Ultra");
                    txt_Qualidade[1].SetText("Alto");
                    txt_Qualidade[2].SetText("Médio");
                    txt_Qualidade[3].SetText("Baixo");
                    txt_Detalhes[0].SetText("Ultra");
                    txt_Detalhes[1].SetText("Alto");
                    txt_Detalhes[2].SetText("Médio");
                    txt_Detalhes[3].SetText("Baixo");
                    txt_AntiAlising[0].SetText("Ligado");
                    txt_AntiAlising[1].SetText("Desligado");
                    txt_Aplicar[0].SetText("Aplicar");
                    


                }
                else
                {
                    txt_Creditos[0].SetText("Credits");
                    txt_Creditos[1].SetText("Programming");
                    txt_Creditos[2].SetText("Characters");
                    txt_Creditos[3].SetText("Car Modeling");
                    txt_start.SetText("START to begin");
                    txt_MenuPrincipal[0].SetText("Story Mode");
                    txt_MenuPrincipal[1].SetText("Credits");
                    txt_MenuPrincipal[2].SetText("Options");
                    txt_MenuPrincipal[3].SetText("Exit");
                    txt_InicioGame[0].text = "New Game";
                    txt_InicioGame[1].text = "Continue";
                    txt_Aviso[0].SetText("Do you want to delete your save and start a new game ?");
                    txt_Aviso[1].SetText("Yes");
                    txt_Aviso[2].SetText("No");
                    txt_Botoes[0].SetText("Select");
                    txt_Botoes[1].SetText("Back");
                    txt_ConfigNomes[0].SetText("Options");
                    txt_ConfigNomes[1].SetText("Full Screen");
                    txt_ConfigNomes[2].SetText("Idiom");
                    txt_ConfigNomes[3].SetText("Resolution");
                    txt_ConfigNomes[4].SetText("Quality");
                    txt_ConfigNomes[5].SetText("Details");
                    txt_Tela[0].SetText("On");
                    txt_Tela[1].SetText("Off");
                    txt_Idioma[0].SetText("Portuguese");
                    txt_Idioma[1].SetText("English");
                    txt_Qualidade[0].SetText("Ultra");
                    txt_Qualidade[1].SetText("High");
                    txt_Qualidade[2].SetText("Medium");
                    txt_Qualidade[3].SetText("Low");
                    txt_Detalhes[0].SetText("Ultra");
                    txt_Detalhes[1].SetText("High");
                    txt_Detalhes[2].SetText("Medium");
                    txt_Detalhes[3].SetText("Low");
                    txt_AntiAlising[0].SetText("On");
                    txt_AntiAlising[1].SetText("Off");
                    txt_Aplicar[0].SetText("Apply");
                    
                }
                break;

            case "Cidade Rica":
                if (PlayerPrefs.GetInt("Idioma") == 0)
                {
                    txt_MapaMundi[0].SetText("Sair");
                    txt_MapaMundi[1].SetText("Você precisa de um carro para poder ir para a corrida");
                    txt_MapaMundi[2].SetText("Sim");
                    txt_MapaMundi[3].SetText("Não");
                    txt_maquinaRecilar[0].SetText("Ferro");
                    txt_maquinaRecilar[1].SetText("Aço");
                    txt_maquinaRecilar[2].SetText("Aluminio");
                    txt_maquinaRecilar[3].SetText("Borracha");
                    txt_maquinaRecilar[4].SetText("Tabela de Itens");
                    txt_maquinaRecilar[5].SetText("Reciclar");
                    txt_maquinaRecilar[6].SetText("Sair");
                    txt_maquinaRecilar[7].SetText("Voltar");
                    txt_maquinaRecilar[8].SetText("Ferro");
                    txt_maquinaRecilar[9].SetText("Aço");
                    txt_maquinaRecilar[10].SetText("Aluminio");
                    txt_maquinaRecilar[11].SetText("Borracha");
                    txt_maquinaRecilar[12].SetText("É preciso tirar todos os itens da tabela 'Reciclar' para poder sair");
                    txt_maquinaRecilar[13].SetText("Reciclar");
                    txt_Start[0].SetText("Pause");
                    txt_Start[1].SetText("Continuar");
                    txt_Start[2].SetText("Configurações");
                    txt_Start[3].SetText("Sair");
                    txt_Start[4].SetText("Configurações");
                    txt_Start[5].SetText("Tela Cheia");
                    txt_Start[6].SetText("Idioma");
                    txt_Start[7].SetText("Resolução");
                    txt_Start[8].SetText("Qualidade");
                    txt_Start[9].SetText("Detalhes");
                    txt_Start[10].SetText("Ligado");
                    txt_Start[11].SetText("Ligado");
                    txt_Start[12].SetText("Desligado");
                    txt_Start[13].SetText("Português");
                    txt_Start[14].SetText("Ingles");
                    txt_Start[15].SetText("Ultra");
                    txt_Start[16].SetText("Alto");
                    txt_Start[17].SetText("Médio");
                    txt_Start[18].SetText("Baixo");
                    txt_Start[19].SetText("Ultra");
                    txt_Start[20].SetText("Alto");
                    txt_Start[21].SetText("Médio");
                    txt_Start[22].SetText("Baixo");
                    txt_Start[23].SetText("Ligado");
                    txt_Start[24].SetText("Desligado");
                    txt_Start[25].SetText("Deseja voltar ao menu ?");
                    txt_Start[26].SetText("Sim");
                    txt_Start[27].SetText("Não");
                    txt_TutorialCidade[0].SetText("Bem Vindo a Cidade!");
                    txt_TutorialCidade[1].SetText("Neste mapa, é onde você vai poder reciclar os itens que você coletou na Ilha e contrunstuir peças para o seu carro.");
                    txt_TutorialCidade[2].SetText("Aqui é onde você irá criar todas as peças do seu carro.");
                    txt_TutorialCidade[3].SetText("Nesse mapa todos os itens que você coletou na Ilha, estão guardados.");
                    txt_TutorialCidade[4].SetText("Esses numeros mostram a quantidade de itens que você coletou durante a sua exploração.");
                    txt_TutorialCidade[5].SetText("Nesta tabela, é onde serão colocado os itens para reciclar, onde se você clinar no simbolo de '+' ele add na tabela e se você clicar em '-' ele tira o item desta tabela de Reciclar e coloca o item novamente na tabela da direita.");
                    txt_TutorialCidade[6].SetText("Esses quadrados representam a raridade dos itens, onde cada cor representa uma raridade. Quanto melhor a raridade da peça, melhor o carro irá ficar. (Só é possivel criar peças da mesma raridade).");
                    txt_TutorialCidade[7].SetText("Normal");
                    txt_TutorialCidade[8].SetText("Raro");
                    txt_TutorialCidade[9].SetText("Lendario");
                    txt_TutorialCidade[10].SetText("Ao clicar no nome do item, você irá abrir a receita deste item, com ela, você saberá como criar os itens necessarios para ter um ótimo carro.");
                    txt_TutorialCidade[11].SetText("Comece criando um CHASSI, pois ele é o requisito minimo para poder montar o seu carro na garagem. (Caso não tenha itens o suficiente para criar, volte ao mapa de exploração e colete os itens necessarios).");
                    txt_TutorialCidade[12].SetText("Mapa Mundi");
                    txt_TutorialCidade[13].SetText("Aqui é onde você vai escolher o mapa para poder jogar as corridas.");
                    txt_TutorialCidade[14].SetText("Sempre que ganhar uma corrida, você irá liberar um novo mapa para jogar. Todo mapa jogavel é representado por essa bolinha laranja.");

                }
                else
                {
                    txt_MapaMundi[0].SetText("Exit");
                    txt_MapaMundi[1].SetText("You need a car to run the race");
                    txt_MapaMundi[2].SetText("Yes");
                    txt_MapaMundi[3].SetText("No");
                    txt_maquinaRecilar[0].SetText("Iron");
                    txt_maquinaRecilar[1].SetText("Steel");
                    txt_maquinaRecilar[2].SetText("Aluminum");
                    txt_maquinaRecilar[3].SetText("Rubber");
                    txt_maquinaRecilar[4].SetText("table items");
                    txt_maquinaRecilar[5].SetText("Recycle");
                    txt_maquinaRecilar[6].SetText("Exit");
                    txt_maquinaRecilar[7].SetText("Back");
                    txt_maquinaRecilar[8].SetText("Iron");
                    txt_maquinaRecilar[9].SetText("Steel");
                    txt_maquinaRecilar[10].SetText("Aluminum");
                    txt_maquinaRecilar[11].SetText("Rubber");
                    txt_maquinaRecilar[12].SetText("You must remove all items from the 'Recycle' table before you can exit");
                    txt_maquinaRecilar[13].SetText("Recycle");
                    txt_Start[0].SetText("Pause");
                    txt_Start[1].SetText("Continue");
                    txt_Start[2].SetText("Options");
                    txt_Start[3].SetText("Exit");
                    txt_Start[4].SetText("Options");
                    txt_Start[5].SetText("Full Sreen");
                    txt_Start[6].SetText("Idiom");
                    txt_Start[7].SetText("Resolution");
                    txt_Start[8].SetText("Quality");
                    txt_Start[9].SetText("Details");
                    txt_Start[10].SetText("Apply");
                    txt_Start[11].SetText("On");
                    txt_Start[12].SetText("Off");
                    txt_Start[13].SetText("Portuguese");
                    txt_Start[14].SetText("English");
                    txt_Start[15].SetText("Ultra");
                    txt_Start[16].SetText("High");
                    txt_Start[17].SetText("Medium");
                    txt_Start[18].SetText("Low");
                    txt_Start[19].SetText("Ultra");
                    txt_Start[20].SetText("High");
                    txt_Start[21].SetText("Medium");
                    txt_Start[22].SetText("Low");
                    txt_Start[23].SetText("On");
                    txt_Start[24].SetText("Off");
                    txt_Start[25].SetText("Do you want to go back to menu?");
                    txt_Start[26].SetText("Yes");
                    txt_Start[27].SetText("No");
                    txt_TutorialCidade[0].SetText("Welcome to the City!");
                    txt_TutorialCidade[1].SetText("On this map, it is where you will be able to recycle the items you have collected on the Island and counter parts for your car.");
                    txt_TutorialCidade[2].SetText("This is where you will create all the parts of your car.");
                    txt_TutorialCidade[3].SetText("In this map all the items you have collected on the Island are stored.");
                    txt_TutorialCidade[4].SetText("These numbers show the amount of items you collected during your exploration.");
                    txt_TutorialCidade[5].SetText("In this table, it is where the items to recycle will be placed, where if you click on the '+' symbol it adds to the table and if you click '-' it takes the item from this Recycle table and puts the item back into the table from the right.");
                    txt_TutorialCidade[6].SetText("These squares represent the rarity of items, where each color represents a rarity. The rarer the part, the better the car will stay. (It is only possible to create pieces of the same rarity).");
                    txt_TutorialCidade[7].SetText("Normal");
                    txt_TutorialCidade[8].SetText("Rare");
                    txt_TutorialCidade[9].SetText("Legendary");
                    txt_TutorialCidade[10].SetText("By clicking on the item name, you will open the recipe for this item, with it, you will know how to create the items needed to have a great car.");
                    txt_TutorialCidade[11].SetText("Start by creating a CHASSI as it is the minimum requirement to be able to mount your car in the garage. (If you do not have enough items to create, go back to the farm map and collect the items you need).");
                    txt_TutorialCidade[12].SetText("World map");
                    txt_TutorialCidade[13].SetText("Here's where you'll pick the map so you can play the races.");
                    txt_TutorialCidade[14].SetText("Whenever you win a race, you will release a new map to play. Every playable map is represented by this orange ball.");
                }
                break;
            case "Garagem":
                if (PlayerPrefs.GetInt("Idioma") == 0)
                {
                    txt_Sair.SetText("Voltar");
                    txt_Status[0].SetText("Peso");
                    txt_Status[1].SetText("Freio");
                    txt_Status[2].SetText("Velocidade");
                    txt_Peças[0].SetText("MOTOR");
                    txt_Peças[1].SetText("CHASSI");
                    txt_Peças[2].SetText("TRANSMISSÃO");
                    txt_Peças[3].SetText("PNEU");
                    txt_Peças[4].SetText("FREIO");
                    txt_AvisoGaragem.SetText("Precisa criar um CHASSI para poder adicionar as outras peças");
                }
                else
                {
                    txt_Sair.SetText("Back");
                    txt_Status[0].SetText("Weight");
                    txt_Status[1].SetText("Brake");
                    txt_Status[2].SetText("Speed");
                    txt_Peças[0].SetText("ENGINE");
                    txt_Peças[1].SetText("BODY");
                    txt_Peças[2].SetText("TRANSMISSION");
                    txt_Peças[3].SetText("WHEEL");
                    txt_Peças[4].SetText("BRAKE");
                    txt_AvisoGaragem.SetText("You need to create the BODY to add another parts");
                }
                break;
            case "Mapa Exploracao":
                if(PlayerPrefs.GetInt("Idioma") == 0) {

                    txt_Start[0].SetText("Pause");
                    txt_Start[1].SetText("Continuar");
                    txt_Start[2].SetText("Configurações");
                    txt_Start[3].SetText("Sair");
                    txt_Start[4].SetText("Configurações");
                    txt_Start[5].SetText("Tela Cheia");
                    txt_Start[6].SetText("Idioma");
                    txt_Start[7].SetText("Resolução");
                    txt_Start[8].SetText("Qualidade");
                    txt_Start[9].SetText("Detalhes");
                    txt_Start[10].SetText("Ligado");
                    txt_Start[11].SetText("Desligado");
                    txt_Start[12].SetText("Ligado");
                    txt_Start[13].SetText("Português");
                    txt_Start[14].SetText("Ingles");
                    txt_Start[15].SetText("Ultra");
                    txt_Start[16].SetText("Alto");
                    txt_Start[17].SetText("Médio");
                    txt_Start[18].SetText("Baixo");
                    txt_Start[19].SetText("Ultra");
                    txt_Start[20].SetText("Alto");
                    txt_Start[21].SetText("Médio");
                    txt_Start[22].SetText("Baixo");
                    txt_Start[23].SetText("Ligado");
                    txt_Start[24].SetText("Desligado");
                    txt_Start[25].SetText("Deseja voltar ao menu ?");
                    txt_Start[26].SetText("Sim");
                    txt_Start[27].SetText("Não");
                    txt_TempoExploracao.SetText("Tempo:");
                    txt_ColetarItem.SetText("Coletando itens...");
                    txt_SairIlha.SetText("Para sair da Ilha");
                    txt_Container.SetText("Para coletar os itens");
                    txt_Tutorial[0].SetText("Pular");
                    txt_Tutorial[1].SetText("Correr");
                    txt_Tutorial[2].SetText("Movimentar o jogador");
                    txt_Tutorial[3].SetText("Movimentar a camera");


                } else {

                    txt_Start[0].SetText("Pause");
                    txt_Start[1].SetText("Continue");
                    txt_Start[2].SetText("Options");
                    txt_Start[3].SetText("Exit");
                    txt_Start[4].SetText("Options");
                    txt_Start[5].SetText("Full Sreen");
                    txt_Start[6].SetText("Idiom");
                    txt_Start[7].SetText("Resolution");
                    txt_Start[8].SetText("Quality");
                    txt_Start[9].SetText("Details");
                    txt_Start[10].SetText("Apply");
                    txt_Start[11].SetText("On");
                    txt_Start[12].SetText("Off");
                    txt_Start[13].SetText("Portuguese");
                    txt_Start[14].SetText("English");
                    txt_Start[15].SetText("Ultra");
                    txt_Start[16].SetText("High");
                    txt_Start[17].SetText("Medium");
                    txt_Start[18].SetText("Low");
                    txt_Start[19].SetText("Ultra");
                    txt_Start[20].SetText("High");
                    txt_Start[21].SetText("Medium");
                    txt_Start[22].SetText("Low");
                    txt_Start[23].SetText("On");
                    txt_Start[24].SetText("Off");
                    txt_Start[25].SetText("Do you want to go back to menu?");
                    txt_Start[26].SetText("Yes");
                    txt_Start[27].SetText("No");
                    txt_TempoExploracao.SetText("Time:");
                    txt_ColetarItem.SetText("Collecting itens...");
                    txt_SairIlha.SetText("To leave the Island");
                    txt_Container.SetText("To collect itens");
                    txt_Tutorial[0].SetText("Jump");
                    txt_Tutorial[1].SetText("Run");
                    txt_Tutorial[2].SetText("Move the player");
                    txt_Tutorial[3].SetText("Move the camera");


                }
                break;
            case "Mapa da Ponte":
                if (PlayerPrefs.GetInt("Idioma") == 0)
                {
                    txt_posicoes[0].SetText("ST: Posição ");
                    txt_posicoes[1].SetText(": Voltas");
                    txt_posicoes[2].SetText("Velocidade:");
                    txt_posicoes[3].SetText("Acelerar");
                    txt_posicoes[4].SetText("Movimentar");
                    txt_posicoes[5].SetText("Turbo");
                    txt_posicoes[6].SetText("Freio");
                    txt_Start[0].SetText("Pause");
                    txt_Start[1].SetText("Continuar");
                    txt_Start[2].SetText("Configurações");
                    txt_Start[3].SetText("Sair");
                    txt_Start[4].SetText("Configurações");
                    txt_Start[5].SetText("Tela Cheia");
                    txt_Start[6].SetText("Idioma");
                    txt_Start[7].SetText("Resolução");
                    txt_Start[8].SetText("Qualidade");
                    txt_Start[9].SetText("Detalhes");
                    txt_Start[10].SetText("Aplicar");
                    txt_Start[11].SetText("Ligado");
                    txt_Start[12].SetText("Desligado");
                    txt_Start[13].SetText("Português");
                    txt_Start[14].SetText("Ingles");
                    txt_Start[15].SetText("Ultra");
                    txt_Start[16].SetText("Alto");
                    txt_Start[17].SetText("Médio");
                    txt_Start[18].SetText("Baixo");
                    txt_Start[19].SetText("Ultra");
                    txt_Start[20].SetText("Alto");
                    txt_Start[21].SetText("Médio");
                    txt_Start[22].SetText("Baixo");
                    txt_Start[23].SetText("Ligado");
                    txt_Start[24].SetText("Desligado");
                    txt_Start[25].SetText("Deseja voltar ao menu ?");
                    txt_Start[26].SetText("Sim");
                    txt_Start[27].SetText("Não");
                }
                else
                {
                    txt_posicoes[0].SetText("ST: Position ");
                    txt_posicoes[1].SetText(": Laps");
                    txt_posicoes[2].SetText("Speed:");
                    txt_posicoes[3].SetText("Acelerar");
                    txt_posicoes[4].SetText("Movement");
                    txt_posicoes[5].SetText("Turbo");
                    txt_posicoes[6].SetText("Brake");
                    txt_Start[0].SetText("Pause");
                    txt_Start[1].SetText("Continue");
                    txt_Start[2].SetText("Options");
                    txt_Start[3].SetText("Exit");
                    txt_Start[4].SetText("Options");
                    txt_Start[5].SetText("Full Sreen");
                    txt_Start[6].SetText("Idiom");
                    txt_Start[7].SetText("Resolution");
                    txt_Start[8].SetText("Quality");
                    txt_Start[9].SetText("Details");
                    txt_Start[10].SetText("Apply");
                    txt_Start[11].SetText("On");
                    txt_Start[12].SetText("Off");
                    txt_Start[13].SetText("Portuguese");
                    txt_Start[14].SetText("English");
                    txt_Start[15].SetText("Ultra");
                    txt_Start[16].SetText("High");
                    txt_Start[17].SetText("Medium");
                    txt_Start[18].SetText("Low");
                    txt_Start[19].SetText("Ultra");
                    txt_Start[20].SetText("High");
                    txt_Start[21].SetText("Medium");
                    txt_Start[22].SetText("Low");
                    txt_Start[23].SetText("On");
                    txt_Start[24].SetText("Off");
                    txt_Start[25].SetText("Do you want to go back to menu?");
                    txt_Start[26].SetText("Yes");
                    txt_Start[27].SetText("No");
                }
                break;
            case "Mapa Montanha":
                if (PlayerPrefs.GetInt("Idioma") == 0)
                {
                    txt_posicoes[0].SetText("ST: Posição ");
                    txt_posicoes[1].SetText(": Voltas");
                    txt_posicoes[2].SetText("Velocidade:");
                    txt_Start[0].SetText("Pause");
                    txt_Start[1].SetText("Continuar");
                    txt_Start[2].SetText("Configurações");
                    txt_Start[3].SetText("Sair");
                    txt_Start[4].SetText("Configurações");
                    txt_Start[5].SetText("Tela Cheia");
                    txt_Start[6].SetText("Idioma");
                    txt_Start[7].SetText("Resolução");
                    txt_Start[8].SetText("Qualidade");
                    txt_Start[9].SetText("Detalhes");
                    txt_Start[10].SetText("Aplicar");
                    txt_Start[11].SetText("Ligado");
                    txt_Start[12].SetText("Desligado");
                    txt_Start[13].SetText("Português");
                    txt_Start[14].SetText("Ingles");
                    txt_Start[15].SetText("Ultra");
                    txt_Start[16].SetText("Alto");
                    txt_Start[17].SetText("Médio");
                    txt_Start[18].SetText("Baixo");
                    txt_Start[19].SetText("Ultra");
                    txt_Start[20].SetText("Alto");
                    txt_Start[21].SetText("Médio");
                    txt_Start[22].SetText("Baixo");
                    txt_Start[23].SetText("Ligado");
                    txt_Start[24].SetText("Desligado");
                    txt_Start[25].SetText("Deseja voltar ao menu ?");
                    txt_Start[26].SetText("Sim");
                    txt_Start[27].SetText("Não");
                }
                else
                {
                    txt_posicoes[0].SetText("ST: Position ");
                    txt_posicoes[1].SetText(": Laps");
                    txt_posicoes[2].SetText("Speed:");
                    txt_Start[0].SetText("Pause");
                    txt_Start[1].SetText("Continue");
                    txt_Start[2].SetText("Options");
                    txt_Start[3].SetText("Exit");
                    txt_Start[4].SetText("Options");
                    txt_Start[5].SetText("Full Sreen");
                    txt_Start[6].SetText("Idiom");
                    txt_Start[7].SetText("Resolution");
                    txt_Start[8].SetText("Quality");
                    txt_Start[9].SetText("Details");
                    txt_Start[10].SetText("Apply");
                    txt_Start[11].SetText("On");
                    txt_Start[12].SetText("Off");
                    txt_Start[13].SetText("Portuguese");
                    txt_Start[14].SetText("English");
                    txt_Start[15].SetText("Ultra");
                    txt_Start[16].SetText("High");
                    txt_Start[17].SetText("Medium");
                    txt_Start[18].SetText("Low");
                    txt_Start[19].SetText("Ultra");
                    txt_Start[20].SetText("High");
                    txt_Start[21].SetText("Medium");
                    txt_Start[22].SetText("Low");
                    txt_Start[23].SetText("On");
                    txt_Start[24].SetText("Off");
                    txt_Start[25].SetText("Do you want to go back to menu?");
                    txt_Start[26].SetText("Yes");
                    txt_Start[27].SetText("No");
                }
                break;
            case "Mapa do Gelo":
                if (PlayerPrefs.GetInt("Idioma") == 0)
                {
                    txt_posicoes[0].SetText("ST: Posição ");
                    txt_posicoes[1].SetText(": Voltas");
                    txt_posicoes[2].SetText("Velocidade:");
                    txt_Start[0].SetText("Pause");
                    txt_Start[1].SetText("Continuar");
                    txt_Start[2].SetText("Configurações");
                    txt_Start[3].SetText("Sair");
                    txt_Start[4].SetText("Configurações");
                    txt_Start[5].SetText("Tela Cheia");
                    txt_Start[6].SetText("Idioma");
                    txt_Start[7].SetText("Resolução");
                    txt_Start[8].SetText("Qualidade");
                    txt_Start[9].SetText("Detalhes");
                    txt_Start[10].SetText("Aplicar");
                    txt_Start[11].SetText("Ligado");
                    txt_Start[12].SetText("Desligado");
                    txt_Start[13].SetText("Português");
                    txt_Start[14].SetText("Ingles");
                    txt_Start[15].SetText("Ultra");
                    txt_Start[16].SetText("Alto");
                    txt_Start[17].SetText("Médio");
                    txt_Start[18].SetText("Baixo");
                    txt_Start[19].SetText("Ultra");
                    txt_Start[20].SetText("Alto");
                    txt_Start[21].SetText("Médio");
                    txt_Start[22].SetText("Baixo");
                    txt_Start[23].SetText("Ligado");
                    txt_Start[24].SetText("Desligado");
                    txt_Start[25].SetText("Deseja voltar ao menu ?");
                    txt_Start[26].SetText("Sim");
                    txt_Start[27].SetText("Não");
                }
                else
                {
                    txt_posicoes[0].SetText("ST: Position ");
                    txt_posicoes[1].SetText(": Laps");
                    txt_posicoes[2].SetText("Speed:");
                    txt_Start[0].SetText("Pause");
                    txt_Start[1].SetText("Continue");
                    txt_Start[2].SetText("Options");
                    txt_Start[3].SetText("Exit");
                    txt_Start[4].SetText("Options");
                    txt_Start[5].SetText("Full Sreen");
                    txt_Start[6].SetText("Idiom");
                    txt_Start[7].SetText("Resolution");
                    txt_Start[8].SetText("Quality");
                    txt_Start[9].SetText("Details");
                    txt_Start[10].SetText("Apply");
                    txt_Start[11].SetText("On");
                    txt_Start[12].SetText("Off");
                    txt_Start[13].SetText("Portuguese");
                    txt_Start[14].SetText("English");
                    txt_Start[15].SetText("Ultra");
                    txt_Start[16].SetText("High");
                    txt_Start[17].SetText("Medium");
                    txt_Start[18].SetText("Low");
                    txt_Start[19].SetText("Ultra");
                    txt_Start[20].SetText("High");
                    txt_Start[21].SetText("Medium");
                    txt_Start[22].SetText("Low");
                    txt_Start[23].SetText("On");
                    txt_Start[24].SetText("Off");
                    txt_Start[25].SetText("Do you want to go back to menu?");
                    txt_Start[26].SetText("Yes");
                    txt_Start[27].SetText("No");
                }
                break;

            case "Feedback":
                if (PlayerPrefs.GetInt("Idioma") == 0)
                {
                    txt_Botao.SetText("Voltar");
                    if (PlayerPrefs.GetInt("ResultadoCorrida") == 0)
                    {
                        img_VenceuPortugues.SetActive(true);
                        img_VenceuIngles.SetActive(false);
                        img_PerdeuPortugues.SetActive(false);
                        img_PerdeuIngles.SetActive(false);
                        if (PlayerPrefs.GetInt("Personagem") == 0)
                        {
                            personagemVencedor[0].SetActive(true);
                            personagemPerdedor[0].SetActive(false);
                        }
                        else
                        {
                            personagemVencedor[1].SetActive(true);
                            personagemPerdedor[1].SetActive(false);
                        }
                    }
                    else
                    {
                        img_VenceuPortugues.SetActive(false);
                        img_VenceuIngles.SetActive(false);
                        img_PerdeuPortugues.SetActive(true);
                        img_PerdeuIngles.SetActive(false);
                        if (PlayerPrefs.GetInt("Personagem") == 0)
                        {
                            personagemVencedor[0].SetActive(false);
                            personagemPerdedor[0].SetActive(true);
                        }
                        else
                        {
                            personagemVencedor[1].SetActive(false);
                            personagemPerdedor[1].SetActive(true);
                        }
                    }
                    PlayerPrefs.DeleteKey("ResultadoCorrida");
                }
                else
                {
                    txt_Botao.SetText("Back");
                    if (PlayerPrefs.GetInt("ResultadoCorrida") == 0)
                    {

                        img_VenceuPortugues.SetActive(false);
                        img_VenceuIngles.SetActive(true);
                        img_PerdeuPortugues.SetActive(false);
                        img_PerdeuIngles.SetActive(false);
                        if (PlayerPrefs.GetInt("Personagem") == 0)
                        {
                            personagemVencedor[0].SetActive(true);
                            personagemPerdedor[0].SetActive(false);
                        }
                        else
                        {
                            personagemVencedor[1].SetActive(true);
                            personagemPerdedor[1].SetActive(false);
                        }
                    }
                    else
                    {
                        img_VenceuPortugues.SetActive(false);
                        img_VenceuIngles.SetActive(false);
                        img_PerdeuPortugues.SetActive(false);
                        img_PerdeuIngles.SetActive(true);
                        if (PlayerPrefs.GetInt("Personagem") == 0)
                        {
                            personagemVencedor[0].SetActive(false);
                            personagemPerdedor[0].SetActive(true);
                        }
                        else
                        {
                            personagemVencedor[1].SetActive(false);
                            personagemPerdedor[1].SetActive(true);
                        }
                    }
                    PlayerPrefs.DeleteKey("ResultadoCorrida");
                }
                break;
        }
    }


    public void btn_Voltar() {
        loading.carregarLoading("Cidade Rica");
    }

}
