using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("HUD")]
    public TextMeshProUGUI dinheiroText;   // ← NOVO campo, pra arrastar o DinheiroText do Canvas

    [Header("Dinheiro")]
    public int dinheiroInicial = 1000;
    public int dinheiroAtual;
    public int displayedScore = 1000;
    public float countSpeed = 300f;

    [Header("Telas")]
    public GameObject painelCard;
    public GameObject painelDerrota;
    public GameObject painelVitória;

    [Header("Áudio")]
    public SoundManager soundManager;

    private bool jogoTerminou = false;

    void Start()
    {
        dinheiroAtual = dinheiroInicial;
        AtualizarHUD();   // ← NOVO, já mostra o valor inicial assim que o jogo começa
    }

    void Update()
    {
        // Atualiza o score mostrado na tela, se for diferente do valor real.
        if(displayedScore != dinheiroAtual){
            //faz a transição suave do valor mostrado para o valor real, usando MoveTowards
            displayedScore = Mathf.RoundToInt(Mathf.MoveTowards(displayedScore, dinheiroAtual, countSpeed * Time.deltaTime));
            AtualizarHUD();
        }
    }

    // Chamado pelo CardManager toda vez que o jogador escolhe uma opção.
    public void AplicarResultado(Outcome resultado)
    {
        if (jogoTerminou) return; // trava, evita somar depois do jogo já ter acabado

        dinheiroAtual += resultado.deltaDinheiro;

        if (dinheiroAtual <= 0)
        {
            Vitoria();
        }
        
    }

    void Vitoria()
    {
        jogoTerminou = true;
        Debug.Log("VITÓRIA! Dinheiro chegou a " + dinheiroAtual + " (zero ou negativo).");

        painelCard.SetActive(false);
        painelVitória.SetActive(true);
        soundManager.TocarSomVitoria();
        
    }

    // Chamado pelo CardManager quando o baralho acabar sem o jogador ter vencido.
    public void BaralhoAcabou()
    {
        if (jogoTerminou) return;

        jogoTerminou = true;
        Debug.Log("Baralho acabou! Dinheiro final: " + dinheiroAtual);

        painelCard.SetActive(false);
        painelDerrota.SetActive(true);
        soundManager.TocarSomDerrota();
        // Aqui depois entra uma tela de "fim de jogo sem vitória", se fizer sentido pro seu design.
    }


    // NOVO método
    void AtualizarHUD()
    {
        dinheiroText.text = "$" + displayedScore.ToString();
    }
}