﻿using StateSmith.Common;
using StateSmith.SmGraph.Visitors;
using System.Collections.Generic;
using System.Linq;

#nullable enable

namespace StateSmith.SmGraph
{
    /// <summary>
    /// Allow state machines to be nested. Why? Allows you to test
    /// that section of the state machine design independently of the rest.
    /// Very helpful in large designs.
    /// </summary>
    public class StateMachine : NamedVertex
    {
        internal HashSet<string> _events = new HashSet<string>();
        internal List<HistoryVertex> historyStates = new();
        public string variables = "";

        public StateMachine(string name) : base(name)
        {
        }

        public List<string> GetEventListCopy()
        {
            List<string> list = _events.ToList();
            list.Sort();
            return list;
        }

        /// <summary>
        /// Includes self
        /// </summary>
        /// <returns></returns>
        public List<NamedVertex> GetNamedVerticesCopy()
        {
            SmToNamedVerticesVisitor visitor = new();
            this.Accept(visitor);
            return visitor.namedVertices;
        }

        public override void Accept(VertexVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
