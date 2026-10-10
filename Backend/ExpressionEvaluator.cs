
public static class ExpressionEvaluator
{
    public static double Evalute(string infix) => EvalutePostfix(ToPostfix(infix));
    private static string ToPostfix(string infix)
    {
        var postfix = string.Empty;
        var stack = new Stack<char>();

        for (int i = 0; i < infix.Length; i++)
        {
            char item = infix[i];

            if (char.IsWhiteSpace(item))
                continue;

            if (char.IsDigit(item) || item == '.')
           
            {
                while (i < infix.Length &&
                      (char.IsDigit(infix[i]) || infix[i] == '.'))
                {
                      postfix += infix[i];
                      i++;
                } 
                      postfix += ' ';
                      i--;
            }
            
            else if (item == '(')
            
            {
                stack.Push(item);
            }
           
            else if (item == ')')
           
            {
                while (stack.Count > 0 && stack.Peek() != '(')
                
                {
                    postfix += stack.Pop() + " ";
                }

                if 
                  
                   (stack.Count == 0)
                   throw new System.Exception("Invalid expression.");

                   stack.Pop();
            }
           
            else
            
            if (IsOperator(item))
           
            {
                while  (stack.Count > 0 &&
                       stack.Peek() != '(' &&
                       (PriorityStack(stack.Peek()) > PriorityStack(item) ||
                       (PriorityStack(stack.Peek()) == PriorityStack(item)
                       && item != '^')))
                {
                    postfix += stack.Pop() + " ";
                }

                stack.Push(item);
            }
            else
            {
                throw new System.Exception("Invalid expression.");
            }
        }

        while (stack.Count > 0)
        
        {
            if (stack.Peek() == '(')
            throw new System.Exception("Invalid expression.");
            postfix += stack.Pop() + " ";
        }

        return postfix;
    }

    private static int PriorityStack(char op) => op switch
    {
        '^' => 3,
        '*' => 2,
        '/' => 2,
        '+' => 1,
        '-' => 1,
        '(' => 0,
        _ => throw new System.Exception("Invalid expression."),
    };

    private static bool IsOperator(char item) =>
    item == '^' || item == '*' || item == '/' || item == '+' || item == '-';

    private static double EvalutePostfix(string postfix)
    {
        var stack = new Stack<double>();
        var number = string.Empty;

        foreach (char item in postfix)
        {
            if (char.IsDigit(item) || item == '.')
           
            {
                number += item;
            }
           
            else if (item == ' ')
           
            {
                 if (number.Length > 0)
               
                {
                    stack.Push(double.Parse(  number,
                    System.Globalization.CultureInfo.InvariantCulture));
                    number = string.Empty;
                }
            }
            
            else if (IsOperator(item))
           
            {
                if 
                    (number.Length > 0)
                {
                    stack.Push(double.Parse(
                    number,
                    System.Globalization.CultureInfo.InvariantCulture));

                    number = string.Empty;
                }

                if 
                (stack.Count < 2)
                throw new System.Exception("Invalid expression.");

                var ope2 = stack.Pop();
                var ope1 = stack.Pop();

                stack.Push(Calculate(ope1, ope2, item));
            }
        }

        if 
       
          (number.Length > 0)
        {
          stack.Push(double.Parse(
          number,
          System.Globalization.CultureInfo.InvariantCulture));
        }

        if
           
        (stack.Count != 1)
        throw new System.Exception("Invalid expression.");
        return stack.Pop();
    }

    private static double Calculate(
    double ope1, double ope2, char item) => item switch
    
    {
      '*' => ope1 * ope2,
      '/' => ope1 / ope2,
      '+' => ope1 + ope2,
      '-' => ope1 - ope2,
      '^' => System.Math.Pow(ope1, ope2),
       _ => throw new System.Exception("Invalid expression."),
     
    };

}