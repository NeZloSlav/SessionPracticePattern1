using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Documents;

namespace SessionPractice1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Regex emailReg = new Regex(@"^[a-zA-Zа-яА-Я0-9\.\w_]+@[a-zA-Z]+\.[a-zA-Z]"); //Формат ввода почты
        private readonly Regex phoneReg = new Regex(@"^[7-8][0-9]{10}$"); //Формат ввода телефона
        private readonly Regex pasSeriesReg = new Regex(@"^[0-9]{4}$"); //Формат ввода серии паспорта
        private readonly Regex pasNumberReg = new Regex(@"^[0-9]{6}$"); //Формат ввода номера паспорта
        private readonly Regex fioReg = new Regex(@"^[a-zA-Zа-яА-Я]{0,30}$"); //Формат ввода ФИО (текстовых полей без цифр и символов)
        private readonly Regex salaryReg = new Regex(@"^[1-9][0-9]{4,6}$"); //Формат ввода зарплаты (целое число без знаков после запятой)

        private readonly List<string> roleList = new List<string> {"Директор", "Менеджер", "Уборщик" }; //Список должностей в выпадающем списке

        public MainWindow()
        {
            InitializeComponent();

            CmbRole.ItemsSource = roleList;
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            TxtBoxLastName.Text = "";
            TxtBoxFirstName.Text = "";
            TxtBoxPatronymic.Text = "";
            TxtBoxPassportSeries.Text = "";
            TxtBoxPassportNumber.Text = "";
            DatePickerBirth.Text = "";
            TxtBoxPhone.Text = "";
            TxtBoxEmail.Text = "";
            TxtBoxSalary.Text = "";
        } //При нажатии на кнопку "Стереть" очищает все текстовые поля

        private void ButtonAdd_Click(object sender, RoutedEventArgs e) //При нажатии на кнопку "Добавить" запускает проверку, после чего показывает ошибки или добавляет сотрудника
        {
            if (string.IsNullOrEmpty(TxtBoxLastName.Text) || string.IsNullOrEmpty(TxtBoxFirstName.Text) || string.IsNullOrEmpty(TxtBoxPatronymic.Text)
                || string.IsNullOrEmpty(TxtBoxPassportSeries.Text) || string.IsNullOrEmpty(TxtBoxPassportNumber.Text) || string.IsNullOrEmpty(TxtBoxPhone.Text)
                || string.IsNullOrEmpty(TxtBoxEmail.Text) || string.IsNullOrEmpty(TxtBoxSalary.Text)) //проверка на незаполненные поля
            {
                MessageBox.Show("Пожалуйста, заполните все поля", "Отмена", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (IsFIORight(TxtBoxLastName.Text, TxtBoxFirstName.Text, TxtBoxPatronymic.Text) && IsPassportRight(TxtBoxPassportSeries.Text, TxtBoxPassportNumber.Text) && IsBirthdateRight(DatePickerBirth.SelectedDate.Value.ToString())
                && IsPhoneRight(TxtBoxPhone.Text) && IsEmailRight(TxtBoxEmail.Text) && IsSalaryRight(TxtBoxSalary.Text)) //проверка на правильность введённых данных в полях
            {
                MessageBox.Show("Сотрудник добавлен!");
            }
        }

        private bool IsPhoneRight(string phone)
        {
            if (!phoneReg.IsMatch(phone)) 
            {
                MessageBox.Show("Пожалуйста, введите корректный номер телефона\nФормат ввода: 8xxxxxxxxxx", "Отмена", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        } // Проверка на совпадение введённых данных с форматом ввода номера телефона

        private bool IsEmailRight(string email)
        {
            if (!emailReg.IsMatch(email))
            {
                MessageBox.Show("Пожалуйста, введите корректный адрес эл. почты\nФормат ввода: x@x.x", "Отмена", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        } // Проверка на совпадение введённых данных с форматом ввода почты

        private bool IsPassportRight(string series, string number)
        {
            if (!pasSeriesReg.IsMatch(series))
            {
                MessageBox.Show("Пожалуйста, введите корректную серию паспорта\nФормат ввода: xxxx", "Отмена", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (!pasNumberReg.IsMatch(number))
            {
                MessageBox.Show("Пожалуйста, введите корректный номер паспорта\nФормат ввода: xxxxxx", "Отмена", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        } // Проверка на совпадение введённых данных с форматом ввода серии и номера паспорта

        private bool IsFIORight(string LName, string FName, string Patr)
        {
            if (!fioReg.IsMatch(LName) || !fioReg.IsMatch(FName) || !fioReg.IsMatch(Patr))
            {
                MessageBox.Show("Пожалуйста, введите корректное ФИО сотрудника", "Отмена", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        } // Проверка на совпадение введённых данных с форматом ввода фамилии, имени и отчества (текстовых полей)

        private bool IsSalaryRight(string salary)
        {
            if (!salaryReg.IsMatch(salary))
            {
                MessageBox.Show("Пожалуйста, введите корректную зарплату сотрудника", "Отмена", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        } // Проверка на совпадение введённых данных с форматом ввода зарплаты

        private bool IsBirthdateRight(string birthdate) //Проверка на введённый возраст (>=16)
        {
            DateTime birth = DateTime.Parse(birthdate);

            if (DateTime.Today.Year - birth.Year < 16)
            {
                MessageBox.Show("Нельзя добавить сотрудника, возраст которого меньше 16 лет.", "Отмена", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }
    }
}
