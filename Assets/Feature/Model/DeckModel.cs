using UnityEngine;

[CreateAssetMenu(fileName = nameof(DeckModel), menuName = "Solitaire/" + nameof(DeckModel))]
public class DeckModel : ScriptableObject
{
    [SerializeField] [Min(1)] private int _suitsCount = 4;
    [SerializeField] [Min(1)] private int _ranksCount = 13;

    public int SuitsCount => _suitsCount;
    public int RanksCount => _ranksCount;
    public int CardCount => _suitsCount * _ranksCount;
}
