using APBD_3.Models;
using APBD_3.Validators;
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
    }
}
