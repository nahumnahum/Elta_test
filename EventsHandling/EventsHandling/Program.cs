public class ButtonClickedEventArgs : EventArgs
{
    public string ButtonName { get; set; }

    public ButtonClickedEventArgs(string buttonName)
    {
        ButtonName = buttonName;
    }
}


public class Button
{

    public event EventHandler<ButtonClickedEventArgs> ButtonClicked;

    public void Click()
    {
        OnButtonClicked(new ButtonClickedEventArgs("MyButton"));
    }

    protected virtual void OnButtonClicked(ButtonClickedEventArgs e)
    {
        ButtonClicked?.Invoke(this, e);
    }
}


public class ButtonListener
{
    public ButtonListener(Button button)
    {
        button.ButtonClicked += Button_ButtonClicked;
    }

    private void Button_ButtonClicked(object sender, ButtonClickedEventArgs e)
    {
        Console.WriteLine($"Button '{e.ButtonName}' was clicked!");
    }
}


class Program
{
    static void Main(string[] args)
    {
        Button myButton = new Button();

        ButtonListener buttonListener = new ButtonListener(myButton);

        myButton.Click();
    }
}
