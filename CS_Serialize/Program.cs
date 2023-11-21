using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

[Serializable]
class person
{

    private string name;
    private string bthdt;

    [NonSerialized]
    private string juminNum;

    public string Name
    {
        get
        {
            if (string.IsNullOrEmpty(name)) return "No Name";
            else return name;
        }
        set
        {
            if (string.IsNullOrEmpty(value)) throw new Exception("name is empty");
            else name = value;
        }
    }
    public int age
    {
        get
        {
            if (string.IsNullOrEmpty(bthdt)) return -1;
            else
            {
                DateTime date1 = DateTime.ParseExact(bthdt, "yyyyMMdd", null);
                DateTime date2 = DateTime.Now;

                int differenceInYears = date2.Year - date1.Year;
                if (date2 < date1.AddYears(differenceInYears)) differenceInYears--;
                return differenceInYears;
            }
        }
    }
    public string BthDt
    {
        get
        {
            if (string.IsNullOrEmpty(bthdt)) return "No BirthDate";
            else return bthdt;
        }
        set
        {
            if (string.IsNullOrEmpty(value)) throw new Exception("BirthDate is Empty");
            else bthdt = value;
        }
    }

    string JuminNum
    {
        get
        {
            if (string.IsNullOrEmpty(juminNum)) return "No Jumin";
            else return juminNum;
        }
        set
        {
            if (string.IsNullOrEmpty(value)) throw new Exception("Jumin is Empty");
            else juminNum = value;
        }
    }

    public string getInfo()
    {
        return "name : " + Name + "\n" + "bthdt : " + BthDt + "\n" + "age : " + age;
    }
}



namespace CS_Serialize
{
    class Program
    {
        static void Main(string[] args)
        {
            person p = new person();

            try
            {
                p.Name = "이름";
                p.BthDt = "20000101";
                Console.WriteLine(p.getInfo());
                BinaryFormatter bf = new BinaryFormatter();
                MemoryStream ms = new MemoryStream();
                bf.Serialize(ms, p);

                Byte[] b = new byte[ms.Length];
                b = ms.GetBuffer();

                ms = new MemoryStream();
                ms.Write(b, 0, b.Length);
                ms.Seek(0, SeekOrigin.Begin);
                person p2 = ((person)bf.Deserialize(ms));
                p2.BthDt = "20001201";
                Console.WriteLine(p2.getInfo());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
