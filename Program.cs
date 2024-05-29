namespace lab1;

// See https://aka.ms/new-console-template for more information
class Program
{
  static void Main(string[] args)
  {
    var vector1 = new Vector(5);
    var vector2 = new Vector(5, false);
    
    vector2[0] = 322;
    vector2[1] = 727;
    vector2[3] = 2048;

    (vector1 + vector2).Print();
  }
}
