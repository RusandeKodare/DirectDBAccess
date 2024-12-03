using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectDBAccess
{
    public interface IIO
    {
        public string ReadLine();
        public void WriteLine(string line);
    }
    public class IO : IIO
    {
        public string ReadLine()
        {
            return Console.ReadLine()??"";
        }

        public void WriteLine(string line)
        {
            Console.WriteLine(line);
        }
    }
    public class MockIO : IIO
    {
        public string ReadLine()
        {
            return "John";
        }
        public void WriteLine(string line)
        {
            Debug.WriteLine(line);
        }
    }
}
