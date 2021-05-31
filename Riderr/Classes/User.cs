using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Riderr.Classes
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Age")]
        public int Age { get; set; }
        
        [Display(Name = "Phone number")]
        public string Phone { get; set; }
        
        [Display(Name = "Email address")]
        public string Email { get; set; }
        //public string[] Images { get; set; }
        //public bool HasImage()
        //{
        //    if (Images.Length > 0 || !(Images is null))
        //    {
        //        return true;
        //    }
        //    else return false;
        //}
        public string ImageId { get; set; }
        public bool HasImage()
        {
            if (ImageId is null) return false;
            else return true;
        }
    }
}
