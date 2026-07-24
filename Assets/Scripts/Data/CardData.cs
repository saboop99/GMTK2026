using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Cards/Card")]
public class CardData : ScriptableObject
{
    [Header("Identificação")]
    public string id;

    [Header("Layout da Carta")]
    public string titulo;
    public Sprite arteCarta;
    [TextArea(2, 4)]
    public string descricao;

    [Header("Botão: Contratar")]
    public Outcome contratar;

    [Header("Botão: Não Contratar")]
    public Outcome naoContratar;
}
