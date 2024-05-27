using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mathGame
{
    public class mathGameLogic
    {
        public List<String> gameHistory { get; set; } = new List<String>();

        public void showMenu() 
        {
            System.Console.WriteLine("Hi! Enter an option to select the operation you want to perform");
            System.Console.WriteLine("1. Summation");
            System.Console.WriteLine("2. Subtraction");
            System.Console.WriteLine("3. Multiplication");
            System.Console.WriteLine("4. Division");
            System.Console.WriteLine("5. Random");
            System.Console.WriteLine("6. Show history");
            System.Console.WriteLine("7. Difficulty");
            System.Console.WriteLine("8. Exit");
        }

        public int mathOperation(int firstNum, int secondNum, char operation)
        {
            switch (operation)
            {
                case '+':
                    gameHistory.Add($"{firstNum} + {secondNum} = {firstNum + secondNum}");
                    return firstNum + secondNum;
                case '-':
                    gameHistory.Add($"{firstNum} - {secondNum} = {firstNum - secondNum}");
                    return firstNum - secondNum;
                case '*':
                    gameHistory.Add($"{firstNum} * {secondNum} = {firstNum * secondNum }");
                    return firstNum * secondNum;
                case '/':
                    while (firstNum < 0 || firstNum > 100)
                    {
                        try
                        {
                            System.Console.WriteLine("Please enter a number between 0-100");
                            firstNum = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (System.Exception)
                        {
                            //do nothing
                        }
                    }
                    gameHistory.Add($"{firstNum} / {secondNum} = {firstNum / secondNum}");
                    return firstNum / secondNum;
            }
            return 0;
        }
    }
}