using UnityEngine;

[System.Serializable]
public class Outcome
{
    [TextArea(2, 4)]
    public string textoResultado;
    public int deltaDinheiro;

    [Header("Opcional - adicionar depois")]
    public Sprite arteResultado;   // pode ficar null por enquanto, sem problema
}
