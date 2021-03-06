﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ER.Eventos.IO.Domain.Core.Events
{
    public abstract class Message
    {
        public string MessageType { get; protected set; }

        public Guid AggregateId { get; set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}
