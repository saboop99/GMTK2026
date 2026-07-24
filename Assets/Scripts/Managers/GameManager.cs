using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("HUD")]
    public TextMeshProUGUI dinheiroText;   // ← NOVO campo, pra arrastar o DinheiroText do Canvas

    [Header("Dinheiro")]
    public int dinheiroInicial = 1000;
    public int dinheiroAtual;

    private bool jogoTerminou = false;

    void Start()
    {
        dinheiroAtual = dinheiroInicial;
        AtualizarHUD();   // ← NOVO, já mostra o valor inicial assim que o jogo começa
    }

    // Chamado pelo CardManager toda vez que o jogador escolhe uma opção.
    public void AplicarResultado(Outcome resultado)
    {
        if (jogoTerminou) return; // trava, evita somar depois do jogo já ter acabado

        dinheiroAtual += resultado.deltaDinheiro;
        AtualizarHUD();   // ← MUDOU (antes era só Debug.Log, agora também atualiza o texto)

        if (dinheiroAtual <= 0)
        {
            Vitoria();
        }
    }

    void Vitoria()
    {
        jogoTerminou = true;
        Debug.Log("VITÓRIA! Dinheiro chegou a " + dinheiroAtual + " (zero ou negativo).");
        // Aqui depois entra a tela de vitória, se quiser.
    }

    // Chamado pelo CardManager quando o baralho acabar sem o jogador ter vencido.
    public void BaralhoAcabou()
    {
        if (jogoTerminou) return;

        jogoTerminou = true;
        Debug.Log("Baralho acabou! Dinheiro final: " + dinheiroAtual);
        // Aqui depois entra uma tela de "fim de jogo sem vitória", se fizer sentido pro seu design.
    }


    // NOVO método
    void AtualizarHUD()
    {
        dinheiroText.text = "Dinheiro: " + dinheiroAtual;
    }
}