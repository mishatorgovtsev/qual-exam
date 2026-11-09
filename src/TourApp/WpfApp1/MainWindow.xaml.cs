using System;
using System.Windows;
using Microsoft.Win32;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private TouristicAgency agency;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            int size;
            if (!int.TryParse(txtSize.Text, out size) || size <= 0)
            {
                MessageBox.Show("Размер массива должен быть целым числом больше нуля.",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            agency = new TouristicAgency(size);
            lstUnsorted.Items.Clear();
            lstSorted.Items.Clear();

            txtDestination.IsEnabled = true;
            txtDuration.IsEnabled = true;
            txtPrice.IsEnabled = true;
            btnAdd.IsEnabled = true;
            btnSort.IsEnabled = false;
            btnSave.IsEnabled = false;

            txtStatus.Text = "Добавлено 0 из " + size;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            string destination = txtDestination.Text.Trim();
            if (destination == "")
            {
                MessageBox.Show("Введите направление поездки.",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int duration;
            if (!int.TryParse(txtDuration.Text, out duration) || duration <= 0)
            {
                MessageBox.Show("Продолжительность должна быть целым числом больше нуля.",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            decimal price;
            if (!decimal.TryParse(txtPrice.Text, out price) || price < 0)
            {
                MessageBox.Show("Цена должна быть числом не меньше нуля.",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Tour tour = new Tour(destination, duration, price);
            agency.AddTour(tour);
            lstUnsorted.Items.Add(tour.ToString());

            txtDestination.Clear();
            txtDuration.Clear();
            txtPrice.Clear();
            txtDestination.Focus();

            txtStatus.Text = "Добавлено " + agency.Count + " из " + agency.Capacity;

            if (agency.Count == agency.Capacity)
            {
                txtDestination.IsEnabled = false;
                txtDuration.IsEnabled = false;
                txtPrice.IsEnabled = false;
                btnAdd.IsEnabled = false;
                btnSort.IsEnabled = true;
                txtStatus.Text = "Массив заполнен. Нажмите «Сортировать»";
            }
        }

        private void BtnSort_Click(object sender, RoutedEventArgs e)
        {
            agency.SortDescending();

            lstSorted.Items.Clear();
            foreach (Tour tour in agency.GetTours())
            {
                lstSorted.Items.Add(tour.ToString());
            }

            btnSave.IsEnabled = true;
            txtStatus.Text = "Массив отсортирован. Можно сохранить в файл";
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Текстовые файлы (*.txt)|*.txt";
            dialog.FileName = "tours.txt";

            if (dialog.ShowDialog() == true)
            {
                try
                {
                    agency.SaveToFile(dialog.FileName);
                    txtStatus.Text = "Сохранено: " + dialog.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Не удалось сохранить файл: " + ex.Message,
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}