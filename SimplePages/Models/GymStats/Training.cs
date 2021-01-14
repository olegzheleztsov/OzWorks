using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SimplePages.Models.GymStats
{
    public class Training
    {
        
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        public DateTime Date { get; set; }
        
        public List<PhysicalExercise> Exercises { get; set; }
        
    }
}