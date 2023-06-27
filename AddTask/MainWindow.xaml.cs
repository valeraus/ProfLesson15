using System.Threading.Tasks;
using System.Windows;

namespace AddTask
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            int number = int.Parse(NumberTextBox.Text);

            // Виклик асинхронного методу Addition з використанням Task.Run
            int result = await Task.Run(() => Addition(number, number)).ConfigureAwait(false);

            // Оновлення елементів керування у потоці власника
            await Dispatcher.InvokeAsync(() => NumberTextBox.Text = result.ToString());

        }
        private int Addition(int num1, int num2)
        {
            // Додавання двох чисел
            return num1 + num2;
        }
    }
}
