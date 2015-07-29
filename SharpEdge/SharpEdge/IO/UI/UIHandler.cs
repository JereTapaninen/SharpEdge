using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpEdge.IO.UI
{
    public class UIHandler : IHandler
    {
        public HandlerId Id { get { return HandlerId.UI; } }

        public bool Running { get; set; }

        public UIHandler()
        {

        }

        public void Start()
        {

        }

        public void Stop()
        {

        }

        public void Destroy()
        {

        }

        public void OnUpdate(int frameTime)
        {
            Console.Log("Meme!");
        }
    }
}
