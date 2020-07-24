using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*Написать класс Money, предназначенный для хранения денежной суммы (в гривнах и копейках).
Для класса реализовать перегрузку операторов + (сложение денежных сумм), – (вычитание сумм),
/ (деление суммы на целое число), * (умножение суммы на целое число),
++ (сумма увеличивается на 1 копейку), -- (сумма уменьшается на 1 копейку), <, >, ==, !=.
Класс не может содержать отрицательную сумму. В случае если при исполнении какой-либо операции
получается отрицательная сумма денег, то класс генерирует исключительную ситуацию «Банкрот».
Программа должна с помощью меню продемонстрировать все возможности класса Money.
Обработка исключительных ситуаций производится в программе.*/
namespace C_sharp_DZ_7_1
{
    public class Money
    {
        int bills;
        int coins;
        public int Bills
        {
            get { return bills; }
            set 
            { 
                if (value > 0) bills = value;
                if (value < 0) 
                { 
                    throw new Exception("Банкрот");
                }
            }
        }
        public int Coins
        {
            get { return coins; }
            set
            {
                if (value >= 0) coins = value;
                if (value < 0)
                {
                    coins = 100 - Math.Abs(value);
                    Bills -= 1;
                }
                if (value >= 100)
                {
                    coins = value % 100;
                    Bills += (value - value % 100) / 100;
                }
            }
        }    
        public Money() { }
        public Money(int _bills, int _coins)
        {
            Bills = _bills;
            Coins = _coins;
        }
        public static Money operator +(Money s1, Money s2)
        {
            Money s = new Money
            {
                Bills = s1.Bills + s2.Bills,
                Coins = s1.Coins + s2.Coins
            };
            return s;
        }

