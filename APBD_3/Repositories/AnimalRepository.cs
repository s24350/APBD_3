using APBD_3.Models;
using System.Data.SqlClient;

// Repository is responsible for communicating with DB
// [controller] <-> [service] <-> [repository] <-> [database]

namespace APBD_3.Repositories
{
    public class AnimalRepository : IAnimalRepository
    {
        //IConfiguration works authomatically thanks to connectionBuilder
        private IConfiguration _configuration;
        public AnimalRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        } 
        public IEnumerable<Animal> GetAnimals(string orderBy)
        {
            //using var con = new SqlConnection(_configuration["ConnectionStrings:DeafultConnection"]);
            //using var con = new SqlConnection(_configuration.GetValue<string>("ConnectionString"));
            //using var con = new SqlConnection("server=db-mssql16.pjwstk.edu.pl;database=Animals; Integrated Security=True;Trusted_Connection=yes;TrustServerCertificate=True");
            
            using var con = new SqlConnection("Data Source=db-mssql16.pjwstk.edu.pl;Initial Catalog=s24350;Integrated Security=True;TrustServerCertificate=True");
            con.Open();

            using var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT Id, Name, Description, Category, Area FROM Animals ORDER BY Name ASC";
            //cmd.Parameters.AddWithValue("@sortingParam", orderBy); - ! - not working
            //can't add parameter into ORDER BY Clause
            var dr = cmd.ExecuteReader();
            var animals = new List<Animal>();

            while (dr.Read())
            {
                var row = new Animal {
                    IdAnimal = (int)dr["Id"],
                    Name = dr["Name"].ToString(),
                    Description = dr["Description"].ToString(),
                    Category = dr["Category"].ToString(),
                    Area = dr["Area"].ToString()
                };
                animals.Add(row);
            }
            return animals;
        }
    }
}
