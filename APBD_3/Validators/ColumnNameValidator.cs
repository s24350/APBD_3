namespace APBD_3.Validators
{
    public class ColumnNameValidator : IColumnNameValidator
    {
       
        private static List<string> _columns = new List<string> {"Id", "Name", "Description", "Category", "Area"};

        public bool Validate(string columnName) {
            return _columns.Contains(columnName);
        }
    }
}
