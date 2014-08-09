using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace RealEstate.Tests.PocoTests
{
    public class PocoTests
    {
        public PocoTests()
        {
            JsonWriterSettings.Defaults.Indent = true;
        }

        [Test]
        public void AutomaticSerialization()
        {
            var person = new Person {Age = 54, Firstname = "Bob"};
            person.Address.Add("101 Some Road");
            person.Address.Add("Unit 514");
            person.Contact.Email = "sample@email.com";
            person.Contact.Phone = "123-456-7890";

            var json = person.ToJson();
            Console.WriteLine(json);
        }

        [Test]
        public void SerializationAttributes()
        {
            var person = new Person {Age = 15, Firstname = "Jack"};
            Console.WriteLine(person.ToJson());
        }

        [Test]
        public void DateTimeSerialization()
        {
            var order = new Order() {OrderDateTime = new DateTime(2014, 1, 2, 11, 30, 0)};
            var serializedOrder = order.ToBson();

            var deserializedOrder = BsonSerializer.Deserialize<Order>(serializedOrder);
            Assert.AreNotEqual(order.OrderDateTime, deserializedOrder.OrderDateTime);
        }

        [Test]
        public void LocalDateTimeSerialization()
        {
            var order = new LocalOrder() {OrderDateTime = new DateTime(2014, 1, 2, 11, 30, 0)};
            var serializedOrder = order.ToBson();
            Console.WriteLine(order.ToJson());

            var deserializedOrder = BsonSerializer.Deserialize<LocalOrder>(serializedOrder);
            Assert.AreEqual(order.OrderDateTime, deserializedOrder.OrderDateTime);
        }
      

        public class Order
        {
            public DateTime OrderDateTime { get; set; }
        }

        public class LocalOrder
        {
            [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
            public DateTime OrderDateTime { get; set; } 
        }

        public class Person
        {
            public string Firstname { get; set; }
            public int Age { get; set; }

            public List<string> Address = new List<string>();
            public Contact Contact = new Contact();

            [BsonIgnore]
            public string IgnoreMe { get; set; }

            [BsonElement("New")]
            public string Old { get; set; }

            [BsonElement]
            [BsonIgnoreIfNull]
            private string Encapsulated;

            [BsonIgnoreIfNull]
            public string Referring { get; set; }
        }

        public class Contact
        {
            public string Phone { get; set; }
            public string Email { get; set; } 
        }
    }
}