using APBD_3.Enums;
using APBD_3.Models;
using APBD_3.Validators;
using System.Collections.Generic;
using System.Data.SqlClient;

// Repository is responsible for communicating with DB
// [controller] <-> [service] <-> [repository] <-> [database]

namespace APBD_3.Repositories
{
    public class AnimalRepository : IAnimalRepository
    {
        //IConfiguration works authomatically thanks to connectionBuilder
        private IConfiguration _configuration;
        private IColumnNameValidator _columnNameValidator;
        public AnimalRepository(IConfiguration configuration, IColumnNameValidator columnNameValidator)
        {
            _configuration = configuration;
            _columnNameValidator = columnNameValidator;
        } 
        public IEnumerable<Animal> GetAnimals(string orderBy)
        {
            //using var con = new SqlConnection(_configuration["ConnectionString:DeafultConnection"]); - ! - not working (why?)
            //using var con = new SqlConnection(_configuration.GetValue<string>("ConnectionString")); - ! - not working (why?)
            using var con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")); //- this one is ok
            //using var con = new SqlConnection("Data Source=db-mssql16.pjwstk.edu.pl;Initial Catalog=s24350;Integrated Security=True;TrustServerCertificate=True"); //- this one is ok too.
            con.Open();

            var animals = new List<Animal>();

            //not allowing any other column names than defined in ColumnNameValidator class
            if (!_columnNameValidator.Validate(orderBy))
            {
                return animals;
            }
           
            using var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT Id, Name, Description, Category, Area FROM Animals ORDER BY "+orderBy+" ASC";
            //cmd.Parameters.AddWithValue("@sortingParam", orderBy); - ! - not working
            //can't add parameter into ORDER BY Clause
            var dr = cmd.ExecuteReader();
            
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

        public int GetMaxId() {
            using var con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            con.Open();
            using var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT MAX(Id) FROM Animals";
            int count = (int) cmd.ExecuteScalar();
            return count;
        }

        public int PostAnimal(Animal animal)
        {
            using var con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            con.Open();
            using var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO Animals VALUES(@Name,@Description,@Category,@Area)";
            cmd.Parameters.AddWithValue("@Id", animal.IdAnimal);
            cmd.Parameters.AddWithValue("@Name", animal.Name);
            cmd.Parameters.AddWithValue("@Description", animal.Description);
            cmd.Parameters.AddWithValue("@Category", animal.Category);
            cmd.Parameters.AddWithValue("@Area", animal.Area);

            var affectedCount = cmd.ExecuteNonQuery();
            return affectedCount;
        }

        public Animal GetAnimalById(int idAnimal)
        {
            using var con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            con.Open();
            using var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT Name, Description, Category, Area FROM Animals WHERE Id=@IdAnimal";
            cmd.Parameters.AddWithValue("@IdAnimal", idAnimal);
            var dr = cmd.ExecuteReader();
            dr.Read();
            return new Animal
            {
                IdAnimal = idAnimal,
                Name = dr["Name"].ToString(),
                Description = dr["Description"].ToString(),
                Category = dr["Category"].ToString(),
                Area = dr["Area"].ToString()
            }; 
        }

        public int UpdateAnimal(Animal animal)
        {
            using var con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            con.Open();
            using var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE Animals SET Name=@Name,Description=@Description,Category=@Category,Area=@Area WHERE Id=@Id";
            cmd.Parameters.AddWithValue("@Id", animal.IdAnimal);
            cmd.Parameters.AddWithValue("@Name", animal.Name);
            cmd.Parameters.AddWithValue("@Description", animal.Description);
            cmd.Parameters.AddWithValue("@Category", animal.Category);
            cmd.Parameters.AddWithValue("@Area", animal.Area);

            var affectedCount = cmd.ExecuteNonQuery();
            return affectedCount;
        }

        public int DeleteAnimal(int idAnimal)
        {
            using var con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            con.Open();
            using var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "DELETE FROM Animals WHERE Id = @IdAnimal";
            cmd.Parameters.AddWithValue("@IdAnimal", idAnimal);

            var affectedCount = cmd.ExecuteNonQuery();
            return affectedCount;
        }

        public List<int> GetExistingIds()
        {
            using var con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            con.Open();
            using var cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT Id FROM Animals";
            var dr = cmd.ExecuteReader();

            List<int> ids = new List<int>();

            while (dr.Read())
            {
                ids.Add((int)dr["Id"]);
            }
            return ids;
        }
    }
}
