using System;
using SingleLinkedListLib;

namespace SingleLinkedListApp
{
    internal class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            DemonstrateFloatList();
        }

        /// операції над списком дійсних чисел
        private static void DemonstrateFloatList()
        {
            // створення списку та додавання елементів після другого вузла
            SingleLinkedList<float> list = new SingleLinkedList<float>();
            float[] initialValues = { 1.0f, 2.0f, 3.5f, -4.0f, 5.5f, -1.5f, 8.0f, -3.0f, 6.0f, 2.5f };

            Console.WriteLine("Додавання елементів після другого вузла списку");
            foreach (float value in initialValues)
                list.Add(value);

            // виведення через foreach
            Console.WriteLine("\nСписок:");
            PrintList(list);
            Console.WriteLine($"Кількість елементів: {list.Count}");

            // індексація (читання за індексом)
            Console.WriteLine("\nЧитання за індексом:");
            Console.WriteLine($"list[0] = {list[0]}");
            Console.WriteLine($"list[4] = {list[4]}");
            Console.WriteLine($"list[9] = {list[9]}");

            // операція 1: перший від'ємний елемент списку
            float firstNegative = list.FindFirstNegative();
            Console.WriteLine($"\nПерший від'ємний елемент списку:");
            Console.WriteLine(list.HasNegative()
                ? $"Знайдено: {firstNegative}"
                : "Від'ємних елементів немає");

            // операція 2: сума елементів більших за середнє значення
            double average = list.GetAverage();
            double sumAboveAverage = list.SumGreaterThan((float)average);
            Console.WriteLine($"\nСума елементів більших за середнє значення:");
            Console.WriteLine($"Середнє = {average:F4}");
            Console.WriteLine($"Сума елементів > {average:F4}: {sumAboveAverage:F4}");

            // операція 3: новий список зі значень позитивних елементів
            float positiveThreshold = 0.0f;
            SingleLinkedList<float> positives = list.GetElementsGreaterThan(positiveThreshold);
            Console.WriteLine($"\nПозитивні елементи (більші за {positiveThreshold}):");
            Console.Write("Новий список: ");
            PrintList(positives);

            // видалення елементу за номером
            int removeIndex = 2;
            Console.WriteLine($"\nВидалення елементу за індексом [{removeIndex}]" +
                              $" (значення: {list[removeIndex]}):");
            list.RemoveAt(removeIndex);
            Console.Write("Список після видалення: ");
            PrintList(list);

            // операція 4: видалення всіх від'ємних елементів
            Console.WriteLine("\nВидалення всіх від'ємних елементів:");
            list.RemoveAllNegatives();
            Console.Write("Список після видалення: ");
            PrintList(list);
        }

        /// виводить елементи списку у консоль
        private static void PrintList<T>(SingleLinkedList<T> list) where T : IComparable<T>
        {
            Console.Write("[ ");
            bool first = true;
            foreach (T item in list)
            {
                if (!first) Console.Write(", ");
                Console.Write(item);
                first = false;
            }
            Console.WriteLine(" ]");
        }
    }
}
