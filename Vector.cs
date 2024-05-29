namespace lab1;

public partial class Vector {
  public int Length { get; private set; }

  private List<double> Data { get; set; }

  public Vector()
  {
    Length = 1;
    Data = new List<double>([0.0]);
  }

  public Vector(int length)
  {
    Length = length;
    Data = Enumerable
      .Range(0, length)
      .ToList()
      .ConvertAll(x => (double)x);
  }

  public Vector(int length, bool fill)
  {
    Length = length;
    Data = fill
      ? Enumerable
        .Range(0, length)
        .ToList()
        .ConvertAll(x => (double)x)
      : Enumerable.Repeat(0.0, length).ToList();
  }

  public double this[int i]
  {
    get { return Data.ElementAt(i); }
    set { Data[i] = value; }
  }

  public void Print()
  {
    Console.WriteLine(String.Join(", ", Data));
  }

  #region Add

  public static Vector operator +(Vector a, double b)
  {
    a.Add(b);

    return a;
  }

  public static Vector operator +(Vector a, Vector b)
  {
    a.Add(b);

    return a;
  }

  public void Add(double value)
  {
    Data = Data.Select(x => x + value).ToList();
  }

  public void Add(Vector other)
  {
    Data = DoAction(other, (item1, item2) => item1 + item2);
  }

  #endregion

  #region Substract

  public static Vector operator -(Vector a, double b)
  {
    a.Substract(b);

    return a;
  }

  public static Vector operator -(Vector a, Vector b)
  {
    a.Substract(b);

    return a;
  }

  public void Substract(double value)
  {
    Data = Data.Select(x => x - value).ToList();
  }

  public void Substract(Vector other)
  {
    Data = DoAction(other, (item1, item2) => item1 - item2);
  }

  #endregion

  #region Multiply

  public static Vector operator *(Vector a, double b)
  {
    a.Multiply(b);

    return a;
  }

  public static Vector operator *(Vector a, Vector b)
  {
    a.Multiply(b);

    return a;
  }

  public void Multiply(double value)
  {
    Data = Data.Select(x => x * value).ToList();
  }

  public void Multiply(Vector other)
  {
    Data = DoAction(other, (item1, item2) => item1 * item2);
  }

  #endregion

  #region Compare

  public static bool operator >(Vector a, Vector b)
  {
    return a.DoCompareAction(b, (item1, item2) => item1 > item2);
  }

  public static bool operator >=(Vector a, Vector b)
  {
    return a.DoCompareAction(b, (item1, item2) => item1 >= item2);
  }

  public static bool operator <(Vector a, Vector b)
  {
    return a.DoCompareAction(b, (item1, item2) => item1 < item2);
  }

  public static bool operator <=(Vector a, Vector b)
  {
    return a.DoCompareAction(b, (item1, item2) => item1 <= item2);
  }

  #endregion

  #region Equals

  public static bool operator ==(Vector a, Vector b)
  {
    return a.DoCompareAction(b, (item1, item2) => item1 == item2);
  }

  public static bool operator !=(Vector a, Vector b)
  {
    return a.DoCompareAction(b, (item1, item2) => item1 != item2);
  }

  #endregion

  private bool DoCompareAction(Vector other, Func<double, double, bool> callback)
  {
    if (Length != other.Length) {
      throw new DifferentLengthException();
    }

    return Data
      .Select((x, i) => new { item = x, index = i })
      .All((x) => {
        return callback(x.item, other[x.index]);
      });
  }

  private List<double> DoAction(Vector other, Func<double, double, double> callback)
  {
    if (Length != other.Length) {
      throw new DifferentLengthException();
    }

    return Data
      .Select((x, i) => new { item = x, index = i })
      .Select((x) => {
        return callback(x.item, other[x.index]);
      })
      .ToList();
  }

  private class DifferentLengthException : Exception {}
}