using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts;
public class MyQueue
{
    public string Msg { get; set; }

    public MyQueue(string msg)
    {
        Msg = msg;
    }

    // Required for deserialization
    public MyQueue() { }
}
