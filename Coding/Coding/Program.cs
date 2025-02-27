using System;
using System.Collections.Generic;
using System.Linq;

namespace Coding
{
    class Program
    {
        // Метод для генерации битовой последовательности на основе входных данных
        static List<int> GenerateBit(int groupNumber, int studentNumber, int lastNameLength, int firstNameLength)
        {
            // Преобразуем числа в двоичные строки
            string groupNumberBin = Convert.ToString(groupNumber, 2).PadLeft(6, '0');
            string studentNumberBin = Convert.ToString(studentNumber, 2).PadLeft(5, '0');
            string lastNameLengthBin = Convert.ToString(lastNameLength, 2).PadLeft(4, '0');
            string firstNameLengthBin = Convert.ToString(firstNameLength, 2).PadLeft(4, '0');

            // Строка с фиксированной 9-битной вставкой "000000000"
            string bitString = groupNumberBin + studentNumberBin + "000000000" + lastNameLengthBin + firstNameLengthBin;

            // Преобразуем строку в список битов
            return bitString.Select(c => int.Parse(c.ToString())).ToList();
        }

        // NRZ-кодирование
        static List<int> NRZ(List<int> bitSequence) => new List<int>(bitSequence);

        // NRZI-кодирование
        static List<int> NRZI(List<int> bitSequence)
        {
            List<int> sequence = new List<int>();
            int lastLevel = 0; // Начальное состояние

            foreach (int bit in bitSequence)
            {
                if (bit == 1)
                    lastLevel = (lastLevel == 0) ? 1 : 0;
                sequence.Add(lastLevel);
            }

            return sequence;
        }

        // AMI-кодирование
        static List<int> AMI(List<int> bitSequence)
        {
            List<int> sequence = new List<int>();
            int lastLevel = 1;

            foreach (int bit in bitSequence)
            {
                if (bit == 0)
                {
                    sequence.Add(0);
                }
                else
                {
                    sequence.Add(lastLevel);
                    lastLevel = (lastLevel == 0) ? 1 : 0;
                }
            }

            return sequence;
        }

        // Манчестерское кодирование
        static List<int> Manchester(List<int> bitSequence)
        {
            List<int> sequence = new List<int>();
            foreach (int bit in bitSequence)
            {
                sequence.Add(bit == 0 ? 1 : 0);
                sequence.Add(bit == 0 ? 0 : 1);
            }
            return sequence;
        }

        static void Main(string[] args)
        {
            int groupNumber;
            while (true)
            {
                Console.Write("Введите номер группы: ");
                if (int.TryParse(Console.ReadLine(), out groupNumber))
                    break;
                Console.WriteLine("Ошибка: Введите числовое значение номера группы.");
            }

            int studentNumber;
            while (true)
            {
                Console.Write("Введите номер в списке: ");
                if (int.TryParse(Console.ReadLine(), out studentNumber))
                    break;
                Console.WriteLine("Ошибка: Введите числовое значение номера в списке.");
            }

            string lastName;
            while (true)
            {
                Console.Write("Введите фамилию: ");
                lastName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(lastName) && lastName.All(char.IsLetter))
                    break;
                Console.WriteLine("Ошибка: Фамилия должна содержать только буквы.");
            }

            string firstName;
            while (true)
            {
                Console.Write("Введите имя: ");
                firstName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(firstName) && firstName.All(char.IsLetter))
                    break;
                Console.WriteLine("Ошибка: Имя должно содержать только буквы.");
            }

            int lastNameLength = lastName.Length;
            int firstNameLength = firstName.Length;

            // Генерация битовой последовательности
            List<int> bitSequence = GenerateBit(groupNumber, studentNumber, lastNameLength, firstNameLength);

            // Кодирование
            List<int> nrz = NRZ(bitSequence);
            List<int> nrzi = NRZI(bitSequence);
            List<int> ami = AMI(bitSequence);
            List<int> manchester = Manchester(bitSequence);

            // Вывод результатов
            Console.WriteLine("Исходная последовательность битов: " + string.Join("", bitSequence));
            Console.WriteLine("NRZ кодирование: " + string.Join("", nrz));
            Console.WriteLine("NRZI кодирование: " + string.Join("", nrzi));
            Console.WriteLine("AMI кодирование: " + string.Join("", ami));
            Console.WriteLine("Манчестерское кодирование: " + string.Join("", manchester));

            Console.ReadKey();
        }
    }
}
