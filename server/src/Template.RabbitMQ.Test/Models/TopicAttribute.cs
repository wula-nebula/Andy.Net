using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Template.RabbitMQ.Test
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class TopicAttribute : Attribute
    {
        protected TopicAttribute(string name, bool isPartial = false)
        {
            Name = name;
            IsPartial = isPartial;
        }

        /// <summary>
        /// Topic or exchange route key name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Defines whether this attribute defines a topic subscription partial.
        /// The defined topic will be combined with a topic subscription defined on class level,
        /// which results for example in subscription on "class.method".
        /// </summary>
        public bool IsPartial { get; }

        /// <summary>
        /// Default group name is CapOptions setting.(Assembly name)
        /// kafka --> groups.id
        /// rabbit MQ --> queue.name
        /// </summary>
        public string Group { get; set; } = default!;
    }
}
