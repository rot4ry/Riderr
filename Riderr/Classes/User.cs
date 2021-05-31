using MongoDB.Bson;

namespace Riderr.Classes
{
    public class User
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
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
