using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab2;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window

{
    public MainWindow()
    {
        InitializeComponent();
        FirstNameBox.TextChanged += ValidateInput;
        LastNameBox.TextChanged += ValidateInput;
        EmailBox.TextChanged += ValidateInput;
        BirthDatePicker.SelectedDateChanged += ValidateInput;
    }

    private void ValidateInput(object sender, EventArgs e)
    {
        ProceedButton.IsEnabled =
            !string.IsNullOrWhiteSpace(FirstNameBox.Text) &&
            !string.IsNullOrWhiteSpace(LastNameBox.Text) &&
            !string.IsNullOrWhiteSpace(EmailBox.Text) &&
            BirthDatePicker.SelectedDate.HasValue;
    }

    private async void ProceedButton_Click(object sender, RoutedEventArgs e)
    {
        ProceedButton.IsEnabled = false;

        string firstName = FirstNameBox.Text;
        string lastName = LastNameBox.Text;
        string email = EmailBox.Text;
        DateTime birthDate = BirthDatePicker.SelectedDate.Value;

        if (!Person.ValidateBirthDate(birthDate) || !Person.ValidateEmail(email))
        {
            ProceedButton.IsEnabled = true;
            return;
        }

        var person = await Task.Run(() =>
        {
            return new Person(firstName, lastName, email, birthDate);
        });


        TextBlockFisrtName.Text = person.FirstName;
        TextBlockLastName.Text = person.LastName;
        TextBlockEmail.Text = person.Email;
        TextBlockBirthdayDate.Text = person.BirthDate.ToString("dd.MM.yyyy");
        TextBlockIsAdult.Text = person.IsAdult ? "Yes" : "No";
        TextBlockChineseZodiac.Text = person.ChineseSign;
        TextBlockSunSign.Text = person.SunSign;
        TextBlockIsBirthdayToday.Text = person.IsBirthday ? "Yes" : "No";
        TextBlockWishes.Text = person.IsBirthday ? "Happy birthday!" : "";


        ProceedButton.IsEnabled = true;

    }
    private void ExitApplication(object sender, RoutedEventArgs e)
    {
        Close();
    }
}