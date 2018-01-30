using System;
using System.Globalization;

namespace EquationSolver.Logic.Parser
{
    internal class ExpressionParser
    {
        public string Input;

        private int _position = -1;
        private int _character;
        
        public ExpressionParser(string input)
        {
            Input = input.ToLower();
        }

        public double Parse()
        {
            NextChar();
            var result = ParseExpression();

            if (_position >= Input.Length)
                return result;
            throw new ArgumentException("Incorrect input");
        }

        private void NextChar()
        {
            _character = (++_position < Input.Length ? Input[_position] : -1);
        }


        // Grammar:
        // expression = term | expression `+` term | expression `-` term
        // term = factor | term `*` factor | term `/` factor
        // factor = `+` factor | `-` factor | `(` expression `)`
        // | number | functionName factor | factor `^` factor

        private double ParseExpression()
        {
            var result = ParseTerm();

            for (;;)
            {
                if (Eat('+'))
                    result += ParseTerm();
                else if (Eat('-'))
                    result -= ParseTerm();
                else
                    return result;
            }
        }

        private double ParseTerm()
        {
            var result = ParseFactor();

            for (;;)
            {
                if (Eat('*'))
                    result *= ParseFactor();
                else if (Eat('/'))
                    result /= ParseFactor();
                else
                    return result;
            }
        }

        private double ParseFactor()
        {
            if (Eat('+'))
                return ParseFactor(); 
            if (Eat('-'))
                return -ParseFactor(); 

            double result;
            var startPos = _position;

            if (Eat('('))
            {
                result = ParseExpression();

                Eat(')');
            }
            else if ((_character >= '0' && _character <= '9') || _character == '.')
            { 
                while ((_character >= '0' && _character <= '9') || _character == '.')
                    NextChar();

                var substring = IndexSubstring(Input, startPos, _position);

                result = double.Parse(substring, CultureInfo.InvariantCulture);
            }
            else switch (_character)
            {
                case 'e':
                    NextChar();
                    result = Math.E;
                    break;
                case 'p':
                    NextChar();
                    if (_character == 'i')
                    {
                        NextChar();
                        result = Math.PI;
                    }
                    else
                        throw new ArgumentException($"Unknown factor: p{_character}");
                    break;
                default:
                    if (_character >= 'a' && _character <= 'z')
                    {
                        while (_character >= 'a' && _character <= 'z')
                            NextChar();

                        var function = IndexSubstring(Input, startPos, _position);
                        result = ParseFactor();

                        switch (function.ToLower())
                        {
                            case "sgn":
                                result = Math.Sign(result);
                                break;
                            case "sqrt":
                                result = Math.Sqrt(result);
                                break;
                            case "abs":
                                result = Math.Abs(result);
                                break;
                            case "ln":
                                result = Math.Log(result);
                                break;
                            case "log":
                                var logBase = ParseFactor();

                                // ReSharper disable once CompareOfFloatsByEqualityOperator
                                if (result <= 0 || result == 1)
                                {
                                    throw new ArgumentException($"Incorrect logarithm base: {result}");
                                }
                                result = Math.Log(logBase)/Math.Log(result);
                                break;
                            case "sin":
                                result = Math.Sin(result);
                                break;
                            case "arcsin":
                                result = Math.Asin(result);
                                break;
                            case "cos":
                                result = Math.Cos(result);
                                break;
                            case "arccos":
                                result = Math.Acos(result);
                                break;
                            case "tg":
                                result = Math.Tan(result);
                                break;
                            case "arctg":
                                result = Math.Atan(result);
                                break;
                            case "ctg":
                                result = 1/Math.Tan(result);
                                break;
                            case "arcctg":
                                result = Math.PI/2 - Math.Atan(result);
                                break;
                            case "sinh":
                                result = Math.Sinh(result);
                                break;
                            case "cosh":
                                result = Math.Cosh(result);
                                break;
                            case "tgh":
                                result = Math.Tanh(result);
                                break;
                            case "ctgh":
                                result = (Math.Exp(2*result) + 1)/(Math.Exp(2*result) - 1);
                                break;
                            case "arsinh":
                                result = Math.Log(result + Math.Sqrt(result*result + 1));
                                break;
                            case "arcosh":
                                result = Math.Log(result + Math.Sqrt(result*result - 1));
                                break;
                            case "artgh":
                                result = (Math.Log((1 + result)/(1 - result)))/2;
                                break;
                            case "arctgh":
                                result = (Math.Log((result + 1)/(result - 1)))/2;
                                break;
                            default:
                                throw new ArgumentException($"Unknown function: {function.ToLower()}");
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Unexpected factor");
                    }
                    break;
            }

            if (Eat('^'))
                result = Math.Pow(result, ParseFactor());

            return result;
        }

        private bool Eat(int charToEat)
        {
            while (_character == ' ')
                NextChar();

            if (_character != charToEat)
                return false;

            NextChar();
            return true;
        }

        private static string IndexSubstring(string input, int start, int end)
        {
            return input.Substring(start, end - start );
        }
    }
}

