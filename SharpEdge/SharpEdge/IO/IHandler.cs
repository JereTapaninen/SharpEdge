using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpEdge.IO
{
    /// <summary>
    /// All the different handlers
    /// </summary>
    public enum HandlerId
    {
        /// <summary>
        /// The UI handler
        /// </summary>
        UI = 0,
        /// <summary>
        /// The Event handler
        /// </summary>
        Event = 1
    }

    /// <summary>
    /// IHandler
    /// The basic framework for every handler (UI, Output, etc.)
    /// </summary>
    public interface IHandler
    {
        /// <summary>
        /// The Id of the handler
        /// </summary>
        HandlerId Id { get; }

        /// <summary>
        /// Gets or sets whether the IHandler is running or not.
        /// </summary>
        bool Running { get; set; }

        /// <summary>
        /// This method starts the handler.
        /// </summary>
        void Start();

        /// <summary>
        /// This method stops the handler.
        /// </summary>
        void Stop();

        /// <summary>
        /// This method gets called every 10msec and is used to update the handler
        /// </summary>
        /// <param name="frameTime">The amount of time that has passed.</param>
        void OnUpdate(int frameTime);
        
        /// <summary>
        /// This method is called upon program destruction and destroys the handler
        /// </summary>
        void Destroy();
    }
}
