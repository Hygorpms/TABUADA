namespace TabuAda2._0;

public partial class MainPage : ContentPage
{
	
	public MainPage()
	{
		InitializeComponent();
	}

    private async void Jogar_Clicked(object sender, EventArgs e)
    {
		 await Navigation.PushAsync(new Game());
    }
}


