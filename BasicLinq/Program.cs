
using System.Text.Json;

List<int> numbers = new List<int> {
    42,
    87,
    16,
    73,
    29,
    8,
    55,
    68,
    94,
    8,
    3,
    61
};


List<string> fruitBasket = new List<string> {
    "apple",
    "berry",
    "cherry",
    "date",
    "elderberry",
    "fuji apple",
    "grape"
};

Console.WriteLine();

JsonSerializerOptions jsonOptions = new JsonSerializerOptions { WriteIndented = true };



Console.WriteLine($"numbers:\n\n{JsonSerializer.Serialize(numbers, jsonOptions)}");

// Console.WriteLine("\n----------------------------------------------");

// int singleElem = numbers.SingleOrDefault(num => num == 8);

// Console.WriteLine($"singleElem:\n\n{JsonSerializer.Serialize(singleElem, jsonOptions)}");

// Console.WriteLine("\n----------------------------------------------");

// int firstElem = numbers.FirstOrDefault(num => num == 8);

// Console.WriteLine($"firstElem:\n\n{JsonSerializer.Serialize(firstElem, jsonOptions)}");

Console.WriteLine("\n=============================================\n");
Console.WriteLine("\n=============================================\n");

int singleNumElem = numbers
    .Where(num => num < 5)
    .SingleOrDefault();

// Console.WriteLine($"Filtered List:\n{JsonSerializer.Serialize(numbers.Where(num => num < 5))}", jsonOptions);

Console.WriteLine($"singleNumElem:\n\n{JsonSerializer.Serialize(singleNumElem, jsonOptions)}");


// int firstNumElem = numbers
//     .Where(num => num < 10)
//     .SingleOrDefault();

// Console.WriteLine($"firstNumElem:\n\n{JsonSerializer.Serialize(firstNumElem, jsonOptions)}");





// Console.WriteLine($"fruitBasket:\n\n{JsonSerializer.Serialize(fruitBasket, jsonOptions)}");


// Console.WriteLine("\n----------------------------------------------");

Console.WriteLine();








