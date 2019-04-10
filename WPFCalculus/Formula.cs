using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace WPFCalculus
{
    public struct Formula
    {
        public Stack TokenQueue;
        public Stack<Operator> operatorStack; //Instance stacks
        public Stack operandStack;
        public Dictionary<dynamic, dynamic>Coordinates;
        

        public readonly string formula; //Instance formula



        public Formula(string formulaStr)
        {

            formula = formulaStr;
            
            Coordinates = new Dictionary<dynamic, dynamic>();
            operandStack = new Stack();
            operatorStack = new Stack<Operator>();
            TokenQueue = new Stack();
            //Read it 





            for (var x = Space.XMin; x <= Space.XMax; x += Space.Steps)
            {

                

                string input = formulaStr.Replace("x", $"{x}");







                const string regexStr = @"((?:^\-?\d+)|(?:(?<=[-+^/*])(?:\-?\d+)))";
                Console.WriteLine(input);
                var operands = Regex.Matches(input, regexStr);
                
                foreach (var s in operands)
                {
                    Console.WriteLine(s);
                    TokenQueue.Push(s);
                }

                //var test = Regex.Match(formulaStr, "\\[*/^+-+?\\]\\(*/^+-+?\\)");
                //Console.WriteLine(test);
                // Filling operator stack


                string replacement = Regex.Replace(input, regexStr, "");
                var operators = Regex.Matches(replacement, @"[*^+/-]");


                for (int i = 0; i < operators.Count; i++)
                {
                    List<Operator> QueueOperators = new List<Operator>();

                    string curOperator = operators[i].Value; //current operator value(string)
                    int curPresedence = 0; //current presedence instance


                    switch (curOperator)
                    {
                        //If operands have brackets around them evaluate them to the highest presedence (5+2) has to be calculated first 
                        case "(":
                        case ")":
                            curPresedence = 3;

                            break;
                        case "^":
                            Console.WriteLine("Given presedence 3 to: " + curOperator);
                            curPresedence = 3;
                            break;
                        case "/":
                        case "*":
                            curPresedence = 2;
                            Console.WriteLine("Given presedence 2 to: " + curOperator);
                            break;
                        case "+":
                        case "-":
                            curPresedence = 1;
                            Console.WriteLine("Given presedence 1 to: " + curOperator);
                            break;
                    }

                    if (operatorStack.Count != 0 && operatorStack.Peek().Presedence > curPresedence)
                    {
                        QueueOperators.Clear();
                        while (operatorStack.Count != 0 && operatorStack.Peek().Presedence > curPresedence)
                        {
                            QueueOperators.Add(operatorStack.Peek()); // Add to list

                            operatorStack.Pop();
                        } //When done


                        operatorStack.Push(new Operator(curOperator, curPresedence));

                        QueueOperators.Reverse();

                        foreach (var op in QueueOperators)
                        {
                            operatorStack.Push(op); //  Push it back in the good order (reversed)

                        }



                    }
                    else
                    {

                        operatorStack.Push(new Operator(curOperator, curPresedence)); // Just push it


                    }

                }



                //To make postfix do:



                Console.WriteLine("Token Count = " + TokenQueue.Count);
                while(2 <= TokenQueue.Count && 1 <= operatorStack.Count)
                {
                   

                    string curOperator = operatorStack.Peek().OperatorStr; //current operator value(string)


                    Console.WriteLine(curOperator);
                    switch (curOperator)
                    {
                        //If operands have brackets around them evaluate them to the highest presedence (5+2) has to be calculated first 
                        case "(":
                        case ")":
                            break;
                        case "^":
                            var Exponent = int.Parse(TokenQueue.Pop().ToString());
                            var Base = int.Parse(TokenQueue.Pop().ToString());
                            operatorStack.Pop();
                            var result = Math.Pow(Base, Exponent);
                            Console.WriteLine($"{Base}^ {Exponent} = {result}");
                            TokenQueue.Push(result);
                            break;
                        case "/":
                            var first = int.Parse(TokenQueue.Pop().ToString());
                            var second = int.Parse(TokenQueue.Pop().ToString());
                            operatorStack.Pop();
                            result = second / first;
                            Console.WriteLine($"{second} / {first} = {result}");
                            TokenQueue.Push(result);
                            break;
                        case "*":
                            first = int.Parse(TokenQueue.Pop().ToString());
                            second = int.Parse(TokenQueue.Pop().ToString());
                            operatorStack.Pop();
                            result = second * first;
                            Console.WriteLine($"{second} * {first} = {result}");
                            TokenQueue.Push(result);
                            break;
                        case "+":
                            Console.WriteLine("case +");
                            first = int.Parse(TokenQueue.Pop().ToString());
                            second = int.Parse(TokenQueue.Pop().ToString());
                            operatorStack.Pop();
                            result = second + first;
                            Console.WriteLine($"{second} - {first} = {second - first}");
                            TokenQueue.Push(result);
                            break;
                        case "-":
                            Console.WriteLine("case -");
                            first = int.Parse(TokenQueue.Pop().ToString());
                            second = int.Parse(TokenQueue.Pop().ToString());
                            operatorStack.Pop();
                            result = second - first;
                            Console.WriteLine($"{second} - {first} = {second - first}");

                            TokenQueue.Push(result);

                            break;
                    }
                }



                //cos(angle) = adjacent / hypotenuse



                if(TokenQueue.Count > 0)
                    Coordinates.Add(x, int.Parse(TokenQueue.Pop().ToString()));
                
            }
        }
        

        public dynamic Square(dynamic number)
        {
            number /= number;
            return number;
        }

        

        public dynamic Exponent(dynamic baseNum, dynamic exponentNum)
        {
            if (exponentNum > 1)
            {
                baseNum *= baseNum;
                exponentNum -= 1;
                Exponent(baseNum, exponentNum);
            }
            return baseNum;
            
            
        }

        public override string ToString()
        {
            return formula;
        }


        public void Dispose()
        {
            
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
      

    }
}
