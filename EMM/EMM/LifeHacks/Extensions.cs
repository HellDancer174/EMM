using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace LifeHacks
{
    public static class Extensions
    {
        public static T DeepCopy<T>(this T self)
        {
            if (!typeof(T).IsSerializable) throw new ArgumentException("Type must be Serializable");
            if (ReferenceEquals(self, null)) return default(T);
            var formatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                formatter.Serialize(stream, self);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }

        }
    }
}
