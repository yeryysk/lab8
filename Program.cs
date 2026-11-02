using System;

namespace Rozumatic_ASD_Lab8
{
    // ==========================================
    // БАЗОВА ЧАСТИНА: СТЕК НА БАЗІ МАСИВУ
    // (Код зі скріншотів, зібраний докупи)
    // ==========================================
    
    public class ArrayStack<T>
    {
        private T[] _items;
        private int _top = -1;

        public int Count => _top + 1;
        public int Capacity => _items.Length;
        public bool IsEmpty => _top == -1;

        public ArrayStack(int capacity = 5)
        {
            _items = new T[capacity];
        }

        // Додавання елемента на вершину стеку - O(1)
        public void Push(T item)
        {
            if (_top == _items.Length - 1)
            {
                throw new StackOverflowException($"Стек переповнено! Максимальна місткість: {_items.Length}");
            }
            _items[++_top] = item;
        }

        // Вилучення елемента з вершини стеку - O(1)
        public T Pop()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Неможливо виконати Pop: Стек порожній!");
            }
            T item = _items[_top];
            _items[_top--] = default!; // очищення для збирача сміття
            return item;
        }

        // Перегляд вершини без вилучення - O(1)
        public T Peek()
        {
            if (IsEmpty) throw new InvalidOperationException("Стек порожній!");
            return _items[_top];
        }

        public void Print()
        {
            Console.Write($"[Count={Count}/{Capacity}]: Bottom -> [ ");
            for (int i = 0; i <= _top; i++) Console.Write($"{_items[i]} ");
            Console.WriteLine("] -> TOP");
        }
    }

    // ==========================================
    // ІНДИВІДУАЛЬНЕ ЗАВДАННЯ: ВАРІАНТ 1
    // Стек на базі динамічного однозв'язного списку
    // ==========================================
    
    public class StackNode<T>
    {
        public T Data { get; set; }
        public StackNode<T>? Next { get; set; }

        public StackNode(T data)
        {
            Data = data;
            Next = null;
        }
    }

    public class LinkedStack<T>
    {
        private StackNode<T>? _head = null;
        private int _count = 0;

        public int Count => _count;
        public bool IsEmpty => _head == null;

        // Додавання елемента у вершину (на початок списку) - O(1)
        public void Push(T item)
        {
            StackNode<T> newNode = new StackNode<T>(item);
            newNode.Next = _head;
            _head = newNode;
            _count++;
            // Стек ніколи не переповнюється, обмежений лише пам'яттю
        }

        // Вилучення з вершини - O(1)
        public T Pop()
        {
            if (IsEmpty)
            {
                throw new InvalidOperationException("Неможливо виконати Pop: Стек порожній!");
            }
            
            T data = _head!.Data;
            _head = _head.Next;
            _count--;
            return data;
        }

        // Перегляд вершини - O(1)
        public T Peek()
        {
            if (IsEmpty) throw new InvalidOperationException("Стек порожній!");
            return _head!.Data;
        }

        public void Print()
        {
            Console.Write($"[Count={Count}]: TOP -> [ ");
            StackNode<T>? current = _head;
            while (current != null)
            {
                Console.Write($"{current.Data} ");
                current = current.Next;
            }
            Console.WriteLine("] -> Bottom (null)");
        }
    }


    // ==========================================
    // ГОЛОВНИЙ КЛАС ПРОГРАМИ
    // ==========================================
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==========================================================");
            Console.WriteLine(" РОЗУМАТИК // ЛАБОРАТОРНА РОБОТА №8: СТЕК (LIFO)");
            Console.WriteLine("==========================================================\n");

            // --- ТЕСТУВАННЯ БАЗОВОГО МАСИВУ (Зі скріншотів) ---
            Console.WriteLine("--- 1. Тестування ArrayStack (з обробкою переповнення) ---");
            ArrayStack<string> undoHistory = new ArrayStack<string>(capacity: 4);

            Console.WriteLine("--- Додавання операцій у стек (Push) ---");
            undoHistory.Push("Написано текст");
            undoHistory.Push("Змінено шрифт");
            undoHistory.Push("Вставлено таблицю");
            undoHistory.Push("Змінено колір");
            undoHistory.Print();

            Console.WriteLine($"\n🔍 Поточна вершина (Peek): \"{undoHistory.Peek()}\"");

            Console.WriteLine("\n--- Скасування операцій (Undo / Pop) ---");
            while (!undoHistory.IsEmpty)
            {
                string action = undoHistory.Pop();
                Console.WriteLine($" ↩️ Скасовано: \"{action}\"");
                undoHistory.Print();
            }

            Console.WriteLine("\n--- Перевірка обробки винятків при вилученні з порожнього стеку ---");
            try
            {
                undoHistory.Pop();
            }
            catch (Exception ex)
            {
                Console.WriteLine($" ⚠️ Перехоплено очікувану помилку: {ex.Message}");
            }
            
            // --- ТЕСТУВАННЯ ІНДИВІДУАЛЬНОГО ЗАВДАННЯ (ВАРІАНТ 1) ---
            Console.WriteLine("\n==========================================================");
            Console.WriteLine(" ІНДИВІДУАЛЬНЕ ЗАВДАННЯ (ВАРІАНТ 1): LinkedStack<T>");
            Console.WriteLine("==========================================================");
            
            LinkedStack<int> dynamicStack = new LinkedStack<int>();
            
            Console.WriteLine("Додаємо елементи (10, 20, 30) у динамічний стек...");
            dynamicStack.Push(10);
            dynamicStack.Push(20);
            dynamicStack.Push(30);
            dynamicStack.Print(); // Очікується: TOP -> [ 30 20 10 ]

            Console.WriteLine($"\n🔍 Вершина (Peek): {dynamicStack.Peek()}");

            Console.WriteLine($"\n⬇️ Вилучаємо (Pop): {dynamicStack.Pop()}");
            dynamicStack.Print(); // Очікується: TOP -> [ 20 10 ]

            Console.WriteLine("\n✅ Роботу зі стеком завершено успішно!");
        }
    }
}
