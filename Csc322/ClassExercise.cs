namespace Simulation;

public class A
{
    private int i = 7;

    protected int F(int j)
    {
        return i + j;
    }
}

public class B : A
{
    public void G()
    {
        // Console.WriteLine("i: {0}", i);
        Console.WriteLine("F(5): {0}", F(5));
        Console.Read();
    }
}

