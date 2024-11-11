namespace DesignPattern;

using System;
using System.Collections.Generic;

// 動物接口
public interface IAnimal
{
    void Accept(IVisitor visitor);
}

// 具體動物類
public class Elephant : IAnimal
{
    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}

public class Bird : IAnimal
{
    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}

public class Fish : IAnimal
{
    public void Accept(IVisitor visitor)
    {
        visitor.Visit(this);
    }
}

// 訪問者接口
public interface IVisitor
{
    void Visit(Elephant elephant);
    void Visit(Bird bird);
    void Visit(Fish fish);
}

// 具體訪問者：餵食員
public class FeedingVisitor : IVisitor
{
    public void Visit(Elephant elephant)
    {
        Console.WriteLine("餵大象吃草");
    }

    public void Visit(Bird bird)
    {
        Console.WriteLine("餵鳥吃種子");
    }

    public void Visit(Fish fish)
    {
        Console.WriteLine("餵魚吃魚食");
    }
}

// 具體訪問者：獸醫
public class VetVisitor : IVisitor
{
    public void Visit(Elephant elephant)
    {
        Console.WriteLine("檢查大象的腳和鼻子");
    }

    public void Visit(Bird bird)
    {
        Console.WriteLine("檢查鳥的翅膀");
    }

    public void Visit(Fish fish)
    {
        Console.WriteLine("檢查魚的鰓");
    }
}

// 動物園類
public class Zoo
{
    private List<IAnimal> animals = new List<IAnimal>();

    public void AddAnimal(IAnimal animal)
    {
        animals.Add(animal);
    }

    public void Accept(IVisitor visitor)
    {
        foreach (var animal in animals)
        {
            animal.Accept(visitor);
        }
    }
}

class Program
{
    static void ZooMain(string[] args)
    {
        Zoo zoo = new Zoo();
        zoo.AddAnimal(new Elephant());
        zoo.AddAnimal(new Bird());
        zoo.AddAnimal(new Fish());
    
        Console.WriteLine("餵食時間：");
        zoo.Accept(new FeedingVisitor());
    
        Console.WriteLine("\n健康檢查時間：");
        zoo.Accept(new VetVisitor());
    }
}

