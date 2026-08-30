namespace WiesnKrisn.RollerCoaster
{
    public class WrongKeyCounter
    {
        public static WrongKeyCounter Instance = new WrongKeyCounter();
        
        private int _counter = 0;
        
        private WrongKeyCounter() { }
        
        public void IncreaseCounter()
        {
            _counter++;
            
            RollerCoasterSounds.Instance.PlayScream();
        }

        public void ResetCounter() => _counter = 0;
        
        public int GetCounter() => _counter;
    }
}