using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class CutsceneJogo : MonoBehaviour {

    public string[] txt_DialogosString = new string[16];
    public TextMeshProUGUI txt_caixaDialogo, txt_nomePlayer;
    public PlayableDirector adanPlayble, selenaPlayble, playbleReceber;
    public GameObject adanScene, adanAnimacao, selenaScene, selenaAnimacao, textoDialogos, playbleAdan, playbleSelena;
    private Animator animator;
    public int contadorDialogos = 0;
    private float tempoCena;

    private Loading loading;

    private Scene cena;
    private bool podeApertar = true, mostrandoCena;
    public float tempoEspera;

	// Use this for initialization
	void Start () {
        loading = FindObjectOfType(typeof(Loading)) as Loading;
        cena = SceneManager.GetActiveScene();
        if (cena.name == "Mapa Cutscene") {
            // verifica qual personagem esta ativo e coloca o texto dele na tela
            if (PlayerPrefs.GetInt("Personagem") == 0)
            {
                adanScene.SetActive(true);
                selenaScene.SetActive(false);
                txt_nomePlayer.SetText("Adan");

                if (PlayerPrefs.GetInt("Idioma") == 0)
                {
                    txt_DialogosString[0] = "Mais que dia lindo hoje está sendo.";
                    txt_caixaDialogo.SetText(txt_DialogosString[0]);
                    txt_DialogosString[1] = "Ultimamente o pessoal da cidade está jogando mais coisas fora.";
                    txt_DialogosString[2] = "É realmente uma pena eles terem que jogar todo esse material fora, eles poderiam reutilizar todos.";
                    txt_DialogosString[3] = "Bom... Se eles não usam, então eu uso no lugar deles, vou mostra como materiais que eles consideram 'INUTEIS' podem realmente ter valor.";
                    txt_DialogosString[4] = "Ultimamente os Containers tem bastante materiais de qualidade, creio que com isso eu finalmente possa montar o meu tão sonhado carro";
                    txt_DialogosString[5] = "Sabe... Eu sempre quis competir em um campeonato de corrida, mas eu sou considerado 'Pobre' para isso";
                    txt_DialogosString[6] = "Eu venho bastante para essa ilha, por que aqui, eu posso conseguir materiais reciclaveis para construir o meu carrinho";
                    txt_DialogosString[7] = "Boooom... está na hora de levantar deste chão, passei quase o dia inteiro aqui pensando em muitas coisas diferentes.";
                    // personagem faz a animação de levantar do chão
                    txt_DialogosString[8] = "Ai Ai... Como estava otimo ficar deitado.";
                    txt_DialogosString[9] = "As pessoas daquela cidade deveriam vim aqui alguma vez, esse lugar é demais";
                    txt_DialogosString[10] = "Elas tem uma maquina de reciclar mas não usam... Eu acho ela genial, pois graças a ela vou poder realizar um sonho.";
                    txt_DialogosString[11] = "Mal vejo a hora de começar a competir.";
                    txt_DialogosString[12] = "Nossa como eu ficaria feliz em ganhar aquele campeonato, imagine só a noticia:";
                    txt_DialogosString[13] = "'Garoto com materiais reciclados é o campeão mundial do campeonato de corrida.'";
                    txt_DialogosString[14] = "As pessoas dariam muito mais valor a todo esse material, assim parar de jogar tudo fora, ao inves de reciclar.";
                    txt_DialogosString[15] = "Booom... agora vamos dá uma volta pela ilha, vamos ver se algum Container tem algum material legal.";
                    // muda de mapa

                } else {
                    txt_DialogosString[0] = "What a beautiful day today is being.";
                    txt_caixaDialogo.SetText(txt_DialogosString[0]);
                    txt_DialogosString[1] = "Lately the town folks are throwing more things away.";
                    txt_DialogosString[2] = "It's really a shame they have to throw all that stuff away, they could reuse it all.";
                    txt_DialogosString[3] = "Well ... If they do not use it, then I use instead of them, I'll show you how materials they consider 'INTHE' can really have value.";
                    txt_DialogosString[4] = "Lately the Containers have enough quality materials, I believe that with this I can finally assemble my so dreamed car";
                    txt_DialogosString[5] = "You know ... I've always wanted to compete in a racing championship, but I'm considered 'Poor' for that.";
                    txt_DialogosString[6] = "I come quite to this island, why here, I can get recyclable materials to build my cart";
                    txt_DialogosString[7] = "Boooom ... it's time to get up off this floor, I spent most of the day here thinking about many different things.";
                    // personagem faz a animação de levantar do chão
                    txt_DialogosString[8] = "Oh Ai ... How great it was to lie down.";
                    txt_DialogosString[9] = "The people of that city should have come here sometime, this place is too much";
                    txt_DialogosString[10] = "They have a machine to recycle but they do not use ... I think it's great, because thanks to it I can achieve a dream.";
                    txt_DialogosString[11] = "I can not wait to start competing.";
                    txt_DialogosString[12] = "Wow how I would be happy to win that championship, just imagine the news:";
                    txt_DialogosString[13] = "'Boy with recycled materials is the world champion of the race championship.'";
                    txt_DialogosString[14] = "People would put a lot more value on all this stuff, so stop throwing everything away, instead of recycling.";
                    txt_DialogosString[15] = "Well... now let's take a look around the island, let's see if any Container has any legal stuff.";
                    // muda de mapa
                }
                playbleReceber = adanPlayble;
                animator = adanAnimacao.GetComponent<Animator>();
            } else {
                adanScene.SetActive(false);
                selenaScene.SetActive(true);
                txt_nomePlayer.SetText("Selena");
                if (PlayerPrefs.GetInt("Idioma") == 0)
                {
                    txt_DialogosString[0] = "Meu Deus, que dia mais fantastico... \nNem parece que passei horas dormindo aqui hein.";
                    txt_caixaDialogo.SetText(txt_DialogosString[0]);
                    txt_DialogosString[1] = "Nossa, esse pessoal da cidade estão jogando 'lixo' com mais frequencia aqui.";
                    txt_DialogosString[2] = "Poxa... Eu realmente adoro ir até os Containers e verificar os materiais que tem lá.\nMal sabem o que estão perdendo.";
                    txt_DialogosString[3] = "Certo... Acho que eles pensam coisas como 'AI ESSE NEGOCIO É INUTIL DEIXA EU ME LIVRAR DISSO'.\nEsses materiais são demais, nada é inutil, eles que não sabem valorizar.";
                    txt_DialogosString[4] = "Nas ultimas semanas, eles tem jogado coisas de valor fora... Boom já como eles não querem, irei usar esses materiais e construir o MEU CARRINHO DOS SONHOS.";
                    txt_DialogosString[5] = "Irei competir no campeonato mundial e irei vencer.\nAssim eles darão valor ao 'LIXO' que eles jogam fora, estou certa disso.";
                    txt_DialogosString[6] = "Eu realmente amo essa ilha, que lugar agradavel.\nAcho que deveria convencer as pessoas as virem aqui, olha esse clima de paz, fico horas aqui.";
                    txt_DialogosString[7] = "Certo certo certo... Vamos levantar, por que já dormi muito, aposto que minha cara está toda amassada de tanto dormi.";
                    // personagem faz a animação de levantar do chão
                    txt_DialogosString[8] = "Ai Ai... gente que preguiça, estava tão gostoso ficar ali, talvez outra hora eu volte.";
                    txt_DialogosString[9] = "Esse lugar é perfeito, aquele pessoal da cidade realmente não sabem o que estão perdendo.";
                    txt_DialogosString[10] = "Elas tem uma MAQUINA DE RECICLAR, mas eles quase não usam.\nDa vontade de chorar por causa disso. Com essa maquina serei capaz de contruir o meu carrinho, estou chorando de emoção.";
                    txt_DialogosString[11] = "Eles deveriam era me da uma dessa de presente, ai eu mostraria a todos a utilidade dela, mostrando que eu sou capaz de fazer coisas incriveis com materiais reciclaveis.";
                    txt_DialogosString[12] = "Imagine eu com o meu carrinho ganhando aquele campeonato. Aposto que as noticias no mundo seria:";
                    txt_DialogosString[13] = "'Garota com um carro com materiais reciclaveis, ganha a corrida e as pessoas aprendem a respeitar e dar valor ao que elas consideram 'LIXO''.";
                    txt_DialogosString[14] = "Assim eu conseguiria fazer as pessoas do mundo inteiro darem valor ao que elas tem.\nNossa eu mudaria o mundo assim, mas que sonho esse meu.";
                    txt_DialogosString[15] = "Certo agora vamos da uma pequena explorada por essa ilha maravilhosa, tomara que eu ache varios materiais.\nCerto vamos nessa!";
                    // muda de mapa

                }
                else
                {
                    txt_DialogosString[0] = "Omg, what a fantastic day ... It does not look like I've spent hours sleeping here.";
                    txt_caixaDialogo.SetText(txt_DialogosString[0]);
                    txt_DialogosString[1] = "Wow, these city folks are throwing rubbish more often here.";
                    txt_DialogosString[2] = "Wow ... I really love going to the Containers and checking out the materials they have there. Badly they know what they're missing.";
                    txt_DialogosString[3] = "Okay ... I think they think things like 'THAT'S BUSINESS IS UNUSABLE LET ME RELEASE'.\nThese materials are too much, nothing is useless, they do not know how to value.";
                    txt_DialogosString[4] = "In the last few weeks, they've been throwing things out of value ... Boom anymore as they do not want to, I'll use these materials and build MY DREAMS CAR.";
                    txt_DialogosString[5] = "I will compete in the world championship and I will win.\nSo they'll value the 'GARBAGE' they throw away, I'm sure of it.";
                    txt_DialogosString[6] = "I really love this island, what a nice place.\nI think I should convince people to come here, look at this climate of peace, I spend hours here.";
                    txt_DialogosString[7] = "Right right ... Let's get up, because I've slept a lot, I bet my face is all crushed from sleeping.";
                    // personagem faz a animação de levantar do chão
                    txt_DialogosString[8] = "Oh, what a sloth, it was so good to be there, maybe I'll come back later.";
                    txt_DialogosString[9] = "This place is perfect, those people in town really do not know what they are missing out on.";
                    txt_DialogosString[10] = "They have a RECYCLING MACHINE, but they hardly use it.\nThe desire to cry about it. With this machine I will be able to build my car, I am crying with emotion.";
                    txt_DialogosString[11] = "They were supposed to give me one of this gift, then I would show everyone her utility, showing that I am capable of doing amazing things with recyclable materials.";
                    txt_DialogosString[12] = "Imagine me with my cart winning that championship. I bet the news in the world would be:";
                    txt_DialogosString[13] = "'Girl with a car with recyclable materials, wins the race and people learn to respect and value what they consider' GARBAGE '.";
                    txt_DialogosString[14] = "That way I could make people of the whole world value what they have.\nWow, I would change the world like this, but I dream of mine.";
                    txt_DialogosString[15] = "Okay, now we're going to the little island explored by this wonderful island, I hope I find several materials.\nLet's do it!";
                    // muda de mapa
                }
                playbleReceber = selenaPlayble;
                animator = selenaAnimacao.GetComponent<Animator>();
            }

        }


	}



    private void mostraCenaExploracao() {
        playbleReceber.Play();
        mostrandoCena = true;
        tempoCena = (float) playbleReceber.duration;

        StartCoroutine("tempoCenaExploracao");
    }

    IEnumerator tempoCenaExploracao() {
        yield return new WaitForSecondsRealtime(tempoCena);
        textoDialogos.SetActive(true);
        animator.SetBool("levantou", true);
        mostrandoCena = false;
        playbleAdan.SetActive(false);
        playbleSelena.SetActive(false);
    }


    private void LateUpdate()
    {
        if (cena.name == "Mapa Cutscene") {
            if (Input.GetButton("Controle 01 X") && podeApertar && !mostrandoCena && contadorDialogos <= 15)
            {
                contadorDialogos++;
                podeApertar = false;
                if (contadorDialogos <= 15)
                {
                    txt_caixaDialogo.SetText(txt_DialogosString[contadorDialogos]);
                }
                if (contadorDialogos == 8)
                {
                    mostraCenaExploracao();
                    textoDialogos.SetActive(false);
                    animator.SetBool("levantou", true);
                }

                StartCoroutine("tempoApertar");
            } else if(contadorDialogos > 15 && !mostrandoCena) {
                mostrandoCena = true;
                loading.carregarLoading("Mapa Exploracao");
            }
        }
    }

    IEnumerator tempoApertar()
    {
        yield return new WaitForSecondsRealtime(tempoEspera);
        podeApertar = true;

    }

}
