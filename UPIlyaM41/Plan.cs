using System;

namespace UPIlyaM41;

public class Plan
{
    public int id { get; set; }
    public string surname { get; set; }
    public string name { get; set; }
    public string middleName { get; set; }
    public DateTime data { get; set; }
    public string cabinet { get; set; }
}

public class Teachers
{
    public int id { get; set; }
    public string surname { get; set; }
    public string name { get; set; }
    public string middleName { get; set; }
}

public class Cabinets
{
    public int id { get; set; }
    public string cabinet { get; set; }
}

public class Classes
{
    public int id { get; set; }
    public string className { get; set; }
}