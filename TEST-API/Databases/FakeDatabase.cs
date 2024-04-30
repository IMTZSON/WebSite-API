using System.ComponentModel.DataAnnotations;

namespace TEST_API.Databases
{
    public class FakeDatabase
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }        
        public string Address { get; set; }
        public string UserName {  get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string ApiKey { get; set; }
        public string Role { get; set; }

        public List<FakeDatabase> GetDb()
        {
            var db = new List<FakeDatabase>() 
            {
                new FakeDatabase { Id = "3f2504e0-4f89-41d3-9a0c-0305e82c3301", Name = "Sofia", Address = "Via Roma 10", UserName = "User123", Email = "example123@gmail.com", Password = "P@ssw0rd!", ApiKey = "d4c172e1-2f56-49d3-8395-19290847af6f", Role = "SUPERADMIN" },
                new FakeDatabase { Id = "21ec2020-3aea-1069-a2dd-08002b30309d", Name = "Luca", Address = "Corso Vittorio Emanuele 25", UserName = "CoolGirl22", Email = "johndoe@hotmail.com", Password = "Secure123", ApiKey = "6d2b9a9e-1b47-42d6-a232-22c6a315f6a6", Role = "ADMIN" },
                new FakeDatabase { Id = "6ba7b810-9dad-11d1-80b4-00c04fd430c8", Name = "Chiara", Address = "Via Garibaldi 7", UserName = "TechWizard", Email = "sarah.smith@yahoo.com", Password = "LetMeIn2022", ApiKey = "b902985f-67a8-4d28-ad6b-bb1c182de3b5", Role = "ADMIN" },
                new FakeDatabase { Id = "550e8400-e29b-41d4-a716-446655440000", Name = "Marco", Address = "Via Dante 15", UserName = "SoccerFanatic", Email = "codingmaster@outlook.com", Password = "StrongP@ss", ApiKey = "f79735f0-bb4d-49d1-ae7b-0903ea227e12", Role = "OPERATOR" },
                new FakeDatabase { Id = "7f6d8cbb-1e0c-4d71-acbc-64f6c1e7b97d", Name = "Alessia", Address = "Viale dei Mille 42", UserName = "MusicLover789", Email = "musiclover1234@gmail.com", Password = "12345678Aa!", ApiKey = "e1ac4b89-294f-4174-b160-56283d17cad9", Role = "OPERATOR" }
            };

            return db;
        }
    }
}
