using System;
public class SingletonTest
{
    private static SingletonTest Instance = null;
    public static SingletonTest GetInstance()
    {
        if (Instance == null)
            Instance = new SingletonTest();

        return Instance;
    }

    private SingletonTest()
    {

    }
}
