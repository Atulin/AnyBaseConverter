using AnyBaseConverter;

const string input = "HEXA";
const int b = 34;

var res = input.AnyToDecimal(b);
var back = res.DecimalToAny(b);

Console.WriteLine($"Input: {input} with base {b}\nRes2: {res}\nBack: {back}");