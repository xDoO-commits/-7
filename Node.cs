namespace SingleLinkedListLib
{
    /// Вузол односпрямованого списку
    internal class Node<T>
    {
        /// Значення, що зберігається у вузлі
        public T Value { get; set; }

        /// Посилання на наступний вузол
        public Node<T> Next { get; set; }

        /// Ініціалізує новий вузол із заданим значенням
        public Node(T value)
        {
            Value = value;
            Next = null;
        }
    }
}
