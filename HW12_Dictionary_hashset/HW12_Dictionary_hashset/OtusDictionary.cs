using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW12_Dictionary_hashset
{
    internal class OtusDictionary
    {
        private string[] _values;
        private int[] _keys;
        private int _count;

        public OtusDictionary()
        {
            _values = new string[32];
            _keys = new int[32];
            _count = 0;
        }

        // Метод для добавления элемента
        public void Add(int key, string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value), "Значение не может быть null.");
            }

            if (_count >= _values.Length)
            {
                // Увеличение массива
                Resize();
            }

            int index = key % _values.Length;

            // Обработка коллизий при помощи линейного пробирования
            while (_values[index] != null)
            {
                if (_keys[index] == key)
                {
                    // Если ключ уже существует, обновляем его значение
                    _values[index] = value;
                    return;
                }
                index = (index + 1) % _values.Length; // Линейное пробирование
            }

            // Сохранение ключа и значения
            _keys[index] = key;
            _values[index] = value;
            _count++;
        }

        // Метод для получения значения по ключу
        public string Get(int key)
        {
            int index = key % _values.Length;

            while (_values[index] != null)
            {
                if (_keys[index] == key)
                {
                    return _values[index];
                }
                index = (index + 1) % _values.Length; // Линейное пробирование
            }

            return null; // Если элемент не найден
        }

        // Индексатор для доступа по ключу
        public string this[int key]
        {
            get => Get(key);
            set => Add(key, value);
        }

        // Метод для изменения размера массива
        private void Resize()
        {
            int newSize = _values.Length * 2;
            string[] oldValues = _values;
            int[] oldKeys = _keys;

            // Создаем новый массив
            _values = new string[newSize];
            _keys = new int[newSize];
            _count = 0; // Обнуляем счетчик

            // Пересчитываем и копируем все элементы
            for (int i = 0; i < oldValues.Length; i++)
            {
                if (oldValues[i] != null) // Если элемент не пустой
                {
                    Add(oldKeys[i], oldValues[i]);
                }
            }
        }
    }
}
