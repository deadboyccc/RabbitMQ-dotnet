namespace a;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        var kb = new Keyboard("Logitech");
        kb.KeyPress += kb.keyPressListener;
        kb.KeyPress += kb.KeyBroke;
        kb.onPress();
        kb.onPress();
        Console.WriteLine("--------------------------");
        kb.KeyPress -= kb.keyPressListener;
        kb.onPress();
        kb.onPress();
        Console.WriteLine("--------------------------");
        // so basically delegate define the structure of the function that can attach to the event
        // the event is like a list of function pointers that matches the delegate sig, these functions being pointed to fire when the event happens aka get called 
        kb.onPress();
    }

    class Keyboard
    {
        public string? madeIn
        {
            get; set;
        }
        public Keyboard(string man)
        {
            madeIn = man;
        }
        // defining a delete which is a function pointer - it will be pointing to funcitons that handle key presses
        public delegate void KeyPressHandler(object sender, EventArgs e);
        // the event itself: which is kindaaa like a list that has many implementation of the KeyPressHandler signature that gets triggered when the event is called with 


        // initiate the event of type KeyPressHandler | event being called key press, and will be attached to delegates of the above sig
        public event KeyPressHandler? KeyPress;
        // create the method that matches that triggeres the event

        public void onPress()
        {
            if (KeyPress == null) return;
            KeyPress(this, EventArgs.Empty);
            var eventargs = new EventArgs();
            // trigger the event with the current object and EventArgs instance
            KeyPress(this, eventargs);
        }
        public void keyPressListener(object sender, EventArgs e)
        {
            Console.WriteLine("Key Pressed");
        }
        public void KeyBroke(object sender, EventArgs e)
        {
            Console.WriteLine("Key Broke");
        }
    }
}
