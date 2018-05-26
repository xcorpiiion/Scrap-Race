using UnityEngine;
using System.Collections;
public class Otimizacao : MonoBehaviour
{
    public GameObject[] _OBJETOS;
    private GameObject JOGADOR;
    private float DistanciaDoPlayer;
    public float DistanciaMaxima = 40;
    void Start()
    {
        JOGADOR = GameObject.FindWithTag("Player");
    }
    void Update()
    {
        for (int i = 0; i < _OBJETOS.Length; i++)
        {
            DistanciaDoPlayer = Vector3.Distance(JOGADOR.transform.position, _OBJETOS[i].transform.position);
            if (DistanciaDoPlayer >= DistanciaMaxima)
            {
                _OBJETOS[i].SetActive(false);
            }
            else
            {
                _OBJETOS[i].SetActive(true);
            }
        }
    }
}