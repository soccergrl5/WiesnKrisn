namespace WiesnKrisn.Roles
{
    public class Clue
    {
        private Witnesses _witness;
        private string _clue;

        public Clue(Witnesses witness, string clue)
        {
            _witness = witness;
            _clue    = clue;
        }
        
        public Witnesses GetWitness() => _witness;
        public string GetClue() => _clue;
    }
}