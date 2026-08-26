using System.Collections.Generic;
using UnityEngine;
using WiesnKrisn.UI;

namespace WiesnKrisn.Roles
{
    public class CluesManager : MonoBehaviour
    {
        public static CluesManager Instance {get; private set;}
        
        private List<Clue>[] _clues = new []
        {
            new List<Clue>(),
            new List<Clue>(),
            new List<Clue>(),
            new List<Clue>(),
            new List<Clue>(),
            new List<Clue>(),
            new List<Clue>(),
            new List<Clue>(),
            new List<Clue>(),
            new List<Clue>()
        };

        private bool _resetBook;

        private void Awake()
        {
            Instance = this;
        }

        public void AddClue(int category, Witnesses witness, string clue)
        {
            if (witness == Witnesses.KarussellParents)
                witness = Witnesses.KarussellKid;
            
            if (witness == Witnesses.SaufiGroup2)
                witness = Witnesses.SaufiGroup;
            
            if (witness == Witnesses.AperoliGroup2)
                witness = Witnesses.AperoliGroup;

            foreach (Clue collected in _clues[category])
                if (witness == collected.GetWitness())
                    return;
            
            _clues[category].Add(new Clue(witness, clue));
            DetectiveBookUI.Instance.AddClue(witness, clue, category, _clues[category].Count - 1);
        }

        public void FillUpDetectiveBook()
        {
            if (_resetBook)
            {
                _resetBook = false;
                DetectiveBookUI.Instance.ResetColors();
            }
            
            for (int i = 0; i < _clues.Length; i++)
            {
                for (int j = 0; j < _clues[i].Count; j++)
                {
                    Witnesses witness = _clues[i][j].GetWitness();
                    string clue       = _clues[i][j].GetClue();
                    
                    DetectiveBookUI.Instance.AddClue(witness, clue, i, j);
                }
            }
        }

        public void ResetManager()
        {
            foreach (List<Clue> clue in _clues)
            {
                clue.Clear();
            }
        }
        
        public void ResetBook() => _resetBook = true;
    }
}