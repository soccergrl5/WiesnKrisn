namespace WiesnKrisn
{
    public class InputBlock
    {
        public static InputBlock Instance = new InputBlock();
        
        private InputBlock(){}
        
        private bool _blocked = false;
        private bool _paused = false;
        private bool _textbox = false;
        
        public void BlockInput() => _blocked = true;
        public void UnBlockInput() => _blocked = false;
        public bool IsBlocked() => _blocked;
        
        public void OnPause() => _paused = true;
        public void OnResume() => _paused = false;
        public bool IsPaused() => _paused;
        
        public void TextboxShown() => _textbox = true;
        public void TextboxHidden() => _textbox = false;
        public bool IsTextbox() => _textbox;
    }
}