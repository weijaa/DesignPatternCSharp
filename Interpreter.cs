using System;
using System.Collections.Generic;

abstract class Expression
{
    public abstract double Interpret();
}

class NumberExpression : Expression
{
    private double _number;

    public NumberExpression(double number)
    {
        _number = number;
    }

    public override double Interpret()
    {
        return _number;
    }
}

abstract class OperatorExpression : Expression
{
    protected Expression _left;
    protected Expression _right;

    public OperatorExpression(Expression left, Expression right)
    {
        _left = left;
        _right = right;
    }
}

class AddExpression : OperatorExpression
{
    public AddExpression(Expression left, Expression right)
        : base(left, right) { }

    public override double Interpret()
    {
        return _left.Interpret() + _right.Interpret();
    }
}

class SubtractExpression : OperatorExpression
{
    public SubtractExpression(Expression left, Expression right)
        : base(left, right) { }

    public override double Interpret()
    {
        return _left.Interpret() - _right.Interpret();
    }
}

class MultiplyExpression : OperatorExpression
{
    public MultiplyExpression(Expression left, Expression right)
        : base(left, right) { }

    public override double Interpret()
    {
        return _left.Interpret() * _right.Interpret();
    }
}

class DivideExpression : OperatorExpression
{
    public DivideExpression(Expression left, Expression right)
        : base(left, right) { }

    public override double Interpret()
    {
        return _left.Interpret() / _right.Interpret();
    }
}

class Interpreter
{
    private string _expression;
    private int _index;

    public Interpreter(string expression)
    {
        _expression = expression;
        _index = 0;
    }

    public Expression ParseExpression()
    {
        Expression left = ParseTerm();

        while (_index < _expression.Length)
        {
            char op = _expression[_index];
            if (op != '+' && op != '-')
                break;

            _index++;
            Expression right = ParseTerm();

            if (op == '+')
                left = new AddExpression(left, right);
            else
                left = new SubtractExpression(left, right);
        }

        return left;
    }

    private Expression ParseTerm()
    {
        Expression left = ParseFactor();

        while (_index < _expression.Length)
        {
            char op = _expression[_index];
            if (op != '*' && op != '/')
                break;

            _index++;
            Expression right = ParseFactor();

            if (op == '*')
                left = new MultiplyExpression(left, right);
            else
                left = new DivideExpression(left, right);
        }

        return left;
    }

    private Expression ParseFactor()
    {
        if (_index < _expression.Length && _expression[_index] == '(')
        {
            _index++;
            Expression expr = ParseExpression();
            if (_index < _expression.Length && _expression[_index] == ')')
                _index++;
            return expr;
        }

        return ParseNumber();
    }

    private Expression ParseNumber()
    {
        int startIndex = _index;
        while (_index < _expression.Length && (char.IsDigit(_expression[_index]) || _expression[_index] == '.'))
        {
            _index++;
        }
        double number = double.Parse(_expression.Substring(startIndex, _index - startIndex));
        return new NumberExpression(number);
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("请输入一個四則運算式：");
        string input = Console.ReadLine();
        
        try
        {
            Interpreter interpreter = new Interpreter(input);
            Expression expression = interpreter.ParseExpression();
            double result = expression.Interpret();
            Console.WriteLine($"结果是: {result}");
            var compute = new System.Data.DataTable().Compute(input, null);
            Console.WriteLine($"System.Data結果: {compute}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"error: {ex.Message}");
        }
    }
}
