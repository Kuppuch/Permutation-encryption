using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Permutation_encryption {
    class Program {
        static char[] mass = new char[] { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 'к', 'л', 'м', 'н',
                'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я' };

        static void Main(string[] args) {
            
            Console.WriteLine("Выберите действие");
            Console.WriteLine("1 - Зашифровать текст");
            Console.WriteLine("2 - Расшифровать текст");

            string key = "";

            while (key != null) {
                key = Console.ReadLine();
                if (key == "1") {
                    Crypt();
                    break;
                } else if (key == "2") {
                    DeCrypt();
                    break;
                } else {
                    Console.WriteLine("Повторите ввод");
                }
            }

            Console.ReadKey();
        }

        public static void DeCrypt() {
            Console.WriteLine("Исходный текст: ");
            Console.WriteLine();
            string[] text = File.ReadAllLines("input2.txt");
            string key = File.ReadAllText("key.txt");
            int count = 0;
            int[] index = new int[key.Length]; // Массив для индексов букв
            int[] index2 = new int[key.Length]; // Массив для номеров букв в ключе
            string[] output = new string[key.Length]; // Массив для разбивания строк
            for (int i = 0; i < text.Length; i++) {
                text[i] = text[i].ToLower();
                for (int j = 0; j < text[i].Length; j++) {
                    if (text[i][j] == ' ') {
                        count++;
                        continue;
                    }
                    output[i + count] += text[i][j];
                }

            }
            for (int i = 0; i < output.Length; i++) {
                Console.WriteLine(output[i]);
            }
            Console.WriteLine();
            Console.WriteLine();


            Console.Write("Ключ: " + key);

            for (int i = 0; i < key.Length; i++) {
                index[i] = Array.IndexOf(mass, key[i]);
            }

            int counter = 0, min = index[0], min_pos = 0;
            for (int i = 0; i < index.Length; i++) {
                for (int j = 0; j < index.Length; j++) {
                    if (index[j] < min) {
                        min = index[j];
                        min_pos = j;
                    }
                }
                index[min_pos] = int.MaxValue;
                min = int.MaxValue;
                index2[min_pos] = counter++;
            }

            int[] crypt = new int[key.Length];
            string[] kvalue = new string[index2.Length];
            for (int i = 0; i < crypt.Length; i++) {
                crypt[i] = i;
                kvalue[i] = output[i];
            }
            Console.WriteLine();
            Console.WriteLine();

            string[] sort = new string[index2.Length];
            for (int i = 0; i < index2.Length; i++) {
                for (int j = 0; j < output.Length; j++) {
                    if (j == index2[i]) {
                        sort[i] = output[j];
                    }
                }
            }

            Console.WriteLine("Отсортированный массив строк: ");
            Console.WriteLine();
            for (int i = 0; i < sort.Length; i++) {
                Console.WriteLine(/*i + " " +*/ sort[i]);
            }
            Console.WriteLine();

            string str = "";
            int a = 0;
            for (int i = 0; i < sort[0].Length; i++) { // Так смело взял первую строку, потому-что она по-любому есть и по-любому длинее следующих
                for (int j = 0; j < sort.Length; j++) {
                    if (i < sort[j].Length) { // Чтобы не писались лишние буквы в конец строки
                        str += sort[j][i % sort[j].Length];
                    }
                }
                a++; 
            }

            str += "            "; // Навсякий накидываю место в конце строки, чтоб потом в цикле не ругаться с ней
            string originStr = "";
            for (int i = 0; i < str.Length; i++) {
                if (str[i] == '.') {
                    originStr += ". ";
                    i++;
                }
                originStr += str[i];
            }

            //Console.WriteLine();
            //Console.WriteLine(str);
            Console.WriteLine();
            Console.WriteLine(originStr);
        }





        public static void Crypt() {
            Console.WriteLine("Исходный текст: ");
            string[] text = File.ReadAllLines("input.txt");
            string output = "";
            for (int i = 0; i < text.Length; i++) {
                text[i] = text[i].ToLower();
                for (int j = 0; j < text[i].Length; j++) {
                    if (text[i][j] != ' ') {
                        output += text[i][j];
                    }
                }
                Console.Write(output);
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Ключ: ");

            string key = File.ReadAllText("key.txt");
            int[] index = new int[key.Length];
            int[] index2 = new int[key.Length];
            Console.Write(key);

            for (int i = 0; i < key.Length; i++) {
                index[i] = Array.IndexOf(mass, key[i]);
                ;
            }

            int counter = 0, min = index[0], min_pos = 0;
            for (int i = 0; i < index.Length; i++) {
                for (int j = 0; j < index.Length; j++) {
                    if (index[j] < min) {
                        min = index[j];
                        min_pos = j;
                    }
                }
                index[min_pos] = int.MaxValue;
                min = int.MaxValue;
                index2[min_pos] = counter++;
            }
            Console.WriteLine();
            for (int i = 0; i < index2.Length; i++) {
                Console.Write(index2[i]);
            }
            Console.WriteLine();
            string[] str = new string[index2.Length];
            for (int i = 0; i < output.Length; i++) {
                Console.Write(output[i]);
                if (i % index2.Length == index2.Length - 1)
                    Console.WriteLine();
                str[i % str.Length] += output[i];
            }
            Array.Sort(index2, str);
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("Зашифрованный текст: ");
            for (int i = 0; i < str.Length; i++) {
                Console.Write(str[i] + " ");
            }

        }
    }
}
