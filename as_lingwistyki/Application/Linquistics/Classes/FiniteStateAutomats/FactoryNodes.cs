using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Linquistics
{
    class FactoryNodes
    {
        static public Node CreateStartNode(Point screenP,string name)
        {
            return new Node(0, screenP, name);
        }
        static public Node CreateCommonNode(Point screenP,string name)
        {
            return new Node(1, screenP, name);
        }
        static public Node CreateAcceptNode(Point screenP,string name)
        {
            return new Node(2, screenP,name);
        }
    }
}
