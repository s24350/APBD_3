namespace APBD_3.Validators
{
    public interface IIndexValidator
    {
        public bool IsIndexInAnimalsTable(int idAnimal, List<int> listOfIds);
    }
}
