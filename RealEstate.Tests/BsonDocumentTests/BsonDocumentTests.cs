using System;
using System.Runtime.InteropServices;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using NUnit.Framework;

namespace RealEstate.Tests.BsonDocumentTests
{

    public class BsonDocumentTests
    {

        public BsonDocumentTests()
        {
            JsonWriterSettings.Defaults.Indent = true;
        }

        [Test]
        public void EmptyDocument()
        {
            var document = new BsonDocument();
            Console.WriteLine(document);
        }

        [Test]
        public void AddElements()
        {
            var person = new BsonDocument()
            {
                { "age", new BsonInt32(51)},
                { "isAlive", true }
            };
            person.Add("firstname", new BsonString("Bob"));

            Console.WriteLine(person);
        }

        [Test]
        public void AddingArrays()
        {
            var person = new BsonDocument();
            person.Add("address",
                new BsonArray(new[] {"101 Some Road", "Unit 504"}));

            Console.WriteLine(person);
        }

        [Test]
        public void EmbeddedDocument()
        {
            var person = new BsonDocument()
            {
                {
                    "contact", new BsonDocument
                    {
                        {"phone", "123-456-7890"},
                        {"email", "sample@email.com"}
                    } 
                }
            };

            Console.WriteLine(person);
        }

        [Test]
        public void BsonValueConversions()
        {
            var person = new BsonDocument()
            {
                {"age", 54}
            };
            Assert.Throws<InvalidCastException>(() => Console.WriteLine(person["age"].AsDouble + 10));
            Console.WriteLine(person["age"].AsInt32 + 10);
            Console.WriteLine(person["age"].ToDouble() + 10);
            Console.WriteLine(person["age"].IsInt32);
            Console.WriteLine(person["age"].IsString);
        }

        [Test]
        public void ToBson()
        {
            var person = new BsonDocument()
            {
                {"firstname", "Bob"}
            };

            var bson = person.ToBson();
            Console.WriteLine(BitConverter.ToString(bson));

            var deserializedPerson = BsonSerializer.Deserialize<BsonDocument>(bson);
            Console.WriteLine(deserializedPerson);
        }
    }
}