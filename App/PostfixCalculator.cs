using System;
using System.Collections.Generic;
using System.Linq;

namespace App
{
    public static class PostfixCalculator
    {
        private static string[] operators = { "+", "-", "*", "/", "(", ")" };
        public static string Calculate(string postfixExpression)
        {
            if (postfixExpression == "") return "0";
            if (string.IsNullOrEmpty(postfixExpression) || IsNotCorrect(postfixExpression))
            {
                throw new FormatException();
            }

            var stack = new Stack<int>();
            foreach (var token in postfixExpression.Split(' '))
            {
                if (int.TryParse(token, out int operand))
                {
                    stack.Push(operand);
                }
                else
                {
                    if (stack.Count < 2)
                    {
                        throw new FormatException();
                    }
                    int operand2 = stack.Pop();
                    int operand1 = stack.Pop();
                    switch (token)
                    {
                        case "+":
                            stack.Push(operand1 + operand2);
                            break;
                        case "-":
                            stack.Push(operand1 - operand2);
                            break;
                        case "*":
                            stack.Push(operand1 * operand2);
                            break;
                        case "/":
                            stack.Push(operand1 / operand2);
                            break;
                    }
                }
            }
            if (stack.Count != 1)
            {
                throw new FormatException();
            }
            return stack.Pop().ToString();
        }

        private static bool IsNotCorrect(string postfixExpression)
        {
            foreach (var token in postfixExpression.Split(' '))
            {
                if (!operators.Contains(token) && !int.TryParse(token, out int operand))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
