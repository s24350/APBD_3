namespace APBD_3.Validators
{
    public class ColumnNameValidator : IColumnNameValidator
    {
       
        private static List<string> _columns = new List<string> {"id", "name", "description", "category", "area"};

        public bool Validate(string columnName) {
            return _columns.Contains(columnName.ToLower());
        }
    }
}
