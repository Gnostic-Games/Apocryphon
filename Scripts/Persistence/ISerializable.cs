using System;
using Newtonsoft.Json.Linq;

namespace Apocryphon.Persistence
{
    /// <summary>
    /// Denotes objects which can be serialized into JSON for saving.
    /// Any object which should have its data saved should inherit this.
    /// </summary>
    public interface ISerializable
    {
        public JToken               Serialize();
        public static ISerializable Deserialize(JToken obj) =>
            throw new NotImplementedException();
    }
}