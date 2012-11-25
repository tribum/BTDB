using BTDB.EventStoreLayer;
using BTDB.StreamLayer;
using NUnit.Framework;

namespace BTDBTest
{
    [TestFixture]
    public class TypeSerializersTest
    {
        ITypeSerializers _ts;
        ITypeSerializersMapping _mapping;

        [SetUp]
        public void Setup()
        {
            _ts = new TypeSerializers();
            _mapping = _ts.CreateMapping();
        }

        [Test]
        public void CanSerializeString()
        {
            var writer = new ByteBufferWriter();
            var storedDescriptorCtx = _mapping.StoreNewDescriptors(writer, "Hello");
            _mapping.StoreObject(writer, "Hello");
            Assert.Null(storedDescriptorCtx);
            _mapping.CommitNewDescriptors(null);
            var reader = new ByteBufferReader(writer.Data);
            var obj = _mapping.LoadObject(reader);
            Assert.AreEqual("Hello", obj);
        }

        [Test]
        public void CanSerializeInt()
        {
            var writer = new ByteBufferWriter();
            var storedDescriptorCtx = _mapping.StoreNewDescriptors(writer, 12345);
            _mapping.StoreObject(writer, 12345);
            Assert.Null(storedDescriptorCtx);
            _mapping.CommitNewDescriptors(null);
            var reader = new ByteBufferReader(writer.Data);
            var obj = _mapping.LoadObject(reader);
            Assert.AreEqual(12345, obj);
        }

        [Test]
        public void CanSerializeSimpleTypes()
        {
            CanSerializeSimpleValue((byte)42);
            CanSerializeSimpleValue((sbyte)-20);
            CanSerializeSimpleValue((short)-1234);
            CanSerializeSimpleValue((ushort)1234);
            CanSerializeSimpleValue((uint)123456789);
            CanSerializeSimpleValue(-123456789012L);
            CanSerializeSimpleValue(123456789012UL);
        }

        void CanSerializeSimpleValue(object value)
        {
            var writer = new ByteBufferWriter();
            var storedDescriptorCtx = _mapping.StoreNewDescriptors(writer, value);
            _mapping.StoreObject(writer, value);
            Assert.Null(storedDescriptorCtx);
            _mapping.CommitNewDescriptors(null);
            var reader = new ByteBufferReader(writer.Data);
            var obj = _mapping.LoadObject(reader);
            Assert.AreEqual(value, obj);
        }

        public class SimpleDto
        {
            public string StringField { get; set; }
            public int IntField { get; set; }
        }

        [Test, Ignore]
        public void CanSerializeSimpleDto()
        {
            var writer = new ByteBufferWriter();
            var value = new SimpleDto { IntField = 42, StringField = "Hello" };
            var storedDescriptorCtx = _mapping.StoreNewDescriptors(writer, value);
            _mapping.StoreObject(writer, value);
            _mapping.CommitNewDescriptors(storedDescriptorCtx);
            var reader = new ByteBufferReader(writer.Data);
            var obj = _mapping.LoadObject(reader);
            Assert.AreEqual(value, obj);
        }

    }
}