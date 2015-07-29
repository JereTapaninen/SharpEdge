using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SharpEdge.IO.Threading
{
    /// <summary>
    /// Helps with asynchronous stuff.
    /// </summary>
    public static class AsyncHelper
    {
        /// <summary>
        /// The list of all async threads going on currently.
        /// </summary>
        private static List<Thread> AsyncThreads { get; set; }

        /// <summary>
        /// Executes a method in a new thread with parameters.
        /// A timer can be added.
        /// </summary>
        /// <param name="name">The async action name</param>
        /// <param name="method">The method</param>
        /// <param name="parameters">The parameters of the method</param>
        /// <param name="afterN">Optional. Uses milliseconds. Used for delaying method execution.</param>
        public static void ExecuteMethod(string name, Action<dynamic> method, dynamic parameters, int afterN = 0)
        {
            var nThread = new Thread(() =>
            {
                Thread.Sleep(afterN);

                method(parameters);
            });
            nThread.Name = name;

            AsyncThreads.Add(nThread);

            nThread.Start();
        }

        /// <summary>
        /// Executes a method in a new thread without parameters.
        /// A timer can be added.
        /// </summary>
        /// <param name="method">The method</param>
        /// <param name="parameters">The parameters of the method</param>
        /// <param name="afterN">Optional. Uses milliseconds. Used for delaying method execution.</param>
        public static void ExecuteMethod(string name, Action method, int afterN = 0)
        {
            var nThread = new Thread(() =>
            {
                Thread.Sleep(afterN);

                method();
            });
            nThread.Name = name;

            AsyncThreads.Add(nThread);
            
            nThread.Start();
        }

        /// <summary>
        /// Executes an updating method with an interval and stop switch.
        /// </summary>
        /// <param name="method">The method to update continuously.</param>
        /// <param name="stopWhenFalse">The switch (when this function returns false, stops the updating)</param>
        /// <param name="idleWhileFalse">True if you want the bot to continue as soon as the switch returns true again.</param>
        /// <param name="interval">The interval between updates. Set to 0 for no updates.</param>
        public static void ExecuteUpdatingMethod(string name, Action<int> method, Func<dynamic, bool> stopWhenFalse, dynamic parameters, bool idleWhileFalse = true, int interval = 1)
        {
            var nThread = new Thread(() =>
            {
                var frameTime = 0;

                do
                {
                    while (stopWhenFalse(parameters))
                    {
                        if (frameTime + interval > int.MaxValue)
                            frameTime = 0;

                        method(frameTime);

                        Thread.Sleep(interval);

                        frameTime += interval;
                    }

                    Thread.Sleep(1);
                } while (idleWhileFalse);
            });
            nThread.Name = name;
            
            AsyncThreads.Add(nThread);

            nThread.Start();
        }

        /// <summary>
        /// Use this only when stopping the program and destroying the handlers.
        /// This uses Thread.Abort, which, in of itself, is evil already, and doing that if not really needed, is not the answer.
        /// </summary>
        /// <param name="name"></param>
        public static void CancelAsyncTask(string name)
        {
            try
            {
                AsyncThreads.Find(thread => thread.Name == name).Abort();
            }
            catch (Exception)
            { }
        }

        /// <summary>
        /// Waits until function a's return value is equal to boolean b
        /// This function presumes that you are using this from a different thread than the UI handling thread.
        /// PLEASE DO NOT USE THIS IN THE MAIN UI THREAD.
        /// </summary>
        /// <param name="a">The function</param>
        /// <param name="b">The bool</param>
        public static void WaitUntil(Func<bool> a, bool b)
        {
            while (a() != b)
            {
                Thread.Sleep(1);
            }
        }
    }
}
