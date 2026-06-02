public class Bird : Animal, IFlyable, ITestable {
    public void Fly() {
        Console.WriteLine("The bird is flying.");
    }

    public void QualityCheck() {
        Console.WriteLine("Doing quality check!");
    }

    public void Speak() {
        Console.WriteLine("Tweet!");
    }


}