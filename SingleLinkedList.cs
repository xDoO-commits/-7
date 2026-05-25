using System;
using System.Collections;
using System.Collections.Generic;

namespace SingleLinkedListLib
{
    /// Односпрямований список із вставкою після другого елементу
    public class SingleLinkedList<T> : IEnumerable<T> where T : IComparable<T>
    {
        /// голова списку
        private Node<T> _head;

        /// кількість елементів у списку
        private int _count;

        public int Count => _count;

        /// ініціалізація порожнього односпрямованого списку
        public SingleLinkedList()
        {
            _head = null;
            _count = 0;
        }

        /// додає елемент після другого вузла списку
        /// якщо елементів менше двох — додає у кінець
        public void Add(T value)
        {
            Node<T> newNode = new Node<T>(value);

            if (_head == null)
            {
                _head = newNode;
            }
            else if (_head.Next == null)
            {
                _head.Next = newNode;
            }
            else
            {
                newNode.Next = _head.Next.Next;
                _head.Next.Next = newNode;
            }

            _count++;
        }

        /// повертає елемент за його індексом
        public T this[int index]
        {
            get
            {
                ValidateIndex(index);
                return GetNodeAt(index).Value;
            }
        }

        /// видалення елементу за його порядковим номером
        public void RemoveAt(int index)
        {
            ValidateIndex(index);

            if (index == 0)
            {
                _head = _head.Next;
            }
            else
            {
                Node<T> previous = GetNodeAt(index - 1);
                previous.Next = previous.Next.Next;
            }

            _count--;
        }

        /// знаходить перший від'ємний елемент списку
        /// повертає default якщо від'ємних елементів немає
        public T FindFirstNegative()
        {
            Node<T> current = _head;

            while (current != null)
            {
                if (current.Value.CompareTo(default(T)) < 0)
                    return current.Value;

                current = current.Next;
            }

            return default(T);
        }

        /// повертає true якщо у списку є хоча б один від'ємний елемент
        public bool HasNegative()
        {
            Node<T> current = _head;

            while (current != null)
            {
                if (current.Value.CompareTo(default(T)) < 0)
                    return true;

                current = current.Next;
            }

            return false;
        }

        /// обчислює середнє арифметичне всіх елементів списку
        public double GetAverage()
        {
            if (_count == 0)
                throw new InvalidOperationException("Список порожній.");

            double sum = 0;
            Node<T> current = _head;

            while (current != null)
            {
                sum += Convert.ToDouble(current.Value);
                current = current.Next;
            }

            return sum / _count;
        }

        /// обчислює суму елементів, що перевищують заданий поріг
        public double SumGreaterThan(T threshold)
        {
            double sum = 0;
            Node<T> current = _head;

            while (current != null)
            {
                if (current.Value.CompareTo(threshold) > 0)
                    sum += Convert.ToDouble(current.Value);

                current = current.Next;
            }

            return sum;
        }

        /// повертає новий список із елементів, що перевищують заданий поріг
        public SingleLinkedList<T> GetElementsGreaterThan(T threshold)
        {
            SingleLinkedList<T> result = new SingleLinkedList<T>();
            Node<T> current = _head;

            while (current != null)
            {
                if (current.Value.CompareTo(threshold) > 0)
                    result.AppendToEnd(current.Value);

                current = current.Next;
            }

            return result;
        }

        /// видаляє з поточного списку всі від'ємні елементи
        public void RemoveAllNegatives()
        {
            while (_head != null && _head.Value.CompareTo(default(T)) < 0)
            {
                _head = _head.Next;
                _count--;
            }

            if (_head == null)
                return;

            Node<T> current = _head;

            while (current.Next != null)
            {
                if (current.Next.Value.CompareTo(default(T)) < 0)
                {
                    current.Next = current.Next.Next;
                    _count--;
                }
                else
                {
                    current = current.Next;
                }
            }
        }

        /// додає елемент у кінець списку (використовується при побудові нового списку)
        private void AppendToEnd(T value)
        {
            Node<T> newNode = new Node<T>(value);

            if (_head == null)
            {
                _head = newNode;
            }
            else
            {
                Node<T> current = _head;
                while (current.Next != null)
                    current = current.Next;
                current.Next = newNode;
            }

            _count++;
        }

        /// перевіряє коректність індексу та генерує виняток при порушенні меж
        private void ValidateIndex(int index)
        {
            if (index < 0 || index >= _count)
                throw new IndexOutOfRangeException(
                    $"Індекс {index} виходить за межі списку (розмір: {_count}).");
        }

        /// повертає вузол за вказаним індексом
        private Node<T> GetNodeAt(int index)
        {
            Node<T> current = _head;

            for (int i = 0; i < index; i++)
                current = current.Next;

            return current;
        }

        /// повертає перелічувач для ітерації по елементах списку
        /// забезпечує підтримку оператора foreach
        public IEnumerator<T> GetEnumerator()
        {
            Node<T> current = _head;

            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
