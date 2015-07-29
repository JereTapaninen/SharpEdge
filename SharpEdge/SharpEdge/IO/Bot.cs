using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpEdge.IO.UI;
using SharpEdge.IO.Threading;

namespace SharpEdge.IO
{
    /// <summary>
    /// Bot
    /// Handles everything that has anything to do with the bot
    /// </summary>
    public static class Bot
    {
        /// <summary>
        /// A list of handlers. Self-explanatory
        /// </summary>
        private static List<IHandler> Handlers { get; set; }

        /// <summary>
        /// Gets the SharpEdge UI API
        /// </summary>
        public static UIHandler UI
        {
            get
            {
                return GetHandler(HandlerId.UI) as UIHandler;
            }
        }

        /// <summary>
        /// Initializes the SharpEdge bot.
        /// </summary>
        public static void Initialize()
        {
            Handlers.Add(new UIHandler());
        }

        /// <summary>
        /// Starts the bot completely
        /// </summary>
        public static void Start()
        {
            foreach (var handler in Handlers)
                AsyncHelper.ExecuteUpdatingMethod("handler" + (int)handler.Id, handler.OnUpdate, (handla) => { return handla.Running; }, handler, true, 10);
        }

        /// <summary>
        /// Stops the bot on its tracks
        /// </summary>
        public static void Stop()
        {

        }

        /// <summary>
        /// The OnUpdate method which, after the bot is started, occurs every ~ 10 msec.
        /// </summary>
        /// <param name="frameTime">The current frametime</param>
        private static void OnUpdate(int frameTime)
        {

        }

        /// <summary>
        /// Gets a handler specified by the Id
        /// </summary>
        /// <param name="id">The Id of the wanted handler</param>
        /// <returns>The wanted handler</returns>
        public static IHandler GetHandler(HandlerId id)
        {
            return Handlers.Find(handler => handler.Id == id);
        }
    }
}
