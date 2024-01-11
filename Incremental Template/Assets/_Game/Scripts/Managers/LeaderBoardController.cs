using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using SincappStudio;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class LeaderBoardController : MonoBehaviour
{
    [HideInInspector] public int LastPlayerOrder;
    [SerializeField] private Transform _contentHolder;
    [SerializeField] private TextMeshProUGUI _playerText;
    private List<TextMeshProUGUI> _userTexts = new List<TextMeshProUGUI>();
    private Animator _animator;
    #region Names

    private List<string> _names = new List<string>()
    {
        "Mahesh", "Jeff", "Dave", "Allen O'neill",
        "Monica", "Henry", "Raj Kumar", "Mark",
        "Rose Tracey", "Mike", "Harry", "Ross",
        "Bruce", "Cook",
        "Carolyn", "Morgan",
        "Albert", "Walker",
        "Randy", "Reed",
        "Larry", "Barnes",
        "Lois", "Wilson",
        "Jesse", "Campbell",
        "Ernest", "Rogers",
        "Theresa", "Patterson",
        "Henry", "Simmons",
        "Michelle", "Perry",
        "Frank", "Butler",
        "Shirley", "Brooks",
        "Rachel", "Edwards",
        "Christopher", "Perez",
        "Thomas", "Baker",
        "Sara", "Moore",
        "Chris", "Bailey",
        "Roger", "Johnson",
        "Marilyn", "Thompson",
        "Anthony", "Evans",
        "Julie", "Hall",
        "Paula", "Phillips",
        "Annie", "Hernandez",
        "Dorothy", "Murphy",
        "Alice", "Howard", "Ruth", "Jackson",
        "Debra", "Allen",
        "Gerald", "Harris",
        "Raymond", "Carter",
        "Jacqueline", "Torres",
        "Joseph", "Nelson",
        "Carlos", "Sanchez",
        "Ralph", "Clark",
        "Jean", "Alexander",
        "Stephen", "Roberts",
        "Eric", "Long",
        "Amanda", "Scott",
        "Teresa", "Diaz",
    };

    #endregion

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void SetLeaderNames()
    {
        PersistData persistData = PersistData.Instance;

        _userTexts = _contentHolder.GetComponentsInChildren<TextMeshProUGUI>(true).ToList();

        for (int i = 0; i < _userTexts.Count; i++)
        {
            var randomNameIndex = Random.Range(0, _names.Count);

            if (persistData.CurrentPlayerRank < 3) // Player First Place
            {
                _userTexts[i].SetText($"#{2 + i} {_names[randomNameIndex]}");
            }
            else
            {
                _userTexts[i].SetText($"#{PersistData.Instance.CurrentPlayerRank - 2 + i} {_names[randomNameIndex]}");
            }

            _names.RemoveAt(randomNameIndex);
        }
    }

    public void PlayLeaderAnim()
    {
        PersistData persistData = PersistData.Instance;
        SetLeaderNames();
        
        if (persistData.CurrentPlayerRank < 3) // Player First Place
        {
            _playerText.SetText($"#1 PLAYER");
            _animator.SetBool("isFirstPlace", true);
        }
        else
        {
            _playerText.SetText($"#{LastPlayerOrder} PLAYER");
            _animator.SetBool("isFirstPlace", false);
        }

        _animator.Play("ScrollLeaderBoard");
    }

    public void TweenPlayerOrder()
    {
        PersistData persistData = PersistData.Instance;

        if (persistData.CurrentPlayerRank < 3) return;

        DOTween.To(() => LastPlayerOrder, x => LastPlayerOrder = x, PersistData.Instance.CurrentPlayerRank,
            1.5f).OnUpdate(() =>
        {
            _playerText.SetText($"#{LastPlayerOrder} PLAYER");
        });
    }
}