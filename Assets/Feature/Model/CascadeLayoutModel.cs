using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(CascadeLayoutModel), menuName = "Solitaire/" + nameof(CascadeLayoutModel))]
public class CascadeLayoutModel : ScriptableObject
{
    [SerializeField] private List<int> _columnCardCounts = new List<int>() { 3, 4, 5, 6 };

    public IReadOnlyList<int> ColumnCardCounts => _columnCardCounts;
}
