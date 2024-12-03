using System.Diagnostics;

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
        private int Index;

        public MockIO(int index)
        {
            Index = index;
        }
        public string ReadLine()
        {
            if (Index == 1)
            {
                Index++;
                return "John";
            }
            else
            {
                return "Doe";
            }
            
        }
        public void WriteLine(string line)
        {
            Debug.WriteLine(line);
        }
    }
}
