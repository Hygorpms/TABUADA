using System.Diagnostics;
using System.Linq;
using System.Text;

namespace TabuAda2._0;

public partial class Game : ContentPage
{
    int pontuacao = 0;

    private Stopwatch stopwatch = new Stopwatch();

    private bool isRunning = true;

    public Game()
    {
        InitializeComponent();

    }

    private string currentInput = string.Empty;
    private double result = 0;

    private void OnNumberClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        currentInput += button.Text;
        UpdateDisplay();
    }

    private void OnOperationClicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        double number;
        if (double.TryParse(currentInput, out number))
        {
            switch (button.Text)
            {
                case "+":
                    result += number;
                    break;
                case "-":
                    result -= number;
                    break;
            }
            currentInput = string.Empty;
            UpdateDisplay();
        }
    }

    private void OnEqualClicked(object sender, EventArgs e)
    {
        double number;
        if (double.TryParse(currentInput, out number))
        {
            result += number;
            currentInput = result.ToString();
            result = 0;
            UpdateDisplay();
        }
    }

    private void UpdateDisplay()
    {
        CalculatorDisplay.Text = currentInput;
    }

    private async void confirmar_Clicked(object sender, EventArgs e)
    {



        if (calculo.Text.Split("x").Count() > 1)
        {
            var valorinserido = Int32.Parse(CalculatorDisplay.Text);

            var resultadoesperado = calculo.Text.Split("x").ToList().ConvertAll(e => Int32.Parse(e.Trim())).Aggregate((acc,num) => (acc)*(num));

            if (valorinserido == resultadoesperado)
            {
                pontuacao += 10;
                LBPONTUACAO.Text = pontuacao.ToString();

            }
            else if (valorinserido == null)
            {
            }

            else
            {
                pontuacao = 0;
                LBPONTUACAO.Text = "000";

                stopwatch.Reset();
                TEMPO.Text = "00:00";

                calculo.Text = "PERDEU";

            }

        }
        else
        {
            // Iniciar o cronômetro
            stopwatch.Start();
            Device.StartTimer(TimeSpan.FromMilliseconds(100), UpdateElapsedTime); // Atualiza a cada 100 milissegundos
            confirmar.Text = "CONFIRMAR";

        }
        Random rnd = new Random();
        int v1 = rnd.Next(1, 10);
        int v2 = rnd.Next(1, 10);
        int total = v1 * v2;
        CalculatorDisplay.Text = string.Empty;
        currentInput = string.Empty;

        calculo.Text = v1 + " x " + v2 ;


        //string texto = await DisplayPromptAsync("Pergunta",
        //    "Quanto é " + v1 + " x " + v2 + "?", "Responder");

        //try
        //{
        //    int resp = Convert.ToInt32(texto);
        //    if (resp == total)
        //    {
        //        pontuacao += 10;
        //        LBPONTUACAO.Text = pontuacao.ToString();

        //    }
        //    else if (texto == null)
        //    {
        //    }

        //    else
        //    {
        //        pontuacao = 0;
        //        LBPONTUACAO.Text = "000";

        //        stopwatch.Reset();
        //        TEMPO.Text = "00:00";

        //    }
        //}
        //catch (Exception ex)
        //{
        //}
    }
    private bool UpdateElapsedTime()


    {
        if (isRunning)
        {
            // Atualizar o texto do botão com o tempo decorrido
            TimeSpan elapsed = stopwatch.Elapsed;
            TEMPO.Text = $"{elapsed.ToString(@"mm\:ss")}"; // Exibe minutos, segundos e milissegundos  
        }


        return isRunning;
    }

    private async void zerar_Clicked(object sender, EventArgs e)
    {
        bool resp = await DisplayAlert("SCORE", "QUER ZERAR O SCORE?", "SIM", "NÃO");
        if (resp == true)
        {
            pontuacao = 0;
            LBPONTUACAO.Text = "000";

            stopwatch.Reset();
            TEMPO.Text = "00:00";
        }
    }

    private async void home_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

        private void OnBackspaceClicked(object sender, EventArgs e)
    {
        if (currentInput.Length > 0)
        {
          currentInput=  currentInput.Remove(currentInput.Length - 1, 1);

            CalculatorDisplay.Text = currentInput;
            // You can display or process the updated input here
        }
    }
}

