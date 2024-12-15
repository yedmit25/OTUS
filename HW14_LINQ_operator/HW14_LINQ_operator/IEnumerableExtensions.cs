using System;
using System.Collections.Generic;
using System.Linq;

public static class IEnumerableExtensions
{
    /// <summary>
    /// Возвращает топ X процентов элементов из коллекции по убыванию.
    /// </summary>
    /// <typeparam name="T">Тип элементов в коллекции.</typeparam>
    /// <param name="source">Исходная коллекция.</param>
    /// <param name="percentage">Процент элементов для выборки (между 1 и 100).</param>
    /// <returns>Коллекция топ X процентов элементов.</returns>
    /// <exception cref="ArgumentException">Если percentage < 1 или percentage > 100.</exception>
    public static IEnumerable<T> Top<T>(this IEnumerable<T> source, int percentage)
    {
        try
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source), "Source collection cannot be null.");

            if (percentage < 1 || percentage > 100)
                throw new ArgumentException("Percentage must be between 1 and 100.", nameof(percentage));

            // Считаем количество элементов, которое нужно вернуть
            var count = (int)Math.Ceiling(source.Count() * (percentage / 100.0));

            return source
                .OrderByDescending(x => x)
                .Take(count);
        }
        catch (ArgumentNullException ex)
        {
            // Логируем или обрабатываем ошибку обнуленной коллекции
            Console.WriteLine($"Error: {ex.Message}");
            return Enumerable.Empty<T>(); // Возвращаем пустую коллекцию
        }
        catch (ArgumentException ex)
        {
            // Логируем или обрабатываем аргументы
            Console.WriteLine($"Error: {ex.Message}");
            return Enumerable.Empty<T>(); // Возвращаем пустую коллекцию
        }
        catch (Exception ex)
        {
            // Общая обработка исключений
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            return Enumerable.Empty<T>(); // Возвращаем пустую коллекцию
        }
    }

    /// <summary>
    /// Возвращает топ X процентов элементов из коллекции по убыванию заданного поля.
    /// </summary>
    /// <typeparam name="T">Тип элементов в коллекции.</typeparam>
    /// <typeparam name="TKey">Тип поля, по которому выбираем топ.</typeparam>
    /// <param name="source">Исходная коллекция.</param>
    /// <param name="percentage">Процент элементов для выборки (между 1 и 100).</param>
    /// <param name="selector">Функция выборки ключа.</param>
    /// <returns>Коллекция топ X процентов элементов.</returns>
    public static IEnumerable<T> Top<T, TKey>(this IEnumerable<T> source, int percentage, Func<T, TKey> selector)
    {
        try
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source), "Source collection cannot be null.");

            if (selector == null)
                throw new ArgumentNullException(nameof(selector), "Selector cannot be null.");

            if (percentage < 1 || percentage > 100)
                throw new ArgumentException("Percentage must be between 1 and 100.", nameof(percentage));

            // Считаем количество элементов, которое нужно вернуть
            var count = (int)Math.Ceiling(source.Count() * (percentage / 100.0));

            return source
                .OrderByDescending(selector)
                .Take(count);
        }
        catch (ArgumentNullException ex)
        {
            // Логируем или обрабатываем ошибку обнуленной коллекции
            Console.WriteLine($"Error: {ex.Message}");
            return Enumerable.Empty<T>(); // Возвращаем пустую коллекцию
        }
        catch (ArgumentException ex)
        {
            // Логируем или обрабатываем аргументы
            Console.WriteLine($"Error: {ex.Message}");
            return Enumerable.Empty<T>(); // Возвращаем пустую коллекцию
        }
        catch (Exception ex)
        {
            // Общая обработка исключений
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            return Enumerable.Empty<T>(); // Возвращаем пустую коллекцию
        }
    }
}