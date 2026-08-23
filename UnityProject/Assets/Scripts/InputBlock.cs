namespace WiesnKrisn
{
    public class InputBlock
    {
        public static InputBlock Instance = new InputBlock();
        
        private bool _blocked = false;
        private bool _paused = false;
        
        public void BlockInput() => _blocked = true;
        public void UnBlockInput() => _blocked = false;
        public bool IsBlocked() => _blocked;
        
        public void OnPause() => _paused = true;
        public void OnResume() => _paused = false;
        public bool IsPaused() => _paused;
    }
}