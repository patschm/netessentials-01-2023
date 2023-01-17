namespace Calculator;

public partial class CalculatorApp : Form
{
    public CalculatorApp()
    {
        InitializeComponent();
    }

    private async void button1_Click(object sender, EventArgs e)
    {
        //var mainThread = SynchronizationContext.Current;

        var strA = txtA.Text;
        var strB = txtB.Text;

        if (int.TryParse(strA, out int ia) && int.TryParse(strB, out int ib))
        {
            var result = await LongAddAsync(ia, ib);
            UpdateAnswer(result);

            //Task.Run(()=>LongAdd(ia, ib))
            //    .ContinueWith(async t =>
            //    {
            //        mainThread.Post(UpdateAnswer, t.Result);
            //        UpdateAnswer(t.Result);
            //    });    

            //var result = LongAdd(ia, ib); 
            //UpdateAnswer(result);  
        }     
    }

    private void UpdateAnswer(object result)
    {
        lblAnswer.Text = result.ToString();
    }

    private int LongAdd(int a, int b)
    {
        Task.Delay(10000).Wait();
        return a + b;
    }
    private Task<int> LongAddAsync(int a, int b)
    {
        return Task.Run(() => LongAdd(a, b));
    }
}