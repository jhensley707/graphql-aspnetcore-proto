using System;
using System.Collections.Generic;
using System.Linq;
using AuditLog.Types;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace AuditLog.MongoDB
{
    public class MongoDataAccess : IDataAccess
    {
        private string _connectionString;

        public MongoDataAccess(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException("connectionString");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException("Value cannot be empty", "connectionString");
            }

            //BsonClassMap.RegisterClassMap<AuditLogEntry>(cm =>
            //{
            //    cm.MapMember(c => c.Name).SetElementName("eventType");
            //});
        }

        private AuditLogEntry MapToAuditLogEntry(BsonDocument document)
        {
            if (document == null) { return null; }
            return new AuditLogEntry
            {
                Id = document["_id"].AsObjectId.ToString(),
                Name = document["eventType"].AsString,
                UserId = document["user"].AsString,
                Outcome = document["outcome"].AsString,
                IpAddress = document["ipAddress"].AsString,
                Description = document["description"].AsString,
                //TimestampUtc = DateTime.Parse(document["dateTimeUtc"].AsString, null, System.Globalization.DateTimeStyles.RoundtripKind)
                TimestampUtc = document["dateTimeUtc"].AsString,
            };

        }

        private IMongoCollection<BsonDocument> GetMongoAuditCollection(string database)
        {
            var client = new MongoClient(_connectionString);
            var db = client.GetDatabase(database);
            var collection = db.GetCollection<BsonDocument>("audit");
            return collection;
        }

        public AuditLogClient GetAuditLogClientByName(string name)
        {
            return new AuditLogClient { Name = name };
        }

        public void AddAuditLogEntry(string database, AuditLogEntry entry)
        {
            var collection = GetMongoAuditCollection(database);
            var document = new BsonDocument
            {
                { "eventType", entry.Name },
                { "ipAddress", entry.IpAddress },
                { "user", entry.UserId },
                { "description", entry.Description },
                { "outcome", entry.Outcome },
                { "dateTimeUtc", entry.TimestampUtc }
            };
            collection.InsertOne(document);
            entry.Id = document["_id"].AsObjectId.ToString();
        }

        public IEnumerable<AuditLogEntry> GetAuditLogEntries(string database, FilterCriteria filterCriteria)
        {
            var results = new List<AuditLogEntry>();
            FilterDefinition<BsonDocument> filter = null;
            var collection = GetMongoAuditCollection(database);

            var projection = Builders<BsonDocument>.Projection.Exclude("_id");
            if (filterCriteria != null && filterCriteria.Id != null)
            {
                filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(filterCriteria.Id));
            }

            if (filter == null)
            {
                var documents = collection.Find(new BsonDocument()).ToList();
                results.AddRange(documents.Select(MapToAuditLogEntry));
            }
            else
            {
                var cursor = collection.Find(filter).ToCursor();
                foreach (var document in cursor.ToEnumerable())
                {
                    results.Add(MapToAuditLogEntry(document));
                }
            }

            return results;
        }
    }
}
