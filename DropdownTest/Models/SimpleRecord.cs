namespace DropdownTest.Models;

public class SimpleRecord
{
    public string Code { get; set; }
    public string Name { get; set; }


    public SimpleRecord() { }
    public SimpleRecord(string code, string name)
    {
        this.Code = code;
        this.Name = name;
    }
}
