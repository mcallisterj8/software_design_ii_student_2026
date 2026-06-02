public class Bird : Animal, IFlyable {
    public void Fly() {
        Console.WriteLine("The bird is flying.");
    }

    public void Speak() {
        Console.WriteLine("Tweet!");
    }


}