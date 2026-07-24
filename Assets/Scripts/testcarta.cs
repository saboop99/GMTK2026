using UnityEngine;

public class TesteCardData : MonoBehaviour
{
    public CardData cartaTeste; // arraste a Carta_Estagiario aqui no Inspector

    void Start()
    {
        Debug.Log("Título: " + cartaTeste.titulo);
        Debug.Log("Descrição: " + cartaTeste.descricao);
        Debug.Log("Se contratar, ganha/perde: " + cartaTeste.contratar.deltaDinheiro);
        Debug.Log("Texto do resultado: " + cartaTeste.contratar.textoResultado);
        Debug.Log("Se não contratar, ganha/perde: " + cartaTeste.naoContratar.deltaDinheiro);
    }
}