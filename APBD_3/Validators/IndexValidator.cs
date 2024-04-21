using APBD_3.Repositories;

namespace APBD_3.Validators
{
    public class IndexValidator : IIndexValidator
    {
        public bool IsIndexInAnimalsTable(int idAnimal, List<int> listOfIds)
        {
            return listOfIds.Contains(idAnimal);    
        }
    }
}
