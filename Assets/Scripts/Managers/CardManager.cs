using UnityEngine;
using System.Collections.Generic;

public class CardManager : MonoBehaviour
{
    [Header("Baralho")]
    public List<CardData> baralho;

    [Header("Referência à UI")]
    public CardUI cardUI;

    void Start()
    {
        // Se inscreve no evento do CardUI: quando o jogador clicar em
        // "Continuar", o método MostrarProximaCarta vai rodar.
        cardUI.OnContinuar += MostrarProximaCarta;

        MostrarProximaCarta();
    }

    void OnDestroy()
    {
        // Boa prática: remove a inscrição do evento quando esse objeto
        // for destruído, evitando erros/memory leak.
        cardUI.OnContinuar -= MostrarProximaCarta;
    }

    public void MostrarProximaCarta()
    {
        if (baralho.Count == 0)
        {
            Debug.Log("Baralho acabou! Fim de jogo (por enquanto, só um aviso no Console).");
            return;
        }

        int index = Random.Range(0, baralho.Count);
        CardData cartaSorteada = baralho[index];
        baralho.RemoveAt(index); // remove pra não repetir a mesma carta

        cardUI.ExibirCarta(cartaSorteada);
    }
}