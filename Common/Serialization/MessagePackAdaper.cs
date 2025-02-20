using MessagePack;
using MessagePack.Resolvers;

namespace TerrariaClone.Common.Serialization
{
    public class MessagePackAdapter : ISerializer
    {
        public byte[] Serialize<T>(T obj)
        {
            return MessagePackSerializer.Serialize(obj, ContractlessStandardResolver.Options);
        }

        public T Deserialize<T>(byte[] data)
        {
            return MessagePackSerializer.Deserialize<T>(data, ContractlessStandardResolver.Options);
        }
    }
}
