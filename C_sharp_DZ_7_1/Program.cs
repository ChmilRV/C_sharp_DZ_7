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
            Money s = new Money();
            //try
            //{
                s.Bills = s1.Bills - s2.Bills;
                s.Coins = s1.Coins - s2.Coins;
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"{ex.Message}");
            //}           
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
            //try
            //{
                int tempCoins = s1.Bills * 100 + s1.Coins;
                tempCoins /= n;
                s.Coins = tempCoins % 100;
                s.Bills = (tempCoins - s.Coins) / 100;
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"{ex.Message}");
            //}
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
            Money s = new Money();
            //try
            //{
                s.Bills = s1.Bills;
                s.Coins = s1.Coins - 1;
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine($"{ex.Message}");
            //}
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
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Домашнее задание №7. Класс Money.";

            
            try
            {
                Money Sum_1 = new Money(10, 25);
                Money Sum_2 = new Money(12, 90);
                Console.WriteLine($"{Sum_1} + {Sum_2} = {Sum_1 + Sum_2}\n");
                Money Sum_3 = new Money(12, 99);
                Money Sum_4 = new Money(3, 00);
                Console.WriteLine($"{Sum_3} - {Sum_4} = {Sum_3 - Sum_4}\n");
                //Console.WriteLine($"{Sum_4} - {Sum_3} = {Sum_4 - Sum_3}\n");
                Money Sum_5 = new Money(15, 25);
                int n = 5;
                Console.WriteLine($"{Sum_5} * {n} = {Sum_5 * n}\n");
                Console.WriteLine($"{n} * {Sum_5} = {n * Sum_5}\n");
                Money Sum_6 = new Money(12, 70);
                int r = 3;
                Console.WriteLine($"{Sum_6} / {r} = {Sum_6 / r}\n");
                Money Sum_7 = new Money(5, 75);
                int m = 3;
                Console.WriteLine($"{Sum_7} / {m} = {Sum_7 / m}\n");
                Money Sum_8 = new Money(0, 0);
                int k = 0;
                Console.WriteLine($"{Sum_8} / {k} = {Sum_8 / k}\n");
                Console.WriteLine($"{Sum_7} ++ = {++Sum_7}\n");
                Console.WriteLine($"{Sum_8} -- = {--Sum_8}\n");


            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
           



            



            Console.ReadKey();
        }
    }
}
