using System;
using System.Collections.Generic;
using System.Text;

namespace ER.Eventos.IO.Domain.Core.Events
{
    public class Event : Message
    {
        protected Event()
        {
            Timestamp = DateTime.Now;
        }

        public DateTime Timestamp { get; private set; }
    }
}
