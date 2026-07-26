using UnityEngine;
using System.Collections.Generic;

public class CardManager : MonoBehaviour
{
    [Header("Baralho")]
    public List<CardData> baralho;

    [Header("Referências")]
    public CardUI cardUI;
    public GameManager gameManager;
    public SoundManager soundManager;

    void Start()
    {
        cardUI.OnContinuar += MostrarProximaCarta;
        cardUI.OnEscolha += gameManager.AplicarResultado;

        MostrarProximaCarta();
    }

    void OnDestroy()
    {
        cardUI.OnContinuar -= MostrarProximaCarta;
        cardUI.OnEscolha -= gameManager.AplicarResultado;
    }

    public void MostrarProximaCarta()
    {
        if (baralho.Count == 0)
        {
            gameManager.BaralhoAcabou();
            return;
        }
        
        //soundManager.TocarCard(); // Toca o som de embaralhar cartas 

        int index = Random.Range(0, baralho.Count);
        CardData cartaSorteada = baralho[index];
        baralho.RemoveAt(index);

        cardUI.ExibirCarta(cartaSorteada);

        
    }
}