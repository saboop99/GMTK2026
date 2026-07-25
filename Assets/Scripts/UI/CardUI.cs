using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class CardUI : MonoBehaviour
{
    [Header("Referências - Estado Pergunta")]
    public TextMeshProUGUI tituloText;
    public Image arteImage;
    public TextMeshProUGUI descricaoText;
    public Button btnContratar;
    public Button btnNaoContratar;

    [Header("Referências - Estado Resultado")]
    public TextMeshProUGUI resultadoText;
    public Button btnContinuar;

    [Header("Animação ao Continuar")]
    public Animator animator;                    // o Animator que já tem sua animação pronta
    public string nomeTrigger = "Continuar";      // nome do Trigger configurado no Animator
    public float delayAntesDaAnimacao = 0.3f;     // pequeno delay antes de disparar a animação
    public float tempoParaTrocarCarta = 0.25f;    // quanto tempo DENTRO da animação, até a carta estar "de costas"
    public float duracaoAnimacaoRestante = 0.25f; // tempo restante da animação, depois de trocar a carta

    // Evento que avisa "o jogador terminou de ver o resultado, pode seguir".
    // O CardManager vai se inscrever nesse evento pra saber a hora de
    // sortear e mostrar a próxima carta.
    public System.Action OnContinuar;

    // Evento que avisa "o jogador escolheu essa opção, aqui está o resultado".
    // O CardManager vai se inscrever nesse evento pra repassar pro GameManager
    // e somar o dinheiro no saldo total.
    public System.Action<Outcome> OnEscolha;

    private CardData cartaAtual;

    void Start()
    {
        btnContratar.onClick.AddListener(() => Escolher(true));
        btnNaoContratar.onClick.AddListener(() => Escolher(false));
        btnContinuar.onClick.AddListener(() => StartCoroutine(ContinuarComAnimacao()));
    }

    IEnumerator ContinuarComAnimacao()
    {
        // Pequeno delay antes de disparar a animação, pra não parecer abrupto
        yield return new WaitForSeconds(delayAntesDaAnimacao);

        animator.SetTrigger(nomeTrigger);

        // Espera só até o momento em que a carta está "de costas" (virada,
        // não visível pro jogador) — é aí que trocamos o conteúdo dela
        yield return new WaitForSeconds(tempoParaTrocarCarta);

        OnContinuar?.Invoke(); // avisa o CardManager: sorteia e chama ExibirCarta AGORA

        // Espera o restante da animação terminar (a carta girando de volta,
        // já mostrando o conteúdo novo)
        yield return new WaitForSeconds(duracaoAnimacaoRestante);
    }

    public void ExibirCarta(CardData carta)
    {
        cartaAtual = carta;

        tituloText.text = carta.titulo;
        descricaoText.text = carta.descricao;

        if (carta.arteCarta != null)
            arteImage.sprite = carta.arteCarta;

        // Estado pergunta visível
        descricaoText.gameObject.SetActive(true);
        btnContratar.gameObject.SetActive(true);
        btnNaoContratar.gameObject.SetActive(true);

        // Estado resultado escondido
        resultadoText.gameObject.SetActive(false);
        btnContinuar.gameObject.SetActive(false);
    }

    void Escolher(bool contratou)
    {
        Outcome resultado = contratou ? cartaAtual.contratar : cartaAtual.naoContratar;

        OnEscolha?.Invoke(resultado); // avisa quem estiver ouvindo (o CardManager)

        // Esconde estado pergunta
        btnContratar.gameObject.SetActive(false);
        btnNaoContratar.gameObject.SetActive(false);

        // Troca o texto da descrição pelo texto do resultado (a descrição
        // original da carta some, e no lugar dela aparece o desfecho)
        descricaoText.text = resultado.textoResultado;

        // Mostra estado resultado
        resultadoText.gameObject.SetActive(true);
        btnContinuar.gameObject.SetActive(true);

        string sinal = resultado.deltaDinheiro >= 0 ? "+" : "";
        resultadoText.text = sinal + resultado.deltaDinheiro;
        resultadoText.color = resultado.deltaDinheiro >= 0 ? Color.red : Color.green;
    }
}