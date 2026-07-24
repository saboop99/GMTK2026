using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        btnContinuar.onClick.AddListener(() => OnContinuar?.Invoke());
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

        // Mostra estado resultado
        resultadoText.gameObject.SetActive(true);
        btnContinuar.gameObject.SetActive(true);

        string sinal = resultado.deltaDinheiro >= 0 ? "+" : "";
        resultadoText.text = resultado.textoResultado + "\n" + sinal + resultado.deltaDinheiro;
        resultadoText.color = resultado.deltaDinheiro >= 0 ? Color.green : Color.red;
    }
}