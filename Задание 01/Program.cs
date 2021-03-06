using System;
using System.Threading; //Подключаем библиотеку для работы с потоками


namespace Задание_01
{
    //1.Имеется пустой участок земли (двумерный массив) и план сада, который необходимо реализовать.
    //Эту задачу выполняют два садовника, которые не хотят встречаться друг с другом.
    //Первый садовник начинает работу с верхнего левого угла сада и перемещается слева направо,
    //сделав ряд, он спускается вниз.
    //Второй садовник начинает работу с нижнего правого угла сада и перемещается снизу вверх,
    //сделав ряд, он перемещается влево.
    //Если садовник видит, что участок сада уже выполнен другим садовником, он идет дальше.
    //Садовники должны работать параллельно.
    //Создать многопоточное приложение, моделирующее работу садовников.
    class Program
    {
        private static int[,] garden; //Объявляем массив сада
        private static int x; //Размер сада по оси OX
        private static int y; //Размер сада по оси OY

        private static void gardener1()
        //Метод первого садовника двигается с верхнего левого угла сада и перемещается слева направо, сделав ряд, он спускается вниз
        {
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    if (garden[j, i] == 0)
                        garden[j, i] = 1; //Если значение ячейки сада '0', то она помечается меткой садовника 1
                    Thread.Sleep(1); //Метод SLEEP взят из примера в интеренете !!!! Смысл использования плохо понятен !!!
                }
            }
        }
        private static void gardener2()
        //Метод второго садовника двигается с нижнего правого угла сада и перемещается снизу вверх, сделав ряд, он перемещается влево
        {
            for (int i = x - 1; i > 0; i--)
            {
                for (int j = y - 1; j > 0; j--)
                {
                    if (garden[i, j] == 0)
                        garden[i, j] = 2; //Если значение ячейки сада '0', то она помечается меткой садовника 2
                    Thread.Sleep(1);
                }
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Здравствуйте!");
            Console.WriteLine("Введите размеры виртуального сада для отработки двумя садовниками [X, Y]:");
            x = Convert.ToInt32(Console.ReadLine());
            y = Convert.ToInt32(Console.ReadLine());

            garden = new int[x, y]; //Создаём экземпляр сада размерми XoY

            Thread g1 = new Thread(gardener1); //Создаём переменную делегата садовника 1
            Thread g2 = new Thread(gardener2); //Создаём переменную делегата садовника 2
                        
            g2.Start(); //Запускаем поток садовника 2
            g1.Start(); //Запускаем поток садовника 1
                        
            g2.Join(); //Метод JOIN взят из примера в интеренете !!!!
            g1.Join(); //Смысл использования плохо понятен !!!

            //Данным циклом выводим на консоль ячейки сада
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    Console.Write(garden[j, i] + " ");
                }
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}

//P.S Смысл использования методов sleep() и join() плохо понятен, но без них не работает!!!!

//На JAVA ресурсе такое объяснение:
//Для приостановки выполнения текущего потока на какое-то время, используем метод sleep()
//t2 не начнет работу, пока t1 не завершит свою.
//Метод join() можно использовать, чтобы гарантировать последовательность выполнения потоков. 