        public static Money operator -(Money s1, Money s2)
        {
            Money s = new Money
            {
                Bills = s1.Bills - s2.Bills,
                Coins = s1.Coins - s2.Coins
            };
            return s;
        }
        public static Money operator *(Money s1, int n)
        {
            Money s = new Money
            {
                Bills = s1.Bills * n,
                Coins = s1.Coins * n
            };
            return s;
        }
        public static Money operator *(int n, Money s1)
        {
            return s1 * n;
        }
        public static Money operator /(Money s1, int n)
        {
            Money s = new Money();
            int tempCoins = s1.Bills * 100 + s1.Coins;
            tempCoins /= n;
            s.Coins = tempCoins % 100;
            s.Bills = (tempCoins - s.Coins) / 100;
            return s;
        }
        public static Money operator ++(Money s1)
        {
            Money s = new Money
            {
                Bills = s1.Bills,
                Coins = s1.Coins + 1
            };
            return s;
        }
        public static Money operator --(Money s1)
        {
            Money s = new Money
            {
                Bills = s1.Bills,
                Coins = s1.Coins - 1
            };
            return s;
        }
        public static bool operator >(Money s1, Money s2)
        {
            return (s1.Bills*100+s1.Coins) > (s2.Bills * 100 + s2.Coins);
        }
        public static bool operator <(Money s1, Money s2)
        {
            return (s1.Bills * 100 + s1.Coins) < (s2.Bills * 100 + s2.Coins);
        }
        public static bool operator ==(Money s1, Money s2)
        {
            return s1.Equals(s2);
        }
        public static bool operator !=(Money s1, Money s2)
        {
            return !(s1 == s2);
        }
        public override bool Equals(object obj)
        {
            Money s = (Money)obj;
            return (Bills == s.Bills && Coins == s.Coins);
        }
        public override int GetHashCode()
        {
            return (Convert.ToInt32((Bills ^ Coins) & 0xFFFFFFFF));
        }
        public override string ToString()
        {
            if (Bills != 0) return $"{Bills} гр. {Coins:D2} коп.";
            else return $"{Coins:D2} коп.";
        }
        public Money EnterSum()
        {
            Money Sum = new Money();
            Console.Write("Гривны: ");
            if (Int32.TryParse(Console.ReadLine(), out int SumBills))
            {
                Sum.Bills = SumBills;
            }
            else
            {
                throw new Exception("Некорректный ввод суммы.");
            }
            Console.Write("Копейки: ");
            if (Int32.TryParse(Console.ReadLine(), out int SumCoins))
            {
                Sum.Coins = SumCoins;
            }
            else
            {
                throw new Exception("Некорректный ввод суммы.");
            }
            return Sum;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Домашнее задание №7. Класс Money.";
            bool _exit = true;
            do
            {
                try
                {
                    Console.WriteLine("Демонстрация возможностей класса Money.");
                    Console.WriteLine("Предназначен для хранения и обработки денежых сумм (в гривнах и копейках).");
                    Console.WriteLine("Варианты использования:");
                    Console.WriteLine("1 - Сложение денежных сумм.");
                    Console.WriteLine("2 - Вычитание денежных сумм.");
                    Console.WriteLine("3 - Деление суммы на целое число.");
                    Console.WriteLine("4 - Умножение суммы на целое число.");
                    Console.WriteLine("5 - Сумма увеличивается на 1 копейку.");
                    Console.WriteLine("6 - Сумма уменьшается на 1 копейку.");
                    Console.WriteLine("7 - Сравнение двух сумм.");
                    Console.WriteLine("e - Выход.");
                    Console.Write("Введите вариант использования: ");
                    string selection = Console.ReadLine();
                    switch (selection)
                    {
                        case "1":
                            Console.WriteLine("Введите первую сумму:");
                            Money Sum_1 = new Money();
                            Sum_1 = Sum_1.EnterSum();
                            Console.WriteLine("Введите вторую сумму:");
                            Money Sum_2 = new Money();
                            Sum_2 = Sum_2.EnterSum();
                            Console.WriteLine($"{Sum_1} + {Sum_2} = {Sum_1 + Sum_2}");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case "2":
                            Console.WriteLine("Введите первую сумму:");
                            Money Sum_3 = new Money();
                            Sum_3 = Sum_3.EnterSum();
                            Console.WriteLine("Введите вторую сумму:");
                            Money Sum_4 = new Money();
                            Sum_4 = Sum_4.EnterSum();
                            Console.WriteLine($"{Sum_3} - {Sum_4} = {Sum_3 - Sum_4}");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case "3":
                            Console.WriteLine("Введите первую сумму:");
                            Money Sum_5 = new Money();
                            Sum_5 = Sum_5.EnterSum();
                            Console.WriteLine("Введите целое число:");
                            if (Int32.TryParse(Console.ReadLine(), out int n))
                            {
                                Console.WriteLine($"{Sum_5} / {n} = {Sum_5 / n}");
                            }
                            else
                            {
                                throw new Exception("Некорректный ввод числа.");
                            }
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case "4":
                            Console.WriteLine("Введите первую сумму:");
                            Money Sum_6 = new Money();
                            Sum_6 = Sum_6.EnterSum();
                            Console.WriteLine("Введите целое число:");
                            if (Int32.TryParse(Console.ReadLine(), out int m))
                            {
                                Console.WriteLine($"{Sum_6} * {m} = {Sum_6 * m}");
                            }
                            else
                            {
                                throw new Exception("Некорректный ввод числа.");
                            }
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case "5":
                            Console.WriteLine("Введите сумму:");
                            Money Sum_7 = new Money();
                            Sum_7 = Sum_7.EnterSum();
                            Console.WriteLine($"{Sum_7} + 1 коп. = {++Sum_7}");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case "6":
                            Console.WriteLine("Введите сумму:");
                            Money Sum_8 = new Money();
                            Sum_8 = Sum_8.EnterSum();
                            Console.WriteLine($"{Sum_8} - 1 коп. = {--Sum_8}");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case "7":
                            Console.WriteLine("Введите первую сумму:");
                            Money Sum_9 = new Money();
                            Sum_9 = Sum_9.EnterSum();
                            Console.WriteLine("Введите вторую сумму:");
                            Money Sum_10 = new Money();
                            Sum_10 = Sum_10.EnterSum();
                            if (Sum_9 > Sum_10) Console.WriteLine($"{Sum_9} больше {Sum_10}.");
                            if (Sum_9 < Sum_10) Console.WriteLine($"{Sum_9} меньше {Sum_10}.");
                            if (Sum_9 == Sum_10) Console.WriteLine($"{Sum_9} равна {Sum_10}.");
                            if (Sum_9 != Sum_10) Console.WriteLine($"{Sum_9} не равна {Sum_10}.");
                            Console.WriteLine("Press any key to continue...");
                            Console.ReadKey();
                            Console.Clear();
                            break;
                        case "e":
                        case "у":
                            Console.WriteLine("Завершение программы.\nPress any key to continue...");
                            Console.ReadKey();
                            _exit = false;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            while (_exit);
        }
    }
}