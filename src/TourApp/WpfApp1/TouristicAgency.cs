using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace WpfApp1
{
    public class TouristicAgency
    {
        private Tour[] tours;
        private int count;

        public int Count
        {
            get { return count; }
        }

        public int Capacity
        {
            get { return tours.Length; }
        }

        public TouristicAgency(int size)
        {
            if (size <= 0)
                throw new ArgumentException("Размер массива должен быть больше нуля");
            tours = new Tour[size];
            count = 0;
        }

        public void AddTour(Tour tour)
        {
            if (count >= tours.Length)
                throw new InvalidOperationException("Массив уже заполнен");
            tours[count] = tour;
            count++;
        }

        public Tour[] GetTours()
        {
            Tour[] result = new Tour[count];
            Array.Copy(tours, result, count);
            return result;
        }

        // Сортировка по убыванию: сначала продолжительность, при равенстве цена
        public void SortDescending()
        {
            for (int i = 0; i < count - 1; i++)
            {
                for (int j = 0; j < count - 1 - i; j++)
                {
                    if (IsLess(tours[j], tours[j + 1]))
                    {
                        Tour temp = tours[j];
                        tours[j] = tours[j + 1];
                        tours[j + 1] = temp;
                    }
                }
            }
        }

        private static bool IsLess(Tour a, Tour b)
        {
            if (a.Duration != b.Duration)
                return a.Duration < b.Duration;
            return a.Price < b.Price;
        }

        // Формат файла: текст, одна строка на тур, поля через ";"
        public void SaveToFile(string path)
        {
            using (StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8))
            {
                writer.WriteLine("Направление;Продолжительность;Цена");
                for (int i = 0; i < count; i++)
                {
                    writer.WriteLine(tours[i].Destination + ";" +
                                     tours[i].Duration + ";" +
                                     tours[i].Price.ToString(CultureInfo.InvariantCulture));
                }
            }
        }
    }
}