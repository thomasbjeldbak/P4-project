using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CobraCompiler
{
    public class Data
    {

        object data;

        public Data(object data)
        {
            this.data = data;
        }

        public double toDouble()
        {
            return (double)data;
        }

        public int toInt()
        {
            return (int)data;
        }

        public bool toBoolean()
        {
            return (bool)data;
        }

        // For for-loop temp vars
        public void add1()
        {
            this.data = (double)this.data + 1;
        }

        public bool equals(object obj)
        {
            if (data == obj) return true;
            if (data == null || this.GetType() != obj.GetType()) return false;
            Data compare = (Data)obj;
            return this.data.Equals(compare.data);
        }
        public string toString()
        {
            return data.ToString();
        }
    }

}
