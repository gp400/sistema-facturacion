int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
var firstNumbers = numbers.TakeWhile(n => n < 6);
Console.WriteLine(string.Join(" ", firstNumbers));